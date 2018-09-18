using System;
using System.Collections.Generic;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class ClrRuntimeAdapter : IClrRuntime
    {
        /// <inheritdoc />
        public ClrRuntimeAdapter(Microsoft.Diagnostics.Runtime.ClrRuntime runtime)
        {
            _runtime = runtime ?? throw new ArgumentNullException(nameof(runtime));
        }

        internal Microsoft.Diagnostics.Runtime.ClrRuntime _runtime;

        /// <inheritdoc />
        public IEnumerable<ulong> EnumerateFinalizerQueueObjectAddresses()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IEnumerable<int> EnumerateGCThreads()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IEnumerable<IClrHandle> EnumerateHandles()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IEnumerable<IClrMemoryRegion> EnumerateMemoryRegions()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IEnumerable<IClrException> EnumerateSerializedExceptions()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void Flush()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ICcwData GetCcwDataByAddress(ulong addr)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IClrHeap GetHeap()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IClrMethod GetMethodByAddress(ulong ip)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IClrMethod GetMethodByHandle(ulong methodHandle)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IClrThreadPool GetThreadPool()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool ReadMemory(ulong address, byte[] buffer, int bytesRequested, out int bytesRead)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool ReadPointer(ulong address, out ulong value)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IList<IClrAppDomain> AppDomains { get; }

        /// <inheritdoc />
        public IClrInfo ClrInfo { get; }

        /// <inheritdoc />
        public IDataTarget DataTarget { get; }

        /// <inheritdoc />
        public IClrHeap Heap { get; }

        /// <inheritdoc />
        public int HeapCount { get; }

        /// <inheritdoc />
        public IList<IClrModule> Modules { get; }

        /// <inheritdoc />
        public int PointerSize { get; }

        /// <inheritdoc />
        public bool ServerGC { get; }

        /// <inheritdoc />
        public IClrAppDomain SharedDomain { get; }

        /// <inheritdoc />
        public IClrAppDomain SystemDomain { get; }

        /// <inheritdoc />
        public IClrThreadPool ThreadPool { get; }

        /// <inheritdoc />
        public IList<IClrThread> Threads { get; }
    }
}