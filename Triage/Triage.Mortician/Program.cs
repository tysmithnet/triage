﻿using System;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using CommandLine;
using Common.Logging;
using Microsoft.Diagnostics.Runtime;

namespace Triage.Mortician
{
    /// <summary>
    ///     Entry point class
    /// </summary>
    internal class Program
    {
        /// <summary>
        ///     Entry point to the application
        /// </summary>
        /// <param name="args">The arguments.</param>
        private static void Main(string[] args)
        {
            var log = LogManager.GetLogger(typeof(Program));

            log.Trace("Hello world");

            Parser.Default.ParseArguments<CommandLineOptions>(args).WithParsed(options =>
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
                
                var repositoryFactory = new RepositoryFactory(compositionContainer, new FileInfo(options.DumpFilePath));
                repositoryFactory.RegisterRepositories();   

                var engine = compositionContainer.GetExportedValue<Engine>();
                engine.Process(CancellationToken.None).Wait();

#if DEBUG
                Console.WriteLine("Success.. press any key");
                Console.ReadKey();
#endif
            });
        }
    }
}