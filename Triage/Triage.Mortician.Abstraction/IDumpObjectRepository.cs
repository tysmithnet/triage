using System.Collections.Generic;

namespace Triage.Mortician.Abstraction
{
    public interface IDumpObjectRepository
    {
        IDumpObject Get(ulong address);
        IEnumerable<IDumpObject> Get();
    }
}