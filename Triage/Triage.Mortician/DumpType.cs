using System.Collections.Generic;

namespace Triage.Mortician
{
    public class DumpType
    {
        protected internal IList<DumpType> InterfacesInternal = new List<DumpType>()
            ; // todo: should this be concurrent?

        protected internal Dictionary<ulong, DumpObject> ObjectsInternal = new Dictionary<ulong, DumpObject>();
        public int BaseSize { get; protected internal set; }
        public DumpType BaseDumpType { get; protected internal set; }
        public DumpModule Module { get; protected internal set; }
        public IEnumerable<DumpType> Interfaces { get; protected internal set; }

        public bool IsAbstract { get; protected internal set; }
        public bool IsInterface { get; protected internal set; }
        public bool IsArray { get; protected internal set; }
        public bool IsEnum { get; protected internal set; }
        public bool ContainsPointers { get; protected internal set; }
        public bool IsException { get; protected internal set; }
        public bool IsFinalizable { get; protected internal set; }
        public bool IsInternal { get; protected internal set; }
        public bool IsRuntimeType { get; protected internal set; }
        public bool IsProtected { get; protected internal set; }
        public bool IsPrivate { get; protected internal set; }
        public bool IsPointer { get; protected internal set; }
        public bool IsSealed { get; protected internal set; }
        public bool IsPrimitive { get; protected internal set; }
        public bool IsString { get; protected internal set; }
        public IEnumerable<DumpObject> Objects { get; protected internal set; }
        public string Name { get; protected internal set; }
        public ulong MethodTable { get; protected internal set; }
    }
}