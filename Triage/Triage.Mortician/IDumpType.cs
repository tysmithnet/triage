using System.Collections.Generic;

namespace Triage.Mortician
{
    /// <summary>
    ///     Represents a managed type that was found in the dump
    /// </summary>
    public interface IDumpType
    {
        /// <summary>
        ///     Gets the base size of the type
        /// </summary>
        /// <value>
        ///     The base size of the type
        /// </value>
        int BaseSize { get; }

        /// <summary>
        ///     Gets the base type if this type inherits another type
        /// </summary>
        /// <value>
        ///     The type of the base.
        /// </value>
        IDumpType BaseDumpType { get; }

        /// <summary>
        ///     Gets the module that this type is defined in
        /// </summary>
        /// <value>
        ///     The module that this type is defined in
        /// </value>
        IModule Module { get; }

        /// <summary>
        ///     Gets the interfaces this type implements
        /// </summary>
        /// <value>
        ///     The interfaces this type implements
        /// </value>
        IEnumerable<IDumpType> Interfaces { get; }

        /// <summary>
        ///     Gets a value indicating whether this type is abstract.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this type is abstract; otherwise, <c>false</c>.
        /// </value>
        bool IsAbstract { get; }

        /// <summary>
        ///     Gets a value indicating if this type is an interface
        /// </summary>
        /// <value>
        ///     <c>true</c> if this type is an interface; otherwise, <c>false</c>.
        /// </value>
        bool IsInterface { get; }

        /// <summary>
        ///     Gets a value indicating whether this type is an array type.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this type is an array; otherwise, <c>false</c>.
        /// </value>
        bool IsArray { get; }

        /// <summary>
        ///     Gets a value indicating whether this type is an enum
        /// </summary>
        /// <value>
        ///     <c>true</c> if this type is an enum; otherwise, <c>false</c>.
        /// </value>
        bool IsEnum { get; }

        /// <summary>
        ///     Gets a value indicating whether this type contains pointers (unsafe code).
        /// </summary>
        /// <value>
        ///     <c>true</c> if this type contains pointers; otherwise, <c>false</c>.
        /// </value>
        bool ContainsPointers { get; }

        /// <summary>
        ///     Gets a value indicating whether this type is an Exception
        /// </summary>
        /// <value>
        ///     <c>true</c> if this type is an exception; otherwise, <c>false</c>.
        /// </value>
        bool IsException { get; }

        /// <summary>
        ///     Gets a value indicating whether this type is a finalisable type
        /// </summary>
        /// <value>
        ///     <c>true</c> if this type is a finalizable type; otherwise, <c>false</c>.
        /// </value>
        bool IsFinalizable { get; }

        /// <summary>
        ///     Gets a value indicating whether this type is internal
        /// </summary>
        /// <value>
        ///     <c>true</c> if this type is internal; otherwise, <c>false</c>.
        /// </value>
        bool IsInternal { get; }

        /// <summary>
        ///     Gets a value indicating whether this type is castable to System.Type
        /// </summary>
        /// <value>
        ///     <c>true</c> if this type is a runtime type; otherwise, <c>false</c>.
        /// </value>
        bool IsRuntimeType { get; }

        /// <summary>
        ///     Gets a value indicating whether this type is protected
        /// </summary>
        /// <value>
        ///     <c>true</c> if this type is protected; otherwise, <c>false</c>.
        /// </value>
        bool IsProtected { get; }

        /// <summary>
        ///     Gets a value indicating whether this type is private
        /// </summary>
        /// <value>
        ///     <c>true</c> if this type is private; otherwise, <c>false</c>.
        /// </value>
        bool IsPrivate { get; }

        /// <summary>
        ///     Gets a value indicating whether this type is a pointer
        /// </summary>
        /// <value>
        ///     <c>true</c> if this type is pointer; otherwise, <c>false</c>.
        /// </value>
        bool IsPointer { get; }

        /// <summary>
        ///     Gets a value indicating whether this type is sealed
        /// </summary>
        /// <value>
        ///     <c>true</c> if this type is sealed; otherwise, <c>false</c>.
        /// </value>
        bool IsSealed { get; }

        /// <summary>
        ///     Gets a value indicating whether this type is a primitive type (int, float, etc)
        /// </summary>
        /// <value>
        ///     <c>true</c> if this type is a primitive type; otherwise, <c>false</c>.
        /// </value>
        bool IsPrimitive { get; }

        /// <summary>
        ///     Gets a value indicating whether this type is System.String
        /// </summary>
        /// <value>
        ///     <c>true</c> if this type is System.String; otherwise, <c>false</c>.
        /// </value>
        bool IsString { get; }

        /// <summary>
        ///     Gets all objects of this type
        /// </summary>
        /// <value>
        ///     The objects of this type
        /// </value>
        IEnumerable<IDumpObject> Objects { get; }
    }
}