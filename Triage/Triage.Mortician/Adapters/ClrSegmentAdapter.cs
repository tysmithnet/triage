using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class ClrSegmentAdapter : IClrSegment
    {
        [Import]
        internal IConverter Converter { get; set; }
        /// <inheritdoc />
        public ClrSegmentAdapter(Microsoft.Diagnostics.Runtime.ClrSegment segment)
        {
            Segment = segment ?? throw new ArgumentNullException(nameof(segment));
            Heap = Converter.Convert(segment.Heap);
        }

        internal Microsoft.Diagnostics.Runtime.ClrSegment Segment;

        /// <inheritdoc />
        public IEnumerable<ulong> EnumerateObjectAddresses() => Segment.EnumerateObjectAddresses();

        /// <inheritdoc />
        public ulong GetFirstObject(out IClrType type)
        {
            var res = Segment.GetFirstObject(out var first);
            type = first != null ? Converter.Convert(first) : null;
            return res;
        }

        /// <inheritdoc />
        public int GetGeneration(ulong obj) => Segment.GetGeneration(obj);

        /// <inheritdoc />
        public ulong NextObject(ulong objRef) => Segment.NextObject(objRef);

        /// <inheritdoc />
        public ulong NextObject(ulong objRef, out IClrType type)
        {
            var res = Segment.NextObject(objRef, out var nextType);
            type = nextType != null ? Converter.Convert(nextType) : null;
            return res;
        }

        /// <inheritdoc />
        public ulong CommittedEnd => Segment.CommittedEnd;

        /// <inheritdoc />
        public ulong End => Segment.End;

        /// <inheritdoc />
        public ulong FirstObject => Segment.FirstObject;

        /// <inheritdoc />
        public ulong Gen0Length => Segment.Gen0Length;

        /// <inheritdoc />
        public ulong Gen0Start => Segment.Gen0Start;

        /// <inheritdoc />
        public ulong Gen1Length => Segment.Gen1Length;

        /// <inheritdoc />
        public ulong Gen1Start => Segment.Gen1Start;

        /// <inheritdoc />
        public ulong Gen2Length => Segment.Gen2Length;

        /// <inheritdoc />
        public ulong Gen2Start => Segment.Gen2Start;

        /// <inheritdoc />
        public IClrHeap Heap { get; }

        /// <inheritdoc />
        public bool IsEphemeral => Segment.IsEphemeral;

        /// <inheritdoc />
        public bool IsLarge => Segment.IsLarge;

        /// <inheritdoc />
        public ulong Length => Segment.Length;

        /// <inheritdoc />
        public int ProcessorAffinity => Segment.ProcessorAffinity;

        /// <inheritdoc />
        public ulong ReservedEnd => Segment.ReservedEnd;

        /// <inheritdoc />
        public ulong Start => Segment.Start;
    }
}