using System;
using System.Collections.Generic;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class ClrThreadPoolAdapter : IClrThreadPool
    {
        /// <inheritdoc />
        public ClrThreadPoolAdapter(Microsoft.Diagnostics.Runtime.ClrThreadPool threadPool)
        {
            _threadPool = threadPool ?? throw new ArgumentNullException(nameof(threadPool));
        }

        internal Microsoft.Diagnostics.Runtime.ClrThreadPool _threadPool;

        /// <inheritdoc />
        public IEnumerable<IManagedWorkItem> EnumerateManagedWorkItems()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IEnumerable<INativeWorkItem> EnumerateNativeWorkItems()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public int CpuUtilization { get; }

        /// <inheritdoc />
        public int FreeCompletionPortCount { get; }

        /// <inheritdoc />
        public int IdleThreads { get; }

        /// <inheritdoc />
        public int MaxCompletionPorts { get; }

        /// <inheritdoc />
        public int MaxFreeCompletionPorts { get; }

        /// <inheritdoc />
        public int MaxThreads { get; }

        /// <inheritdoc />
        public int MinCompletionPorts { get; }

        /// <inheritdoc />
        public int MinThreads { get; }

        /// <inheritdoc />
        public int RunningThreads { get; }

        /// <inheritdoc />
        public int TotalThreads { get; }
    }
}