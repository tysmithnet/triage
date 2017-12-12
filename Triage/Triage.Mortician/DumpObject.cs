using System.Collections.Generic;
using System.Diagnostics;
using Triage.Mortician.Abstraction;

namespace Triage.Mortician
{
    [DebuggerDisplay("{FullTypeName} : {Size} : {Address}")]
    public class DumpObject : IDumpObject
    {
        internal IList<IDumpObject> ReferencesInternal = new List<IDumpObject>();

        // todo: constructor args are already unwieldy, refactor to factory
        public DumpObject(ulong address, string fullTypeName, ulong size, int gen)
        {
            Address = address;
            FullTypeName = fullTypeName;
            Size = size;
            Gen = gen;
        }

        public ulong Address { get; internal set; }
        public string FullTypeName { get; internal set; }
        public ulong Size { get; internal set; }
        public int Gen { get; internal set; }
        public IReadOnlyCollection<IDumpObject> References { get; internal set; }

        public void AddReference(IDumpObject obj)
        {
            ReferencesInternal.Add(obj);
        }
    }
}