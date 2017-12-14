using System.Collections.Generic;

namespace Triage.Mortician
{
    /// <summary>
    ///     Represents an object that is capable of managing the threads extracted from the dump
    /// </summary>
    public interface IDumpThreadRepository
    {
        /// <summary>
        ///     Gets the thread with the specified os id
        /// </summary>
        /// <param name="osId">The os identifier.</param>
        /// <returns>The thread with the specified os id</returns>
        IDumpThread Get(uint osId);

        /// <summary>
        ///     Gets all the threads
        /// </summary>
        /// <returns>All of the threads</returns>
        IEnumerable<IDumpThread> Get();
    }
}