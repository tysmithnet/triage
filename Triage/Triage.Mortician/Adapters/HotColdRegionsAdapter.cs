using System;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class HotColdRegionsAdapter : IHotColdRegions
    {
        /// <inheritdoc />
        public HotColdRegionsAdapter(Microsoft.Diagnostics.Runtime.HotColdRegions hotColdRegions)
        {
            HotColdRegions = hotColdRegions ?? throw new ArgumentNullException(nameof(hotColdRegions));
        }

        internal Microsoft.Diagnostics.Runtime.HotColdRegions HotColdRegions;

        /// <inheritdoc />
        public uint ColdSize => HotColdRegions.ColdSize;

        /// <inheritdoc />
        public ulong ColdStart => HotColdRegions.ColdStart;

        /// <inheritdoc />
        public uint HotSize => HotColdRegions.HotSize;

        /// <inheritdoc />
        public ulong HotStart => HotColdRegions.HotStart;
    }
}