// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 12-10-2017
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-01-2018
// ***********************************************************************
// <copyright file="Program.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using CommandLine;
using Newtonsoft.Json;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using Triage.Mortician.Core;

namespace Triage.Mortician
{
    /// <summary>
    ///     Entry point class
    /// </summary>
    internal class Program
    {
        /// <summary>
        ///     The log
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="dependencyInjectionTransformer">The dependency injection transformer.</param>
        /// <returns>Program status code</returns>
        internal int DefaultExecution(DefaultOptions options,
            Func<CompositionContainer, CompositionContainer> dependencyInjectionTransformer = null)
        {
            // todo: this feels like an awkward interface, it can probably be better
            var blacklistedAssemblies = options.BlackListedAssemblies;
            var blacklistedTypes = options.BlackListedTypes;
            var executionLocation = typeof(Program).Assembly.Location;
            var aggregateCatalog = new AggregateCatalog();
            var morticianAssemblyFiles =
                Directory.EnumerateFiles(Path.GetDirectoryName(executionLocation),
                        "Triage.Mortician.*.*")
                    .Where(f => Regex.IsMatch(f, "(dll|exe)$",
                        RegexOptions.IgnoreCase)); // todo: not ideal to require assembly name
            var toLoad =
                morticianAssemblyFiles
                    .Except(AppDomain.CurrentDomain.GetAssemblies()
                        .Where(a => !a.IsDynamic)
                        .Select(x => x.Location))
                    .Select(x => new FileInfo(x))
                    .Where(x => !blacklistedAssemblies.Contains(x.Name));

            aggregateCatalog.Catalogs.Add(new TypeCatalog(options.AdditionalTypes));
            foreach (var assembly in toLoad)
                try
                {
                    var addedAssembly = Assembly.LoadFile(assembly.FullName);
                    var definedTypes = addedAssembly.DefinedTypes;
                    var filteredTypes = definedTypes.Where(t =>
                        !blacklistedTypes.Select(x => x.ToLower()).Contains(t?.FullName?.ToLower()));
                    var typeCatalog = new TypeCatalog(filteredTypes);
                    aggregateCatalog.Catalogs.Add(typeCatalog);
                }
                catch (Exception e)
                {
                    Log.Error("Unable to load {FullName}, it will not be available because {Message}",
                        assembly.FullName, e.Message);
                }

            var typesToLoad = Assembly.GetExecutingAssembly().DefinedTypes;
            aggregateCatalog.Catalogs.Add(new TypeCatalog(typesToLoad));
            var batch = new CompositionBatch();
            foreach (var setting in InflateSettings(options)) batch.AddPart(setting);

            var compositionContainer = new CompositionContainer(aggregateCatalog);
            compositionContainer.Compose(batch);
            var componentFactory = new CoreComponentFactory(compositionContainer, new FileInfo(options.DumpFile));
            componentFactory.Setup();
            componentFactory.RegisterRepositories(options);
            compositionContainer = dependencyInjectionTransformer?.Invoke(compositionContainer) ?? compositionContainer;
            var engine = compositionContainer.GetExportedValue<IEngine>();
            engine.Process().Wait();
            return 0;
        }

        /// <summary>
        ///     Inflates the settings.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <returns>IEnumerable&lt;ISettings&gt;.</returns>
        internal IEnumerable<ISettings> InflateSettings(DefaultOptions options)
        {
            var serializer = new SettingsJsonConverter();

            if (!string.IsNullOrWhiteSpace(options.SettingsFile))
            {
                try
                {
                    var text = File.ReadAllText(options.SettingsFile);
                    return JsonConvert.DeserializeObject<IEnumerable<ISettings>>(text, serializer);
                }
                catch (FileNotFoundException e)
                {
                    Log.Error(e, "Could not find settings file at {SettingsFileLocation}", options.SettingsFile);
                }
            }
            else if (File.Exists("settings.json"))
            {
                Log.Information("Settings file was not provided, but found one at the default path");
                var text = File.ReadAllText("settings.json");
                return JsonConvert.DeserializeObject<IEnumerable<ISettings>>(text, serializer);
            }
            else
            {
                Log.Information(
                    "No settings file was provided, and no default settings file exists -creating one using the discovered plugins");
                var settings = AppDomain.CurrentDomain.GetAssemblies()
                    ?.SelectMany(x => x.DefinedTypes.Where(t => typeof(ISettings).IsAssignableFrom(t)));
                var json = JsonConvert.SerializeObject(settings, Formatting.Indented);
                File.WriteAllText("settings.json", json, Encoding.UTF8);
            }

            return new ISettings[0];
        }

        /// <summary>
        ///     Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        internal static void Main(string[] args)
        {
            var program = new Program();

            // todo: better way to configure logging
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.WithThreadId()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
                {
                    AutoRegisterTemplate = true,
                    AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6,
                    QueueSizeLimit = 1
                }).CreateLogger();

            Log.Information("Starting mortician at {UtcNow} UTC", DateTime.UtcNow);

            program.WarnIfNoDebuggingKitOnPath();
            Parser.Default.ParseArguments<DefaultOptions>(args).MapResult(
                options => program.DefaultExecution(options),
                errs => -1
            );
        }

        /// <summary>
        ///     CLRMd relies on certain debugging specific assemblies to inspect memory dumps. You get these assemblies
        ///     with the Windows Debugging Kit. Install it from https://docs.microsoft.com/en-us/windows-hardware/drivers/debugger/
        ///     Then you must add to your path where these assemblies are:
        ///     C:\Program Files (x86)\Windows Kits\10\Debuggers\x64
        ///     C:\Program Files (x86)\Windows Kits\10\Debuggers\x64\winext
        /// </summary>
        internal void WarnIfNoDebuggingKitOnPath()
        {
            var path = Environment.GetEnvironmentVariable("PATH") ?? "";
            if (!Regex.IsMatch(path, @"[Dd]ebuggers[/\\]x64"))
                Log.Warning(
                    "Did not find Debuggers\\x64 in PATH. Did you install the Windows Debugging Kit and set Debuggers\\x64 as part of PATH?");

            if (!Regex.IsMatch(path, @"[Dd]ebuggers[/\\]x64[/\\]winext"))
                Log.Warning(
                    "Did not find Debuggers\\x64\\winext in PATH. Did you install the Windows Debugging Kit and set Debuggers\\x64\\winext as part of PATH?");
        }
    }
}