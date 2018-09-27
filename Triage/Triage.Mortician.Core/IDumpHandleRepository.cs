using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triage.Mortician.Core
{
    public interface IDumpHandleRepository
    {
        IEnumerable<DumpHandle> Get();
    }

    public class DumpHandleRepository : IDumpHandleRepository
    {
        public DumpHandleRepository(Dictionary<ulong, DumpHandle> handleStore)
        {
            HandlesInternal = handleStore.Values.ToList();
        }

        internal IList<DumpHandle> HandlesInternal { get; set; }

        /// <inheritdoc />
        public IEnumerable<DumpHandle> Get() => HandlesInternal;
    }
}
