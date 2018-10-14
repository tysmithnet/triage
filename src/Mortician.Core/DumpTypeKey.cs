// ***********************************************************************
// Assembly         : Mortician
// Author           : @tysmithnet
// Created          : 12-19-2017
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-02-2018
// ***********************************************************************
// <copyright file="DumpTypeKey.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Mortician.Core
{
    /// <summary>
    ///     Type used to represent a unique type extracted from the memory dump
    ///     The method table alone is not sufficent to identify a type because generics
    ///     can share the same method table
    /// </summary>
    /// <seealso cref="System.IComparable{Mortician.Core.DumpTypeKey}" />
    /// <seealso cref="System.IComparable" />
    /// <seealso cref="System.IEquatable{Mortician.Core.DumpTypeKey}" />
    [DebuggerDisplay("{AssemblyId} - {TypeName}")]
    public class DumpTypeKey : IEquatable<DumpTypeKey>, IComparable<DumpTypeKey>, IComparable
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DumpTypeKey" /> struct.
        /// </summary>
        /// <param name="assemblyId">The method table.</param>
        /// <param name="typeName">Name of the type.</param>
        public DumpTypeKey(ulong assemblyId, string typeName)
        {
            AssemblyId = assemblyId;
            TypeName = typeName;
        }

        /// <summary>
        ///     Compares to.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>System.Int32.</returns>
        /// <inheritdoc />
        public int CompareTo(DumpTypeKey other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var assemblyIdComparison = AssemblyId.CompareTo(other.AssemblyId);
            if (assemblyIdComparison != 0) return assemblyIdComparison;
            return string.Compare(TypeName, other.TypeName, StringComparison.Ordinal);
        }

        /// <summary>
        ///     Compares to.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="ArgumentException">DumpTypeKey</exception>
        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            if (!(obj is DumpTypeKey)) throw new ArgumentException($"Object must be of type {nameof(DumpTypeKey)}");
            return CompareTo((DumpTypeKey) obj);
        }

        /// <summary>
        ///     Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <inheritdoc />
        public bool Equals(DumpTypeKey other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return AssemblyId == other.AssemblyId && string.Equals(TypeName, other.TypeName);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{TypeName} : {AssemblyId}";
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
            return Equals((DumpTypeKey) obj);
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
                return (AssemblyId.GetHashCode() * 397) ^ (TypeName != null ? TypeName.GetHashCode() : 0);
            }
        }

        /// <summary>
        ///     Implements the &gt; operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >(DumpTypeKey left, DumpTypeKey right) =>
            Comparer<DumpTypeKey>.Default.Compare(left, right) > 0;

        /// <summary>
        ///     Implements the &gt;= operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(DumpTypeKey left, DumpTypeKey right) =>
            Comparer<DumpTypeKey>.Default.Compare(left, right) >= 0;

        /// <summary>
        ///     Implements the &lt; operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(DumpTypeKey left, DumpTypeKey right) =>
            Comparer<DumpTypeKey>.Default.Compare(left, right) < 0;

        /// <summary>
        ///     Implements the &lt;= operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(DumpTypeKey left, DumpTypeKey right) =>
            Comparer<DumpTypeKey>.Default.Compare(left, right) <= 0;

        /// <summary>
        ///     Gets or sets the assembly id to which this type belongs
        /// </summary>
        /// <value>The method table.</value>
        public ulong AssemblyId { get; }

        /// <summary>
        ///     Gets or sets the name of the type.
        /// </summary>
        /// <value>The name of the type.</value>
        public string TypeName { get; }
    }
}