// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 12-19-2017
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-01-2018
// ***********************************************************************
// <copyright file="DumpTypeKey.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace Triage.Mortician.Core
{
    /// <summary>
    ///     Type used to represent a unique type extracted from the memory dump
    ///     The method table alone is not sufficent to identify a type because generics
    ///     can share the same method table
    /// </summary>
    /// <seealso cref="System.IEquatable{Triage.Mortician.Core.DumpTypeKey}" />
    public class DumpTypeKey : IEquatable<DumpTypeKey>
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
        ///     Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <inheritdoc />
        public bool Equals(DumpTypeKey other) =>
            AssemblyId == other.AssemblyId && string.Equals(TypeName, other.TypeName);

        /// <summary>
        ///     Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is DumpTypeKey && Equals((DumpTypeKey) obj);
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