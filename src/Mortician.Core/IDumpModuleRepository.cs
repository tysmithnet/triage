// ***********************************************************************
// Assembly         : Mortician
// Author           : @tysmithnet
// Created          : 09-17-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="IDumpModuleRepository.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;

namespace Mortician.Core
{
    /// <summary>
    ///     Interface IDumpModuleRepository
    /// </summary>
    public interface IDumpModuleRepository
    {
        /// <summary>
        ///     Gets the specified assembly identifier.
        /// </summary>
        /// <param name="assemblyId">The assembly identifier.</param>
        /// <param name="moduleName">Name of the module.</param>
        /// <returns>DumpModule.</returns>
        DumpModule Get(ulong assemblyId, string moduleName);

        /// <summary>
        ///     Gets this instance.
        /// </summary>
        /// <returns>IEnumerable&lt;DumpModule&gt;.</returns>
        IEnumerable<DumpModule> Modules { get; }
    }
}