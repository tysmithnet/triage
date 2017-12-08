using System;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using Common.Logging;
using Microsoft.Diagnostics.Runtime;

namespace Triage.Mortician
{
    class Program
    {
        static void Main(string[] args)
        {
            var log = LogManager.GetLogger(typeof(Program));

            log.Trace("Hello world");

            Parser.Default.ParseArguments<CommandLineOptions>(args).WithParsed(options =>
            {
                var assemblyCatalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
                var aggregateCatalog = new AggregateCatalog(assemblyCatalog);
                var compositionContainer = new CompositionContainer(aggregateCatalog);
                var heapObjectExtractors = compositionContainer.GetExportedValues<IDumpObjectExtractor>().ToList();
                using (var dt = DataTarget.LoadCrashDump(options.DumpFilePath))
                {
                    var rt = dt.ClrVersions.Single().CreateRuntime();
                    var stopWatch = Stopwatch.StartNew();
                    var dumpObjectRepository = new DumpObjectRepository(rt, heapObjectExtractors);
                    log.Trace($"DumpObjectRepository created in {TimeSpan.FromMilliseconds(stopWatch.ElapsedMilliseconds).ToString()}");
                    var debuggerProxy = new DebuggerProxy(dt.DebuggerInterface);
                    stopWatch.Restart();
                    var threadRepo = new DumpThreadRepository(rt, debuggerProxy, dumpObjectRepository);
                    log.Trace($"DumpThreadRepository created in {TimeSpan.FromMilliseconds(stopWatch.ElapsedMilliseconds).ToString()}");
                }

#if DEBUG
                Console.ReadKey();
#endif
            });
        }      
    }              
}
