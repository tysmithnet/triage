// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 12-19-2017
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-25-2018
// ***********************************************************************
// <copyright file="DumpModuleRepository.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using Triage.Mortician.Core;

namespace Triage.Mortician.Repositories
{
    /// <summary>
    ///     An object capable of managing all the discovered modules in the memory dump
    /// </summary>
    /// <seealso cref="Triage.Mortician.Core.IDumpModuleRepository" />
    /// <seealso cref="IDumpModuleRepository" />
    public class DumpModuleRepository : IDumpModuleRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DumpModuleRepository" /> class.
        /// </summary>
        /// <param name="dumpModules">The dump modules.</param>
        /// <exception cref="System.ArgumentNullException">dumpModules</exception>
        /// <exception cref="ArgumentNullException">dumpModules</exception>
        protected internal DumpModuleRepository(Dictionary<DumpModuleKey, DumpModule> dumpModules)
        {
            DumpModules = dumpModules ?? throw new ArgumentNullException(nameof(dumpModules));
        }

        /// <summary>
        ///     The extracted modules found in the memory dump
        ///     Note that a module is identified by the tuble (assemblyId, moduleName)
        /// </summary>
        protected internal Dictionary<DumpModuleKey, DumpModule> DumpModules;

        /// <summary>
        ///     Gets the specified assembly identifier.
        /// </summary>
        /// <param name="assemblyId">The assembly identifier.</param>
        /// <param name="moduleName">Name of the module.</param>
        /// <returns>DumpModule.</returns>
        public DumpModule Get(ulong assemblyId, string moduleName) => DumpModules[new DumpModuleKey(assemblyId, moduleName)];

        /// <summary>
        ///     Gets this instance.
        /// </summary>
        /// <returns>IEnumerable&lt;DumpModule&gt;.</returns>
        public IEnumerable<DumpModule> Get() => DumpModules.Values;
    }
}