// ***********************************************************************
// Assembly         : Mortician
// Author           : @tysmithnet
// Created          : 12-19-2017
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-02-2018
// ***********************************************************************
// <copyright file="DumpObject.cs" company="">
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
    ///     Represents a managed object on the managed heap
    /// </summary>
    /// <seealso cref="System.IEquatable{Mortician.Core.DumpObject}" />
    /// <seealso cref="System.IComparable{Mortician.Core.DumpObject}" />
    /// <seealso cref="System.IComparable" />
    [DebuggerDisplay("{FullTypeName} : {Size} : {Address}")]
    public class DumpObject : IComparable<DumpObject>, IComparable, IEquatable<DumpObject>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DumpObject" /> class.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="name">The name.</param>
        /// <param name="size">The size.</param>
        /// <param name="gen">The gen.</param>
        public DumpObject(ulong address, string name, ulong size, int gen)
        {
            Address = address;
            FullTypeName = name;
            Size = size;
            Gen = gen;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DumpObject" /> class.
        /// </summary>
        /// <inheritdoc />
        internal DumpObject()
        {
        }

        /// <summary>
        ///     The objects that reference this object
        /// </summary>
        protected internal Dictionary<ulong, DumpObject> ReferencersInternal =
            new Dictionary<ulong, DumpObject>(); // todo: don't use concurrent dictionary.. write happens single threaded and reads happen read only

        /// <summary>
        ///     The references that this object has
        /// </summary>
        protected internal Dictionary<ulong, DumpObject> ReferencesInternal =
            new Dictionary<ulong, DumpObject>();

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
        public int CompareTo(DumpObject other)
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
        /// <exception cref="ArgumentException">DumpObject</exception>
        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            if (!(obj is DumpObject)) throw new ArgumentException($"Object must be of type {nameof(DumpObject)}");
            return CompareTo((DumpObject) obj);
        }

        /// <summary>
        ///     Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <inheritdoc />
        public bool Equals(DumpObject other)
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
            return Equals((DumpObject) obj);
        }

        /// <summary>
        ///     Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        /// <inheritdoc />
        public override int GetHashCode() => Address.GetHashCode();

        /// <summary>
        ///     Get a short description of the object.
        /// </summary>
        /// <returns>A short description of this object</returns>
        /// <remarks>The return value is intended to be shown on a single line</remarks>
        public virtual string ToShortDescription() => $"{FullTypeName} : {Size} : 0x{Address:x16}";

        /// <summary>
        ///     Adds a reference to the list of objects that this object has
        /// </summary>
        /// <param name="obj">The object to add.</param>
        protected internal void AddReference(DumpObject obj)
        {
            if (!ReferencesInternal.ContainsKey(obj.Address))
                ReferencesInternal.Add(obj.Address, obj);
        }

        /// <summary>
        ///     Adds the referencer.
        /// </summary>
        /// <param name="obj">The object.</param>
        protected internal void AddReferencer(DumpObject obj)
        {
            if (!ReferencersInternal.ContainsKey(obj.Address))
                ReferencersInternal.Add(obj.Address, obj);
        }

        /// <summary>
        ///     Implements the &gt; operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >(DumpObject left, DumpObject right) =>
            Comparer<DumpObject>.Default.Compare(left, right) > 0;

        /// <summary>
        ///     Implements the &gt;= operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(DumpObject left, DumpObject right) =>
            Comparer<DumpObject>.Default.Compare(left, right) >= 0;

        /// <summary>
        ///     Implements the &lt; operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(DumpObject left, DumpObject right) =>
            Comparer<DumpObject>.Default.Compare(left, right) < 0;

        /// <summary>
        ///     Implements the &lt;= operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(DumpObject left, DumpObject right) =>
            Comparer<DumpObject>.Default.Compare(left, right) <= 0;

        /// <summary>
        ///     Gets the address of this object
        /// </summary>
        /// <value>The address of this object</value>
        public ulong Address { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether [contains pointers].
        /// </summary>
        /// <value><c>true</c> if [contains pointers]; otherwise, <c>false</c>.</value>
        public bool ContainsPointers { get; set; }

        /// <summary>
        ///     Gets the full name of the type of this object.
        /// </summary>
        /// <value>The full name of the type of this object.</value>
        public string FullTypeName { get; protected internal set; }

        /// <summary>
        ///     Gets the gc generation for this object. 0,1,2,3 where 3 is the large object heap
        /// </summary>
        /// <value>The gc generation for this object</value>
        public int Gen { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is array.
        /// </summary>
        /// <value><c>true</c> if this instance is array; otherwise, <c>false</c>.</value>
        public bool IsArray { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is boxed.
        /// </summary>
        /// <value><c>true</c> if this instance is boxed; otherwise, <c>false</c>.</value>
        public bool IsBoxed { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is finalizable.
        /// </summary>
        /// <value><c>true</c> if this instance is finalizable; otherwise, <c>false</c>.</value>
        public bool IsFinalizable { get; set; }

        /// <summary>
        ///     Gets a value indicating whether this instance is in finalizer queue.
        /// </summary>
        /// <value><c>true</c> if this instance is in finalizer queue; otherwise, <c>false</c>.</value>
        public bool IsInFinalizerQueue { get; internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is managed work item.
        /// </summary>
        /// <value><c>true</c> if this instance is managed work item; otherwise, <c>false</c>.</value>
        public bool IsManagedWorkItem { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is null.
        /// </summary>
        /// <value><c>true</c> if this instance is null; otherwise, <c>false</c>.</value>
        public bool IsNull { get; set; }

        /// <summary>
        ///     Gets the objects that reference this object
        /// </summary>
        /// <value>The referencers.</value>
        public IEnumerable<DumpObject> Referencers => ReferencersInternal.Values;

        /// <summary>
        ///     Gets the references that this object has.
        /// </summary>
        /// <value>The references that this object has.</value>
        public IEnumerable<DumpObject> References => ReferencesInternal.Values;

        /// <summary>
        ///     Gets the size of this object
        ///     Note that this is the type heap for most types, but will be the size of the array in a byte[] for example
        /// </summary>
        /// <value>The size of this object</value>
        public ulong Size { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the type of the object
        /// </summary>
        /// <value>The type of the dump.</value>
        public DumpType Type { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the threads.
        /// </summary>
        /// <value>The threads.</value>
        internal ISet<DumpThread> ThreadsInternal { get; set; } = new SortedSet<DumpThread>();

        public IEnumerable<DumpThread> Threads => ThreadsInternal;
    }
}