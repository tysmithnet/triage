// ***********************************************************************
// Assembly         : Triage.Mortician.Core
// Author           : @tysmithnet
// Created          : 09-29-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-01-2018
// ***********************************************************************
// <copyright file="DumpModuleKey.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace Triage.Mortician.Core
{
    /// <summary>
    ///     Class DumpModuleKey.
    /// </summary>
    /// <seealso cref="System.IEquatable{Triage.Mortician.Core.DumpModuleKey}" />
    public class DumpModuleKey : IEquatable<DumpModuleKey>
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