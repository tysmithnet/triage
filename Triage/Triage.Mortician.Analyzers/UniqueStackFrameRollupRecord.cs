using System.Collections.Generic;
using Triage.Mortician.Domain;

namespace Triage.Mortician.Analyzers
{
    public class UniqueStackFrameRollupRecord
    {
        public string DisplayString { get; set; }
        public IReadOnlyList<DumpThread> Threads { get; set; }
    }
}