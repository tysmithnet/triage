// ***********************************************************************
// Assembly         : Triage.Mortician.Core
// Author           : @tysmithnet
// Created          : 10-01-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-01-2018
// ***********************************************************************
// <copyright file="DumpMemoryRegion.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Core
{
    /// <summary>
    ///     Class DumpMemoryRegion.
    /// </summary>
    public class DumpMemoryRegion
    {
        /// <summary>
        ///     Gets or sets the address.
        /// </summary>
        /// <value>The address.</value>
        public ulong Address { get; set; }

        /// <summary>
        ///     Gets or sets the type of the gc segment.
        /// </summary>
        /// <value>The type of the gc segment.</value>
        public GcSegmentType GcSegmentType { get; set; }

        /// <summary>
        ///     Gets or sets the heap number.
        /// </summary>
        /// <value>The heap number.</value>
        public int HeapNumber { get; set; }

        /// <summary>
        ///     Gets or sets the type of the memory region.
        /// </summary>
        /// <value>The type of the memory region.</value>
        public ClrMemoryRegionType MemoryRegionType { get; set; }

        /// <summary>
        ///     Gets or sets the size.
        /// </summary>
        /// <value>The size.</value>
        public ulong Size { get; set; }
    }
}