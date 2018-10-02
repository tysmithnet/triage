// ***********************************************************************
// Assembly         : Triage.Mortician.Core
// Author           : @tysmithnet
// Created          : 09-26-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-01-2018
// ***********************************************************************
// <copyright file="IDumpHandleRepository.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;

namespace Triage.Mortician.Core
{
    /// <summary>
    ///     Interface IDumpHandleRepository
    /// </summary>
    public interface IDumpHandleRepository
    {
        /// <summary>
        ///     Gets this instance.
        /// </summary>
        /// <returns>IEnumerable&lt;DumpHandle&gt;.</returns>
        IEnumerable<DumpHandle> Get();
    }
}