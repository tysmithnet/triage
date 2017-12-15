using System;
using System.Collections.Generic;

namespace Triage.Mortician
{
    /// <summary>
    ///     An object capable of managing the discovered app domains from the memory dump
    /// </summary>
    public class DumpAppDomainRepository
    {
        /// <summary>
        ///     The application domains
        /// </summary>
        protected internal Dictionary<ulong, DumpAppDomain> AppDomains;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DumpAppDomainRepository" /> class. The key
        ///     to the dictionary is the address of the app domain in memory
        /// </summary>
        /// <param name="appDomains">The application domains.</param>
        /// <exception cref="ArgumentNullException">appDomains</exception>
        protected internal DumpAppDomainRepository(Dictionary<ulong, DumpAppDomain> appDomains)
        {
            AppDomains = appDomains ?? throw new ArgumentNullException(nameof(appDomains));
        }
    }
}