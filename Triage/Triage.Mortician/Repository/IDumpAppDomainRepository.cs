// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-17-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="IDumpAppDomainRepository.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using Triage.Mortician.Domain;

namespace Triage.Mortician.Repository
{
    /// <summary>
    ///     Interface IDumpAppDomainRepository
    /// </summary>
    public interface IDumpAppDomainRepository
    {
        /// <summary>
        ///     Gets the app domain associated with the provided address
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns>DumpAppDomain.</returns>
        DumpAppDomain Get(ulong address);

        /// <summary>
        ///     Gets all the extracted appd domains
        /// </summary>
        /// <returns>IEnumerable&lt;DumpAppDomain&gt;.</returns>
        IEnumerable<DumpAppDomain> Get();
    }
}