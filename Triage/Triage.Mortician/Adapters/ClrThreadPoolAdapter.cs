// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-19-2018
// ***********************************************************************
// <copyright file="ClrThreadPoolAdapter.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Diagnostics.Runtime;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    /// <summary>
    ///     Class ClrThreadPoolAdapter.
    /// </summary>
    /// <seealso cref="Triage.Mortician.Core.ClrMdAbstractions.IClrThreadPool" />
    internal class ClrThreadPoolAdapter : IClrThreadPool
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ClrThreadPoolAdapter" /> class.
        /// </summary>
        /// <param name="threadPool">The thread pool.</param>
        /// <exception cref="ArgumentNullException">threadPool</exception>
        /// <inheritdoc />
        public ClrThreadPoolAdapter(ClrThreadPool threadPool)
        {
            ThreadPool = threadPool ?? throw new ArgumentNullException(nameof(threadPool));
        }

        /// <summary>
        ///     The thread pool
        /// </summary>
        internal ClrThreadPool ThreadPool;

        /// <summary>
        ///     Enumerates work items on the thread pool (managed side).
        /// </summary>
        /// <returns>IEnumerable&lt;IManagedWorkItem&gt;.</returns>
        /// <inheritdoc />
        public IEnumerable<IManagedWorkItem> EnumerateManagedWorkItems() =>
            ThreadPool.EnumerateManagedWorkItems().Select(Converter.Convert);

        /// <summary>
        ///     Enumerates the work items on the threadpool (native side).
        /// </summary>
        /// <returns>IEnumerable&lt;INativeWorkItem&gt;.</returns>
        /// <inheritdoc />
        public IEnumerable<INativeWorkItem> EnumerateNativeWorkItems() =>
            ThreadPool.EnumerateNativeWorkItems().Select(Converter.Convert);

        /// <summary>
        ///     Returns the CPU utilization of the threadpool (as a percentage out of 100).
        /// </summary>
        /// <value>The cpu utilization.</value>
        /// <inheritdoc />
        public int CpuUtilization => ThreadPool.CpuUtilization;

        /// <summary>
        ///     The number of free completion port threads.
        /// </summary>
        /// <value>The free completion port count.</value>
        /// <inheritdoc />
        public int FreeCompletionPortCount => ThreadPool.FreeCompletionPortCount;

        /// <summary>
        ///     The number of idle threadpool threads in the process.
        /// </summary>
        /// <value>The idle threads.</value>
        /// <inheritdoc />
        public int IdleThreads => ThreadPool.IdleThreads;

        /// <summary>
        ///     Returns the maximum number of completion ports.
        /// </summary>
        /// <value>The maximum completion ports.</value>
        /// <inheritdoc />
        public int MaxCompletionPorts => ThreadPool.MaxCompletionPorts;

        /// <summary>
        ///     The maximum number of free completion port threads.
        /// </summary>
        /// <value>The maximum free completion ports.</value>
        /// <inheritdoc />
        public int MaxFreeCompletionPorts => ThreadPool.MaxFreeCompletionPorts;

        /// <summary>
        ///     The maximum number of threadpool threads allowable.
        /// </summary>
        /// <value>The maximum threads.</value>
        /// <inheritdoc />
        public int MaxThreads => ThreadPool.MaxThreads;

        /// <summary>
        ///     Returns the minimum number of completion ports (if any).
        /// </summary>
        /// <value>The minimum completion ports.</value>
        /// <inheritdoc />
        public int MinCompletionPorts => ThreadPool.MinCompletionPorts;

        /// <summary>
        ///     The minimum number of threadpool threads allowable.
        /// </summary>
        /// <value>The minimum threads.</value>
        /// <inheritdoc />
        public int MinThreads => ThreadPool.MinThreads;

        /// <summary>
        ///     The number of running threadpool threads in the process.
        /// </summary>
        /// <value>The running threads.</value>
        /// <inheritdoc />
        public int RunningThreads => ThreadPool.RunningThreads;

        /// <summary>
        ///     The total number of threadpool worker threads in the process.
        /// </summary>
        /// <value>The total threads.</value>
        /// <inheritdoc />
        public int TotalThreads => ThreadPool.TotalThreads;

        /// <summary>
        ///     Gets or sets the converter.
        /// </summary>
        /// <value>The converter.</value>
        [Import]
        internal IConverter Converter { get; set; }
    }
}