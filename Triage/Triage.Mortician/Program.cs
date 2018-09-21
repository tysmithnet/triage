// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 12-10-2017
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="Program.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using CommandLine;
using Common.Logging;
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
        internal static ILog Log = LogManager.GetLogger(typeof(Program));

        /// <summary>
        ///     Perform the default execution
        /// </summary>
        /// <param name="options">The options.</param>
        /// <returns>Program status code</returns>
        internal static int DefaultExecution(DefaultOptions options,
            Func<CompositionContainer, CompositionContainer> dependencyInjectionTransformer = null)
        {
            // todo: fix
            var blacklistedAssemblies = options.BlackListedAssemblies;
            var blacklistedTypes = options.BlackListedTypes;
            var executionLocation = typeof(Program).Assembly.Location;
            var morticianAssemblyFiles =
                Directory.EnumerateFiles(Path.GetDirectoryName(executionLocation),
                        "Triage.Mortician.*.*")
                    .Where(f => Regex.IsMatch(f, "(dll|exe)$",
                        RegexOptions.IgnoreCase)); // todo: not ideal to require assembly name
            var toLoad =
                morticianAssemblyFiles.Except(AppDomain.CurrentDomain.GetAssemblies().Select(x => x.Location))
                    .Select(x => new FileInfo(x)).Where(x => !blacklistedAssemblies.Contains(x.Name));

            var aggregateCatalog = new AggregateCatalog();
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
                    Log.Error($"Unable to load {assembly.FullName}, it will not be available because {e.Message}");
                }

            aggregateCatalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            var compositionContainer = new CompositionContainer(aggregateCatalog);
            // todo: allow for export/import manipulation
            var repositoryFactory = new CoreComponentFactory(compositionContainer, new FileInfo(options.DumpFile));
            repositoryFactory.RegisterRepositories(options);
            compositionContainer = dependencyInjectionTransformer?.Invoke(compositionContainer);

            var engine = compositionContainer.GetExportedValue<IEngine>();
            engine.Process().Wait();
            return 0;
        }

        /// <summary>
        ///     Entry point to the application
        /// </summary>
        /// <param name="args">The arguments.</param>
        internal static void Main(string[] args)
        {
            Log.Trace($"Starting mortician at {DateTime.UtcNow.ToString()} UTC");
            WarnIfNoDebuggingKitOnPath();

            Parser.Default.ParseArguments<DefaultOptions>(args).MapResult(
                options => DefaultExecution(options),
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
        internal static void WarnIfNoDebuggingKitOnPath()
        {
            var path = Environment.GetEnvironmentVariable("PATH") ?? "";
            if (!Regex.IsMatch(path, @"[Dd]ebuggers[/\\]x64"))
                Log.Warn(
                    "Did not find Debuggers\\x64 in PATH. Did you install the Windows Debugging Kit and set Debuggers\\x64 as part of PATH?");

            if (!Regex.IsMatch(path, @"[Dd]ebuggers[/\\]x64[/\\]winext"))
                Log.Warn(
                    "Did not find Debuggers\\x64\\winext in PATH. Did you install the Windows Debugging Kit and set Debuggers\\x64\\winext as part of PATH?");
        }
    }
}