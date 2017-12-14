using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triage.Mortician
{
    public class DumpModule
    {
        public string AssemblyName { get; }
        public string FileName { get; }
        public ulong AssemblyId { get; }
        public bool IsDynamic { get; }
        public IEnumerable<DumpAppDomain> AppDomains { get; }
        public DebuggableAttribute DebuggingMode { get; }
        public ulong ImageBase { get; }
        public bool IsFile { get; }
        public string Name { get; }
        public ulong Size { get; }
        public string PdbFile { get; }
    }
}
