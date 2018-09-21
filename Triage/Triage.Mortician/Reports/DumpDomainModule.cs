// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-21-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-21-2018
// ***********************************************************************
// <copyright file="DumpDomainModule.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Triage.Mortician.Reports
{
    /// <summary>
    ///     Class DumpDomainModule. This class cannot be inherited.
    /// </summary>
    public sealed class DumpDomainModule
    {
        /// <summary>
        ///     Gets the address.
        /// </summary>
        /// <value>The address.</value>
        public ulong Address { get; internal set; }

        /// <summary>
        ///     Gets the location.
        /// </summary>
        /// <value>The location.</value>
        public string Location { get; internal set; }
    }
}