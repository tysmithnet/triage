// ***********************************************************************
// Assembly         : Triage.Mortician.Core
// Author           : @tysmithnet
// Created          : 09-27-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-02-2018
// ***********************************************************************
// <copyright file="ManagedCodeLocation.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;

namespace Triage.Mortician.Core
{
    /// <summary>
    ///     Class ManagedCodeLocation.
    /// </summary>
    /// <seealso cref="System.IEquatable{Triage.Mortician.Core.ManagedCodeLocation}" />
    /// <seealso cref="System.IComparable{Triage.Mortician.Core.ManagedCodeLocation}" />
    /// <seealso cref="Triage.Mortician.Core.CodeLocation" />
    public class ManagedCodeLocation : CodeLocation, IEquatable<ManagedCodeLocation>, IComparable<ManagedCodeLocation>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ManagedCodeLocation" /> class.
        /// </summary>
        /// <param name="methodDescriptor">The method descriptor.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="method">The method.</param>
        /// <inheritdoc />
        public ManagedCodeLocation(ulong methodDescriptor, ulong offset, string method) : base(null, method, offset)
        {
            MethodDescriptor = methodDescriptor;
        }

        /// <summary>
        ///     Compares to.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>System.Int32.</returns>
        /// <inheritdoc />
        public int CompareTo(ManagedCodeLocation other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var codeLocationComparison = base.CompareTo(other);
            if (codeLocationComparison != 0) return codeLocationComparison;
            return MethodDescriptor.CompareTo(other.MethodDescriptor);
        }

        /// <summary>
        ///     Compares to.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="ArgumentException">ManagedCodeLocation</exception>
        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            if (!(obj is ManagedCodeLocation))
                throw new ArgumentException($"Object must be of type {nameof(ManagedCodeLocation)}");
            return CompareTo((ManagedCodeLocation) obj);
        }

        /// <summary>
        ///     Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <inheritdoc />
        public bool Equals(ManagedCodeLocation other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && MethodDescriptor == other.MethodDescriptor;
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
            return Equals((ManagedCodeLocation) obj);
        }

        /// <summary>
        ///     Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode() * 397) ^ MethodDescriptor.GetHashCode();
            }
        }

        /// <summary>
        ///     Implements the &gt; operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >(ManagedCodeLocation left, ManagedCodeLocation right) =>
            Comparer<ManagedCodeLocation>.Default.Compare(left, right) > 0;

        /// <summary>
        ///     Implements the &gt;= operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(ManagedCodeLocation left, ManagedCodeLocation right) =>
            Comparer<ManagedCodeLocation>.Default.Compare(left, right) >= 0;

        /// <summary>
        ///     Implements the &lt; operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(ManagedCodeLocation left, ManagedCodeLocation right) =>
            Comparer<ManagedCodeLocation>.Default.Compare(left, right) < 0;

        /// <summary>
        ///     Implements the &lt;= operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(ManagedCodeLocation left, ManagedCodeLocation right) =>
            Comparer<ManagedCodeLocation>.Default.Compare(left, right) <= 0;

        /// <summary>
        ///     Gets the method descriptor.
        /// </summary>
        /// <value>The method descriptor.</value>
        public ulong MethodDescriptor { get; }
    }
}