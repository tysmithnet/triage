using System;
using System.Collections.Generic;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class ClrSegmentAdapter : IClrSegment
    {
        /// <inheritdoc />
        public ClrSegmentAdapter(Microsoft.Diagnostics.Runtime.ClrSegment segment)
        {
            _segment = segment ?? throw new ArgumentNullException(nameof(segment));
        }

        internal Microsoft.Diagnostics.Runtime.ClrSegment _segment;

        /// <inheritdoc />
        public IEnumerable<ulong> EnumerateObjectAddresses()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ulong GetFirstObject(out IClrType type)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public int GetGeneration(ulong obj)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ulong NextObject(ulong objRef)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ulong NextObject(ulong objRef, out IClrType type)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ulong CommittedEnd { get; }

        /// <inheritdoc />
        public ulong End { get; }

        /// <inheritdoc />
        public ulong FirstObject { get; }

        /// <inheritdoc />
        public ulong Gen0Length { get; }

        /// <inheritdoc />
        public ulong Gen0Start { get; }

        /// <inheritdoc />
        public ulong Gen1Length { get; }

        /// <inheritdoc />
        public ulong Gen1Start { get; }

        /// <inheritdoc />
        public ulong Gen2Length { get; }

        /// <inheritdoc />
        public ulong Gen2Start { get; }

        /// <inheritdoc />
        public IClrHeap Heap { get; }

        /// <inheritdoc />
        public bool IsEphemeral { get; }

        /// <inheritdoc />
        public bool IsLarge { get; }

        /// <inheritdoc />
        public ulong Length { get; }

        /// <inheritdoc />
        public int ProcessorAffinity { get; }

        /// <inheritdoc />
        public ulong ReservedEnd { get; }

        /// <inheritdoc />
        public ulong Start { get; }
    }
}