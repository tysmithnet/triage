using System;
using System.Collections.Generic;

namespace Triage.Mortician
{
    /// <summary>
    ///     An object capable of managing all the discovered modules in the memory dump
    /// </summary>
    public class DumpModuleRepository
    {
        /// <summary>
        ///     The extracted modules found in the memory dump
        ///     Note that a module is identified by the tuble (assemblyId, moduleName)
        /// </summary>
        protected internal Dictionary<(ulong, string), DumpModule> DumpModules;

        protected internal DumpModuleRepository(Dictionary<(ulong, string), DumpModule> dumpModules)
        {
            DumpModules = dumpModules ?? throw new ArgumentNullException(nameof(dumpModules));
        }
    }
}