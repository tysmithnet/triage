using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Core
{
    public class DumpHandle
    {
        public ulong Address { get; set; }
        public ulong ObjectAddress { get; set; }
        public DumpType ObjectType { get; set; }
        public bool IsStrong { get; set; }
        public bool IsPinned { get; set; }
        public HandleType HandleType { get; set; }
        public uint RefCount { get; set; }
        public ulong DependentTarget { get; set; }
        public DumpType DependentType { get; set; }
        public DumpAppDomain AppDomain { get; set; }
    }
}
