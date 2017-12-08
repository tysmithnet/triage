using System;
using System.ComponentModel.Composition.Hosting;
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
                    var dumpObjectRepository = new DumpObjectRepository(rt, heapObjectExtractors);
                    var debuggerProxy = new DebuggerProxy(dt.DebuggerInterface);
                    var threadRepo = new DumpThreadRepository(rt, debuggerProxy, dumpObjectRepository);
                }

#if DEBUG
                Console.ReadKey();
#endif
            });
        }      
    }
}
