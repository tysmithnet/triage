// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 12-19-2017
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-25-2018
// ***********************************************************************
// <copyright file="DumpThreadRepository.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using Common.Logging;
using Triage.Mortician.Core;

namespace Triage.Mortician.Repository
{
    /// <summary>
    ///     Represents a repository that stores threads that were extracted from the memory dump
    /// </summary>
    /// <seealso cref="Triage.Mortician.Core.IDumpThreadRepository" />
    /// <seealso cref="IDumpThreadRepository" />
    public class DumpThreadRepository : IDumpThreadRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DumpThreadRepository" /> class.
        /// </summary>
        /// <param name="dumpThreads">The dump threads.</param>
        /// <exception cref="System.ArgumentNullException">dumpThreads</exception>
        /// <exception cref="ArgumentNullException">dumpThreads</exception>
        protected internal DumpThreadRepository(Dictionary<uint, DumpThread> dumpThreads)
        {
            DumpThreads = dumpThreads ?? throw new ArgumentNullException(nameof(dumpThreads));
        }

        /// <summary>
        ///     Gets or sets the dump threads.
        /// </summary>
        /// <value>
        ///     The dump threads.
        /// </value>
        protected internal Dictionary<uint, DumpThread> DumpThreads;

        /// <summary>
        ///     The log
        /// </summary>
        protected internal ILog Log = LogManager.GetLogger(typeof(DumpThreadRepository));

        /// <summary>
        ///     Gets the thread with the provided id
        /// </summary>
        /// <param name="osId">The os identifier.</param>
        /// <returns>Get the thread with the operation system id provided</returns>
        /// <exception cref="System.IndexOutOfRangeException"></exception>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public DumpThread Get(uint osId)
        {
            if (DumpThreads.ContainsKey(osId))
                return DumpThreads[osId];
            Log.Debug($"OsId: {osId} was requested, but not found");
            throw new IndexOutOfRangeException($"There is no thread with os id = {osId} registered");
        }

        /// <summary>
        ///     Gets all the threads extracted from the memory dump
        /// </summary>
        /// <returns>All the threads extracted from the memory dump</returns>
        public IEnumerable<DumpThread> Get() => DumpThreads.Values;
    }
}