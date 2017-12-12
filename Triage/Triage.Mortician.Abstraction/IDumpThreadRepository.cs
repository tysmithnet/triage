using System.Collections.Generic;

namespace Triage.Mortician.Abstraction
{
    public interface IDumpThreadRepository
    {
        IDumpThread Get(uint osId);
        IEnumerable<IDumpThread> Get();
    }
}