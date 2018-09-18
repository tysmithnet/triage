// ***********************************************************************
// Assembly         : Triage.Mortician.Core
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="IClrMemoryRegion.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Triage.Mortician.Core.ClrMdAbstractions
{
    /// <summary>
    ///     Interface IClrMemoryRegion
    /// </summary>
    public interface IClrMemoryRegion
    {
        /// <summary>
        ///     Returns a string describing the region of memory (for example "JIT Code Heap"
        ///     or "GC Segment").
        /// </summary>
        /// <param name="detailed">
        ///     Whether or not to include additional data such as the module,
        ///     AppDomain, or GC Heap associaed with it.
        /// </param>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        string ToString(bool detailed);

        /// <summary>
        ///     Equivalent to GetDisplayString(false).
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        string ToString();

        /// <summary>
        ///     The start address of the memory region.
        /// </summary>
        /// <value>The address.</value>
        ulong Address { get; set; }

        /// <summary>
        ///     The AppDomain pointer that corresponds to this heap.  You can obtain the
        ///     name of the AppDomain index or name by calling the appropriate function
        ///     on RuntimeBase.
        ///     Note:  HasAppDomainData must be true before getting this property.
        /// </summary>
        /// <value>The application domain.</value>
        IClrAppDomain AppDomain { get; }

        /// <summary>
        ///     Returns the gc segment type associated with this data.  Only callable if
        ///     HasGCHeapData is true.
        /// </summary>
        /// <value>The type of the gc segment.</value>
        GCSegmentType GCSegmentType { get; set; }

        /// <summary>
        ///     Returns the heap number associated with this data.  Returns -1 if no
        ///     GC heap is associated with this memory region.
        /// </summary>
        /// <value>The heap number.</value>
        int HeapNumber { get; set; }

        /// <summary>
        ///     The Module pointer that corresponds to this heap.  You can obtain the
        ///     filename of the module with this property.
        ///     Note:  HasModuleData must be true or this property will be null.
        /// </summary>
        /// <value>The module.</value>
        string Module { get; }

        /// <summary>
        ///     The size of the memory region in bytes.
        /// </summary>
        /// <value>The size.</value>
        ulong Size { get; set; }

        /// <summary>
        ///     The type of heap/memory that the region contains.
        /// </summary>
        /// <value>The type.</value>
        ClrMemoryRegionType Type { get; set; }
    }
}