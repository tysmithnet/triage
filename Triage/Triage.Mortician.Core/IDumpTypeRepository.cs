// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-17-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-24-2018
// ***********************************************************************
// <copyright file="IDumpTypeRepository.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;

namespace Triage.Mortician.Core
{
    /// <summary>
    ///     Interface IDumpTypeRepository
    /// </summary>
    public interface IDumpTypeRepository
    {
        /// <summary>
        ///     Gets this instance.
        /// </summary>
        /// <returns>IEnumerable&lt;DumpType&gt;.</returns>
        IEnumerable<DumpType> Types { get; }

        DumpType Get(DumpTypeKey type);
    }
}