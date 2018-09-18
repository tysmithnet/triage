using System.Collections.Generic;
using System.Threading;

namespace Triage.Mortician.Core
{
    public interface IClrHeap
    {
        /// <summary>
        /// Obtains the type of an object at the given address.  Returns null if objRef does not point to
        /// a valid managed object.
        /// </summary>
        ClrType GetObjectType(ulong objRef);

        /// <summary>
        /// Returns whether this version of CLR has component MethodTables.  Component MethodTables were removed from
        /// desktop CLR in v4.6, and do not exist at all on .Net Native.  If this method returns false, all component
        /// MethodTables will be 0, and expected to be 0 when an argument to a function.
        /// </summary>
        bool HasComponentMethodTables { get; }

        /// <summary>
        /// Returns the runtime associated with this heap.
        /// </summary>
        ClrRuntime Runtime { get; }

        /// <summary>
        /// A heap is has a list of contiguous memory regions called segments.  This list is returned in order of
        /// of increasing object addresses.  
        /// </summary>
        IList<ClrSegment> Segments { get; }

        /// <summary>
        /// Sets the stackwalk policy for enumerating roots.  See ClrRootStackwalkPolicy for more information.
        /// Setting this field can invalidate the root cache.
        /// <see cref="ClrRootStackwalkPolicy"/>
        /// </summary>
        ClrRootStackwalkPolicy StackwalkPolicy { get; set; }

        /// <summary>
        /// Returns true if the heap is cached, false otherwise.
        /// </summary>
        bool IsHeapCached { get; }

        /// <summary>
        /// Returns whether the roots of the process are cached or not.
        /// </summary>
        bool AreRootsCached { get; }

        /// <summary>
        /// Returns the ClrType representing free space on the GC heap.
        /// </summary>
        ClrType Free { get; }

        /// <summary>
        /// Returns true if the GC heap is in a consistent state for heap enumeration.  This will return false
        /// if the process was stopped in the middle of a GC, which can cause the GC heap to be unwalkable.
        /// Note, you may still attempt to walk the heap if this function returns false, but you will likely
        /// only be able to partially walk each segment.
        /// </summary>
        bool CanWalkHeap { get; }

        /// <summary>
        /// TotalHeapSize is defined as the sum of the length of all segments.  
        /// </summary>
        ulong TotalHeapSize { get; }

        /// <summary>
        /// Pointer size of on the machine (4 or 8 bytes).  
        /// </summary>
        int PointerSize { get; }

        /// <summary>
        /// Attempts to retrieve the MethodTable and component MethodTable from the given object.
        /// Note that this some ClrTypes cannot be uniquely determined by MethodTable alone.  In
        /// Desktop CLR (prior to v4.6), arrays of reference types all use the same MethodTable but
        /// also carry a second MethodTable (called the component MethodTable) to determine the
        /// array element types. Note this function has undefined behavior if you do not pass a
        /// valid object reference to it.
        /// </summary>
        /// <param name="obj">The object to get the MethodTable of.</param>
        /// <param name="methodTable">The MethodTable for the given object.</param>
        /// <param name="componentMethodTable">The component MethodTable of the given object.</param>
        /// <returns>True if methodTable was filled, false if we failed to read memory.</returns>
        bool TryGetMethodTable(ulong obj, out ulong methodTable, out ulong componentMethodTable);

        /// <summary>
        /// Attempts to retrieve the MethodTable from the given object.
        /// Note that this some ClrTypes cannot be uniquely determined by MethodTable alone.  In
        /// Desktop CLR, arrays of reference types all use the same MethodTable.  To uniquely
        /// determine an array of referneces you must also have its component type.
        /// Note this function has undefined behavior if you do not pass a valid object reference
        /// to it.
        /// </summary>
        /// <param name="obj">The object to get the MethodTablee of.</param>
        /// <returns>The MethodTable of the object, or 0 if the address could not be read from.</returns>
        ulong GetMethodTable(ulong obj);

        /// <summary>
        /// Retrieves the EEClass from the given MethodTable.  EEClasses do not exist on
        /// .Net Native. 
        /// </summary>
        /// <param name="methodTable">The MethodTable to get the EEClass from.</param>
        /// <returns>The EEClass for the given MethodTable, 0 if methodTable is invalid or
        /// does not exist.</returns>
        ulong GetEEClassByMethodTable(ulong methodTable);

        /// <summary>
        /// Retrieves the MethodTable associated with the given EEClass.
        /// </summary>
        /// <param name="eeclass">The eeclass to get the method table from.</param>
        /// <returns>The MethodTable for the given EEClass, 0 if eeclass is invalid
        /// or does not exist.</returns>
        ulong GetMethodTableByEEClass(ulong eeclass);

        /// <summary>
        /// Returns a  wrapper around a System.Exception object (or one of its subclasses).
        /// </summary>
        ClrException GetExceptionObject(ulong objRef);

        /// <summary>
        /// Enumerate the roots of the process.  (That is, all objects which keep other objects alive.)
        /// Equivalent to EnumerateRoots(true).
        /// </summary>
        IEnumerable<ClrRoot> EnumerateRoots();

        /// <summary>
        /// Caches all relevant heap information into memory so future heap operations run faster and
        /// do not require touching the debuggee.
        /// </summary>
        /// <param name="cancelToken">A cancellation token to stop caching the heap.</param>
        void CacheHeap(CancellationToken cancelToken);

        /// <summary>
        /// Releases all cached object data to reclaim memory.
        /// </summary>
        void ClearHeapCache();

        /// <summary>
        /// This method caches many roots so that subsequent calls to EnumerateRoots run faster.
        /// </summary>
        void CacheRoots(CancellationToken cancelToken);

        /// <summary>
        /// This method clears any previously cached roots to reclaim memory.
        /// </summary>
        void ClearRootCache();

        /// <summary>
        /// Looks up a type by name.
        /// </summary>
        /// <param name="name">The name of the type.</param>
        /// <returns>The ClrType matching 'name', null if the type was not found, and undefined if more than one
        /// type shares the same name.</returns>
        ClrType GetTypeByName(string name);

        /// <summary>
        /// Retrieves the given type by its MethodTable/ComponentMethodTable pair.
        /// </summary>
        /// <param name="methodTable">The ClrType.MethodTable for the requested type.</param>
        /// <param name="componentMethodTable">The ClrType's component MethodTable for the requested type.</param>
        /// <returns>A ClrType object, or null if no such type exists.</returns>
        ClrType GetTypeByMethodTable(ulong methodTable, ulong componentMethodTable);

        /// <summary>
        /// Retrieves the given type by its MethodTable/ComponentMethodTable pair.  Note this is only valid if
        /// the given type's component MethodTable is 0.
        /// </summary>
        /// <param name="methodTable">The ClrType.MethodTable for the requested type.</param>
        /// <returns>A ClrType object, or null if no such type exists.</returns>
        ClrType GetTypeByMethodTable(ulong methodTable);

        /// <summary>
        /// Enumerate the roots in the process.
        /// </summary>
        /// <param name="enumerateStatics">True if we should enumerate static variables.  Enumerating with statics 
        /// can take much longer than enumerating without them.  Additionally these will be be "double reported",
        /// since all static variables are pinned by handles on the HandleTable (which is also enumerated with 
        /// EnumerateRoots).  You would want to enumerate statics with roots if you care about what exact statics
        /// root what objects, but not if you care about performance.</param>
        IEnumerable<ClrRoot> EnumerateRoots(bool enumerateStatics);

        /// <summary>
        /// Enumerates all types in the runtime.
        /// </summary>
        /// <returns>An enumeration of all types in the target process.  May return null if it's unsupported for
        /// that version of CLR.</returns>
        IEnumerable<ClrType> EnumerateTypes();

        /// <summary>
        /// Enumerates all finalizable objects on the heap.
        /// </summary>
        IEnumerable<ulong> EnumerateFinalizableObjectAddresses();

        /// <summary>
        /// Enumerates all managed locks in the process.  That is anything using System.Monitor either explictly
        /// or implicitly through "lock (obj)".  This is roughly equivalent to combining SOS's !syncblk command
        /// with !dumpheap -thinlock.
        /// </summary>
        IEnumerable<BlockingObject> EnumerateBlockingObjects();

        /// <summary>
        /// Enumerates all objects on the heap.  This is equivalent to enumerating all segments then walking
        /// each object with ClrSegment.FirstObject, ClrSegment.NextObject, but in a simple enumerator
        /// for easier use in linq queries.
        /// </summary>
        /// <returns>An enumerator for all objects on the heap.</returns>
        IEnumerable<ulong> EnumerateObjectAddresses();

        /// <summary>
        /// Enumerates all objects on the heap.
        /// </summary>
        /// <returns>An enumerator for all objects on the heap.</returns>
        IEnumerable<ClrObject> EnumerateObjects();

        /// <summary>
        /// Get the size by generation 0, 1, 2, 3.  The large object heap is Gen 3 here. 
        /// The sum of all of these should add up to the TotalHeapSize.  
        /// </summary>
        ulong GetSizeByGen(int gen);

        /// <summary>
        /// Returns the generation of an object.
        /// </summary>
        int GetGeneration(ulong obj);

        /// <summary>
        /// Returns the object after this one on the segment.
        /// </summary>
        /// <param name="obj">The object to find the next for.</param>
        /// <returns>The next object on the segment, or 0 if the object was the last one on the segment.</returns>
        ulong NextObject(ulong obj);

        /// <summary>
        /// Returns the GC segment for the given object.
        /// </summary>
        ClrSegment GetSegmentByAddress(ulong objRef);

        /// <summary>
        /// Returns true if the given address resides somewhere on the managed heap.
        /// </summary>
        bool IsInHeap(ulong address);

        /// <summary>
        /// Returns a string representation of this heap, including the size and number of segments.
        /// </summary>
        /// <returns>The string representation of this heap.</returns>
        string ToString();

        /// <summary>
        /// Read 'count' bytes from the ClrHeap at 'address' placing it in 'buffer' starting at offset 'offset'
        /// </summary>
        int ReadMemory(ulong address, byte[] buffer, int offset, int count);

        /// <summary>
        /// Attempts to efficiently read a pointer from memory.  This acts exactly like ClrRuntime.ReadPointer, but
        /// there is a greater chance you will hit a chache for a more efficient memory read.
        /// </summary>
        /// <param name="addr">The address to read.</param>
        /// <param name="value">The pointer value.</param>
        /// <returns>True if we successfully read the value, false if addr is not mapped into the process space.</returns>
        bool ReadPointer(ulong addr, out ulong value);
    }
}