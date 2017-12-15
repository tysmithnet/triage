using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Common.Logging;

namespace Triage.Mortician
{
    /// <summary>
    ///     Represents a repository that stores threads that were extracted from the memory dump
    /// </summary>
    public class DumpThreadRepository
    {
        /// <summary>
        ///     Gets or sets the dump threads.
        /// </summary>
        /// <value>
        ///     The dump threads.
        /// </value>
        protected internal Dictionary<uint, DumpThread> DumpThreads;

        /// <summary>
        ///     The log
        /// </summary>
        protected ILog Log = LogManager.GetLogger(typeof(DumpThreadRepository));

        public DumpThreadRepository(Dictionary<uint, DumpThread> dumpThreads)
        {
            DumpThreads = dumpThreads ?? throw new ArgumentNullException(nameof(dumpThreads));
        }

        /// <summary>
        ///     Gets the thread with the provided id
        /// </summary>
        /// <param name="osId">The os identifier.</param>
        /// <returns>Get the thread with the operation system id provided</returns>
        /// <exception cref="IndexOutOfRangeException">If the there isn't a thread registered with the specified id</exception>
        public DumpThread Get(uint osId)
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
        public IEnumerable<DumpThread> Get()
        {
            return DumpThreads.Values;
        }

        /// <summary>
        ///     Populates the runaway data. This is the data on how long threads have been alive
        /// </summary>
        /// <param name="debuggerProxy">The debugger proxy.</param>
        private void PopulateRunawayData(IDebuggerProxy debuggerProxy)
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