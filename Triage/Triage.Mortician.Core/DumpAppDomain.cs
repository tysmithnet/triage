// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-02-2018
// ***********************************************************************
// <copyright file="DumpAppDomain.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;

namespace Triage.Mortician.Core
{
    /// <summary>
    ///     Represents an AppDomain that was found in the memory dump
    /// </summary>
    /// <seealso cref="System.IEquatable{Triage.Mortician.Core.DumpAppDomain}" />
    /// <seealso cref="System.IComparable{Triage.Mortician.Core.DumpAppDomain}" />
    /// <seealso cref="System.IComparable" />
    public class DumpAppDomain : IEquatable<DumpAppDomain>, IComparable<DumpAppDomain>, IComparable
    {
        /// <summary>
        ///     The modules that have been loaded into the app domain.
        ///     Usually there is a 1:1 relationship between modules and assemblies, but
        ///     there are numerous cases where the assembly has multiple modules. This is
        ///     the mutable internal version
        /// </summary>
        protected internal List<DumpModule> LoadedModulesInternal = new List<DumpModule>();

        /// <summary>
        ///     The handles
        /// </summary>
        internal ISet<DumpHandle> HandlesInternal = new SortedSet<DumpHandle>();

        /// <summary>
        ///     Adds the handle.
        /// </summary>
        /// <param name="handle">The handle.</param>
        public void AddHandle(DumpHandle handle)
        {
            HandlesInternal.Add(handle);
        }

        /// <summary>
        ///     Adds the module.
        /// </summary>
        /// <param name="module">The module.</param>
        public void AddModule(DumpModule module)
        {
            LoadedModulesInternal.Add(module);
        }

        /// <summary>
        ///     Compares to.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>System.Int32.</returns>
        /// <inheritdoc />
        public int CompareTo(DumpAppDomain other)
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
        /// <exception cref="ArgumentException">DumpAppDomain</exception>
        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            if (!(obj is DumpAppDomain)) throw new ArgumentException($"Object must be of type {nameof(DumpAppDomain)}");
            return CompareTo((DumpAppDomain) obj);
        }

        /// <summary>
        ///     Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <inheritdoc />
        public bool Equals(DumpAppDomain other)
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
            return Equals((DumpAppDomain) obj);
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
        public static bool operator >(DumpAppDomain left, DumpAppDomain right) =>
            Comparer<DumpAppDomain>.Default.Compare(left, right) > 0;

        /// <summary>
        ///     Implements the &gt;= operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(DumpAppDomain left, DumpAppDomain right) =>
            Comparer<DumpAppDomain>.Default.Compare(left, right) >= 0;

        /// <summary>
        ///     Implements the &lt; operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(DumpAppDomain left, DumpAppDomain right) =>
            Comparer<DumpAppDomain>.Default.Compare(left, right) < 0;

        /// <summary>
        ///     Implements the &lt;= operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(DumpAppDomain left, DumpAppDomain right) =>
            Comparer<DumpAppDomain>.Default.Compare(left, right) <= 0;

        /// <summary>
        ///     Gets or sets the address of where the app domain has been loaded in memory
        /// </summary>
        /// <value>The address.</value>
        public ulong Address { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the application base if one exists e.g. /LM/W3SVC/2/ROOT-1-13157282501912414
        /// </summary>
        /// <value>The application base.</value>
        public string ApplicationBase { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the configuration file for this module
        /// </summary>
        /// <value>The configuration file.</value>
        public string ConfigFile { get; protected internal set; }

        /// <summary>
        ///     Gets the handles.
        /// </summary>
        /// <value>The handles.</value>
        public IEnumerable<DumpHandle> Handles => HandlesInternal;

        /// <summary>
        ///     Gets or sets the loaded modules.
        /// </summary>
        /// <value>The loaded modules.</value>
        public IEnumerable<DumpModule> LoadedModules => LoadedModulesInternal;

        /// <summary>
        ///     Gets or sets the name of this module e.g. System.Net
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; protected internal set; }
    }
}