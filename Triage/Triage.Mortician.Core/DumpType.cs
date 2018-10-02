// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 12-19-2017
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-02-2018
// ***********************************************************************
// <copyright file="DumpType.cs" company="">
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
    ///     An object that represents a type that was extracted from the memory dump
    /// </summary>
    /// <seealso cref="System.IEquatable{Triage.Mortician.Core.DumpType}" />
    /// <seealso cref="System.IComparable{Triage.Mortician.Core.DumpType}" />
    /// <seealso cref="System.IComparable" />
    public class DumpType : IEquatable<DumpType>, IComparable<DumpType>, IComparable
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DumpType" /> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <exception cref="ArgumentNullException">key</exception>
        /// <inheritdoc />
        public DumpType(DumpTypeKey key)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DumpType" /> class.
        /// </summary>
        internal DumpType()
        {
            Key = new DumpTypeKey(0, null);
        }

        /// <summary>
        ///     The objects of this type
        /// </summary>
        protected internal Dictionary<ulong, DumpObject> ObjectsInternal = new Dictionary<ulong, DumpObject>();

        /// <summary>
        ///     Compares to.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>System.Int32.</returns>
        /// <inheritdoc />
        public int CompareTo(DumpType other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return Comparer<DumpTypeKey>.Default.Compare(Key, other.Key);
        }

        /// <summary>
        ///     Compares to.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="ArgumentException">DumpType</exception>
        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            if (!(obj is DumpType)) throw new ArgumentException($"Object must be of type {nameof(DumpType)}");
            return CompareTo((DumpType) obj);
        }

        /// <summary>
        ///     Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <inheritdoc />
        public bool Equals(DumpType other)
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
            return Equals((DumpType) obj);
        }

        /// <summary>
        ///     Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        /// <inheritdoc />
        public override int GetHashCode() => Key != null ? Key.GetHashCode() : 0;

        /// <summary>
        ///     Implements the &gt; operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >(DumpType left, DumpType right) =>
            Comparer<DumpType>.Default.Compare(left, right) > 0;

        /// <summary>
        ///     Implements the &gt;= operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(DumpType left, DumpType right) =>
            Comparer<DumpType>.Default.Compare(left, right) >= 0;

        /// <summary>
        ///     Implements the &lt; operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(DumpType left, DumpType right) =>
            Comparer<DumpType>.Default.Compare(left, right) < 0;

        /// <summary>
        ///     Implements the &lt;= operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(DumpType left, DumpType right) =>
            Comparer<DumpType>.Default.Compare(left, right) <= 0;

        /// <summary>
        ///     Gets or sets the assembly identifier.
        /// </summary>
        /// <value>The assembly identifier.</value>
        public ulong AssemblyId { get; set; }

        /// <summary>
        ///     Gets or sets the size of the type fields
        /// </summary>
        /// <value>The size of the type fields</value>
        public int BaseSize { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the type of base type
        /// </summary>
        /// <value>The type of the base</value>
        public DumpType BaseType { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the type of the component.
        /// </summary>
        /// <value>The type of the component.</value>
        public DumpType ComponentType { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether [contains pointers].
        /// </summary>
        /// <value><c>true</c> if [contains pointers]; otherwise, <c>false</c>.</value>
        public bool ContainsPointers { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the size of the element.
        /// </summary>
        /// <value>The size of the element.</value>
        public int ElementSize { get; set; }

        /// <summary>
        ///     Gets or sets the type of the element.
        /// </summary>
        /// <value>The type of the element.</value>
        public ClrElementType ElementType { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance has simple value.
        /// </summary>
        /// <value><c>true</c> if this instance has simple value; otherwise, <c>false</c>.</value>
        public bool HasSimpleValue { get; set; }

        /// <summary>
        ///     Gets or sets the inheriting types.
        /// </summary>
        /// <value>The inheriting types.</value>
        public List<DumpType> InheritingTypes { get; set; } = new List<DumpType>();

        /// <summary>
        ///     Gets or sets the instance fields.
        /// </summary>
        /// <value>The instance fields.</value>
        public List<DumpTypeField> InstanceFields { get; set; }

        /// <summary>
        ///     Gets or sets the interfaces.
        /// </summary>
        /// <value>The interfaces.</value>
        public List<string> Interfaces { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is abstract.
        /// </summary>
        /// <value><c>true</c> if this instance is abstract; otherwise, <c>false</c>.</value>
        public bool IsAbstract { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is an array.
        /// </summary>
        /// <value><c>true</c> if this instance is array; otherwise, <c>false</c>.</value>
        public bool IsArray { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is enum.
        /// </summary>
        /// <value><c>true</c> if this instance is enum; otherwise, <c>false</c>.</value>
        public bool IsEnum { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is exception.
        /// </summary>
        /// <value><c>true</c> if this instance is exception; otherwise, <c>false</c>.</value>
        public bool IsException { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is finalizable.
        /// </summary>
        /// <value><c>true</c> if this instance is finalizable; otherwise, <c>false</c>.</value>
        public bool IsFinalizable { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is free.
        /// </summary>
        /// <value><c>true</c> if this instance is free; otherwise, <c>false</c>.</value>
        public bool IsFree { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is an interface.
        /// </summary>
        /// <value><c>true</c> if this instance is interface; otherwise, <c>false</c>.</value>
        public bool IsInterface { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is internal.
        /// </summary>
        /// <value><c>true</c> if this instance is internal; otherwise, <c>false</c>.</value>
        public bool IsInternal { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is object reference.
        /// </summary>
        /// <value><c>true</c> if this instance is object reference; otherwise, <c>false</c>.</value>
        public bool IsObjectReference { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is pointer.
        /// </summary>
        /// <value><c>true</c> if this instance is pointer; otherwise, <c>false</c>.</value>
        public bool IsPointer { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is primitive.
        /// </summary>
        /// <value><c>true</c> if this instance is primitive; otherwise, <c>false</c>.</value>
        public bool IsPrimitive { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is private.
        /// </summary>
        /// <value><c>true</c> if this instance is private; otherwise, <c>false</c>.</value>
        public bool IsPrivate { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is protected.
        /// </summary>
        /// <value><c>true</c> if this instance is protected; otherwise, <c>false</c>.</value>
        public bool IsProtected { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is public.
        /// </summary>
        /// <value><c>true</c> if this instance is public; otherwise, <c>false</c>.</value>
        public bool IsPublic { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is runtime type.
        /// </summary>
        /// <value><c>true</c> if this instance is runtime type; otherwise, <c>false</c>.</value>
        public bool IsRuntimeType { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is sealed.
        /// </summary>
        /// <value><c>true</c> if this instance is sealed; otherwise, <c>false</c>.</value>
        public bool IsSealed { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is string.
        /// </summary>
        /// <value><c>true</c> if this instance is string; otherwise, <c>false</c>.</value>
        public bool IsString { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is value class.
        /// </summary>
        /// <value><c>true</c> if this instance is value class; otherwise, <c>false</c>.</value>
        public bool IsValueClass { get; set; }

        /// <summary>
        ///     Gets or sets the key to uniquely identify this type
        /// </summary>
        /// <value>The dump type key.</value>
        public DumpTypeKey Key { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the meta data token.
        /// </summary>
        /// <value>The meta data token.</value>
        public uint MetaDataToken { get; set; }

        /// <summary>
        ///     Gets or sets the method table.
        /// </summary>
        /// <value>The method table.</value>
        public ulong MethodTable { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the module this type is defined in
        /// </summary>
        /// <value>The module.</value>
        public DumpModule Module { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the objects.
        /// </summary>
        /// <value>The objects.</value>
        public IEnumerable<DumpObject> Objects { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the static fields.
        /// </summary>
        /// <value>The static fields.</value>
        public List<DumpTypeField> StaticFields { get; set; } = new List<DumpTypeField>();
    }
}