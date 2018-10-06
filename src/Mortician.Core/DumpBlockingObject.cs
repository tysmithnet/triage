// ***********************************************************************
// Assembly         : Mortician.Core
// Author           : @tysmithnet
// Created          : 09-26-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-01-2018
// ***********************************************************************
// <copyright file="DumpBlockingObject.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using Mortician.Core.ClrMdAbstractions;

namespace Mortician.Core
{
    /// <summary>
    ///     Class DumpBlockingObject.
    /// </summary>
    /// <seealso cref="System.IEquatable{Mortician.Core.DumpBlockingObject}" />
    /// <seealso cref="System.IComparable{Mortician.Core.DumpBlockingObject}" />
    /// <seealso cref="System.IComparable" />
    public class DumpBlockingObject : IEquatable<DumpBlockingObject>, IComparable<DumpBlockingObject>, IComparable
    {
        /// <summary>
        ///     Compares to.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>System.Int32.</returns>
        /// <inheritdoc />
        public int CompareTo(DumpBlockingObject other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return Address.CompareTo(other.Address);
        }

        /// <summary>
        ///     Compares to.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="ArgumentException">DumpBlockingObject</exception>
        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            if (!(obj is DumpBlockingObject))
                throw new ArgumentException($"Object must be of type {nameof(DumpBlockingObject)}");
            return CompareTo((DumpBlockingObject) obj);
        }

        /// <summary>
        ///     Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <inheritdoc />
        public bool Equals(DumpBlockingObject other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Address == other.Address;
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
            return Equals((DumpBlockingObject) obj);
        }

        /// <summary>
        ///     Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        /// <inheritdoc />
        public override int GetHashCode() => Address.GetHashCode();

        /// <summary>
        ///     Implements the &gt; operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >(DumpBlockingObject left, DumpBlockingObject right) =>
            Comparer<DumpBlockingObject>.Default.Compare(left, right) > 0;

        /// <summary>
        ///     Implements the &gt;= operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(DumpBlockingObject left, DumpBlockingObject right) =>
            Comparer<DumpBlockingObject>.Default.Compare(left, right) >= 0;

        /// <summary>
        ///     Implements the &lt; operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(DumpBlockingObject left, DumpBlockingObject right) =>
            Comparer<DumpBlockingObject>.Default.Compare(left, right) < 0;

        /// <summary>
        ///     Implements the &lt;= operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(DumpBlockingObject left, DumpBlockingObject right) =>
            Comparer<DumpBlockingObject>.Default.Compare(left, right) <= 0;

        /// <summary>
        ///     Gets or sets the address.
        /// </summary>
        /// <value>The address.</value>
        public ulong Address { get; set; }

        /// <summary>
        ///     Gets or sets the blocking reason.
        /// </summary>
        /// <value>The blocking reason.</value>
        public BlockingReason BlockingReason { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is locked.
        /// </summary>
        /// <value><c>true</c> if this instance is locked; otherwise, <c>false</c>.</value>
        public bool IsLocked { get; set; }

        /// <summary>
        ///     Gets or sets the owners.
        /// </summary>
        /// <value>The owners.</value>
        public IList<DumpThread> Owners { get; set; }

        /// <summary>
        ///     Gets or sets the recursion count.
        /// </summary>
        /// <value>The recursion count.</value>
        public int RecursionCount { get; set; }

        /// <summary>
        ///     Gets or sets the waiters.
        /// </summary>
        /// <value>The waiters.</value>
        public IList<DumpThread> Waiters { get; set; }
    }
}