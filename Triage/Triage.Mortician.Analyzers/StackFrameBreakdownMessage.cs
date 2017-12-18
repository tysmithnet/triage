using System.Collections.Generic;

namespace Triage.Mortician.Analyzers
{
    public class StackFrameBreakdownMessage : Message
    {
        public IReadOnlyList<StackFrameRollupRecord> Records { get; protected internal set; }
    }
}