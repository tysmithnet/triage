using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Triage.Mortician
{
    public class DumpModule
    {
        public string AssemblyName { get; protected internal set; }
        public string FileName { get; protected internal set; }
        public ulong AssemblyId { get; protected internal set; }
        public bool IsDynamic { get; protected internal set; }
        protected internal IList<DumpAppDomain> AppDomainsInternal = new List<DumpAppDomain>();
        public IEnumerable<DumpAppDomain> AppDomains { get; protected internal set; }
        public DebuggableAttribute.DebuggingModes DebuggingMode { get; protected internal set; }
        public ulong? ImageBase { get; protected internal set; }
        public bool IsFile { get; protected internal set; }
        public string Name { get; protected internal set; }
        public ulong Size { get; protected internal set; }
        public string PdbFile { get; protected internal set; }
        public Guid PdbGuid { get; protected internal set; }            
        protected internal IList<DumpType> TypesInternal = new List<DumpType>();
    }
}