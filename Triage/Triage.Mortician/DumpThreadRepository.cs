using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Common.Logging;
using Microsoft.Diagnostics.Runtime;
using Triage.Mortician.Abstraction;

namespace Triage.Mortician
{
    /// <summary>
    ///     Represents a repository that stores threads that were extracted from the memory dump
    /// </summary>
    /// <seealso cref="Triage.Mortician.Abstraction.IDumpThreadRepository" />
    internal class DumpThreadRepository : IDumpThreadRepository
    {
        /// <summary>
        ///     The log
        /// </summary>
        protected ILog Log = LogManager.GetLogger(typeof(DumpThreadRepository));

        /// <summary>
        ///     Initializes a new instance of the <see cref="DumpThreadRepository" /> class.
        /// </summary>
        /// <param name="rt">The rt.</param>
        /// <param name="debuggerProxy">The debugger proxy.</param>
        /// <param name="dumpRepo">The dump repo.</param>
        public DumpThreadRepository(ClrRuntime rt, DebuggerProxy debuggerProxy, DumpObjectRepository dumpRepo)
        {
            var log = LogManager.GetLogger(typeof(DumpThreadRepository));
            foreach (var clrThread in rt.Threads.Where(t => t.IsAlive))
            {
                var dumpThread = new DumpThread();
                dumpThread.OsId = clrThread.OSThreadId;
                dumpThread.StackFrames = clrThread.StackTrace.Select(x => new DumpStackFrame
                {
                    DisplayString = x.DisplayString
                }).Cast<IDumpStackFrame>().ToList();
                foreach (var clrThreadObject in clrThread.EnumerateStackObjects())
                    if (dumpRepo.HeapObjects.TryGetValue(clrThreadObject.Object, out var dumpObject))
                        dumpThread.StackObjectsInternal.Add(dumpObject);
                    else
                        log.Trace(
                            $"Thread: {clrThread.OSThreadId} has a reference to {clrThreadObject.Object} but it was not in the heap repository");

                DumpThreads.Add(dumpThread.OsId, dumpThread);
            }
            PopulateRunawayData(debuggerProxy);
        }

        /// <summary>
        ///     Gets or sets the dump threads.
        /// </summary>
        /// <value>
        ///     The dump threads.
        /// </value>
        public Dictionary<uint, DumpThread> DumpThreads { get; set; } = new Dictionary<uint, DumpThread>();

        /// <summary>
        ///     Gets the thread with the provided id
        /// </summary>
        /// <param name="osId">The os identifier.</param>
        /// <returns>Get the thread with the operation system id provided</returns>
        /// <exception cref="IndexOutOfRangeException">If the there isn't a thread registered with the specified id</exception>
        public IDumpThread Get(uint osId)
        {
            if (DumpThreads.ContainsKey(osId))
                return DumpThreads[osId];
            Log.Debug($"OsId: {osId} was requested, but not found");
            throw new IndexOutOfRangeException($"There is no thread with os id = {osId} registered");
        }

        /// <summary>
        ///     Gets all the threads extracted from the memory dump
        /// </summary>
        /// <returns>All the threads extracted from the memory dump</returns>
        public IEnumerable<IDumpThread> Get()
        {
            return DumpThreads.Values;
        }

        /// <summary>
        ///     Populates the runaway data. This is the data on how long threads have been alive
        /// </summary>
        /// <param name="debuggerProxy">The debugger proxy.</param>
        private void PopulateRunawayData(DebuggerProxy debuggerProxy)
        {
            var runawayData = debuggerProxy.Execute("!runaway");
            Log.Debug($"Calling !runaway returned: {runawayData}");

            var isUserMode = false;
            var isKernelMode = false;
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
                var index = uint.Parse(match.Groups["index"].Value);
                var id = Convert.ToUInt32(match.Groups["id"].Value, 16);
                var days = uint.Parse(match.Groups["days"].Value);
                var time = match.Groups["time"].Value;
                var timeSpan = TimeSpan.Parse(time, CultureInfo.CurrentCulture);
                timeSpan = timeSpan.Add(TimeSpan.FromDays(days));
                var dumpThread = DumpThreads.Values.SingleOrDefault(x => x.OsId == id);
                if (dumpThread == null)
                {
                    Log.Trace($"Found thread {id} in runaway data but not in thread repo");
                    continue;
                }
                dumpThread.DebuggerIndex = index;

                if (isUserMode)
                    dumpThread.UserModeTime = timeSpan;
                else if (isKernelMode)
                    dumpThread.KernelModeTime = timeSpan;
            }
        }
    }
}