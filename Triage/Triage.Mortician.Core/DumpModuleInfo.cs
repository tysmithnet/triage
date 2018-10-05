// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 10-02-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-02-2018
// ***********************************************************************
// <copyright file="DumpModuleInfo.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Core
{
    /// <summary>
    ///     Class DumpModuleInfo.
    /// </summary>
    /// <seealso cref="System.IEquatable{Triage.Mortician.Core.DumpModuleInfo}" />
    /// <seealso cref="System.IComparable{Triage.Mortician.Core.DumpModuleInfo}" />
    /// <seealso cref="System.IComparable" />
    /// <seealso cref="System.IEquatable{Triage.Mortician.DumpModuleInfo}" />
    public class DumpModuleInfo : IEquatable<DumpModuleInfo>, IComparable<DumpModuleInfo>, IComparable
    {
        /// <summary>
        ///     Compares to.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>System.Int32.</returns>
        /// <inheritdoc />
        public int CompareTo(DumpModuleInfo other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return string.Compare(FileName, other.FileName, StringComparison.Ordinal);
        }

        /// <summary>
        ///     Compares to.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="ArgumentException">DumpModuleInfo</exception>
        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            if (!(obj is DumpModuleInfo))
                throw new ArgumentException($"Object must be of type {nameof(DumpModuleInfo)}");
            return CompareTo((DumpModuleInfo) obj);
        }

        /// <summary>
        ///     Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <inheritdoc />
        public bool Equals(DumpModuleInfo other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(FileName, other.FileName) && FileSize == other.FileSize &&
                   ImageBase == other.ImageBase && IsManaged == other.IsManaged && IsRuntime == other.IsRuntime &&
                   Equals(Pdb, other.Pdb) && Equals(PeFile, other.PeFile) && TimeStamp == other.TimeStamp &&
                   Version.Equals(other.Version);
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
            return Equals((DumpModuleInfo) obj);
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
                var hashCode = FileName != null ? FileName.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (int) FileSize;
                hashCode = (hashCode * 397) ^ ImageBase.GetHashCode();
                hashCode = (hashCode * 397) ^ IsManaged.GetHashCode();
                hashCode = (hashCode * 397) ^ IsRuntime.GetHashCode();
                hashCode = (hashCode * 397) ^ (Pdb != null ? Pdb.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (PeFile != null ? PeFile.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int) TimeStamp;
                hashCode = (hashCode * 397) ^ Version.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        ///     Implements the &gt; operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >(DumpModuleInfo left, DumpModuleInfo right) =>
            Comparer<DumpModuleInfo>.Default.Compare(left, right) > 0;

        /// <summary>
        ///     Implements the &gt;= operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(DumpModuleInfo left, DumpModuleInfo right) =>
            Comparer<DumpModuleInfo>.Default.Compare(left, right) >= 0;

        /// <summary>
        ///     Implements the &lt; operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(DumpModuleInfo left, DumpModuleInfo right) =>
            Comparer<DumpModuleInfo>.Default.Compare(left, right) < 0;

        /// <summary>
        ///     Implements the &lt;= operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(DumpModuleInfo left, DumpModuleInfo right) =>
            Comparer<DumpModuleInfo>.Default.Compare(left, right) <= 0;

        /// <summary>
        ///     Gets or sets the name of the file.
        /// </summary>
        /// <value>The name of the file.</value>
        public string FileName { get; set; }

        /// <summary>
        ///     Gets or sets the size of the file.
        /// </summary>
        /// <value>The size of the file.</value>
        public uint FileSize { get; set; }

        /// <summary>
        ///     Gets or sets the image base.
        /// </summary>
        /// <value>The image base.</value>
        public ulong ImageBase { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is managed.
        /// </summary>
        /// <value><c>true</c> if this instance is managed; otherwise, <c>false</c>.</value>
        public bool IsManaged { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is runtime.
        /// </summary>
        /// <value><c>true</c> if this instance is runtime; otherwise, <c>false</c>.</value>
        public bool IsRuntime { get; set; }

        /// <summary>
        ///     Gets or sets the PDB.
        /// </summary>
        /// <value>The PDB.</value>
        public IPdbInfo Pdb { get; set; }

        /// <summary>
        ///     Gets or sets the pe file.
        /// </summary>
        /// <value>The pe file.</value>
        public IPeFile PeFile { get; set; }

        /// <summary>
        ///     Gets or sets the time stamp.
        /// </summary>
        /// <value>The time stamp.</value>
        public uint TimeStamp { get; set; }

        /// <summary>
        ///     Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        public VersionInfo Version { get; set; }

    }
}