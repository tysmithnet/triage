// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-19-2018
// ***********************************************************************
// <copyright file="ClrSegmentAdapter.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Microsoft.Diagnostics.Runtime;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    /// <summary>
    ///     Class ClrSegmentAdapter.
    /// </summary>
    /// <seealso cref="Triage.Mortician.Core.ClrMdAbstractions.IClrSegment" />
    internal class ClrSegmentAdapter : BaseAdapter, IClrSegment
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ClrSegmentAdapter" /> class.
        /// </summary>
        /// <param name="segment">The segment.</param>
        /// <exception cref="ArgumentNullException">segment</exception>
        /// <inheritdoc />
        public ClrSegmentAdapter(IConverter converter, ClrSegment segment) : base(converter)
        {
            Segment = segment ?? throw new ArgumentNullException(nameof(segment));
            Heap = Converter.Convert(segment.Heap);
        }

        /// <summary>
        ///     The segment
        /// </summary>
        internal ClrSegment Segment;

        /// <summary>
        ///     Enumerates all objects on the segment.
        /// </summary>
        /// <returns>IEnumerable&lt;System.UInt64&gt;.</returns>
        /// <inheritdoc />
        public IEnumerable<ulong> EnumerateObjectAddresses() => Segment.EnumerateObjectAddresses();

        /// <summary>
        ///     FirstObject returns the first object on this segment or 0 if this segment contains no objects.
        /// </summary>
        /// <param name="type">The type of the first object.</param>
        /// <returns>The first object on this segment or 0 if this segment contains no objects.</returns>
        /// <inheritdoc />
        public ulong GetFirstObject(out IClrType type)
        {
            var res = Segment.GetFirstObject(out var first);
            type = first != null ? Converter.Convert(first) : null;
            return res;
        }

        /// <summary>
        ///     Returns the generation of an object in this segment.
        /// </summary>
        /// <param name="obj">An object in this segment.</param>
        /// <returns>
        ///     The generation of the given object if that object lies in this segment.  The return
        ///     value is undefined if the object does not lie in this segment.
        /// </returns>
        /// <inheritdoc />
        public int GetGeneration(ulong obj) => Segment.GetGeneration(obj);

        /// <summary>
        ///     Given an object on the segment, return the 'next' object in the segment.  Returns
        ///     0 when there are no more objects.   (Or enumeration is not possible)
        /// </summary>
        /// <param name="objRef">The object reference.</param>
        /// <returns>System.UInt64.</returns>
        /// <inheritdoc />
        public ulong NextObject(ulong objRef) => Segment.NextObject(objRef);

        /// <summary>
        ///     Given an object on the segment, return the 'next' object in the segment.  Returns
        ///     0 when there are no more objects.   (Or enumeration is not possible)
        /// </summary>
        /// <param name="objRef">The object reference.</param>
        /// <param name="type">The type.</param>
        /// <returns>System.UInt64.</returns>
        /// <inheritdoc />
        public ulong NextObject(ulong objRef, out IClrType type)
        {
            var res = Segment.NextObject(objRef, out var nextType);
            type = nextType != null ? Converter.Convert(nextType) : null;
            return res;
        }

        /// <summary>
        ///     The address of the end of memory committed for the segment (this may be longer than Length).
        /// </summary>
        /// <value>The committed end.</value>
        /// <inheritdoc />
        public ulong CommittedEnd => Segment.CommittedEnd;

        /// <summary>
        ///     The end address of the segment.  All objects in this segment fall within Start &lt;= object &lt; End.
        /// </summary>
        /// <value>The end.</value>
        /// <inheritdoc />
        public ulong End => Segment.End;

        /// <summary>
        ///     FirstObject returns the first object on this segment or 0 if this segment contains no objects.
        /// </summary>
        /// <value>The first object.</value>
        /// <inheritdoc />
        public ulong FirstObject => Segment.FirstObject;

        /// <summary>
        ///     The length of the gen0 portion of this segment.
        /// </summary>
        /// <value>The length of the gen0.</value>
        /// <inheritdoc />
        public ulong Gen0Length => Segment.Gen0Length;

        /// <summary>
        ///     Ephemeral heap sements have geneation 0 and 1 in them.  Gen 1 is always above Gen 2 and
        ///     Gen 0 is above Gen 1.  This property tell where Gen 0 start in memory.   Note that
        ///     if this is not an Ephemeral segment, then this will return End (which makes Gen 0 empty
        ///     for this segment)
        /// </summary>
        /// <value>The gen0 start.</value>
        /// <inheritdoc />
        public ulong Gen0Start => Segment.Gen0Start;

        /// <summary>
        ///     The length of the gen1 portion of this segment.
        /// </summary>
        /// <value>The length of the gen1.</value>
        /// <inheritdoc />
        public ulong Gen1Length => Segment.Gen1Length;

        /// <summary>
        ///     The start of the gen1 portion of this segment.
        /// </summary>
        /// <value>The gen1 start.</value>
        /// <inheritdoc />
        public ulong Gen1Start => Segment.Gen1Start;

        /// <summary>
        ///     The length of the gen2 portion of this segment.
        /// </summary>
        /// <value>The length of the gen2.</value>
        /// <inheritdoc />
        public ulong Gen2Length => Segment.Gen2Length;

        /// <summary>
        ///     The start of the gen2 portion of this segment.
        /// </summary>
        /// <value>The gen2 start.</value>
        /// <inheritdoc />
        public ulong Gen2Start => Segment.Gen2Start;

        /// <summary>
        ///     The GC heap associated with this segment.  There's only one GCHeap per process, so this is
        ///     only a convenience method to keep from having to pass the heap along with a segment.
        /// </summary>
        /// <value>The heap.</value>
        /// <inheritdoc />
        public IClrHeap Heap { get; }

        /// <summary>
        ///     Returns true if this segment is the ephemeral segment (meaning it contains gen0 and gen1
        ///     objects).
        /// </summary>
        /// <value><c>true</c> if this instance is ephemeral; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsEphemeral => Segment.IsEphemeral;

        /// <summary>
        ///     Returns true if this is a segment for the Large Object Heap.  False otherwise.
        ///     Large objects (greater than 85,000 bytes in size), are stored in their own segments and
        ///     only collected on full (gen 2) collections.
        /// </summary>
        /// <value><c>true</c> if this instance is large; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsLarge => Segment.IsLarge;

        /// <summary>
        ///     The number of bytes in the segment.
        /// </summary>
        /// <value>The length.</value>
        /// <inheritdoc />
        public ulong Length => Segment.Length;

        /// <summary>
        ///     The processor that this heap is affinitized with.  In a workstation GC, there is no processor
        ///     affinity (and the return value of this property is undefined).  In a server GC each segment
        ///     has a logical processor in the PC associated with it.  This property returns that logical
        ///     processor number (starting at 0).
        /// </summary>
        /// <value>The processor affinity.</value>
        /// <inheritdoc />
        public int ProcessorAffinity => Segment.ProcessorAffinity;

        /// <summary>
        ///     The address of the end of memory reserved for the segment, but not committed.
        /// </summary>
        /// <value>The reserved end.</value>
        /// <inheritdoc />
        public ulong ReservedEnd => Segment.ReservedEnd;

        /// <summary>
        ///     The start address of the segment.  All objects in this segment fall within Start &lt;= object &lt; End.
        /// </summary>
        /// <value>The start.</value>
        /// <inheritdoc />
        public ulong Start => Segment.Start;

        /// <summary>
        ///     Gets or sets the converter.
        /// </summary>
        /// <value>The converter.</value>
        [Import]
        internal IConverter Converter { get; set; }
    }
}