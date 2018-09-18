// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 12-19-2017
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="DumpModuleRepository.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using Triage.Mortician.Core;
using Triage.Mortician.Domain;

namespace Triage.Mortician.Repository
{
    /// <summary>
    ///     An object capable of managing all the discovered modules in the memory dump
    /// </summary>
    /// <seealso cref="IDumpModuleRepository" />
    public class DumpModuleRepository : IDumpModuleRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DumpModuleRepository" /> class.
        /// </summary>
        /// <param name="dumpModules">The dump modules.</param>
        /// <exception cref="System.ArgumentNullException">dumpModules</exception>
        /// <exception cref="ArgumentNullException">dumpModules</exception>
        protected internal DumpModuleRepository(Dictionary<(ulong, string), DumpModule> dumpModules)
        {
            DumpModules = dumpModules ?? throw new ArgumentNullException(nameof(dumpModules));
        }

        /// <summary>
        ///     The extracted modules found in the memory dump
        ///     Note that a module is identified by the tuble (assemblyId, moduleName)
        /// </summary>
        protected internal Dictionary<(ulong, string), DumpModule> DumpModules;

        /// <summary>
        ///     Gets the specified assembly identifier.
        /// </summary>
        /// <param name="assemblyId">The assembly identifier.</param>
        /// <param name="moduleName">Name of the module.</param>
        /// <returns>DumpModule.</returns>
        public DumpModule Get(ulong assemblyId, string moduleName)
        {
            return DumpModules[(assemblyId, moduleName)];
        }

        /// <summary>
        ///     Gets this instance.
        /// </summary>
        /// <returns>IEnumerable&lt;DumpModule&gt;.</returns>
        public IEnumerable<DumpModule> Get()
        {
            return DumpModules.Values;
        }
    }
}