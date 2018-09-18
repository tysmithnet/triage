using System;
using System.Collections.Generic;
using Triage.Mortician.Domain;

namespace Triage.Mortician.Repository
{
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
        IEnumerable<DumpThread> Get();
    }
}