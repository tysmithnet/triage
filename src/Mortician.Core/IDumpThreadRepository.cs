// ***********************************************************************
// Assembly         : Mortician
// Author           : @tysmithnet
// Created          : 09-17-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="IDumpThreadRepository.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;

namespace Mortician.Core
{
    /// <summary>
    ///     Interface IDumpThreadRepository
    /// </summary>
    public interface IDumpThreadRepository
    {
        /// <summary>
        ///     Gets the thread with the provided id
        /// </summary>
        /// <param name="osId">The os identifier.</param>
        /// <returns>Get the thread with the operation system id provided</returns>
        /// <exception cref="IndexOutOfRangeException">If the there isn't a thread registered with the specified id</exception>
        DumpThread Get(uint osId);

        /// <summary>
        ///     Gets all the threads extracted from the memory dump
        /// </summary>
        /// <returns>All the threads extracted from the memory dump</returns>
        IEnumerable<DumpThread> Threads { get; }
    }
}