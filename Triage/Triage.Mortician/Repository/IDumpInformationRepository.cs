using System;
using System.IO;

namespace Triage.Mortician.Repository
{
    public interface IDumpInformationRepository
    {
        /// <summary>
        ///     Gets or sets the minimum threads in the CLR. This is usually the same as the number of CPU cores
        /// </summary>
        /// <value>
        ///     The minimum threads.
        /// </value>
        int MinThreads { get; }

        /// <summary>
        ///     Gets or sets the maximum number of threads the CLR can have
        /// </summary>
        /// <value>
        ///     The maximum threads.
        /// </value>
        int MaxThreads { get; }

        /// <summary>
        ///     Gets or sets the start time UTC.
        /// </summary>
        /// <value>
        ///     The start time UTC.
        /// </value>
        DateTime StartTimeUtc { get; }

        /// <summary>
        ///     Gets or sets the total size of the heap.
        /// </summary>
        /// <value>
        ///     The total size of the heap.
        /// </value>
        ulong TotalHeapSize { get; }

        /// <summary>
        ///     Gets or sets the total threads.
        /// </summary>
        /// <value>
        ///     The total threads.
        /// </value>
        int TotalThreads { get; }

        /// <summary>
        ///     Gets or sets the number running threads.
        /// </summary>
        /// <value>
        ///     The number running threads.
        /// </value>
        int NumRunningThreads { get; }

        /// <summary>
        ///     Gets or sets the minimum number io completion ports.
        /// </summary>
        /// <value>
        ///     The minimum number io completion ports.
        /// </value>
        int MinNumberIoCompletionPorts { get; }

        /// <summary>
        ///     Gets or sets the maximum number free io completion ports.
        /// </summary>
        /// <value>
        ///     The maximum number free io completion ports.
        /// </value>
        int MaxNumberFreeIoCompletionPorts { get; }

        /// <summary>
        ///     Gets or sets the maximum number io completion ports.
        /// </summary>
        /// <value>
        ///     The maximum number io completion ports.
        /// </value>
        int MaxNumberIoCompletionPorts { get; }

        /// <summary>
        ///     Gets or sets the number idle threads.
        /// </summary>
        /// <value>
        ///     The number idle threads.
        /// </value>
        int NumberIdleThreads { get; }

        /// <summary>
        ///     Gets or sets the number free io completion ports.
        /// </summary>
        /// <value>
        ///     The number free io completion ports.
        /// </value>
        int NumberFreeIoCompletionPorts { get; }

        /// <summary>
        ///     Gets or sets the cpu utilization.
        /// </summary>
        /// <value>
        ///     The cpu utilization.
        /// </value>
        int CpuUtilization { get; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is server gc.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is server gc; otherwise, <c>false</c>.
        /// </value>
        bool IsServerGc { get; }

        /// <summary>
        ///     Gets or sets the heap count.
        /// </summary>
        /// <value>
        ///     The heap count.
        /// </value>
        int HeapCount { get; }

        /// <summary>
        ///     Gets or sets the dump file.
        /// </summary>
        /// <value>
        ///     The dump file.
        /// </value>
        FileInfo DumpFile { get; }

        /// <summary>
        ///     Gets or sets the symbol path.
        /// </summary>
        /// <value>
        ///     The symbol path.
        /// </value>
        string SymbolPath { get; }

        /// <summary>
        ///     Gets or sets the symbol cachce.
        /// </summary>
        /// <value>
        ///     The symbol cachce.
        /// </value>
        string SymbolCache { get; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is mini dump.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is mini dump; otherwise, <c>false</c>.
        /// </value>
        bool IsMiniDump { get; }
    }
}