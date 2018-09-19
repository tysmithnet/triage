// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-19-2018
// ***********************************************************************
// <copyright file="HeapAdapter.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using Microsoft.Diagnostics.Runtime;
using Triage.Mortician.Core.ClrMdAbstractions;
using ClrRootStackwalkPolicy = Triage.Mortician.Core.ClrMdAbstractions.ClrRootStackwalkPolicy;

namespace Triage.Mortician.Adapters
{
    /// <summary>
    ///     Class HeapAdapter.
    /// </summary>
    /// <seealso cref="Triage.Mortician.Core.ClrMdAbstractions.IClrHeap" />
    internal class HeapAdapter : IClrHeap
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="HeapAdapter" /> class.
        /// </summary>
        /// <param name="heap">The heap.</param>
        /// <exception cref="ArgumentNullException">heap</exception>
        /// <inheritdoc />
        public HeapAdapter(ClrHeap heap)
        {
            Heap = heap ?? throw new ArgumentNullException(nameof(heap));
            Runtime = Converter.Convert(heap.Runtime);
            Free = Converter.Convert(heap.Free);
            Segments = heap.Segments.Select(Converter.Convert).ToList();
            StackwalkPolicy = Converter.Convert(heap.StackwalkPolicy);
        }

        /// <summary>
        ///     The heap
        /// </summary>
        internal ClrHeap Heap;

        /// <summary>
        ///     Caches all relevant heap information into memory so future heap operations run faster and
        ///     do not require touching the debuggee.
        /// </summary>
        /// <param name="cancelToken">A cancellation token to stop caching the heap.</param>
        /// <inheritdoc />
        public void CacheHeap(CancellationToken cancelToken) => Heap.CacheHeap(cancelToken);

        /// <summary>
        ///     This method caches many roots so that subsequent calls to EnumerateRoots run faster.
        /// </summary>
        /// <param name="cancelToken">The cancel token.</param>
        /// <inheritdoc />
        public void CacheRoots(CancellationToken cancelToken) => Heap.CacheRoots(cancelToken);

        /// <summary>
        ///     Releases all cached object data to reclaim memory.
        /// </summary>
        /// <inheritdoc />
        public void ClearHeapCache() => Heap.ClearHeapCache();

        /// <summary>
        ///     This method clears any previously cached roots to reclaim memory.
        /// </summary>
        /// <inheritdoc />
        public void ClearRootCache() => Heap.ClearRootCache();

        /// <summary>
        ///     Enumerates all managed locks in the process.  That is anything using System.Monitor either explictly
        ///     or implicitly through "lock (obj)".  This is roughly equivalent to combining SOS's !syncblk command
        ///     with !dumpheap -thinlock.
        /// </summary>
        /// <returns>IEnumerable&lt;IBlockingObject&gt;.</returns>
        /// <inheritdoc />
        public IEnumerable<IBlockingObject> EnumerateBlockingObjects() =>
            Heap.EnumerateBlockingObjects().Select(Converter.Convert);

        /// <summary>
        ///     Enumerates all finalizable objects on the heap.
        /// </summary>
        /// <returns>IEnumerable&lt;System.UInt64&gt;.</returns>
        /// <inheritdoc />
        public IEnumerable<ulong> EnumerateFinalizableObjectAddresses() => Heap.EnumerateFinalizableObjectAddresses();

        /// <summary>
        ///     Enumerates all objects on the heap.  This is equivalent to enumerating all segments then walking
        ///     each object with ClrSegment.FirstObject, ClrSegment.NextObject, but in a simple enumerator
        ///     for easier use in linq queries.
        /// </summary>
        /// <returns>An enumerator for all objects on the heap.</returns>
        /// <inheritdoc />
        public IEnumerable<ulong> EnumerateObjectAddresses() => Heap.EnumerateObjectAddresses();

        /// <summary>
        ///     Enumerates all objects on the heap.
        /// </summary>
        /// <returns>An enumerator for all objects on the heap.</returns>
        /// <inheritdoc />
        public IEnumerable<IClrObject> EnumerateObjects() => Heap.EnumerateObjects().Select(Converter.Convert);

        /// <summary>
        ///     Enumerate the roots of the process.  (That is, all objects which keep other objects alive.)
        ///     Equivalent to EnumerateRoots(true).
        /// </summary>
        /// <returns>IEnumerable&lt;IClrRoot&gt;.</returns>
        /// <inheritdoc />
        public IEnumerable<IClrRoot> EnumerateRoots() => Heap.EnumerateRoots().Select(Converter.Convert);

        /// <summary>
        ///     Enumerate the roots in the process.
        /// </summary>
        /// <param name="enumerateStatics">
        ///     True if we should enumerate static variables.  Enumerating with statics
        ///     can take much longer than enumerating without them.  Additionally these will be be "double reported",
        ///     since all static variables are pinned by handles on the HandleTable (which is also enumerated with
        ///     EnumerateRoots).  You would want to enumerate statics with roots if you care about what exact statics
        ///     root what objects, but not if you care about performance.
        /// </param>
        /// <returns>IEnumerable&lt;IClrRoot&gt;.</returns>
        /// <inheritdoc />
        public IEnumerable<IClrRoot> EnumerateRoots(bool enumerateStatics) =>
            Heap.EnumerateRoots(enumerateStatics).Select(Converter.Convert);

        /// <summary>
        ///     Enumerates all types in the runtime.
        /// </summary>
        /// <returns>
        ///     An enumeration of all types in the target process.  May return null if it's unsupported for
        ///     that version of CLR.
        /// </returns>
        /// <inheritdoc />
        public IEnumerable<IClrType> EnumerateTypes() => Heap.EnumerateTypes().Select(Converter.Convert);

        /// <summary>
        ///     Retrieves the EEClass from the given MethodTable.  EEClasses do not exist on
        ///     .Net Native.
        /// </summary>
        /// <param name="methodTable">The MethodTable to get the EEClass from.</param>
        /// <returns>
        ///     The EEClass for the given MethodTable, 0 if methodTable is invalid or
        ///     does not exist.
        /// </returns>
        /// <inheritdoc />
        public ulong GetEEClassByMethodTable(ulong methodTable) => Heap.GetEEClassByMethodTable(methodTable);

        /// <summary>
        ///     Returns a  wrapper around a System.Exception object (or one of its subclasses).
        /// </summary>
        /// <param name="objRef">The object reference.</param>
        /// <returns>IClrException.</returns>
        /// <inheritdoc />
        public IClrException GetExceptionObject(ulong objRef) => Converter.Convert(Heap.GetExceptionObject(objRef));

        /// <summary>
        ///     Returns the generation of an object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>System.Int32.</returns>
        /// <inheritdoc />
        public int GetGeneration(ulong obj) => Heap.GetGeneration(obj);

        /// <summary>
        ///     Attempts to retrieve the MethodTable from the given object.
        ///     Note that this some ClrTypes cannot be uniquely determined by MethodTable alone.  In
        ///     Desktop CLR, arrays of reference types all use the same MethodTable.  To uniquely
        ///     determine an array of referneces you must also have its component type.
        ///     Note this function has undefined behavior if you do not pass a valid object reference
        ///     to it.
        /// </summary>
        /// <param name="obj">The object to get the MethodTablee of.</param>
        /// <returns>The MethodTable of the object, or 0 if the address could not be read from.</returns>
        /// <inheritdoc />
        public ulong GetMethodTable(ulong obj) => Heap.GetMethodTable(obj);

        /// <summary>
        ///     Retrieves the MethodTable associated with the given EEClass.
        /// </summary>
        /// <param name="eeclass">The eeclass to get the method table from.</param>
        /// <returns>
        ///     The MethodTable for the given EEClass, 0 if eeclass is invalid
        ///     or does not exist.
        /// </returns>
        /// <inheritdoc />
        public ulong GetMethodTableByEEClass(ulong eeclass) => Heap.GetMethodTableByEEClass(eeclass);

        /// <summary>
        ///     Obtains the type of an object at the given address.  Returns null if objRef does not point to
        ///     a valid managed object.
        /// </summary>
        /// <param name="objRef">The object reference.</param>
        /// <returns>IClrType.</returns>
        /// <inheritdoc />
        public IClrType GetObjectType(ulong objRef) => Converter.Convert(Heap.GetObjectType(objRef));

        /// <summary>
        ///     Returns the GC segment for the given object.
        /// </summary>
        /// <param name="objRef">The object reference.</param>
        /// <returns>IClrSegment.</returns>
        /// <inheritdoc />
        public IClrSegment GetSegmentByAddress(ulong objRef) => Converter.Convert(Heap.GetSegmentByAddress(objRef));

        /// <summary>
        ///     Get the size by generation 0, 1, 2, 3.  The large object heap is Gen 3 here.
        ///     The sum of all of these should add up to the TotalHeapSize.
        /// </summary>
        /// <param name="gen">The gen.</param>
        /// <returns>System.UInt64.</returns>
        /// <inheritdoc />
        public ulong GetSizeByGen(int gen) => Heap.GetSizeByGen(gen);

        /// <summary>
        ///     Retrieves the given type by its MethodTable/ComponentMethodTable pair.
        /// </summary>
        /// <param name="methodTable">The ClrType.MethodTable for the requested type.</param>
        /// <param name="componentMethodTable">The ClrType's component MethodTable for the requested type.</param>
        /// <returns>A ClrType object, or null if no such type exists.</returns>
        /// <inheritdoc />
        public IClrType GetTypeByMethodTable(ulong methodTable, ulong componentMethodTable) =>
            Converter.Convert(Heap.GetTypeByMethodTable(methodTable, componentMethodTable));

        /// <summary>
        ///     Retrieves the given type by its MethodTable/ComponentMethodTable pair.  Note this is only valid if
        ///     the given type's component MethodTable is 0.
        /// </summary>
        /// <param name="methodTable">The ClrType.MethodTable for the requested type.</param>
        /// <returns>A ClrType object, or null if no such type exists.</returns>
        /// <inheritdoc />
        public IClrType GetTypeByMethodTable(ulong methodTable) =>
            Converter.Convert(Heap.GetTypeByMethodTable(methodTable));

        /// <summary>
        ///     Looks up a type by name.
        /// </summary>
        /// <param name="name">The name of the type.</param>
        /// <returns>
        ///     The ClrType matching 'name', null if the type was not found, and undefined if more than one
        ///     type shares the same name.
        /// </returns>
        /// <inheritdoc />
        public IClrType GetTypeByName(string name) => Converter.Convert(Heap.GetTypeByName(name));

        /// <summary>
        ///     Returns true if the given address resides somewhere on the managed heap.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns><c>true</c> if [is in heap] [the specified address]; otherwise, <c>false</c>.</returns>
        /// <inheritdoc />
        public bool IsInHeap(ulong address) => Heap.IsInHeap(address);

        /// <summary>
        ///     Returns the object after this one on the segment.
        /// </summary>
        /// <param name="obj">The object to find the next for.</param>
        /// <returns>The next object on the segment, or 0 if the object was the last one on the segment.</returns>
        /// <inheritdoc />
        public ulong NextObject(ulong obj) => Heap.NextObject(obj);

        /// <summary>
        ///     Read 'count' bytes from the ClrHeap at 'address' placing it in 'buffer' starting at offset 'offset'
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="buffer">The buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="count">The count.</param>
        /// <returns>System.Int32.</returns>
        /// <inheritdoc />
        public int ReadMemory(ulong address, byte[] buffer, int offset, int count) =>
            Heap.ReadMemory(address, buffer, offset, count);

        /// <summary>
        ///     Attempts to efficiently read a pointer from memory.  This acts exactly like ClrRuntime.ReadPointer, but
        ///     there is a greater chance you will hit a chache for a more efficient memory read.
        /// </summary>
        /// <param name="addr">The address to read.</param>
        /// <param name="value">The pointer value.</param>
        /// <returns>True if we successfully read the value, false if addr is not mapped into the process space.</returns>
        /// <inheritdoc />
        public bool ReadPointer(ulong addr, out ulong value) => Heap.ReadPointer(addr, out value);

        /// <summary>
        ///     Attempts to retrieve the MethodTable and component MethodTable from the given object.
        ///     Note that this some ClrTypes cannot be uniquely determined by MethodTable alone.  In
        ///     Desktop CLR (prior to v4.6), arrays of reference types all use the same MethodTable but
        ///     also carry a second MethodTable (called the component MethodTable) to determine the
        ///     array element types. Note this function has undefined behavior if you do not pass a
        ///     valid object reference to it.
        /// </summary>
        /// <param name="obj">The object to get the MethodTable of.</param>
        /// <param name="methodTable">The MethodTable for the given object.</param>
        /// <param name="componentMethodTable">The component MethodTable of the given object.</param>
        /// <returns>True if methodTable was filled, false if we failed to read memory.</returns>
        /// <inheritdoc />
        public bool TryGetMethodTable(ulong obj, out ulong methodTable, out ulong componentMethodTable) =>
            Heap.TryGetMethodTable(obj, out methodTable, out componentMethodTable);

        /// <summary>
        ///     Returns whether the roots of the process are cached or not.
        /// </summary>
        /// <value><c>true</c> if [are roots cached]; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool AreRootsCached => Heap.AreRootsCached;

        /// <summary>
        ///     Returns true if the GC heap is in a consistent state for heap enumeration.  This will return false
        ///     if the process was stopped in the middle of a GC, which can cause the GC heap to be unwalkable.
        ///     Note, you may still attempt to walk the heap if this function returns false, but you will likely
        ///     only be able to partially walk each segment.
        /// </summary>
        /// <value><c>true</c> if this instance can walk heap; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool CanWalkHeap => Heap.CanWalkHeap;

        /// <summary>
        ///     Returns the ClrType representing free space on the GC heap.
        /// </summary>
        /// <value>The free.</value>
        /// <inheritdoc />
        public IClrType Free { get; }

        /// <summary>
        ///     Returns whether this version of CLR has component MethodTables.  Component MethodTables were removed from
        ///     desktop CLR in v4.6, and do not exist at all on .Net Native.  If this method returns false, all component
        ///     MethodTables will be 0, and expected to be 0 when an argument to a function.
        /// </summary>
        /// <value><c>true</c> if this instance has component method tables; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool HasComponentMethodTables => Heap.HasComponentMethodTables;

        /// <summary>
        ///     Returns true if the heap is cached, false otherwise.
        /// </summary>
        /// <value><c>true</c> if this instance is heap cached; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsHeapCached => Heap.IsHeapCached;

        /// <summary>
        ///     Pointer size of on the machine (4 or 8 bytes).
        /// </summary>
        /// <value>The size of the pointer.</value>
        /// <inheritdoc />
        public int PointerSize => Heap.PointerSize;

        /// <summary>
        ///     Returns the runtime associated with this heap.
        /// </summary>
        /// <value>The runtime.</value>
        /// <inheritdoc />
        public IClrRuntime Runtime { get; }

        /// <summary>
        ///     A heap is has a list of contiguous memory regions called segments.  This list is returned in order of
        ///     of increasing object addresses.
        /// </summary>
        /// <value>The segments.</value>
        /// <inheritdoc />
        public IList<IClrSegment> Segments { get; }

        /// <summary>
        ///     Sets the stackwalk policy for enumerating roots.  See ClrRootStackwalkPolicy for more information.
        ///     Setting this field can invalidate the root cache.
        ///     <see cref="T:Triage.Mortician.Core.ClrMdAbstractions.ClrRootStackwalkPolicy" />
        /// </summary>
        /// <value>The stackwalk policy.</value>
        /// <inheritdoc />
        public ClrRootStackwalkPolicy StackwalkPolicy { get; set; }

        /// <summary>
        ///     TotalHeapSize is defined as the sum of the length of all segments.
        /// </summary>
        /// <value>The total size of the heap.</value>
        /// <inheritdoc />
        public ulong TotalHeapSize => Heap.TotalHeapSize;

        /// <summary>
        ///     Gets or sets the converter.
        /// </summary>
        /// <value>The converter.</value>
        [Import]
        internal IConverter Converter { get; set; }
    }
}