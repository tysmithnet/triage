using System.Collections.Generic;

namespace Triage.Mortician.Domain
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
        /// <value>
        ///     The type of the base
        /// </value>
        public DumpType BaseDumpType { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the size of the type fields
        /// </summary>
        /// <value>
        ///     The size of the type fields
        /// </value>
        public int BaseSize { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether [contains pointers].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [contains pointers]; otherwise, <c>false</c>.
        /// </value>
        public bool ContainsPointers { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the key to uniquely identify this type
        /// </summary>
        /// <value>
        ///     The dump type key.
        /// </value>
        public DumpTypeKey DumpTypeKey { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is abstract.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is abstract; otherwise, <c>false</c>.
        /// </value>
        public bool IsAbstract { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is an array.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is array; otherwise, <c>false</c>.
        /// </value>
        public bool IsArray { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is enum.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is enum; otherwise, <c>false</c>.
        /// </value>
        public bool IsEnum { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is exception.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is exception; otherwise, <c>false</c>.
        /// </value>
        public bool IsException { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is finalizable.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is finalizable; otherwise, <c>false</c>.
        /// </value>
        public bool IsFinalizable { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is an interface.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is interface; otherwise, <c>false</c>.
        /// </value>
        public bool IsInterface { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is internal.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is internal; otherwise, <c>false</c>.
        /// </value>
        public bool IsInternal { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is pointer.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is pointer; otherwise, <c>false</c>.
        /// </value>
        public bool IsPointer { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is primitive.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is primitive; otherwise, <c>false</c>.
        /// </value>
        public bool IsPrimitive { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is private.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is private; otherwise, <c>false</c>.
        /// </value>
        public bool IsPrivate { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is protected.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is protected; otherwise, <c>false</c>.
        /// </value>
        public bool IsProtected { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is runtime type.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is runtime type; otherwise, <c>false</c>.
        /// </value>
        public bool IsRuntimeType { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is sealed.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is sealed; otherwise, <c>false</c>.
        /// </value>
        public bool IsSealed { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is string.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is string; otherwise, <c>false</c>.
        /// </value>
        public bool IsString { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the method table.
        /// </summary>
        /// <value>
        ///     The method table.
        /// </value>
        public ulong MethodTable { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the module this type is defined in
        /// </summary>
        /// <value>
        ///     The module.
        /// </value>
        public DumpModule Module { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public string Name { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the objects.
        /// </summary>
        /// <value>
        ///     The objects.
        /// </value>
        public IEnumerable<DumpObject> Objects { get; protected internal set; }

        // todo: add methods
    }
}