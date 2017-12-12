using System;
using System.Collections.Generic;

namespace Triage.Mortician.Abstraction
{
    public interface IDumpThread
    {
        uint DebuggerIndex { get; }
        TimeSpan KernelModeTime { get; }
        uint OsId { get; set; }
        IReadOnlyCollection<IDumpObject> StackObjects { get; }
        TimeSpan TotalTime { get; }
        TimeSpan UserModeTime { get; }
        IList<IDumpStackFrame> StackFrames { get; }
    }

    public interface IDumpStackFrame
    {
        string DisplayString { get; }
    }
}