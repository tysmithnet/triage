using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using CommandLine;
using Common.Logging;
using Microsoft.Diagnostics.Runtime;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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
            Log.Trace("Hello world");

            Parser.Default.ParseArguments<DefaultOptions, ConfigOptions>(args).MapResult(
                (DefaultOptions opts) => DefaultExecution(opts),
                (ConfigOptions opts) => ConfigExecution(opts),
                errs => -1
            );
#if DEBUG
            Console.WriteLine("Success.. press any key");
            Console.ReadKey();
#endif
        }

        private static int ConfigExecution(ConfigOptions opts)
        {   
            if (opts.ShouldDisplay)
            {
                try
                {
                    string configText = File.ReadAllText("mortician.config.json");
                    Console.WriteLine(configText);
                    return 0;
                }
                catch (IOException e)
                {
                    Log.Fatal($"Could not read the configuration file: {e}");
                    throw;
                }
            }
            
            var settings = Settings.GetSettings();
            var pairs = opts.Keys.Zip(opts.Values, (s, s1) => (s, s1))
                .ToDictionary(tuple => tuple.Item1, tuple => tuple.Item2);

            settings = settings.Union(pairs)
                .GroupBy(kvp => kvp.Key)
                .Select(group => new KeyValuePair<string, string>(group.Key, group.Last().Value))
                .OrderBy(kvp => kvp.Key)
                .ToDictionary(pair => pair.Key, pair => pair.Value);
            Settings.SaveSettings(settings);
                                    
            return 0;
        }

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

            var repositoryFactory = new RepositoryFactory(compositionContainer, new FileInfo(options.DumpFile));
            repositoryFactory.RegisterRepositories();

            var engine = compositionContainer.GetExportedValue<Engine>();
            engine.Process(CancellationToken.None).Wait();


            return 0;
        }
    }
}