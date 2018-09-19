using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Diagnostics.Runtime;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class ClrRuntimeAdapter : IClrRuntime
    {
        [Import]
        internal IConverter Converter { get; set; }
        /// <inheritdoc />
        public ClrRuntimeAdapter(ClrRuntime runtime)
        {
            Runtime = runtime ?? throw new ArgumentNullException(nameof(runtime));
            AppDomains = runtime.AppDomains.Select(Converter.Convert).ToList();
            ClrInfo = Converter.Convert(runtime.ClrInfo);
            SharedDomain = Converter.Convert(runtime.SharedDomain);
            SystemDomain = Converter.Convert(runtime.SystemDomain);
            ThreadPool = Converter.Convert(runtime.ThreadPool);
            Threads = runtime.Threads.Select(Converter.Convert).ToList();
            Modules = runtime.Modules.Select(Converter.Convert).ToList();
            Heap = Converter.Convert(runtime.Heap);
            DataTarget = Converter.Convert(runtime.DataTarget);
        }

        internal ClrRuntime Runtime;

        /// <inheritdoc />
        /// <inheritdoc />
        public IEnumerable<ulong> EnumerateFinalizerQueueObjectAddresses()
        {
            return Runtime.EnumerateFinalizerQueueObjectAddresses();
        }

        /// <inheritdoc />
        public IEnumerable<int> EnumerateGCThreads()
        {
            return Runtime.EnumerateGCThreads();
        }

        /// <inheritdoc />
        public IEnumerable<IClrHandle> EnumerateHandles()
        {
            return Runtime.EnumerateHandles().Select(Converter.Convert);
        }

        /// <inheritdoc />
        public IEnumerable<IClrMemoryRegion> EnumerateMemoryRegions()
        {
            return Runtime.EnumerateMemoryRegions().Select(Converter.Convert);
        }

        /// <inheritdoc />
        public IEnumerable<IClrException> EnumerateSerializedExceptions()
        {
            return Runtime.EnumerateSerializedExceptions().Select(Converter.Convert);
        }

        /// <inheritdoc />
        public void Flush()
        {
            Runtime.Flush();
        }

        /// <inheritdoc />
        public ICcwData GetCcwDataByAddress(ulong addr)
        {
            var ccwDataByAddress = Runtime.GetCcwDataByAddress(addr);
            return Converter.Convert(ccwDataByAddress);
        }

        /// <inheritdoc />
        public IClrHeap GetHeap()
        {
            return Converter.Convert(Runtime.Heap);
        }

        /// <inheritdoc />
        public IClrMethod GetMethodByAddress(ulong ip)
        {
            return Converter.Convert(Runtime.GetMethodByAddress(ip));
        }

        /// <inheritdoc />
        public IClrMethod GetMethodByHandle(ulong methodHandle)
        {
            return Converter.Convert(Runtime.GetMethodByHandle(methodHandle));
        }

        /// <inheritdoc />
        public IClrThreadPool GetThreadPool()
        {
            return Converter.Convert(Runtime.ThreadPool);
        }

        /// <inheritdoc />
        public bool ReadMemory(ulong address, byte[] buffer, int bytesRequested, out int bytesRead)
        {
            return Runtime.ReadMemory(address, buffer, bytesRequested, out bytesRead);
        }

        /// <inheritdoc />
        public bool ReadPointer(ulong address, out ulong value)
        {
            return Runtime.ReadPointer(address, out value);
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
        public int HeapCount => Runtime.HeapCount;

        /// <inheritdoc />
        public IList<IClrModule> Modules { get; }

        /// <inheritdoc />
        public int PointerSize => Runtime.PointerSize;

        /// <inheritdoc />
        public bool ServerGC => Runtime.ServerGC;

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