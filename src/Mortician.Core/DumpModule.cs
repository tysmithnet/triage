// ***********************************************************************
// Assembly         : Mortician.Core
// Author           : @tysmithnet
// Created          : 09-24-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-02-2018
// ***********************************************************************
// <copyright file="DumpModule.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Mortician.Core.ClrMdAbstractions;

namespace Mortician.Core
{
    /// <summary>
    ///     A module that was discovered in the memory dump
    /// </summary>
    /// <seealso cref="System.IEquatable{Mortician.Core.DumpModule}" />
    /// <seealso cref="System.IComparable{Mortician.Core.DumpModule}" />
    /// <seealso cref="System.IComparable" />
    public class DumpModule : IEquatable<DumpModule>, IComparable<DumpModule>, IComparable
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DumpModule" /> class.
        /// </summary>
        /// <param name="dumpModuleKey">The dump module key.</param>
        /// <exception cref="ArgumentNullException">dumpModuleKey</exception>
        public DumpModule(DumpModuleKey dumpModuleKey)
        {
            Key = dumpModuleKey ?? throw new ArgumentNullException(nameof(dumpModuleKey));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DumpModule" /> class.
        /// </summary>
        internal DumpModule()
        {
        }

        /// <summary>
        ///     The app domains for which this module is loaded
        /// </summary>
        internal ISet<DumpAppDomain> AppDomainsInternal = new SortedSet<DumpAppDomain>();

        /// <summary>
        ///     The types defined in this module
        /// </summary>
        internal ISet<DumpType> TypesInternal = new SortedSet<DumpType>();

        /// <summary>
        ///     Compares to.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>System.Int32.</returns>
        /// <inheritdoc />
        public int CompareTo(DumpModule other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return Comparer<DumpModuleKey>.Default.Compare(Key, other.Key);
        }

        /// <summary>
        ///     Compares to.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="ArgumentException">DumpModule</exception>
        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            if (!(obj is DumpModule)) throw new ArgumentException($"Object must be of type {nameof(DumpModule)}");
            return CompareTo((DumpModule) obj);
        }

        /// <summary>
        ///     Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <inheritdoc />
        public bool Equals(DumpModule other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Key, other.Key);
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
            return Equals((DumpModule) obj);
        }

        /// <summary>
        ///     Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        /// <inheritdoc />
        public override int GetHashCode() => Key != null ? Key.GetHashCode() : 0;

        /// <summary>
        ///     Adds the application domain.
        /// </summary>
        /// <param name="domain">The domain.</param>
        internal void AddAppDomain(DumpAppDomain domain)
        {
            AppDomainsInternal.Add(domain);
        }

        /// <summary>
        ///     Adds the type.
        /// </summary>
        /// <param name="type">The type.</param>
        internal void AddType(DumpType type)
        {
            TypesInternal.Add(type);
        }

        /// <summary>
        ///     Implements the &gt; operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >(DumpModule left, DumpModule right) =>
            Comparer<DumpModule>.Default.Compare(left, right) > 0;

        /// <summary>
        ///     Implements the &gt;= operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(DumpModule left, DumpModule right) =>
            Comparer<DumpModule>.Default.Compare(left, right) >= 0;

        /// <summary>
        ///     Implements the &lt; operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(DumpModule left, DumpModule right) =>
            Comparer<DumpModule>.Default.Compare(left, right) < 0;

        /// <summary>
        ///     Implements the &lt;= operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(DumpModule left, DumpModule right) =>
            Comparer<DumpModule>.Default.Compare(left, right) <= 0;

        /// <summary>
        ///     Gets or sets the application domains.
        /// </summary>
        /// <value>The application domains.</value>
        public IEnumerable<DumpAppDomain> AppDomains => AppDomainsInternal;

        /// <summary>
        ///     Gets the assembly identifier.
        /// </summary>
        /// <value>The assembly identifier.</value>
        public ulong AssemblyId => Key.AssemblyId;

        /// <summary>
        ///     Gets or sets the debugging mode for this assembly (edit and continue, etc)
        /// </summary>
        /// <value>The debugging mode.</value>
        public DebuggableAttribute.DebuggingModes DebuggingMode { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the name of the file if this module is backed by a physical file, null otherwise
        /// </summary>
        /// <value>The name of the file.</value>
        public string FileName { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the address for which this module is loaded in memory, but can be null if
        ///     it not backed by phsical memory
        /// </summary>
        /// <value>The image base.</value>
        public ulong ImageBase { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this module is a dynamic module
        /// </summary>
        /// <value><c>true</c> if this instance is dynamic; otherwise, <c>false</c>.</value>
        public bool IsDynamic { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this module comes from a file
        /// </summary>
        /// <value><c>true</c> if this modules comes from a file; otherwise, <c>false</c>.</value>
        public bool IsFile { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        public DumpModuleKey Key { get; set; }

        /// <summary>
        ///     Gets or sets the name of this module
        /// </summary>
        /// <value>The name.</value>
        public string Name => Key.Name;

        /// <summary>
        ///     Gets or sets the PDB guid if available
        /// </summary>
        /// <value>The PDB unique identifier.</value>
        public IPdbInfo PdbInfo { get; set; }

        /// <summary>
        ///     Gets or sets the size of this module
        /// </summary>
        /// <value>The size.</value>
        public ulong Size { get; protected internal set; }

        /// <summary>
        ///     Gets the types.
        /// </summary>
        /// <value>The types.</value>
        public IEnumerable<DumpType> Types => TypesInternal;
    }
}