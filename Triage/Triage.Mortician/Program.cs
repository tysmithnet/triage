using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using Common.Logging;
using Microsoft.Diagnostics.Runtime;
using Triage.Mortician.Abstraction;

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
                    var heapRepo = new DumpObjectRepository(rt, heapObjectExtractors);
                    var threadRepo = new DumpThreadRepository(rt, heapRepo);
                }

#if DEBUG
                Console.ReadKey();
#endif
            });
        }      
    }

    internal class DumpThreadRepository
    {
        public Dictionary<int, DumpThread> DumpThreads { get; set; }

        public ILog Log { get; set; } = LogManager.GetLogger(typeof(DumpThreadRepository));

        public DumpThreadRepository(ClrRuntime rt, DumpObjectRepository dumpRepo)
        {
            var log = LogManager.GetLogger(typeof(DumpThreadRepository));
            foreach (var clrThread in rt.Threads)
            {
                var dumpThread = new DumpThread();
                
                foreach (var clrThreadObject in clrThread.EnumerateStackObjects())
                {
                    if (dumpRepo.HeapObjects.TryGetValue(clrThreadObject.Address, out IDumpObject dumpObject))
                        dumpThread.StackObjects.Add(dumpObject);
                    else
                        log.Warn($"Thread: {clrThread.OSThreadId} has a reference to {clrThreadObject.Address} but it was not in the heap repository");
                }
            }
        }
    }

    internal class DumpThread
    {
        public IList<IDumpObject> StackObjects { get; set; } = new List<IDumpObject>();
    }
}
