// ***********************************************************************
// Assembly         : Mortician
// Author           : @tysmithnet
// Created          : 09-20-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-26-2018
// ***********************************************************************
// <copyright file="DumpDomainReport.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace Mortician.Reports.DumpDomain
{
    /// <summary>
    ///     Class DumpDomainReport. This class cannot be inherited.
    /// </summary>
    /// <seealso cref="Mortician.Reports.IReport" />
    /// <seealso cref="IReport" />
    [Export]
    public sealed class DumpDomainReport : IReport
    {
        /// <summary>
        ///     Gets the application domains.
        /// </summary>
        /// <value>The application domains.</value>
        public IEnumerable<DumpDomainAppDomain> AppDomains => AppDomainsInternal;

        /// <summary>
        ///     Gets the default domain.
        /// </summary>
        /// <value>The default domain.</value>
        public DumpDomainAppDomain DefaultDomain { get; internal set; }

        /// <summary>
        ///     Gets the raw output.
        /// </summary>
        /// <value>The raw output.</value>
        /// <inheritdoc />
        public string RawOutput { get; internal set; }

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
        internal IList<DumpDomainAppDomain> AppDomainsInternal { get; set; } = new List<DumpDomainAppDomain>();
    }
}