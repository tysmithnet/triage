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
using System.Linq;
using Microsoft.Diagnostics.Runtime;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    /// <summary>
    ///     Class ClrThreadPoolAdapter.
    /// </summary>
    /// <seealso cref="Triage.Mortician.Core.ClrMdAbstractions.IClrThreadPool" />
    internal class ClrThreadPoolAdapter : BaseAdapter, IClrThreadPool
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ClrThreadPoolAdapter" /> class.
        /// </summary>
        /// <param name="threadPool">The thread pool.</param>
        /// <exception cref="ArgumentNullException">threadPool</exception>
        /// <inheritdoc />
        public ClrThreadPoolAdapter(IConverter converter, ClrThreadPool threadPool) : base(converter)
        {
            ThreadPool = threadPool ?? throw new ArgumentNullException(nameof(threadPool));
            CpuUtilization = ThreadPool.CpuUtilization;
            FreeCompletionPortCount = ThreadPool.FreeCompletionPortCount;
            IdleThreads = ThreadPool.IdleThreads;
            MaxCompletionPorts = ThreadPool.MaxCompletionPorts;
            MaxFreeCompletionPorts = ThreadPool.MaxFreeCompletionPorts;
            MaxThreads = ThreadPool.MaxThreads;
            MinCompletionPorts = ThreadPool.MinCompletionPorts;
            MinThreads = ThreadPool.MinThreads;
            RunningThreads = ThreadPool.RunningThreads;
            TotalThreads = ThreadPool.TotalThreads;
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

        public override void Setup()
        {
        }

        /// <summary>
        ///     Returns the CPU utilization of the threadpool (as a percentage out of 100).
        /// </summary>
        /// <value>The cpu utilization.</value>
        /// <inheritdoc />
        public int CpuUtilization { get; internal set; }

        /// <summary>
        ///     The number of free completion port threads.
        /// </summary>
        /// <value>The free completion port count.</value>
        /// <inheritdoc />
        public int FreeCompletionPortCount { get; internal set; }

        /// <summary>
        ///     The number of idle threadpool threads in the process.
        /// </summary>
        /// <value>The idle threads.</value>
        /// <inheritdoc />
        public int IdleThreads { get; internal set; }

        /// <summary>
        ///     Returns the maximum number of completion ports.
        /// </summary>
        /// <value>The maximum completion ports.</value>
        /// <inheritdoc />
        public int MaxCompletionPorts { get; internal set; }

        /// <summary>
        ///     The maximum number of free completion port threads.
        /// </summary>
        /// <value>The maximum free completion ports.</value>
        /// <inheritdoc />
        public int MaxFreeCompletionPorts { get; internal set; }

        /// <summary>
        ///     The maximum number of threadpool threads allowable.
        /// </summary>
        /// <value>The maximum threads.</value>
        /// <inheritdoc />
        public int MaxThreads { get; internal set; }

        /// <summary>
        ///     Returns the minimum number of completion ports (if any).
        /// </summary>
        /// <value>The minimum completion ports.</value>
        /// <inheritdoc />
        public int MinCompletionPorts { get; internal set; }

        /// <summary>
        ///     The minimum number of threadpool threads allowable.
        /// </summary>
        /// <value>The minimum threads.</value>
        /// <inheritdoc />
        public int MinThreads { get; internal set; }

        /// <summary>
        ///     The number of running threadpool threads in the process.
        /// </summary>
        /// <value>The running threads.</value>
        /// <inheritdoc />
        public int RunningThreads { get; internal set; }

        /// <summary>
        ///     The total number of threadpool worker threads in the process.
        /// </summary>
        /// <value>The total threads.</value>
        /// <inheritdoc />
        public int TotalThreads { get; internal set; }
    }
}