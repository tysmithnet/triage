using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Triage.Mortician.Abstraction;

namespace Triage.Mortician
{
    internal class DumpThread : IDumpThread
    {
        public uint OsId { get; set; }
        public TimeSpan KernelModeTime { get; set; }
        public TimeSpan UserModeTime { get; set; }

        private string _stackTrace;
        public string StackTrace => _stackTrace ?? (_stackTrace = string.Join("\n", StackFrames.Select(s => s.DisplayString)));

        // todo: don't expose writable collection
        public IList<IDumpStackFrame> StackFrames { get; set; }
        public IList<IDumpObject> StackObjectsInternal = new List<IDumpObject>();
        public IReadOnlyCollection<IDumpObject> StackObjects { get; set; }
        public TimeSpan TotalTime { get; set; }
        public uint DebuggerIndex { get; set; }

        public DumpThread()
        {
            StackObjects = new ReadOnlyCollection<IDumpObject>(StackObjectsInternal);
        }
    }
}