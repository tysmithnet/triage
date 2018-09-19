using System;
using System.ComponentModel.Composition;
using Microsoft.Diagnostics.Runtime;
using Triage.Mortician.Core.ClrMdAbstractions;
using ClrMemoryRegionType = Triage.Mortician.Core.ClrMdAbstractions.ClrMemoryRegionType;

namespace Triage.Mortician.Adapters
{
    internal class MemoryRegionAdapter : IClrMemoryRegion
    {
        [Import]
        internal IConverter Converter { get; set; }
        public MemoryRegionAdapter(ClrMemoryRegion memoryRegion)
        {
            MemoryRegion = memoryRegion ?? throw new ArgumentNullException(nameof(memoryRegion));
            AppDomain = Converter.Convert(memoryRegion.AppDomain);
            GcSegmentType = Converter.Convert(memoryRegion.GCSegmentType);
            MemoryRegionType = Converter.Convert(memoryRegion.Type);
        }

        internal ClrMemoryRegion MemoryRegion { get; set; }

        /// <inheritdoc />
        public string ToString(bool detailed) => MemoryRegion.ToString(detailed);

        /// <inheritdoc />
        public ulong Address => MemoryRegion.Address;

        /// <inheritdoc />
        public IClrAppDomain AppDomain { get; }

        /// <inheritdoc />
        public GcSegmentType GcSegmentType { get; set; }

        /// <inheritdoc />
        public int HeapNumber => MemoryRegion.HeapNumber;

        /// <inheritdoc />
        public string Module => MemoryRegion.Module;

        /// <inheritdoc />
        public ulong Size => MemoryRegion.Size;

        /// <inheritdoc />
        public ClrMemoryRegionType MemoryRegionType { get; set; }
    }
}