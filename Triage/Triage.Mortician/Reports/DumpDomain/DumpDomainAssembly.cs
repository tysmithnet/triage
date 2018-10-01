// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-21-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-21-2018
// ***********************************************************************
// <copyright file="DumpDomainAssembly.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;

namespace Triage.Mortician.Reports.DumpDomain
{
    /// <summary>
    ///     Class DumpDomainAssembly. This class cannot be inherited.
    /// </summary>
    public sealed class DumpDomainAssembly
    {
        /// <summary>
        ///     Gets the address.
        /// </summary>
        /// <value>The address.</value>
        public ulong Address { get; internal set; }

        /// <summary>
        ///     Gets the class loader.
        /// </summary>
        /// <value>The class loader.</value>
        public ulong ClassLoader { get; internal set; }

        /// <summary>
        ///     Gets the location.
        /// </summary>
        /// <value>The location.</value>
        public string Location { get; internal set; }

        /// <summary>
        ///     Gets the modules.
        /// </summary>
        /// <value>The modules.</value>
        public IEnumerable<DumpDomainModule> Modules => ModulesInternal;

        /// <summary>
        ///     Gets the security descriptor.
        /// </summary>
        /// <value>The security descriptor.</value>
        public ulong SecurityDescriptor { get; internal set; }

        /// <summary>
        ///     Gets or sets the modules internal.
        /// </summary>
        /// <value>The modules internal.</value>
        internal IList<DumpDomainModule> ModulesInternal { get; set; } = new List<DumpDomainModule>();
    }
}