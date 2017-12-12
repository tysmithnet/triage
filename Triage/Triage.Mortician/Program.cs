using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using CommandLine;
using Common.Logging;
using Microsoft.Diagnostics.Runtime;
using Triage.Mortician.Abstraction;

namespace Triage.Mortician
{
    /// <summary>
    /// Entry point class
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Entry point to the application
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
                var heapObjectExtractors = compositionContainer.GetExportedValues<IDumpObjectExtractor>().ToList();
                DumpObjectRepository dumpObjectRepository;
                DebuggerProxy debuggerProxy;
                DumpThreadRepository dumpThreadRepository;
                using (var dt = DataTarget.LoadCrashDump(options.DumpFilePath))
                {
                    var rt = dt.ClrVersions.Single().CreateRuntime();
                    var stopWatch = Stopwatch.StartNew();
                    dumpObjectRepository = new DumpObjectRepository(rt, heapObjectExtractors);
                    log.Trace(
                        $"DumpObjectRepository created in {TimeSpan.FromMilliseconds(stopWatch.ElapsedMilliseconds).ToString()}");
                    debuggerProxy = new DebuggerProxy(dt.DebuggerInterface);
                    stopWatch.Restart();
                    dumpThreadRepository = new DumpThreadRepository(rt, debuggerProxy, dumpObjectRepository);
                    log.Trace(
                        $"DumpThreadRepository created in {TimeSpan.FromMilliseconds(stopWatch.ElapsedMilliseconds).ToString()}");
                }

                compositionContainer.ComposeExportedValue<IDumpObjectRepository>(dumpObjectRepository);
                compositionContainer.ComposeExportedValue<IDebuggerProxy>(debuggerProxy);
                compositionContainer.ComposeExportedValue<IDumpThreadRepository>(dumpThreadRepository);

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