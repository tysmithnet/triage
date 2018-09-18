using System.Collections.Generic;
using Triage.Mortician.Domain;

namespace Triage.Mortician.Repository
{
    public interface IDumpAppDomainRepository
    {
        /// <summary>
        ///     Gets the app domain associated with the provided address
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        DumpAppDomain Get(ulong address);

        /// <summary>
        ///     Gets all the extracted appd domains
        /// </summary>
        /// <returns></returns>
        IEnumerable<DumpAppDomain> Get();
    }
}