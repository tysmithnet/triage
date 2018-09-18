using System;
using System.Collections.Generic;
using Triage.Mortician.Domain;

namespace Triage.Mortician.Repository
{
    /// <summary>
    ///     An object capable of managing all the discovered modules in the memory dump
    /// </summary>
    public class DumpModuleRepository : IDumpModuleRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DumpModuleRepository" /> class.
        /// </summary>
        /// <param name="dumpModules">The dump modules.</param>
        /// <exception cref="ArgumentNullException">dumpModules</exception>
        protected internal DumpModuleRepository(Dictionary<(ulong, string), DumpModule> dumpModules)
        {
            DumpModules = dumpModules ?? throw new ArgumentNullException(nameof(dumpModules));
        }

        /// <summary>
        ///     The extracted modules found in the memory dump
        ///     Note that a module is identified by the tuble (assemblyId, moduleName)
        /// </summary>
        protected internal Dictionary<(ulong, string), DumpModule> DumpModules;

        /// <summary>
        ///     Gets the specified assembly identifier.
        /// </summary>
        /// <param name="assemblyId">The assembly identifier.</param>
        /// <param name="moduleName">Name of the module.</param>
        /// <returns></returns>
        public DumpModule Get(ulong assemblyId, string moduleName)
        {
            return DumpModules[(assemblyId, moduleName)];
        }

        /// <summary>
        ///     Gets this instance.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DumpModule> Get()
        {
            return DumpModules.Values;
        }
    }
}