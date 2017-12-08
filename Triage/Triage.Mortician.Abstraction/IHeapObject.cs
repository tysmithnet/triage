using System.Collections.Generic;

namespace Triage.Mortician.Abstraction
{
    public interface IHeapObject
    {
        ulong Address { get; }
        string FullTypeName { get; }
        ulong Size { get; }
        IReadOnlyCollection<IHeapObject> References { get; }
        void AddReference(IHeapObject obj);
    }
}