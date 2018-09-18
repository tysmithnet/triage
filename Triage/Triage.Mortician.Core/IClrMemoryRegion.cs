namespace Triage.Mortician.Core
{
    public interface IClrMemoryRegion
    {
        /// <summary>
        /// The start address of the memory region.
        /// </summary>
        ulong Address { get; set; }

        /// <summary>
        /// The size of the memory region in bytes.
        /// </summary>
        ulong Size { get; set; }

        /// <summary>
        /// The type of heap/memory that the region contains.
        /// </summary>
        ClrMemoryRegionType Type { get; set; }

        /// <summary>
        /// The AppDomain pointer that corresponds to this heap.  You can obtain the
        /// name of the AppDomain index or name by calling the appropriate function
        /// on RuntimeBase.
        /// Note:  HasAppDomainData must be true before getting this property.
        /// </summary>
        IClrAppDomain AppDomain { get; }

        /// <summary>
        /// The Module pointer that corresponds to this heap.  You can obtain the
        /// filename of the module with this property.
        /// Note:  HasModuleData must be true or this property will be null.
        /// </summary>
        string Module { get; }

        /// <summary>
        /// Returns the heap number associated with this data.  Returns -1 if no
        /// GC heap is associated with this memory region.
        /// </summary>
        int HeapNumber { get; set; }

        /// <summary>
        /// Returns the gc segment type associated with this data.  Only callable if
        /// HasGCHeapData is true.
        /// </summary>
        GCSegmentType GCSegmentType { get; set; }

        /// <summary>
        /// Returns a string describing the region of memory (for example "JIT Code Heap"
        /// or "GC Segment").
        /// </summary>
        /// <param name="detailed">Whether or not to include additional data such as the module,
        /// AppDomain, or GC Heap associaed with it.</param>
        string ToString(bool detailed);

        /// <summary>
        /// Equivalent to GetDisplayString(false).
        /// </summary>
        string ToString();
    }

    /// <summary>
    /// Types of GC segments.
    /// </summary>
    public enum GCSegmentType
    {
        /// <summary>
        /// Ephemeral segments are the only segments to contain Gen0 and Gen1 objects.
        /// It may also contain Gen2 objects, but not always.  Objects are only allocated
        /// on the ephemeral segment.  There is one ephemeral segment per logical GC heap.
        /// It is important to not have too many pinned objects in the ephemeral segment,
        /// or you will run into a performance problem where the runtime runs too many GCs.
        /// </summary>
        Ephemeral,

        /// <summary>
        /// Regular GC segments only contain Gen2 objects.
        /// </summary>
        Regular,

        /// <summary>
        /// The large object heap contains objects greater than a certain threshold.  Large
        /// object segments are never compacted.  Large objects are directly allocated
        /// onto LargeObject segments, and all large objects are considered gen2.
        /// </summary>
        LargeObject
    }
}