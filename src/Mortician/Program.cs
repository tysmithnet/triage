// ***********************************************************************
// Assembly         : Mortician
// Author           : @tysmithnet
// Created          : 12-10-2017
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-08-2018
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
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Commander.NET;
using Mortician.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace Mortician
{
    /// <summary>
    ///     Entry point class
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal class Program
    {
        /// <summary>
        ///     The log
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="dependencyInjectionTransformer">The dependency injection transformer.</param>
        /// <returns>Program status code</returns>
        internal int DefaultExecution(Options options,
            Func<CompositionContainer, CompositionContainer> dependencyInjectionTransformer = null)
        {
            var compositionContainer = SetupCompositionContainer(options, dependencyInjectionTransformer);
            var engine = compositionContainer.GetExportedValue<IEngine>();
            engine.Process().Wait();
            return 0;
        }

        /// <summary>
        ///     Inflates the configuration.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <returns>IConfig.</returns>
        internal IConfig InflateConfig(Options options)
        {
            var json = File.ReadAllText(options.ConfigFile ?? "config.json");
            var jobj = JObject.Parse(json);
            var config = new Config();
            foreach (var prop in jobj)
                switch (prop.Key)
                {
                    case "black_listed_assemblies":
                        config.BlackListedAssemblies = prop.Value.Values<string>().ToArray();
                        break;
                    case "black_listed_types":
                        config.BlackListedTypes = prop.Value.Values<string>().ToArray();
                        break;
                    case "additional_assemblies":
                        config.AdditionalAssemblies = prop.Value.Values<string>().ToArray();
                        break;
                    case "contract_mapping":
                        foreach (var contract in (JObject) prop.Value)
                            config.ContractMapping.Add(contract.Key, contract.Value.Values<string>().ToArray());
                        break;
                }

            return config;
        }

        /// <summary>
        ///     Inflates the settings.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <returns>IEnumerable&lt;ISettings&gt;.</returns>
        internal IEnumerable<ISettings> InflateSettings(Options options)
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
            // print help
            var parser = new CommanderParser<Options>();
            if (!args?.Any() ?? false)
            {
                Console.WriteLine(parser.Usage());
                return;
            }

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

            program.WarnIfNoDebuggingKitOnPath(); // todo: do better
            var options = parser.Parse(args); // todo: handle error with args
            program.DefaultExecution(options);
        }

        /// <summary>
        ///     CLRMd relies on certain debugging specific assemblies to inspect memory dumps. You get these assemblies
        ///     with the Windows Debugging Kit. Install it from https://docs.microsoft.com/en-us/windows-hardware/drivers/debugger/
        ///     Then you must add to your path where these assemblies are:
        ///     C:\Program Files (x86)\Windows Kits\10\Debuggers\x64
        ///     C:\Program Files (x86)\Windows Kits\10\Debuggers\x64\winext
        /// </summary>
        // todo: what about x86
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

        /// <summary>
        ///     Creates the composition container.
        /// </summary>
        /// <param name="aggregateCatalog">The aggregate catalog.</param>
        /// <param name="inflatedSettings">The inflated settings.</param>
        /// <returns>CompositionContainer.</returns>
        internal CompositionContainer CreateCompositionContainer(AggregateCatalog aggregateCatalog,
            List<ISettings> inflatedSettings)
        {
            var typesToLoad = Assembly.GetExecutingAssembly().DefinedTypes;
            aggregateCatalog.Catalogs.Add(new TypeCatalog(typesToLoad));
            var batch = new CompositionBatch();
            foreach (var setting in inflatedSettings) batch.AddPart(setting);

            var compositionContainer = new CompositionContainer(aggregateCatalog);
            compositionContainer.Compose(batch);
            return compositionContainer;
        }

        /// <summary>
        ///     Gets the assembly files to load.
        /// </summary>
        /// <param name="morticianAssemblyFiles">The mortician assembly files.</param>
        /// <returns>IEnumerable&lt;FileInfo&gt;.</returns>
        internal IEnumerable<FileInfo> GetAssemblyFilesToLoad(IEnumerable<string> morticianAssemblyFiles)
        {
            var toLoad =
                morticianAssemblyFiles
                    .Except(AppDomain.CurrentDomain.GetAssemblies()
                        .Where(a => !a.IsDynamic)
                        .Select(x => x.Location))
                    .Select(x => new FileInfo(x));
            return toLoad;
        }

        /// <summary>
        ///     Gets the mortician assemblies.
        /// </summary>
        /// <param name="executionLocation">The execution location.</param>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        internal IEnumerable<string> GetMorticianAssemblies(string executionLocation)
        {
            var morticianAssemblyFiles =
                Directory.EnumerateFiles(Path.GetDirectoryName(executionLocation),
                        "Mortician.*.*")
                    .Where(f => Regex.IsMatch(f, "(dll|exe)$",
                        RegexOptions.IgnoreCase));
            return morticianAssemblyFiles;
        }

        /// <summary>
        ///     Loads the types.
        /// </summary>
        /// <param name="toLoad">To load.</param>
        /// <param name="config">The configuration.</param>
        /// <param name="aggregateCatalog">The aggregate catalog.</param>
        internal void LoadTypes(IEnumerable<FileInfo> toLoad, IConfig config, AggregateCatalog aggregateCatalog)
        {
            foreach (var assembly in toLoad)
                try
                {
                    var addedAssembly = Assembly.LoadFile(assembly.FullName);
                    var definedTypes = addedAssembly.DefinedTypes;
                    var filteredTypes = definedTypes.Where(t =>
                        !config.BlackListedTypes.Select(x => x.ToLower()).Contains(t?.FullName?.ToLower())).Where(t =>
                        config.BlackListedAssemblies.Select(x => x.ToLower())
                            .Contains(t?.Assembly?.FullName?.ToLower()));
                    var typeCatalog = new TypeCatalog(filteredTypes);
                    aggregateCatalog.Catalogs.Add(typeCatalog);
                }
                catch (Exception e)
                {
                    Log.Error("Unable to load {FullName}, it will not be available because {Message}",
                        assembly.FullName, e.Message);
                }
        }

        /// <summary>
        ///     Setups the composition container.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="dependencyInjectionTransformer">The dependency injection transformer.</param>
        /// <returns>CompositionContainer.</returns>
        internal CompositionContainer SetupCompositionContainer(Options options,
            Func<CompositionContainer, CompositionContainer> dependencyInjectionTransformer)
        {
            var config = InflateConfig(options);
            var executionLocation = typeof(Program).Assembly.Location;
            var morticianAssemblyFiles = GetMorticianAssemblies(executionLocation);
            var toLoad = GetAssemblyFilesToLoad(morticianAssemblyFiles);
            var aggregateCatalog = new AggregateCatalog();
            if (options.AdditionalTypes?.Any() ?? false)
                aggregateCatalog.Catalogs.Add(new TypeCatalog(options.AdditionalTypes));
            LoadTypes(toLoad, config, aggregateCatalog);
            var inflatedSettings = InflateSettings(options).ToList();
            var compositionContainer = CreateCompositionContainer(aggregateCatalog, inflatedSettings);
            var componentFactory =
                new CoreComponentFactory(compositionContainer, new FileInfo(options.RunOptions.DumpLocation));
            componentFactory.Setup();
            componentFactory.RegisterRepositories(options);
            compositionContainer = dependencyInjectionTransformer?.Invoke(compositionContainer) ?? compositionContainer;
            return compositionContainer;
        }
    }
}