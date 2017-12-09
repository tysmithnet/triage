using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Triage.Mortician.Abstraction;

namespace Triage.Mortician.Web
{
    internal class DumpThread : IDumpThread
    {
        public uint OsId { get; set; }
        public TimeSpan KernelModeTime { get; set; }
        public TimeSpan UserModeTime { get; set; }
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