// ***********************************************************************
// Assembly         : Mortician
// Author           : @tysmithnet
// Created          : 12-19-2017
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-04-2018
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
using Mortician.Core;
using Mortician.Core.ClrMdAbstractions;

namespace Mortician.Repositories
{
    /// <summary>
    ///     Represents a collection of high level information about the dump being analyzed
    /// </summary>
    /// <seealso cref="Mortician.Core.IDumpInformationRepository" />
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
        /// <exception cref="ArgumentNullException">dataTarget</exception>
        /// <exception cref="System.ArgumentNullException">dataTarget</exception>
        public DumpInformationRepository(IDataTarget dataTarget, IClrRuntime runtime, FileInfo dumpFile)
        {
            CpuUtilization = runtime.ThreadPool.CpuUtilization;
            DumpFile = dumpFile;
            HeapCount = runtime.HeapCount;
            IsMiniDump = dataTarget?.IsMinidump ?? throw new ArgumentNullException(nameof(dataTarget));
            IsServerGc = runtime.IsServerGc;
            MaxNumberFreeIoCompletionPorts = runtime.ThreadPool.MaxFreeCompletionPorts;
            MaxNumberIoCompletionPorts = runtime.ThreadPool.MaxCompletionPorts;
            MaxThreads = runtime.ThreadPool.MaxThreads;
            MinNumberIoCompletionPorts = runtime.ThreadPool.MinCompletionPorts;
            MinThreads = runtime.ThreadPool.MinThreads;
            NumberFreeIoCompletionPorts = runtime.ThreadPool.FreeCompletionPortCount;
            NumberIdleThreads = runtime.ThreadPool.IdleThreads;
            NumRunningThreads = runtime.ThreadPool.RunningThreads;
            ModuleInfosInternal = dataTarget?.EnumerateModules().ToList();
            SymbolCache = dataTarget.SymbolLocator.SymbolCache;
            SymbolPath = dataTarget.SymbolLocator.SymbolPath;
            TotalHeapSize = runtime.Heap.TotalHeapSize;
            TotalThreads = runtime.ThreadPool.TotalThreads;
            StartTimeUtc = DateTime.UtcNow;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DumpInformationRepository" /> class.
        /// </summary>
        internal DumpInformationRepository()
        {
        }

        /// <summary>
        ///     The process modules internal
        /// </summary>
        internal List<IModuleInfo> ModuleInfosInternal;

        /// <summary>
        ///     Gets or sets the cpu utilization.
        /// </summary>
        /// <value>The cpu utilization.</value>
        public int CpuUtilization { get; internal set; }

        /// <summary>
        ///     Gets or sets the dump file.
        /// </summary>
        /// <value>The dump file.</value>
        public FileInfo DumpFile { get; internal set; }

        /// <summary>
        ///     Gets or sets the heap count.
        /// </summary>
        /// <value>The heap count.</value>
        public int HeapCount { get; internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is mini dump.
        /// </summary>
        /// <value><c>true</c> if this instance is mini dump; otherwise, <c>false</c>.</value>
        public bool IsMiniDump { get; internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is server gc.
        /// </summary>
        /// <value><c>true</c> if this instance is server gc; otherwise, <c>false</c>.</value>
        public bool IsServerGc { get; internal set; }

        /// <summary>
        ///     Gets or sets the maximum number free io completion ports.
        /// </summary>
        /// <value>The maximum number free io completion ports.</value>
        public int MaxNumberFreeIoCompletionPorts { get; internal set; }

        /// <summary>
        ///     Gets or sets the maximum number io completion ports.
        /// </summary>
        /// <value>The maximum number io completion ports.</value>
        public int MaxNumberIoCompletionPorts { get; internal set; }

        /// <summary>
        ///     Gets or sets the maximum number of threads the CLR can have
        /// </summary>
        /// <value>The maximum threads.</value>
        public int MaxThreads { get; internal set; }

        /// <summary>
        ///     Gets or sets the minimum number io completion ports.
        /// </summary>
        /// <value>The minimum number io completion ports.</value>
        public int MinNumberIoCompletionPorts { get; internal set; }

        /// <summary>
        ///     Gets or sets the minimum threads in the CLR. This is usually the same as the number of CPU cores
        /// </summary>
        /// <value>The minimum threads.</value>
        public int MinThreads { get; internal set; }

        /// <summary>
        ///     Gets the module infos.
        /// </summary>
        /// <value>The module infos.</value>
        public IEnumerable<IModuleInfo> ModuleInfos => ModuleInfosInternal;

        /// <summary>
        ///     Gets or sets the number free io completion ports.
        /// </summary>
        /// <value>The number free io completion ports.</value>
        public int NumberFreeIoCompletionPorts { get; internal set; }

        /// <summary>
        ///     Gets or sets the number idle threads.
        /// </summary>
        /// <value>The number idle threads.</value>
        public int NumberIdleThreads { get; internal set; }

        /// <summary>
        ///     Gets or sets the number running threads.
        /// </summary>
        /// <value>The number running threads.</value>
        public int NumRunningThreads { get; internal set; }

        /// <summary>
        ///     Gets or sets the start time UTC.
        /// </summary>
        /// <value>The start time UTC.</value>
        public DateTime StartTimeUtc { get; internal set; }

        /// <summary>
        ///     Gets or sets the symbol cachce.
        /// </summary>
        /// <value>The symbol cachce.</value>
        public string SymbolCache { get; internal set; }

        /// <summary>
        ///     Gets or sets the symbol path.
        /// </summary>
        /// <value>The symbol path.</value>
        public string SymbolPath { get; internal set; }

        /// <summary>
        ///     Gets or sets the total size of the heap.
        /// </summary>
        /// <value>The total size of the heap.</value>
        public ulong TotalHeapSize { get; internal set; }

        /// <summary>
        ///     Gets or sets the total threads.
        /// </summary>
        /// <value>The total threads.</value>
        public int TotalThreads { get; internal set; }
    }
}