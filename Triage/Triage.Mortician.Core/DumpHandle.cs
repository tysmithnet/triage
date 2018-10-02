// ***********************************************************************
// Assembly         : Triage.Mortician.Core
// Author           : @tysmithnet
// Created          : 09-26-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-01-2018
// ***********************************************************************
// <copyright file="DumpHandle.cs" company="">
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
    ///     Class DumpHandle.
    /// </summary>
    /// <seealso cref="System.IEquatable{Triage.Mortician.Core.DumpHandle}" />
    /// <seealso cref="System.IComparable{Triage.Mortician.Core.DumpHandle}" />
    /// <seealso cref="System.IComparable" />
    public class DumpHandle : IEquatable<DumpHandle>, IComparable<DumpHandle>, IComparable
    {
        /// <summary>
        ///     Compares to.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>System.Int32.</returns>
        /// <inheritdoc />
        public int CompareTo(DumpHandle other)
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
        /// <exception cref="ArgumentException">DumpHandle</exception>
        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            if (!(obj is DumpHandle)) throw new ArgumentException($"Object must be of type {nameof(DumpHandle)}");
            return CompareTo((DumpHandle) obj);
        }

        /// <summary>
        ///     Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <inheritdoc />
        public bool Equals(DumpHandle other)
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
            return Equals((DumpHandle) obj);
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
        public static bool operator >(DumpHandle left, DumpHandle right) =>
            Comparer<DumpHandle>.Default.Compare(left, right) > 0;

        /// <summary>
        ///     Implements the &gt;= operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(DumpHandle left, DumpHandle right) =>
            Comparer<DumpHandle>.Default.Compare(left, right) >= 0;

        /// <summary>
        ///     Implements the &lt; operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(DumpHandle left, DumpHandle right) =>
            Comparer<DumpHandle>.Default.Compare(left, right) < 0;

        /// <summary>
        ///     Implements the &lt;= operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(DumpHandle left, DumpHandle right) =>
            Comparer<DumpHandle>.Default.Compare(left, right) <= 0;

        /// <summary>
        ///     Gets or sets the address.
        /// </summary>
        /// <value>The address.</value>
        public ulong Address { get; set; }

        /// <summary>
        ///     Gets or sets the application domain.
        /// </summary>
        /// <value>The application domain.</value>
        public DumpAppDomain AppDomain { get; set; }

        /// <summary>
        ///     Gets or sets the dependent target.
        /// </summary>
        /// <value>The dependent target.</value>
        public ulong DependentTarget { get; set; }

        /// <summary>
        ///     Gets or sets the type of the dependent.
        /// </summary>
        /// <value>The type of the dependent.</value>
        public DumpType DependentType { get; set; }

        /// <summary>
        ///     Gets or sets the type of the handle.
        /// </summary>
        /// <value>The type of the handle.</value>
        public HandleType HandleType { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is pinned.
        /// </summary>
        /// <value><c>true</c> if this instance is pinned; otherwise, <c>false</c>.</value>
        public bool IsPinned { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is strong.
        /// </summary>
        /// <value><c>true</c> if this instance is strong; otherwise, <c>false</c>.</value>
        public bool IsStrong { get; set; }

        /// <summary>
        ///     Gets or sets the object address.
        /// </summary>
        /// <value>The object address.</value>
        public ulong ObjectAddress { get; set; }

        /// <summary>
        ///     Gets or sets the type of the object.
        /// </summary>
        /// <value>The type of the object.</value>
        public DumpType ObjectType { get; set; }

        /// <summary>
        ///     Gets or sets the reference count.
        /// </summary>
        /// <value>The reference count.</value>
        public uint RefCount { get; set; }
    }
}