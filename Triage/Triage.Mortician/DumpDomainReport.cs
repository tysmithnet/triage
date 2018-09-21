using System.Collections.Generic;

namespace Triage.Mortician
{
    /// <summary>
    ///     Class DumpDomainReport. This class cannot be inherited.
    /// </summary>
    /// <seealso cref="Triage.Mortician.IReport" />
    public sealed class DumpDomainReport : IReport
    {
        /// <summary>
        ///     Gets the application domains.
        /// </summary>
        /// <value>The application domains.</value>
        public IEnumerable<DumpDomainAppDomain> AppDomains => AppDomainsInternal;

        /// <summary>
        ///     Gets the shared domain.
        /// </summary>
        /// <value>The shared domain.</value>
        public DumpDomainAppDomain SharedDomain { get; internal set; }

        /// <summary>
        ///     Gets the system domain.
        /// </summary>
        /// <value>The system domain.</value>
        public DumpDomainAppDomain SystemDomain { get; internal set; }

        /// <summary>
        ///     Gets or sets the application domains internal.
        /// </summary>
        /// <value>The application domains internal.</value>
        internal IList<DumpDomainAppDomain> AppDomainsInternal { get; set; }
    }
}