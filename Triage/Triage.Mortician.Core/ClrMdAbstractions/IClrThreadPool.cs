using System.Collections.Generic;

namespace Triage.Mortician.Core.ClrMdAbstractions
{
    public interface IClrThreadPool
    {
        /// <summary>
        /// The total number of threadpool worker threads in the process.
        /// </summary>
        int TotalThreads { get; }

        /// <summary>
        /// The number of running threadpool threads in the process.
        /// </summary>
        int RunningThreads { get; }

        /// <summary>
        /// The number of idle threadpool threads in the process.
        /// </summary>
        int IdleThreads { get; }

        /// <summary>
        /// The minimum number of threadpool threads allowable.
        /// </summary>
        int MinThreads { get; }

        /// <summary>
        /// The maximum number of threadpool threads allowable.
        /// </summary>
        int MaxThreads { get; }

        /// <summary>
        /// Returns the minimum number of completion ports (if any).
        /// </summary>
        int MinCompletionPorts { get; }

        /// <summary>
        /// Returns the maximum number of completion ports.
        /// </summary>
        int MaxCompletionPorts { get; }

        /// <summary>
        /// Returns the CPU utilization of the threadpool (as a percentage out of 100).
        /// </summary>
        int CpuUtilization { get; }

        /// <summary>
        /// The number of free completion port threads.
        /// </summary>
        int FreeCompletionPortCount { get; }

        /// <summary>
        /// The maximum number of free completion port threads.
        /// </summary>
        int MaxFreeCompletionPorts { get; }

        /// <summary>
        /// Enumerates the work items on the threadpool (native side).
        /// </summary>
        IEnumerable<INativeWorkItem> EnumerateNativeWorkItems();

        /// <summary>
        /// Enumerates work items on the thread pool (managed side).
        /// </summary>
        /// <returns></returns>
        IEnumerable<IManagedWorkItem> EnumerateManagedWorkItems();
    }
}