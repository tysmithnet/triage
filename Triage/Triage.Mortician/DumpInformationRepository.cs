using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using Microsoft.Diagnostics.Runtime;

namespace Triage.Mortician
{
    [Export]
    public class DumpInformationRepository
    {
        protected internal List<ModuleInfo> ProcessModulesInternal;
        
        protected internal DumpInformationRepository(DataTarget dataTarget, ClrRuntime runtime, FileInfo dumpFile)
        {
            IsMiniDump = dataTarget?.IsMinidump ?? throw new ArgumentNullException(nameof(dataTarget));
            ProcessModulesInternal = dataTarget?.EnumerateModules().ToList();
            SymbolCachce = dataTarget.SymbolLocator.SymbolCache;
            SymbolPath = dataTarget.SymbolLocator.SymbolPath;
            DumpFile = dumpFile;
            HeapCount = runtime.HeapCount;
            IsServerGc = runtime.ServerGC;
            CpuUtilization = runtime.ThreadPool.CpuUtilization;
            NumberFreeIoCompletionPorts = runtime.ThreadPool.FreeCompletionPortCount;
            NumberIdleThreads = runtime.ThreadPool.IdleThreads;
            MaxNumberIoCompletionPorts = runtime.ThreadPool.MaxCompletionPorts;
            MaxNumberFreeIoCompletionPorts = runtime.ThreadPool.MaxFreeCompletionPorts;
            MaxAllowableThreads = runtime.ThreadPool.MaxThreads;
            MinNumberIoCompletionPorts = runtime.ThreadPool.MinCompletionPorts;
            NumberRunningThreads = runtime.ThreadPool.RunningThreads;
            TotalThreads = runtime.ThreadPool.TotalThreads;
        }

        public int TotalThreads { get; set; }

        public int NumberRunningThreads { get; set; }

        public int MinNumberIoCompletionPorts { get; set; }

        public int MaxAllowableThreads { get; set; }

        public int MaxNumberFreeIoCompletionPorts { get; set; }

        public int MaxNumberIoCompletionPorts { get; set; }

        public int NumberIdleThreads { get; set; }

        public int NumberFreeIoCompletionPorts { get; set; }

        public int CpuUtilization { get; set; }

        public bool IsServerGc { get; set; }

        public int HeapCount { get; set; }

        public FileInfo DumpFile { get; set; }

        public string SymbolPath { get; set; }

        public string SymbolCachce { get; set; }

        public bool IsMiniDump { get; protected internal set; }
    }
}