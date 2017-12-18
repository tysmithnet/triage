using System.Collections.Generic;
using Triage.Mortician.Domain;

namespace Triage.Mortician.Analyzers
{
    public class UniqueStacksMessage : Message
    {
        public IReadOnlyList<UniqueStackFrameRollupRecord> UniqueStackFrameRollupRecords { get; set; }
    }
}