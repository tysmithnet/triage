using System;
using System.Collections.Generic;
using Triage.Mortician.Abstraction;

namespace Triage.Mortician
{
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