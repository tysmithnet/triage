using System.Collections.Generic;
using System.Diagnostics;
using Triage.Mortician.Abstraction;

namespace Triage.Mortician.Api
{
    [DebuggerDisplay("{FullTypeName} : {Size} : {Address}")]
    public class DumpObject : IDumpObject
    {
        public ulong Address { get; internal set; }
        public string FullTypeName { get; internal set; }
        public ulong Size { get; internal set; }

        internal IList<IDumpObject> ReferencesInternal = new List<IDumpObject>();
        public IReadOnlyCollection<IDumpObject> References { get; internal set; }

        public void AddReference(IDumpObject obj)
        {
            ReferencesInternal.Add(obj);
        }

        public DumpObject(ulong address, string fullTypeName, ulong size)
        {
            Address = address;
            FullTypeName = fullTypeName;
            Size = size;
        }                                                                      
    }
}