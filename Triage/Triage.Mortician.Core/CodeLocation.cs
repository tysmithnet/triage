// ***********************************************************************
// Assembly         : Triage.Mortician.Core
// Author           : @tysmithnet
// Created          : 09-24-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-02-2018
// ***********************************************************************
// <copyright file="CodeLocation.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;

namespace Triage.Mortician.Core
{
    /// <summary>
    ///     Class CodeLocation.
    /// </summary>
    /// <seealso cref="System.IEquatable{Triage.Mortician.Core.CodeLocation}" />
    /// <seealso cref="System.IComparable{Triage.Mortician.Core.CodeLocation}" />
    /// <seealso cref="System.IComparable" />
    public class CodeLocation : IEquatable<CodeLocation>, IComparable<CodeLocation>, IComparable
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CodeLocation" /> class.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <param name="method">The method.</param>
        /// <param name="offset">The offset.</param>
        /// <inheritdoc />
        public CodeLocation(string module, string method, ulong offset)
        {
            Module = module;
            Method = method;
            Offset = offset;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CodeLocation" /> class.
        /// </summary>
        internal CodeLocation()
        {
        }

        /// <summary>
        ///     Compares to.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>System.Int32.</returns>
        /// <inheritdoc />
        public int CompareTo(CodeLocation other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var methodComparison = string.Compare(Method, other.Method, StringComparison.Ordinal);
            if (methodComparison != 0) return methodComparison;
            var moduleComparison = string.Compare(Module, other.Module, StringComparison.Ordinal);
            if (moduleComparison != 0) return moduleComparison;
            return Offset.CompareTo(other.Offset);
        }

        /// <summary>
        ///     Compares to.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="ArgumentException">CodeLocation</exception>
        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            if (!(obj is CodeLocation)) throw new ArgumentException($"Object must be of type {nameof(CodeLocation)}");
            return CompareTo((CodeLocation) obj);
        }

        /// <summary>
        ///     Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <inheritdoc />
        public bool Equals(CodeLocation other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Method, other.Method) && string.Equals(Module, other.Module) && Offset == other.Offset;
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
            return Equals((CodeLocation) obj);
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
                var hashCode = Method != null ? Method.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (Module != null ? Module.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Offset.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        ///     Implements the &gt; operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >(CodeLocation left, CodeLocation right) =>
            Comparer<CodeLocation>.Default.Compare(left, right) > 0;

        /// <summary>
        ///     Implements the &gt;= operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(CodeLocation left, CodeLocation right) =>
            Comparer<CodeLocation>.Default.Compare(left, right) >= 0;

        /// <summary>
        ///     Implements the &lt; operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(CodeLocation left, CodeLocation right) =>
            Comparer<CodeLocation>.Default.Compare(left, right) < 0;

        /// <summary>
        ///     Implements the &lt;= operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(CodeLocation left, CodeLocation right) =>
            Comparer<CodeLocation>.Default.Compare(left, right) <= 0;

        /// <summary>
        ///     Gets the method.
        /// </summary>
        /// <value>The method.</value>
        public string Method { get; internal set; }

        /// <summary>
        ///     Gets the module.
        /// </summary>
        /// <value>The module.</value>
        public string Module { get; internal set; }

        /// <summary>
        ///     Gets the offset.
        /// </summary>
        /// <value>The offset.</value>
        public ulong Offset { get; internal set; }
    }
}