// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 12-19-2017
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="DumpAppDomainRepository.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using Triage.Mortician.Core;

namespace Triage.Mortician.Repository
{
    /// <summary>
    ///     An object capable of managing the discovered app domains from the memory dump
    /// </summary>
    /// <seealso cref="IDumpAppDomainRepository" />
    public class DumpAppDomainRepository : IDumpAppDomainRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DumpAppDomainRepository" /> class. The key
        ///     to the dictionary is the address of the app domain in memory
        /// </summary>
        /// <param name="appDomains">The application domains.</param>
        /// <exception cref="System.ArgumentNullException">appDomains</exception>
        /// <exception cref="ArgumentNullException">appDomains</exception>
        protected internal DumpAppDomainRepository(Dictionary<ulong, DumpAppDomain> appDomains)
        {
            AppDomains = appDomains ?? throw new ArgumentNullException(nameof(appDomains));
        }

        /// <summary>
        ///     The application domains index by their address
        /// </summary>
        protected internal Dictionary<ulong, DumpAppDomain> AppDomains;

        /// <summary>
        ///     Gets the app domain associated with the provided address
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns>DumpAppDomain.</returns>
        public DumpAppDomain Get(ulong address) => AppDomains[address];

        /// <summary>
        ///     Gets all the extracted appd domains
        /// </summary>
        /// <returns>IEnumerable&lt;DumpAppDomain&gt;.</returns>
        public IEnumerable<DumpAppDomain> Get() => AppDomains.Values;
    }
}