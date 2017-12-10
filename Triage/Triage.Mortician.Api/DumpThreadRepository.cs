using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Common.Logging;
using Microsoft.Diagnostics.Runtime;
using Triage.Mortician.Abstraction;

namespace Triage.Mortician.Api
{
    internal class DumpThreadRepository : IDumpThreadRepository
    {
        public Dictionary<uint, DumpThread> DumpThreads { get; set; } = new Dictionary<uint, DumpThread>();

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
                    IDumpObject dumpObject;
                    if (dumpRepo.HeapObjects.TryGetValue(clrThreadObject.Object, out dumpObject))
                        dumpThread.StackObjectsInternal.Add(dumpObject);
                    else
                        log.Trace($"Thread: {clrThread.OSThreadId} has a reference to {clrThreadObject.Object} but it was not in the heap repository");
                }

                DumpThreads.Add(dumpThread.OsId, dumpThread);
            }
            PopulateRunawayData(debuggerProxy);
        }

        public IDumpThread Get(uint osId)
        {
            if(DumpThreads.ContainsKey(osId))
                return DumpThreads[osId];
            Log.Debug($"OsId: {osId} was requested, but not found");
            throw new IndexOutOfRangeException($"There is no thread with os id = {osId} registered");
        }

        private void PopulateRunawayData(DebuggerProxy debuggerProxy)
        {
            string runawayData = debuggerProxy.Execute("!runaway");
            Log.Debug($"Calling !runaway returned: {runawayData}");

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
                var id = Convert.ToUInt32(match.Groups["id"].Value, 16);
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
}