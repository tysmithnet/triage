// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-19-2018
// ***********************************************************************
// <copyright file="MemoryRegionAdapter.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.ComponentModel.Composition;
using Microsoft.Diagnostics.Runtime;
using Triage.Mortician.Core.ClrMdAbstractions;
using ClrMemoryRegionType = Triage.Mortician.Core.ClrMdAbstractions.ClrMemoryRegionType;

namespace Triage.Mortician.Adapters
{
    /// <summary>
    ///     Class MemoryRegionAdapter.
    /// </summary>
    /// <seealso cref="Triage.Mortician.Core.ClrMdAbstractions.IClrMemoryRegion" />
    internal class MemoryRegionAdapter : BaseAdapter, IClrMemoryRegion
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MemoryRegionAdapter" /> class.
        /// </summary>
        /// <param name="memoryRegion">The memory region.</param>
        /// <exception cref="ArgumentNullException">memoryRegion</exception>
        public MemoryRegionAdapter(IConverter converter, ClrMemoryRegion memoryRegion) : base(converter)
        {
            MemoryRegion = memoryRegion ?? throw new ArgumentNullException(nameof(memoryRegion));
            AppDomain = Converter.Convert(memoryRegion.AppDomain);
            GcSegmentType = Converter.Convert(memoryRegion.GCSegmentType);
            MemoryRegionType = Converter.Convert(memoryRegion.Type);
        }

        /// <summary>
        ///     Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="detailed">
        ///     Whether or not to include additional data such as the module,
        ///     AppDomain, or GC Heap associaed with it.
        /// </param>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        /// <inheritdoc />
        public string ToString(bool detailed) => MemoryRegion.ToString(detailed);

        /// <summary>
        ///     The start address of the memory region.
        /// </summary>
        /// <value>The address.</value>
        /// <inheritdoc />
        public ulong Address => MemoryRegion.Address;

        /// <summary>
        ///     The AppDomain pointer that corresponds to this heap.  You can obtain the
        ///     name of the AppDomain index or name by calling the appropriate function
        ///     on RuntimeBase.
        ///     Note:  HasAppDomainData must be true before getting this property.
        /// </summary>
        /// <value>The application domain.</value>
        /// <inheritdoc />
        public IClrAppDomain AppDomain { get; }

        /// <summary>
        ///     Returns the gc segment type associated with this data.  Only callable if
        ///     HasGCHeapData is true.
        /// </summary>
        /// <value>The type of the gc segment.</value>
        /// <inheritdoc />
        public GcSegmentType GcSegmentType { get; set; }

        /// <summary>
        ///     Returns the heap number associated with this data.  Returns -1 if no
        ///     GC heap is associated with this memory region.
        /// </summary>
        /// <value>The heap number.</value>
        /// <inheritdoc />
        public int HeapNumber => MemoryRegion.HeapNumber;

        /// <summary>
        ///     The type of heap/memory that the region contains.
        /// </summary>
        /// <value>The type.</value>
        /// <inheritdoc />
        public ClrMemoryRegionType MemoryRegionType { get; set; }

        /// <summary>
        ///     The Module pointer that corresponds to this heap.  You can obtain the
        ///     filename of the module with this property.
        ///     Note:  HasModuleData must be true or this property will be null.
        /// </summary>
        /// <value>The module.</value>
        /// <inheritdoc />
        public string Module => MemoryRegion.Module;

        /// <summary>
        ///     The size of the memory region in bytes.
        /// </summary>
        /// <value>The size.</value>
        /// <inheritdoc />
        public ulong Size => MemoryRegion.Size;

        /// <summary>
        ///     Gets or sets the converter.
        /// </summary>
        /// <value>The converter.</value>
        [Import]
        internal IConverter Converter { get; set; }

        /// <summary>
        ///     Gets or sets the memory region.
        /// </summary>
        /// <value>The memory region.</value>
        internal ClrMemoryRegion MemoryRegion { get; set; }
    }
}