// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-19-2018
// ***********************************************************************
// <copyright file="ClrRuntimeAdapter.cs" company="">
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
    ///     Class ClrRuntimeAdapter.
    /// </summary>
    /// <seealso cref="Triage.Mortician.Core.ClrMdAbstractions.IClrRuntime" />
    internal class ClrRuntimeAdapter : BaseAdapter, IClrRuntime
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ClrRuntimeAdapter" /> class.
        /// </summary>
        /// <param name="runtime">The runtime.</param>
        /// <exception cref="ArgumentNullException">runtime</exception>
        /// <inheritdoc />
        public ClrRuntimeAdapter(IConverter converter, ClrRuntime runtime) : base(converter)
        {
            Runtime = runtime ?? throw new ArgumentNullException(nameof(runtime));
            HeapCount = Runtime.HeapCount;
            PointerSize = Runtime.PointerSize;
            IsServerGc = Runtime.ServerGC; PointerSize = Runtime.PointerSize;
            IsServerGc = Runtime.ServerGC;
        }

        /// <summary>
        ///     The runtime
        /// </summary>
        internal ClrRuntime Runtime;

        /// <summary>
        ///     Enumerates all objects currently on the finalizer queue.  (Not finalizable objects, but objects
        ///     which have been collected and will be imminently finalized.)
        /// </summary>
        /// <returns>IEnumerable&lt;System.UInt64&gt;.</returns>
        /// <inheritdoc />
        /// <inheritdoc />
        public IEnumerable<ulong> EnumerateFinalizerQueueObjectAddresses() =>
            Runtime.EnumerateFinalizerQueueObjectAddresses();

        /// <summary>
        ///     Enumerates the OS thread ID of GC threads in the runtime.
        /// </summary>
        /// <returns>IEnumerable&lt;System.Int32&gt;.</returns>
        /// <inheritdoc />
        public IEnumerable<int> EnumerateGCThreads() => Runtime.EnumerateGCThreads();

        /// <summary>
        ///     Enumerates a list of GC handles currently in the process.  Note that this list may be incomplete
        ///     depending on the state of the process when we attempt to walk the handle table.
        /// </summary>
        /// <returns>The list of GC handles in the process, NULL on catastrophic error.</returns>
        /// <inheritdoc />
        public IEnumerable<IClrHandle> EnumerateHandles() => Runtime.EnumerateHandles().Select(Converter.Convert);

        /// <summary>
        ///     Enumerates regions of memory which CLR has allocated with a description of what data
        ///     resides at that location.  Note that this does not return every chunk of address space
        ///     that CLR allocates.
        /// </summary>
        /// <returns>An enumeration of memory regions in the process.</returns>
        /// <inheritdoc />
        public IEnumerable<IClrMemoryRegion> EnumerateMemoryRegions() =>
            Runtime.EnumerateMemoryRegions().Select(Converter.Convert);

        /// <summary>
        ///     In .NET native crash dumps, we have a list of serialized exceptions objects. This property expose them as
        ///     ClrException objects.
        /// </summary>
        /// <returns>IEnumerable&lt;IClrException&gt;.</returns>
        /// <inheritdoc />
        public IEnumerable<IClrException> EnumerateSerializedExceptions() =>
            Runtime.EnumerateSerializedExceptions().Select(Converter.Convert);

        /// <summary>
        ///     Flushes the dac cache.  This function MUST be called any time you expect to call the same function
        ///     but expect different results.  For example, after walking the heap, you need to call Flush before
        ///     attempting to walk the heap again.  After calling this function, you must discard ALL ClrMD objects
        ///     you have cached other than DataTarget and ClrRuntime and re-request the objects and data you need.
        ///     (E.G. if you want to use the ClrHeap object after calling flush, you must call ClrRuntime.GetHeap
        ///     again after Flush to get a new instance.)
        /// </summary>
        /// <inheritdoc />
        public void Flush()
        {
            Runtime.Flush();
        }

        /// <summary>
        ///     Returns the CCW data associated with the given address.  This is used when looking at stowed
        ///     exceptions in CLR.
        /// </summary>
        /// <param name="addr">The address of the CCW obtained from stowed exception data.</param>
        /// <returns>The CcwData describing the given CCW, or null.</returns>
        /// <inheritdoc />
        public ICcwData GetCcwDataByAddress(ulong addr)
        {
            var ccwDataByAddress = Runtime.GetCcwDataByAddress(addr);
            return Converter.Convert(ccwDataByAddress);
        }

        /// <summary>
        ///     Gets the GC heap of the process.
        /// </summary>
        /// <returns>IClrHeap.</returns>
        /// <inheritdoc />
        public IClrHeap GetHeap() => Converter.Convert(Runtime.Heap);

        /// <summary>
        ///     Attempts to get a ClrMethod for the given instruction pointer.  This will return NULL if the
        ///     given instruction pointer is not within any managed method.
        /// </summary>
        /// <param name="ip">The ip.</param>
        /// <returns>IClrMethod.</returns>
        /// <inheritdoc />
        public IClrMethod GetMethodByAddress(ulong ip) => Converter.Convert(Runtime.GetMethodByAddress(ip));

        /// <summary>
        ///     Returns a ClrMethod by its internal runtime handle (on desktop CLR this is a MethodDesc).
        /// </summary>
        /// <param name="methodHandle">The method handle (MethodDesc) to look up.</param>
        /// <returns>The ClrMethod for the given method handle, or null if no method was found.</returns>
        /// <inheritdoc />
        public IClrMethod GetMethodByHandle(ulong methodHandle) =>
            Converter.Convert(Runtime.GetMethodByHandle(methodHandle));

        /// <summary>
        ///     Returns data on the CLR thread pool for this runtime.
        /// </summary>
        /// <returns>IClrThreadPool.</returns>
        /// <inheritdoc />
        public IClrThreadPool GetThreadPool() => Converter.Convert(Runtime.ThreadPool);

        /// <summary>
        ///     Read data out of the target process.
        /// </summary>
        /// <param name="address">The address to start the read from.</param>
        /// <param name="buffer">The buffer to write memory to.</param>
        /// <param name="bytesRequested">How many bytes to read (must be less than/equal to buffer.Length)</param>
        /// <param name="bytesRead">
        ///     The number of bytes actually read out of the process.  This will be less than
        ///     bytes requested if the request falls off the end of an allocation.
        /// </param>
        /// <returns>False if the memory is not readable (free or no read permission), true if *some* memory was read.</returns>
        /// <inheritdoc />
        public bool ReadMemory(ulong address, byte[] buffer, int bytesRequested, out int bytesRead) =>
            Runtime.ReadMemory(address, buffer, bytesRequested, out bytesRead);

        /// <summary>
        ///     Reads a pointer value out of the target process.  This function reads only the target's pointer size,
        ///     so if this is used on an x86 target, only 4 bytes is read and written to val.
        /// </summary>
        /// <param name="address">The address to read from.</param>
        /// <param name="value">The value at that address.</param>
        /// <returns>True if the read was successful, false otherwise.</returns>
        /// <inheritdoc />
        public bool ReadPointer(ulong address, out ulong value) => Runtime.ReadPointer(address, out value);

        public override void Setup()
        {
            AppDomains = Runtime.AppDomains.Select(Converter.Convert).ToList();
            ClrInfo = Converter.Convert(Runtime.ClrInfo);
            SharedDomain = Converter.Convert(Runtime.SharedDomain);
            SystemDomain = Converter.Convert(Runtime.SystemDomain);
            ThreadPool = Converter.Convert(Runtime.ThreadPool);
            Threads = Runtime.Threads.Select(Converter.Convert).ToList();
            Modules = Runtime.Modules.Select(Converter.Convert).ToList();
            Heap = Converter.Convert(Runtime.Heap);
            DataTarget = Converter.Convert(Runtime.DataTarget);
        }

        /// <summary>
        ///     Enumerates the list of appdomains in the process.  Note the System appdomain and Shared
        ///     AppDomain are omitted.
        /// </summary>
        /// <value>The application domains.</value>
        /// <inheritdoc />
        public IList<IClrAppDomain> AppDomains { get; internal set; }

        /// <summary>
        ///     The ClrInfo of the current runtime.
        /// </summary>
        /// <value>The color information.</value>
        /// <inheritdoc />
        public IClrInfo ClrInfo { get; internal set; }

        /// <summary>
        ///     Returns the DataTarget associated with this runtime.
        /// </summary>
        /// <value>The data target.</value>
        /// <inheritdoc />
        public IDataTarget DataTarget { get; internal set; }

        /// <summary>
        ///     Gets the GC heap of the process.
        /// </summary>
        /// <value>The heap.</value>
        /// <inheritdoc />
        public IClrHeap Heap { get; internal set; }

        /// <summary>
        ///     The number of logical GC heaps in the process.  This is always 1 for a workstation
        ///     GC, and usually it's the number of logical processors in a server GC application.
        /// </summary>
        /// <value>The heap count.</value>
        /// <inheritdoc />
        public int HeapCount { get; internal set; }

        /// <summary>
        ///     A list of all modules loaded into the process.
        /// </summary>
        /// <value>The modules.</value>
        /// <inheritdoc />
        public IList<IClrModule> Modules { get; internal set; }

        /// <summary>
        ///     Returns the pointer size of the target process.
        /// </summary>
        /// <value>The size of the pointer.</value>
        /// <inheritdoc />
        public int PointerSize { get; internal set; }

        /// <summary>
        ///     Whether or not the process is running in server GC mode or not.
        /// </summary>
        /// <value><c>true</c> if [server gc]; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsServerGc { get; internal set; }

        /// <summary>
        ///     Give access to the Shared AppDomain
        /// </summary>
        /// <value>The shared domain.</value>
        /// <inheritdoc />
        public IClrAppDomain SharedDomain { get; internal set; }

        /// <summary>
        ///     Give access to the System AppDomain
        /// </summary>
        /// <value>The system domain.</value>
        /// <inheritdoc />
        public IClrAppDomain SystemDomain { get; internal set; }

        /// <summary>
        ///     Returns data on the CLR thread pool for this runtime.
        /// </summary>
        /// <value>The thread pool.</value>
        /// <inheritdoc />
        public IClrThreadPool ThreadPool { get; internal set; }

        /// <summary>
        ///     Enumerates all managed threads in the process.  Only threads which have previously run managed
        ///     code will be enumerated.
        /// </summary>
        /// <value>The threads.</value>
        /// <inheritdoc />
        public IList<IClrThread> Threads { get; internal set; }
    }
}