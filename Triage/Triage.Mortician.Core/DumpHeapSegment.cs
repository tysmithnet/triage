// ***********************************************************************
// Assembly         : Triage.Mortician.Core
// Author           : @tysmithnet
// Created          : 10-01-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-01-2018
// ***********************************************************************
// <copyright file="DumpHeapSegment.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Core
{
    /// <summary>
    ///     Class DumpHeapSegment.
    /// </summary>
    /// <seealso cref="System.IEquatable{Triage.Mortician.Core.DumpHeapSegment}" />
    /// <seealso cref="System.IComparable{Triage.Mortician.Core.DumpHeapSegment}" />
    /// <seealso cref="System.IComparable" />
    public class DumpHeapSegment : IEquatable<DumpHeapSegment>, IComparable<DumpHeapSegment>, IComparable
    {
        /// <summary>
        ///     Compares to.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>System.Int32.</returns>
        /// <inheritdoc />
        public int CompareTo(DumpHeapSegment other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return Start.CompareTo(other.Start);
        }

        /// <summary>
        ///     Compares to.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="ArgumentException">DumpHeapSegment</exception>
        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            if (!(obj is DumpHeapSegment))
                throw new ArgumentException($"Object must be of type {nameof(DumpHeapSegment)}");
            return CompareTo((DumpHeapSegment) obj);
        }

        /// <summary>
        ///     Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <inheritdoc />
        public bool Equals(DumpHeapSegment other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Start == other.Start;
        }

        /// <summary>
        ///     Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((DumpHeapSegment) obj);
        }

        /// <summary>
        ///     Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        /// <inheritdoc />
        public override int GetHashCode() => Start.GetHashCode();

        /// <summary>
        ///     Implements the &gt; operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >(DumpHeapSegment left, DumpHeapSegment right) =>
            Comparer<DumpHeapSegment>.Default.Compare(left, right) > 0;

        /// <summary>
        ///     Implements the &gt;= operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(DumpHeapSegment left, DumpHeapSegment right) =>
            Comparer<DumpHeapSegment>.Default.Compare(left, right) >= 0;

        /// <summary>
        ///     Implements the &lt; operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(DumpHeapSegment left, DumpHeapSegment right) =>
            Comparer<DumpHeapSegment>.Default.Compare(left, right) < 0;

        /// <summary>
        ///     Implements the &lt;= operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(DumpHeapSegment left, DumpHeapSegment right) =>
            Comparer<DumpHeapSegment>.Default.Compare(left, right) <= 0;

        /// <summary>
        ///     Gets or sets the committed end.
        /// </summary>
        /// <value>The committed end.</value>
        public ulong CommittedEnd { get; set; }

        /// <summary>
        ///     Gets or sets the end.
        /// </summary>
        /// <value>The end.</value>
        public ulong End { get; set; }

        /// <summary>
        ///     Gets or sets the first object.
        /// </summary>
        /// <value>The first object.</value>
        public ulong FirstObject { get; set; }

        /// <summary>
        ///     Gets or sets the length of the gen0.
        /// </summary>
        /// <value>The length of the gen0.</value>
        public ulong Gen0Length { get; set; }

        /// <summary>
        ///     Gets or sets the gen0 start.
        /// </summary>
        /// <value>The gen0 start.</value>
        public ulong Gen0Start { get; set; }

        /// <summary>
        ///     Gets or sets the length of the gen1.
        /// </summary>
        /// <value>The length of the gen1.</value>
        public ulong Gen1Length { get; set; }

        /// <summary>
        ///     Gets or sets the gen1 start.
        /// </summary>
        /// <value>The gen1 start.</value>
        public ulong Gen1Start { get; set; }

        /// <summary>
        ///     Gets or sets the length of the gen2.
        /// </summary>
        /// <value>The length of the gen2.</value>
        public ulong Gen2Length { get; set; }

        /// <summary>
        ///     Gets or sets the gen2 start.
        /// </summary>
        /// <value>The gen2 start.</value>
        public ulong Gen2Start { get; set; }

        /// <summary>
        ///     Gets or sets the heap.
        /// </summary>
        /// <value>The heap.</value>
        public IClrHeap Heap { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is ephemeral.
        /// </summary>
        /// <value><c>true</c> if this instance is ephemeral; otherwise, <c>false</c>.</value>
        public bool IsEphemeral { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is large.
        /// </summary>
        /// <value><c>true</c> if this instance is large; otherwise, <c>false</c>.</value>
        public bool IsLarge { get; set; }

        /// <summary>
        ///     Gets or sets the length.
        /// </summary>
        /// <value>The length.</value>
        public ulong Length { get; set; }

        /// <summary>
        ///     Gets or sets the processor affinity.
        /// </summary>
        /// <value>The processor affinity.</value>
        public int ProcessorAffinity { get; set; }

        /// <summary>
        ///     Gets or sets the reserved end.
        /// </summary>
        /// <value>The reserved end.</value>
        public ulong ReservedEnd { get; set; }

        /// <summary>
        ///     Gets or sets the start.
        /// </summary>
        /// <value>The start.</value>
        public ulong Start { get; set; }
    }
}