// ***********************************************************************
// Assembly         : Triage.Mortician.Core
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="IClrSegment.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;

namespace Triage.Mortician.Core.ClrMdAbstractions
{
    /// <summary>
    ///     Interface IClrSegment
    /// </summary>
    public interface IClrSegment
    {
        /// <summary>
        ///     Enumerates all objects on the segment.
        /// </summary>
        /// <returns>IEnumerable&lt;System.UInt64&gt;.</returns>
        IEnumerable<ulong> EnumerateObjectAddresses();

        /// <summary>
        ///     FirstObject returns the first object on this segment or 0 if this segment contains no objects.
        /// </summary>
        /// <param name="type">The type of the first object.</param>
        /// <returns>The first object on this segment or 0 if this segment contains no objects.</returns>
        ulong GetFirstObject(out IClrType type);

        /// <summary>
        ///     Returns the generation of an object in this segment.
        /// </summary>
        /// <param name="obj">An object in this segment.</param>
        /// <returns>
        ///     The generation of the given object if that object lies in this segment.  The return
        ///     value is undefined if the object does not lie in this segment.
        /// </returns>
        int GetGeneration(ulong obj);

        /// <summary>
        ///     Given an object on the segment, return the 'next' object in the segment.  Returns
        ///     0 when there are no more objects.   (Or enumeration is not possible)
        /// </summary>
        /// <param name="objRef">The object reference.</param>
        /// <returns>System.UInt64.</returns>
        ulong NextObject(ulong objRef);

        /// <summary>
        ///     Given an object on the segment, return the 'next' object in the segment.  Returns
        ///     0 when there are no more objects.   (Or enumeration is not possible)
        /// </summary>
        /// <param name="objRef">The object reference.</param>
        /// <param name="type">The type.</param>
        /// <returns>System.UInt64.</returns>
        ulong NextObject(ulong objRef, out IClrType type);

        /// <summary>
        ///     Returns a string representation of this object.
        /// </summary>
        /// <returns>A string representation of this object.</returns>
        string ToString();

        /// <summary>
        ///     The address of the end of memory committed for the segment (this may be longer than Length).
        /// </summary>
        /// <value>The committed end.</value>
        ulong CommittedEnd { get; }

        /// <summary>
        ///     The end address of the segment.  All objects in this segment fall within Start &lt;= object &lt; End.
        /// </summary>
        /// <value>The end.</value>
        ulong End { get; }

        /// <summary>
        ///     FirstObject returns the first object on this segment or 0 if this segment contains no objects.
        /// </summary>
        /// <value>The first object.</value>
        ulong FirstObject { get; }

        /// <summary>
        ///     The length of the gen0 portion of this segment.
        /// </summary>
        /// <value>The length of the gen0.</value>
        ulong Gen0Length { get; }

        /// <summary>
        ///     Ephemeral heap sements have geneation 0 and 1 in them.  Gen 1 is always above Gen 2 and
        ///     Gen 0 is above Gen 1.  This property tell where Gen 0 start in memory.   Note that
        ///     if this is not an Ephemeral segment, then this will return End (which makes Gen 0 empty
        ///     for this segment)
        /// </summary>
        /// <value>The gen0 start.</value>
        ulong Gen0Start { get; }

        /// <summary>
        ///     The length of the gen1 portion of this segment.
        /// </summary>
        /// <value>The length of the gen1.</value>
        ulong Gen1Length { get; }

        /// <summary>
        ///     The start of the gen1 portion of this segment.
        /// </summary>
        /// <value>The gen1 start.</value>
        ulong Gen1Start { get; }

        /// <summary>
        ///     The length of the gen2 portion of this segment.
        /// </summary>
        /// <value>The length of the gen2.</value>
        ulong Gen2Length { get; }

        /// <summary>
        ///     The start of the gen2 portion of this segment.
        /// </summary>
        /// <value>The gen2 start.</value>
        ulong Gen2Start { get; }

        /// <summary>
        ///     The GC heap associated with this segment.  There's only one GCHeap per process, so this is
        ///     only a convenience method to keep from having to pass the heap along with a segment.
        /// </summary>
        /// <value>The heap.</value>
        IClrHeap Heap { get; }

        /// <summary>
        ///     Returns true if this segment is the ephemeral segment (meaning it contains gen0 and gen1
        ///     objects).
        /// </summary>
        /// <value><c>true</c> if this instance is ephemeral; otherwise, <c>false</c>.</value>
        bool IsEphemeral { get; }

        /// <summary>
        ///     Returns true if this is a segment for the Large Object Heap.  False otherwise.
        ///     Large objects (greater than 85,000 bytes in size), are stored in their own segments and
        ///     only collected on full (gen 2) collections.
        /// </summary>
        /// <value><c>true</c> if this instance is large; otherwise, <c>false</c>.</value>
        bool IsLarge { get; }

        /// <summary>
        ///     The number of bytes in the segment.
        /// </summary>
        /// <value>The length.</value>
        ulong Length { get; }

        /// <summary>
        ///     The processor that this heap is affinitized with.  In a workstation GC, there is no processor
        ///     affinity (and the return value of this property is undefined).  In a server GC each segment
        ///     has a logical processor in the PC associated with it.  This property returns that logical
        ///     processor number (starting at 0).
        /// </summary>
        /// <value>The processor affinity.</value>
        int ProcessorAffinity { get; }

        /// <summary>
        ///     The address of the end of memory reserved for the segment, but not committed.
        /// </summary>
        /// <value>The reserved end.</value>
        ulong ReservedEnd { get; }

        /// <summary>
        ///     The start address of the segment.  All objects in this segment fall within Start &lt;= object &lt; End.
        /// </summary>
        /// <value>The start.</value>
        ulong Start { get; }
    }
}