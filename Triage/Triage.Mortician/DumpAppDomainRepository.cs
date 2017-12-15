using System;
using System.Collections.Generic;

namespace Triage.Mortician
{
    public class DumpAppDomainRepository
    {
        protected internal Dictionary<ulong, DumpAppDomain> AppDomains;

        protected internal DumpAppDomainRepository(Dictionary<ulong, DumpAppDomain> appDomainStore)
        {
            AppDomains = appDomainStore ?? throw new ArgumentNullException(nameof(appDomainStore));
        }
    }
}