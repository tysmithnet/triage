// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-17-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
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
        IEnumerable<DumpType> Get();
    }
}