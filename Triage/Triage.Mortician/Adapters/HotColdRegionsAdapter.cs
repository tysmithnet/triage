using System;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class HotColdRegionsAdapter : IHotColdRegions
    {
        /// <inheritdoc />
        public HotColdRegionsAdapter(Microsoft.Diagnostics.Runtime.HotColdRegions hotColdRegions)
        {
            _hotColdRegions = hotColdRegions ?? throw new ArgumentNullException(nameof(hotColdRegions));
        }

        internal Microsoft.Diagnostics.Runtime.HotColdRegions _hotColdRegions;

        /// <inheritdoc />
        public uint ColdSize { get; }

        /// <inheritdoc />
        public ulong ColdStart { get; }

        /// <inheritdoc />
        public uint HotSize { get; }

        /// <inheritdoc />
        public ulong HotStart { get; }
    }
}