using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
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
                    var debuggerProxy = new DebuggerProxy(dt.DebuggerInterface);
                    var threadRepo = new DumpThreadRepository(rt, debuggerProxy, heapRepo);
                }

#if DEBUG
                Console.ReadKey();
#endif
            });
        }      
    }

    internal class DumpThreadRepository
    {
        public Dictionary<uint, DumpThread> DumpThreads { get; set; }

        public ILog Log { get; set; } = LogManager.GetLogger(typeof(DumpThreadRepository));

        public DumpThreadRepository(ClrRuntime rt, DebuggerProxy debuggerProxy, DumpObjectRepository dumpRepo)
        {
            var log = LogManager.GetLogger(typeof(DumpThreadRepository));
            foreach (var clrThread in rt.Threads)
            {
                var dumpThread = new DumpThread();
                dumpThread.OsId = clrThread.OSThreadId;                
                foreach (var clrThreadObject in clrThread.EnumerateStackObjects())
                {
                    if (dumpRepo.HeapObjects.TryGetValue(clrThreadObject.Address, out IDumpObject dumpObject))
                        dumpThread.StackObjects.Add(dumpObject);
                    else
                        log.Trace($"Thread: {clrThread.OSThreadId} has a reference to {clrThreadObject.Address} but it was not in the heap repository");
                }

                DumpThreads.Add(dumpThread.OsId, dumpThread);
            }
            PopulateRunawayData(debuggerProxy);
        }

        private void PopulateRunawayData(DebuggerProxy debuggerProxy)
        {
            string runawayData = debuggerProxy.Execute("!runaway");
            Log.Trace($"Calling !runaway returned: {runawayData}");

            bool isUserMode = false;
            bool isKernelMode = false;
            foreach (var line in runawayData.Split('\n'))
            {
                if (Regex.IsMatch(line, "User Mode Time"))
                {
                    isUserMode = true;
                    continue;
                }
                if (Regex.IsMatch(line, "Kernel Mode Time"))
                {
                    isUserMode = false;
                    isKernelMode = true;
                }
                var match = Regex.Match(line,
                    @"(?<index>\d+):(?<id>[a-zA-Z0-9]+)\s*(?<days>\d+) days (?<time>\d+:\d{2}:\d{2}.\d{3})");
                if (!match.Success) continue;
                var index = UInt32.Parse(match.Groups["index"].Value);
                var id = UInt32.Parse(match.Groups["id"].Value);
                var days = UInt32.Parse(match.Groups["days"].Value);
                var time = match.Groups["time"].Value;
                TimeSpan timeSpan = TimeSpan.Parse(time, CultureInfo.CurrentCulture);
                timeSpan = timeSpan.Add(TimeSpan.FromDays(days));
                var dumpThread = DumpThreads.Values.SingleOrDefault(x => x.OsId == id);
                if (dumpThread == null)
                {
                    Log.Trace($"Found thread {id} in runaway data but not in thread repo");
                    continue;
                }
                dumpThread.DebuggerIndex = index;
                
                if (isUserMode)
                {
                    dumpThread.UserModeTime = timeSpan;
                }
                else if(isKernelMode)
                {
                    dumpThread.KernelModeTime = timeSpan;
                }
            }
        }
    }

    internal class DumpThread
    {
        public IList<IDumpObject> StackObjects { get; set; } = new List<IDumpObject>();
        public uint OsId { get; set; }
        public TimeSpan KernelModeTime { get; set; }
        public TimeSpan UserModeTime { get; set; }
        public TimeSpan TotalTime { get; set; }
        public uint DebuggerIndex { get; set; }
    }
}
