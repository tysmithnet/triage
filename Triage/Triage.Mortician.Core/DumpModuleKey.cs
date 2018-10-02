// ***********************************************************************
// Assembly         : Triage.Mortician.Core
// Author           : @tysmithnet
// Created          : 09-29-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-02-2018
// ***********************************************************************
// <copyright file="DumpModuleKey.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;

namespace Triage.Mortician.Core
{
    /// <summary>
    ///     Class DumpModuleKey.
    /// </summary>
    /// <seealso cref="System.IComparable{Triage.Mortician.Core.DumpModuleKey}" />
    /// <seealso cref="System.IComparable" />
    /// <seealso cref="System.IEquatable{Triage.Mortician.Core.DumpModuleKey}" />
    public class DumpModuleKey : IEquatable<DumpModuleKey>, IComparable<DumpModuleKey>, IComparable
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DumpModuleKey" /> class.
        /// </summary>
        /// <param name="assemblyId">The assembly identifier.</param>
        /// <param name="name">The name.</param>
        /// <exception cref="ArgumentNullException">name</exception>
        /// <inheritdoc />
        public DumpModuleKey(ulong assemblyId, string name)
        {
            AssemblyId = assemblyId;
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        /// <summary>
        ///     Compares to.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>System.Int32.</returns>
        /// <inheritdoc />
        public int CompareTo(DumpModuleKey other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var assemblyIdComparison = AssemblyId.CompareTo(other.AssemblyId);
            if (assemblyIdComparison != 0) return assemblyIdComparison;
            return string.Compare(Name, other.Name, StringComparison.Ordinal);
        }

        /// <summary>
        ///     Compares to.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="ArgumentException">DumpModuleKey</exception>
        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            if (!(obj is DumpModuleKey)) throw new ArgumentException($"Object must be of type {nameof(DumpModuleKey)}");
            return CompareTo((DumpModuleKey) obj);
        }

        /// <summary>
        ///     Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <inheritdoc />
        public bool Equals(DumpModuleKey other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return AssemblyId == other.AssemblyId && string.Equals(Name, other.Name);
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
            return Equals((DumpModuleKey) obj);
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
                return (AssemblyId.GetHashCode() * 397) ^ (Name != null ? Name.GetHashCode() : 0);
            }
        }

        /// <summary>
        ///     Implements the &gt; operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >(DumpModuleKey left, DumpModuleKey right) =>
            Comparer<DumpModuleKey>.Default.Compare(left, right) > 0;

        /// <summary>
        ///     Implements the &gt;= operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(DumpModuleKey left, DumpModuleKey right) =>
            Comparer<DumpModuleKey>.Default.Compare(left, right) >= 0;

        /// <summary>
        ///     Implements the &lt; operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(DumpModuleKey left, DumpModuleKey right) =>
            Comparer<DumpModuleKey>.Default.Compare(left, right) < 0;

        /// <summary>
        ///     Implements the &lt;= operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(DumpModuleKey left, DumpModuleKey right) =>
            Comparer<DumpModuleKey>.Default.Compare(left, right) <= 0;

        /// <summary>
        ///     Gets the assembly identifier.
        /// </summary>
        /// <value>The assembly identifier.</value>
        public ulong AssemblyId { get; }

        /// <summary>
        ///     Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; }
    }
}