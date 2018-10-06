// ***********************************************************************
// Assembly         : Mortician.Core
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="IGCRoot.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using System.Threading;

namespace Mortician.Core.ClrMdAbstractions
{
    /// <summary>
    ///     Interface IGcRoot
    /// </summary>
    public interface IGcRoot
    {
        /// <summary>
        ///     Since GCRoot can be long running, this event will provide periodic updates to how many objects the algorithm
        ///     has processed.  Note that in the case where we search all objects and do not find a path, it's unlikely that
        ///     the number of objects processed will ever reach the total number of objects on the heap.  That's because there
        ///     will be garbage objects on the heap we can't reach.
        /// </summary>
        event GcRootProgressEvent ProgressUpdate;

        /// <summary>
        ///     Builds a cache of the GC heap and roots.  This will consume a LOT of memory, so when calling it you must wrap this
        ///     in
        ///     a try/catch for OutOfMemoryException.
        ///     Note that this function allows you to choose whether we have exact thread callstacks or not.  Exact thread
        ///     callstacks
        ///     will essentially force ClrMD to walk the stack as a real GC would, but this can take 10s of minutes when the thread
        ///     count gets
        ///     into the 1000s.
        /// </summary>
        /// <param name="cancelToken">The cancellation token used to cancel the operation if it's taking too long.</param>
        void BuildCache(CancellationToken cancelToken);

        /// <summary>
        ///     Clears all caches, reclaiming most memory held by this GCRoot object.
        /// </summary>
        void ClearCache();

        /// <summary>
        ///     Returns the path from the start object to the end object (or null if no such path exists).
        /// </summary>
        /// <param name="source">The initial object to start the search from.</param>
        /// <param name="target">The object we are searching for.</param>
        /// <param name="unique">Whether to only enumerate fully unique paths.</param>
        /// <param name="cancelToken">A cancellation token to stop enumeration.</param>
        /// <returns>A path from 'source' to 'target' if one exists, null if one does not.</returns>
        IEnumerable<IList<IClrObject>> EnumerateAllPaths(ulong source, ulong target, bool unique,
            CancellationToken cancelToken);

        /// <summary>
        ///     Enumerates GCRoots of a given object.  Similar to !gcroot.  Note this function only returns paths that are fully
        ///     unique.
        /// </summary>
        /// <param name="target">The target object to search for GC rooting.</param>
        /// <param name="cancelToken">A cancellation token to stop enumeration.</param>
        /// <returns>An enumeration of all GC roots found for target.</returns>
        IEnumerable<IRootPath> EnumerateGCRoots(ulong target, CancellationToken cancelToken);

        /// <summary>
        ///     Enumerates GCRoots of a given object.  Similar to !gcroot.
        /// </summary>
        /// <param name="target">The target object to search for GC rooting.</param>
        /// <param name="unique">Whether to only return fully unique paths.</param>
        /// <param name="cancelToken">A cancellation token to stop enumeration.</param>
        /// <returns>An enumeration of all GC roots found for target.</returns>
        IEnumerable<IRootPath> EnumerateGCRoots(ulong target, bool unique, CancellationToken cancelToken);

        /// <summary>
        ///     Returns the path from the start object to the end object (or null if no such path exists).
        /// </summary>
        /// <param name="source">The initial object to start the search from.</param>
        /// <param name="target">The object we are searching for.</param>
        /// <param name="cancelToken">A cancellation token to stop searching.</param>
        /// <returns>A path from 'source' to 'target' if one exists, null if one does not.</returns>
        IList<IClrObject> FindSinglePath(ulong source, ulong target, CancellationToken cancelToken);

        /// <summary>
        ///     Whether or not to allow GC root to search in parallel or not.  Note that GCRoot does not have to respect this
        ///     flag.  Parallel searching of roots will only happen if a copy of the stack and heap were built using BuildCache,
        ///     and if the entire heap was cached.  Note that ClrMD and underlying APIs do NOT support multithreading, so this
        ///     is only used when we can ensure all relevant data is local memory and we do not need to touch the debuggee.
        /// </summary>
        /// <value><c>true</c> if [allow parallel search]; otherwise, <c>false</c>.</value>
        bool AllowParallelSearch { get; }

        /// <summary>
        ///     Returns the heap that's associated with this GCRoot instance.
        /// </summary>
        /// <value>The heap.</value>
        IClrHeap Heap { get; }

        /// <summary>
        ///     Returns true if all relevant heap and root data is locally cached in this process for fast GCRoot processing.
        /// </summary>
        /// <value><c>true</c> if this instance is fully cached; otherwise, <c>false</c>.</value>
        bool IsFullyCached { get; }

        /// <summary>
        ///     The maximum number of tasks allowed to run in parallel, if GCRoot does a parallel search.
        /// </summary>
        /// <value>The maximum tasks allowed.</value>
        int MaximumTasksAllowed { get; }
    }
}