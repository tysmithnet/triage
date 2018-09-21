using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Triage.Mortician
{
    public class EeStackOutputProcessor : IEeStackOutputProcessor
    {
        public EeStackReport ProcessOutput(string eeStackOutput)
        {
            var lines = eeStackOutput.Split(new string[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
            return null;
        }
    }

    public sealed class EeStackFrame
    {
        public string Callee { get; internal set; }
        public string Caller { get; internal set; }
        public ulong ChildStackPointer { get; internal set; }
        public ulong ReturnAddress { get; internal set; }
    }

    public sealed class EeStackThread
    {
        public IEnumerable<EeStackFrame> StackFrames => StackFramesInternal;
        internal IList<EeStackFrame> StackFramesInternal { get; set; } = new List<EeStackFrame>();
    }

    public sealed class EeStackReport
    {
        public IEnumerable<EeStackThread> Threads => ThreadsInternal;
        internal IList<EeStackThread> ThreadsInternal { get; set; } = new List<EeStackThread>();
    }
}