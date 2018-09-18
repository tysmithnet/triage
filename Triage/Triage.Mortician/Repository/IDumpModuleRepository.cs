using System.Collections.Generic;
using Triage.Mortician.Domain;

namespace Triage.Mortician.Repository
{
    public interface IDumpModuleRepository
    {
        /// <summary>
        ///     Gets the specified assembly identifier.
        /// </summary>
        /// <param name="assemblyId">The assembly identifier.</param>
        /// <param name="moduleName">Name of the module.</param>
        /// <returns></returns>
        DumpModule Get(ulong assemblyId, string moduleName);

        /// <summary>
        ///     Gets this instance.
        /// </summary>
        /// <returns></returns>
        IEnumerable<DumpModule> Get();
    }
}