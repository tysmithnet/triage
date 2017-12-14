using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triage.Mortician
{
    public class DumpType : IDumpType
    {
        public int BaseSize { get; }
        public IDumpType BaseDumpType { get; }
        public IModule Module { get; }
        public IEnumerable<IDumpType> Interfaces { get; }
        public bool IsAbstract { get; }
        public bool IsInterface { get; }
        public bool IsArray { get; }
        public bool IsEnum { get; }
        public bool ContainsPointers { get; }
        public bool IsException { get; }
        public bool IsFinalizable { get; }
        public bool IsInternal { get; }
        public bool IsRuntimeType { get; }
        public bool IsProtected { get; }
        public bool IsPrivate { get; }
        public bool IsPointer { get; }
        public bool IsSealed { get; }
        public bool IsPrimitive { get; }
        public bool IsString { get; }
        public IEnumerable<IDumpObject> Objects { get; }
    }
}
