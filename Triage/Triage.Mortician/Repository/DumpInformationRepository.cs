// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 12-19-2017
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="DumpInformationRepository.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using Microsoft.Diagnostics.Runtime;
using Triage.Mortician.Core;

namespace Triage.Mortician.Repository
{
    /// <summary>
    ///     Represents a collection of high level information about the dump being analyzed
    /// </summary>
    /// <seealso cref="IDumpInformationRepository" />
    [Export]
    public class DumpInformationRepository : IDumpInformationRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DumpInformationRepository" /> class.
        /// </summary>
        /// <param name="dataTarget">The data target.</param>
        /// <param name="runtime">The runtime.</param>
        /// <param name="dumpFile">The dump file.</param>
        /// <exception cref="System.ArgumentNullException">dataTarget</exception>
        protected internal DumpInformationRepository(DataTarget dataTarget, ClrRuntime runtime, FileInfo dumpFile)
        {
            StartTimeUtc = DateTime.UtcNow;
            IsMiniDump = dataTarget?.IsMinidump ?? throw new ArgumentNullException(nameof(dataTarget));
            ProcessModulesInternal = dataTarget?.EnumerateModules().ToList();
            SymbolCache = dataTarget.SymbolLocator.SymbolCache;
            SymbolPath = dataTarget.SymbolLocator.SymbolPath;
            DumpFile = dumpFile;
            HeapCount = runtime.HeapCount;
            IsServerGc = runtime.ServerGC;
            CpuUtilization = runtime.ThreadPool.CpuUtilization;
            NumberFreeIoCompletionPorts = runtime.ThreadPool.FreeCompletionPortCount;
            NumberIdleThreads = runtime.ThreadPool.IdleThreads;
            MaxNumberIoCompletionPorts = runtime.ThreadPool.MaxCompletionPorts;
            MaxNumberFreeIoCompletionPorts = runtime.ThreadPool.MaxFreeCompletionPorts;
            MinNumberIoCompletionPorts = runtime.ThreadPool.MinCompletionPorts;
            NumRunningThreads = runtime.ThreadPool.RunningThreads;
            TotalThreads = runtime.ThreadPool.TotalThreads;
            TotalHeapSize = runtime.Heap.TotalHeapSize;
            MinThreads = runtime.ThreadPool.MinThreads;
            MaxThreads = runtime.ThreadPool.MaxThreads;
        }

        /// <summary>
        ///     The process modules internal
        /// </summary>
        protected internal List<ModuleInfo> ProcessModulesInternal;

        /// <summary>
        ///     Gets or sets the cpu utilization.
        /// </summary>
        /// <value>The cpu utilization.</value>
        public int CpuUtilization { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the dump file.
        /// </summary>
        /// <value>The dump file.</value>
        public FileInfo DumpFile { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the heap count.
        /// </summary>
        /// <value>The heap count.</value>
        public int HeapCount { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is mini dump.
        /// </summary>
        /// <value><c>true</c> if this instance is mini dump; otherwise, <c>false</c>.</value>
        public bool IsMiniDump { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is server gc.
        /// </summary>
        /// <value><c>true</c> if this instance is server gc; otherwise, <c>false</c>.</value>
        public bool IsServerGc { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the maximum number free io completion ports.
        /// </summary>
        /// <value>The maximum number free io completion ports.</value>
        public int MaxNumberFreeIoCompletionPorts { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the maximum number io completion ports.
        /// </summary>
        /// <value>The maximum number io completion ports.</value>
        public int MaxNumberIoCompletionPorts { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the maximum number of threads the CLR can have
        /// </summary>
        /// <value>The maximum threads.</value>
        public int MaxThreads { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the minimum number io completion ports.
        /// </summary>
        /// <value>The minimum number io completion ports.</value>
        public int MinNumberIoCompletionPorts { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the minimum threads in the CLR. This is usually the same as the number of CPU cores
        /// </summary>
        /// <value>The minimum threads.</value>
        public int MinThreads { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the number free io completion ports.
        /// </summary>
        /// <value>The number free io completion ports.</value>
        public int NumberFreeIoCompletionPorts { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the number idle threads.
        /// </summary>
        /// <value>The number idle threads.</value>
        public int NumberIdleThreads { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the number running threads.
        /// </summary>
        /// <value>The number running threads.</value>
        public int NumRunningThreads { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the start time UTC.
        /// </summary>
        /// <value>The start time UTC.</value>
        public DateTime StartTimeUtc { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the symbol cachce.
        /// </summary>
        /// <value>The symbol cachce.</value>
        public string SymbolCache { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the symbol path.
        /// </summary>
        /// <value>The symbol path.</value>
        public string SymbolPath { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the total size of the heap.
        /// </summary>
        /// <value>The total size of the heap.</value>
        public ulong TotalHeapSize { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the total threads.
        /// </summary>
        /// <value>The total threads.</value>
        public int TotalThreads { get; protected internal set; }
    }
}