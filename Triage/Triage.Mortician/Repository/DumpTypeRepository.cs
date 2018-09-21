// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 12-19-2017
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="DumpTypeRepository.cs" company="">
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
    ///     An object capable of managing all the types extracted from the memory dump
    /// </summary>
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
            Types = dumpTypes ?? throw new ArgumentNullException(nameof(dumpTypes));
        }

        /// <summary>
        ///     The types extracted from the memory dump
        /// </summary>
        protected internal Dictionary<DumpTypeKey, DumpType> Types;
    }
}