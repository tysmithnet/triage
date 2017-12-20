﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using CommandLine;
using Common.Logging;

namespace Triage.Mortician
{
    /// <summary>
    ///     Entry point class
    /// </summary>
    internal class Program
    {
        internal static ILog Log = LogManager.GetLogger(typeof(Program));

        /// <summary>
        ///     Entry point to the application
        /// </summary>
        /// <param name="args">The arguments.</param>
        private static void Main(string[] args)
        {
            Log.Trace($"Starting mortician at {DateTime.UtcNow.ToString()} UTC");
            WarnIfNoDebuggingKitOnPath();

            Parser.Default.ParseArguments<DefaultOptions, ConfigOptions>(args).MapResult(
                (DefaultOptions opts) => DefaultExecution(opts),
                (ConfigOptions opts) => ConfigExecution(opts),
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
        private static void WarnIfNoDebuggingKitOnPath()
        {
            var path = Environment.GetEnvironmentVariable("PATH") ?? "";
            if (!Regex.IsMatch(path, @"[Dd]ebuggers[/\\]x64"))
                Log.Warn(
                    "Did not find Debuggers\\x64 in PATH. Did you install the Windows Debugging Kit and set Debuggers\\x64 as part of PATH?");

            if (!Regex.IsMatch(path, @"[Dd]ebuggers[/\\]x64[/\\]winext"))
                Log.Warn(
                    "Did not find Debuggers\\x64\\winext in PATH. Did you install the Windows Debugging Kit and set Debuggers\\x64\\winext as part of PATH?");
        }

        /// <summary>
        ///     Executes the configuration module
        /// </summary>
        /// <param name="configOptions">The the configuration command line options</param>
        /// <returns>Program status code</returns>
        private static int ConfigExecution(ConfigOptions configOptions)
        {
            if (configOptions.ShouldList)
                try
                {
                    var configText = File.ReadAllText("mortician.config.json");
                    Console.WriteLine(configText);
                    return 0;
                }
                catch (IOException e)
                {
                    Console.WriteLine(
                        "Could not load mortician.config.json, use config -k \"some key\" -v \"some value\"");
                    return 1;
                }

            var settings = Settings.GetSettings();

            if (configOptions.KeysToDelete != null && configOptions.KeysToDelete.Any())
            {
                foreach (var key in configOptions.KeysToDelete)
                    if (settings.ContainsKey(key))
                        settings.Remove(key);
                Settings.SaveSettings(settings);
                return 0;
            }

            var pairs = configOptions.Keys.Zip(configOptions.Values, (s, s1) => (s, s1))
                .ToDictionary(tuple => tuple.Item1, tuple => tuple.Item2);

            var newSet = settings.Union(pairs);
            settings = newSet
                .GroupBy(kvp => kvp.Key)
                .Select(group => new KeyValuePair<string, string>(group.Key, group.Last().Value))
                .OrderBy(kvp => kvp.Key)
                .ToDictionary(pair => pair.Key, pair => pair.Value);
            Settings.SaveSettings(settings);

            return 0;
        }

        /// <summary>
        ///     Perform the default execution
        /// </summary>
        /// <param name="options">The options.</param>
        /// <returns>Program status code</returns>
        private static int DefaultExecution(DefaultOptions options)
        {
            var executionLocation = Assembly.GetEntryAssembly().Location;
            var morticianAssemblyFiles =
                Directory.EnumerateFiles(Path.GetDirectoryName(executionLocation), "Triage.Mortician.*.dll");
            var toLoad =
                morticianAssemblyFiles.Except(AppDomain.CurrentDomain.GetAssemblies().Select(x => x.Location));
            foreach (var assembly in toLoad)
                Assembly.LoadFile(assembly);

            var aggregateCatalog = new AggregateCatalog(AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => x.FullName.StartsWith("Triage.Mortician")).Select(x => new AssemblyCatalog(x)));
            var compositionContainer = new CompositionContainer(aggregateCatalog);

            var repositoryFactory = new CoreComponentFactory(compositionContainer, new FileInfo(options.DumpFile));
            repositoryFactory.RegisterRepositories();

            var engine = compositionContainer.GetExportedValue<Engine>();
            engine.Process().Wait();

            return 0;
        }
    }
}