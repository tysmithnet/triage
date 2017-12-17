using System;
using System.Collections.Generic;
using Triage.Mortician.Domain;

namespace Triage.Mortician.Repository
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

        /// <summary>
        ///     Initializes a new instance of the <see cref="DumpModuleRepository" /> class.
        /// </summary>
        /// <param name="dumpModules">The dump modules.</param>
        /// <exception cref="ArgumentNullException">dumpModules</exception>
        protected internal DumpModuleRepository(Dictionary<(ulong, string), DumpModule> dumpModules)
        {
            DumpModules = dumpModules ?? throw new ArgumentNullException(nameof(dumpModules));
        }
    }
}