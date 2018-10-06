// ***********************************************************************
// Assembly         : Mortician
// Author           : @tysmithnet
// Created          : 12-19-2017
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-02-2018
// ***********************************************************************
// <copyright file="DumpObjectRoot.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using Mortician.Core.ClrMdAbstractions;

namespace Mortician.Core
{
    /// <summary>
    ///     An object that represents a GC root discovered in the memory dump
    ///     A GC root is a reference like entity that keeps an object alive in memory
    /// </summary>
    /// <seealso cref="System.IComparable{Mortician.Core.DumpObjectRoot}" />
    /// <seealso cref="System.IComparable" />
    /// <seealso cref="System.IEquatable{Mortician.Core.DumpObjectRoot}" />
    public class DumpObjectRoot : IComparable<DumpObjectRoot>, IComparable, IEquatable<DumpObjectRoot>
    {
        /// <summary>
        ///     Adds the thread.
        /// </summary>
        /// <param name="thread">The thread.</param>
        public void AddThread(DumpThread thread)
        {
            ThreadsInternal.Add(thread);
        }

        /// <summary>
        ///     Compares to.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>System.Int32.</returns>
        /// <inheritdoc />
        public int CompareTo(DumpObjectRoot other)
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
        /// <exception cref="ArgumentException">DumpObjectRoot</exception>
        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            if (!(obj is DumpObjectRoot))
                throw new ArgumentException($"Object must be of type {nameof(DumpObjectRoot)}");
            return CompareTo((DumpObjectRoot) obj);
        }

        /// <summary>
        ///     Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <inheritdoc />
        public bool Equals(DumpObjectRoot other)
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
            return Equals((DumpObjectRoot) obj);
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
        public static bool operator >(DumpObjectRoot left, DumpObjectRoot right) =>
            Comparer<DumpObjectRoot>.Default.Compare(left, right) > 0;

        /// <summary>
        ///     Implements the &gt;= operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(DumpObjectRoot left, DumpObjectRoot right) =>
            Comparer<DumpObjectRoot>.Default.Compare(left, right) >= 0;

        /// <summary>
        ///     Implements the &lt; operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(DumpObjectRoot left, DumpObjectRoot right) =>
            Comparer<DumpObjectRoot>.Default.Compare(left, right) < 0;

        /// <summary>
        ///     Implements the &lt;= operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(DumpObjectRoot left, DumpObjectRoot right) =>
            Comparer<DumpObjectRoot>.Default.Compare(left, right) <= 0;

        /// <summary>
        ///     Gets or sets the address of this object root
        /// </summary>
        /// <value>The address.</value>
        public ulong Address { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the app domain where this root exists if it is associated with an app domain, null otherwise
        /// </summary>
        /// <value>The application domain.</value>
        public DumpAppDomain AppDomain { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the kind of the gc root.
        /// </summary>
        /// <value>The kind of the gc root.</value>
        public GcRootKind GcRootKind { get; set; } // todo: all of these need to be restricted

        /// <summary>
        ///     Gets or sets a value indicating whether this object root points not to the
        ///     object header, but to some location inside the object
        /// </summary>
        /// <value><c>true</c> if this instance is interior pointer; otherwise, <c>false</c>.</value>
        public bool IsInteriorPointer { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the object being kept in memory is
        ///     preventing from being relocated during the compaction phase of GC
        /// </summary>
        /// <value><c>true</c> if this instance is pinned; otherwise, <c>false</c>.</value>
        public bool IsPinned { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is possible false positive.
        ///     This comes from the CLRMd engine that uses heuristics to determine if the object
        ///     being kept alive can in fact be garbage collected
        /// </summary>
        /// <value><c>true</c> if this instance is possible false positive; otherwise, <c>false</c>.</value>
        public bool IsPossibleFalsePositive { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the name of this object root
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the managed to which this root points if possible, null otherwise
        /// </summary>
        /// <value>The rooted object.</value>
        public DumpObject RootedObject { get; protected internal set; }

        /// <summary>
        ///     Gets the stack frame that is keeping where this root can be found
        /// </summary>
        /// <value>The stack frame.</value>
        /// <exception cref="NotImplementedException">You still need to implement stack frame in object root</exception>
        public DumpStackFrame StackFrame { get; protected internal set; }

        /// <summary>
        ///     Gets the threads.
        /// </summary>
        /// <value>The threads.</value>
        public IEnumerable<DumpThread> Threads => ThreadsInternal;

        /// <summary>
        ///     Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public DumpType Type { get; set; }

        /// <summary>
        ///     Gets or sets the threads.
        /// </summary>
        /// <value>The threads.</value>
        internal ISet<DumpThread> ThreadsInternal { get; set; } = new SortedSet<DumpThread>();
    }
}