namespace Triage.Mortician.Core.ClrMdAbstractions
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
}