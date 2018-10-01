// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 12-19-2017
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="DumpType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Core
{
    /// <summary>
    ///     An object that represents a type that was extracted from the memory dump
    /// </summary>
    public class DumpType
    {
        /// <summary>
        ///     The objects of this type
        /// </summary>
        protected internal Dictionary<ulong, DumpObject> ObjectsInternal = new Dictionary<ulong, DumpObject>();

        /// <summary>
        ///     Gets or sets the type of base type
        /// </summary>
        /// <value>The type of the base</value>
        public DumpType BaseType { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the size of the type fields
        /// </summary>
        /// <value>The size of the type fields</value>
        public int BaseSize { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether [contains pointers].
        /// </summary>
        /// <value><c>true</c> if [contains pointers]; otherwise, <c>false</c>.</value>
        public bool ContainsPointers { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the key to uniquely identify this type
        /// </summary>
        /// <value>The dump type key.</value>
        public DumpTypeKey Key { get; protected internal set; }

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

        public bool IsPublic { get; set; }
        public ulong AssemblyId { get; set; }
        public uint MetaDataToken { get; set; }
        public bool IsValueClass { get; set; }
        public bool IsObjectReference { get; set; }
        public bool IsFree { get; set; }
        public bool HasSimpleValue { get; set; }
        public int ElementSize { get; set; }
        public ClrElementType ElementType { get; set; }
        public List<DumpTypeField> InstanceFields { get; set; }
        public List<DumpType> InheritingTypes { get; set; } = new List<DumpType>();
        public DumpType ComponentType { get; set; }
        public List<string> Interfaces { get; set; }
        public List<DumpTypeField> StaticFields { get; set; } = new List<DumpTypeField>();
    }

    public class DumpHeapSegment
    {
        public ulong CommittedEnd { get; set; }
        public ulong End { get; set; }
        public ulong FirstObject { get; set; }
        public ulong Gen0Length { get; set; }
        public ulong Gen0Start { get; set; }
        public ulong Gen1Length { get; set; }
        public ulong Gen1Start { get; set; }
        public ulong Gen2Length { get; set; }
        public ulong Gen2Start { get; set; }
        public IClrHeap Heap { get; set; }
        public bool IsEphemeral { get; set; }
        public bool IsLarge { get; set; }
        public ulong Length { get; set; }
        public int ProcessorAffinity { get; set; }
        public ulong ReservedEnd { get; set; }
        public ulong Start { get; set; }
    }

    public class DumpMemoryRegion
    {
        public ulong Address { get; set; }
        public GcSegmentType GcSegmentType { get; set; }
        public int HeapNumber { get; set; }
        public ClrMemoryRegionType MemoryRegionType { get; set; }
        public ulong Size { get; set; }
    }

    public class DumpTypeField
    {
        public bool HasSimpleValue { get; set; }
        public bool IsInternal { get; set; }
        public bool IsObjectReference { get; set; }
        public bool IsPrimitive { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsProtected { get; set; }
        public bool IsPublic { get; set; }
        public bool IsValueClass { get; set; }
        public string Name { get; set; }
        public int Offset { get; set; }
        public int Size { get; set; }
        public uint Token { get; set; }
        public ClrElementType ElementType { get; set; }
        public DumpType Type { get; set; }
    }
}