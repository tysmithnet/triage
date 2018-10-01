using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Core
{
    public class DumpBlockingObject
    {
        public ulong Address { get; set; }
        public bool IsLocked { get; set; }
        public int RecursionCount { get; set; }
        public IList<DumpThread> Owners { get; set; }
        public IList<DumpThread> Waiters { get; set; }
        public BlockingReason BlockingReason { get; set; }
    }
}
