using System.Collections.Generic;
using Triage.Mortician.Abstraction;

namespace Triage.Mortician
{
    public class HeapObject : IHeapObject
    {
        public ulong Address { get; internal set; }
        public string FullTypeName { get; internal set; }
        public ulong Size { get; internal set; }

        internal IList<IHeapObject> ReferencesInternal = new List<IHeapObject>();
        public IReadOnlyCollection<IHeapObject> References { get; internal set; }

        public void AddReference(IHeapObject obj)
        {
            ReferencesInternal.Add(obj);
        }

        public HeapObject(ulong address, string fullTypeName, ulong size)
        {
            Address = address;
            FullTypeName = fullTypeName;
            Size = size;
        }                                                                      
    }
}