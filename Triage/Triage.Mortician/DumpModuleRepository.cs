using System;
using System.Collections.Generic;

namespace Triage.Mortician
{
    /// <summary>
    ///     An object capable of managing all the discovered modules in the memory dump
    /// </summary>
    public class DumpModuleRepository
    {
        protected internal Dictionary<ulong, DumpModule> DumpModules;

        protected internal DumpModuleRepository(Dictionary<ulong, DumpModule> dumpModules)
        {
            DumpModules = dumpModules ?? throw new ArgumentNullException(nameof(dumpModules));
        }
    }
}