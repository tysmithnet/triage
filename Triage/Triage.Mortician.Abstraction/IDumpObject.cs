using System.Collections.Generic;

namespace Triage.Mortician.Abstraction
{
    public interface IDumpObject
    {
        ulong Address { get; }
        string FullTypeName { get; }
        ulong Size { get; }
        IReadOnlyCollection<IDumpObject> References { get; }
        void AddReference(IDumpObject obj);
    }
}