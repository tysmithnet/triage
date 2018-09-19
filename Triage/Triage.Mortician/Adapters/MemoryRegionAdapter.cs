using System;
using Microsoft.Diagnostics.Runtime;
using Triage.Mortician.Core.ClrMdAbstractions;
using ClrMemoryRegionType = Triage.Mortician.Core.ClrMdAbstractions.ClrMemoryRegionType;

namespace Triage.Mortician.Adapters
{
    internal class MemoryRegionAdapter : IClrMemoryRegion
    {
        public MemoryRegionAdapter(ClrMemoryRegion memoryRegion)
        {
            MemoryRegion = memoryRegion ?? throw new ArgumentNullException(nameof(memoryRegion));
        }

        internal ClrMemoryRegion MemoryRegion { get; set; }

        /// <inheritdoc />
        public string ToString(bool detailed)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ulong Address { get; set; }

        /// <inheritdoc />
        public IClrAppDomain AppDomain { get; }

        /// <inheritdoc />
        public GcSegmentType GCSegmentType { get; set; }

        /// <inheritdoc />
        public int HeapNumber { get; set; }

        /// <inheritdoc />
        public string Module { get; }

        /// <inheritdoc />
        public ulong Size { get; set; }

        /// <inheritdoc />
        public ClrMemoryRegionType Type { get; set; }
    }
}