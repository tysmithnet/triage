// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 12-19-2017
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-25-2018
// ***********************************************************************
// <copyright file="DumpTypeRepository.cs" company="">
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
    ///     An object capable of managing all the types extracted from the memory dump
    /// </summary>
    /// <seealso cref="Triage.Mortician.Core.IDumpTypeRepository" />
    /// <seealso cref="IDumpTypeRepository" />
    public class DumpTypeRepository : IDumpTypeRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DumpTypeRepository" /> class.
        /// </summary>
        /// <param name="dumpTypes">The dump types.</param>
        /// <exception cref="System.ArgumentNullException">dumpTypes</exception>
        /// <exception cref="ArgumentNullException">dumpTypes</exception>
        protected internal DumpTypeRepository(Dictionary<DumpTypeKey, DumpType> dumpTypes)
        {
            TypesInternal = dumpTypes ?? throw new ArgumentNullException(nameof(dumpTypes));
        }

        /// <summary>
        ///     The types extracted from the memory dump
        /// </summary>
        internal Dictionary<DumpTypeKey, DumpType> TypesInternal;

        /// <summary>
        ///     Gets this instance.
        /// </summary>
        /// <returns>IEnumerable&lt;DumpType&gt;.</returns>
        public IEnumerable<DumpType> Types => TypesInternal.Values;

        /// <inheritdoc />
        public DumpType Get(DumpTypeKey type)
        {
            if (TypesInternal.TryGetValue(type, out var res))
            {
                return res;
            }
            throw new KeyNotFoundException($"Unable to find key {type}");
        }
    }
}