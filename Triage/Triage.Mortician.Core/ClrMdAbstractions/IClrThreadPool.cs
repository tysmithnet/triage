// ***********************************************************************
// Assembly         : Triage.Mortician.Core
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="IClrThreadPool.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

namespace Triage.Mortician.Core.ClrMdAbstractions
{
    /// <summary>
    /// Interface IClrThreadPool
    /// </summary>
    public interface IClrThreadPool
    {
        /// <summary>
        /// The total number of threadpool worker threads in the process.
        /// </summary>
        /// <value>The total threads.</value>
        int TotalThreads { get; }

        /// <summary>
        /// The number of running threadpool threads in the process.
        /// </summary>
        /// <value>The running threads.</value>
        int RunningThreads { get; }

        /// <summary>
        /// The number of idle threadpool threads in the process.
        /// </summary>
        /// <value>The idle threads.</value>
        int IdleThreads { get; }

        /// <summary>
        /// The minimum number of threadpool threads allowable.
        /// </summary>
        /// <value>The minimum threads.</value>
        int MinThreads { get; }

        /// <summary>
        /// The maximum number of threadpool threads allowable.
        /// </summary>
        /// <value>The maximum threads.</value>
        int MaxThreads { get; }

        /// <summary>
        /// Returns the minimum number of completion ports (if any).
        /// </summary>
        /// <value>The minimum completion ports.</value>
        int MinCompletionPorts { get; }

        /// <summary>
        /// Returns the maximum number of completion ports.
        /// </summary>
        /// <value>The maximum completion ports.</value>
        int MaxCompletionPorts { get; }

        /// <summary>
        /// Returns the CPU utilization of the threadpool (as a percentage out of 100).
        /// </summary>
        /// <value>The cpu utilization.</value>
        int CpuUtilization { get; }

        /// <summary>
        /// The number of free completion port threads.
        /// </summary>
        /// <value>The free completion port count.</value>
        int FreeCompletionPortCount { get; }

        /// <summary>
        /// The maximum number of free completion port threads.
        /// </summary>
        /// <value>The maximum free completion ports.</value>
        int MaxFreeCompletionPorts { get; }

        /// <summary>
        /// Enumerates the work items on the threadpool (native side).
        /// </summary>
        /// <returns>IEnumerable&lt;INativeWorkItem&gt;.</returns>
        IEnumerable<INativeWorkItem> EnumerateNativeWorkItems();

        /// <summary>
        /// Enumerates work items on the thread pool (managed side).
        /// </summary>
        /// <returns>IEnumerable&lt;IManagedWorkItem&gt;.</returns>
        IEnumerable<IManagedWorkItem> EnumerateManagedWorkItems();
    }
}