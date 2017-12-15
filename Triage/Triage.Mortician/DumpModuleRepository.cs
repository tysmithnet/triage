using System;
using System.Collections.Generic;

namespace Triage.Mortician
{
    public class DumpModuleRepository
    {
        protected internal Dictionary<ulong, DumpModule> DumpModules;

        protected internal DumpModuleRepository(Dictionary<ulong, DumpModule> dumpModules)
        {
            DumpModules = dumpModules ?? throw new ArgumentNullException(nameof(dumpModules));
        }
    }
}