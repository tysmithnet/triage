using System;
using System.Collections.Generic;
using System.Linq;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class ClrThreadPoolAdapter : IClrThreadPool
    {
        /// <inheritdoc />
        public ClrThreadPoolAdapter(Microsoft.Diagnostics.Runtime.ClrThreadPool threadPool)
        {
            ThreadPool = threadPool ?? throw new ArgumentNullException(nameof(threadPool));
        }

        internal Microsoft.Diagnostics.Runtime.ClrThreadPool ThreadPool;

        /// <inheritdoc />
        public IEnumerable<IManagedWorkItem> EnumerateManagedWorkItems() => ThreadPool.EnumerateManagedWorkItems().Select(Converter.Convert);

        /// <inheritdoc />
        public IEnumerable<INativeWorkItem> EnumerateNativeWorkItems() => ThreadPool.EnumerateNativeWorkItems().Select(Converter.Convert);

        /// <inheritdoc />
        public int CpuUtilization => ThreadPool.CpuUtilization;

        /// <inheritdoc />
        public int FreeCompletionPortCount => ThreadPool.FreeCompletionPortCount;

        /// <inheritdoc />
        public int IdleThreads => ThreadPool.IdleThreads;

        /// <inheritdoc />
        public int MaxCompletionPorts => ThreadPool.MaxCompletionPorts;

        /// <inheritdoc />
        public int MaxFreeCompletionPorts => ThreadPool.MaxFreeCompletionPorts;

        /// <inheritdoc />
        public int MaxThreads => ThreadPool.MaxThreads;

        /// <inheritdoc />
        public int MinCompletionPorts => ThreadPool.MinCompletionPorts;

        /// <inheritdoc />
        public int MinThreads => ThreadPool.MinThreads;

        /// <inheritdoc />
        public int RunningThreads => ThreadPool.RunningThreads;

        /// <inheritdoc />
        public int TotalThreads => ThreadPool.TotalThreads;
    }
}