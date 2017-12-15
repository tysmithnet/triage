using System;
using System.Collections.Generic;

namespace Triage.Mortician
{
    public class DumpTypeRepository
    {
        protected internal Dictionary<DumpTypeKey, DumpType> Types;

        protected internal DumpTypeRepository(Dictionary<DumpTypeKey, DumpType> dumpTypes)
        {
            Types = dumpTypes ?? throw new ArgumentNullException(nameof(dumpTypes));
        }
    }
}