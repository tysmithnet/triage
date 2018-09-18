// ***********************************************************************
// Assembly         : Triage.Mortician.Core
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="IClrType.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;

namespace Triage.Mortician.Core.ClrMdAbstractions
{
    /// <summary>
    /// Interface IClrType
    /// </summary>
    public interface IClrType
    {
        /// <summary>
        /// Retrieves the first type handle in EnumerateMethodTables().  MethodTables
        /// are unique to an AppDomain/Type pair, so when there are multiple domains
        /// there will be multiple MethodTable for a class.
        /// </summary>
        /// <value>The method table.</value>
        ulong MethodTable { get; }

        /// <summary>
        /// Returns the metadata token of this type.
        /// </summary>
        /// <value>The metadata token.</value>
        uint MetadataToken { get; }

        /// <summary>
        /// Types have names.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }

        /// <summary>
        /// Returns true if the type CAN contain references to other objects.  This is used in optimizations
        /// and 'true' can always be returned safely.
        /// </summary>
        /// <value><c>true</c> if [contains pointers]; otherwise, <c>false</c>.</value>
        bool ContainsPointers { get; }

        /// <summary>
        /// All types know the heap they belong to.
        /// </summary>
        /// <value>The heap.</value>
        IClrHeap Heap { get; }

        /// <summary>
        /// Returns true if this object is a 'RuntimeType' (that is, the concrete System.RuntimeType class
        /// which is what you get when calling "typeof" in C#).
        /// </summary>
        /// <value><c>true</c> if this instance is runtime type; otherwise, <c>false</c>.</value>
        bool IsRuntimeType { get; }

        /// <summary>
        /// Returns the module this type is defined in.
        /// </summary>
        /// <value>The module.</value>
        IClrModule Module { get; }

        /// <summary>
        /// Returns the ElementType of this Type.  Can return ELEMENT_TYPE_VOID on error.
        /// </summary>
        /// <value>The type of the element.</value>
        ClrElementType ElementType { get; }

        /// <summary>
        /// Returns true if this type is a primitive (int, float, etc), false otherwise.
        /// </summary>
        /// <value><c>true</c> if this instance is primitive; otherwise, <c>false</c>.</value>
        bool IsPrimitive { get; }

        /// <summary>
        /// Returns true if this type is a ValueClass (struct), false otherwise.
        /// </summary>
        /// <value><c>true</c> if this instance is value class; otherwise, <c>false</c>.</value>
        bool IsValueClass { get; }

        /// <summary>
        /// Returns true if this type is an object reference, false otherwise.
        /// </summary>
        /// <value><c>true</c> if this instance is object reference; otherwise, <c>false</c>.</value>
        bool IsObjectReference { get; }

        /// <summary>
        /// Returns the list of interfaces this type implements.
        /// </summary>
        /// <value>The interfaces.</value>
        IList<IClrInterface> Interfaces { get; }

        /// <summary>
        /// Returns whether objects of this type are finalizable.
        /// </summary>
        /// <value><c>true</c> if this instance is finalizable; otherwise, <c>false</c>.</value>
        bool IsFinalizable { get; }

        /// <summary>
        /// Returns true if this type is marked Public.
        /// </summary>
        /// <value><c>true</c> if this instance is public; otherwise, <c>false</c>.</value>
        bool IsPublic { get; }

        /// <summary>
        /// returns true if this type is marked Private.
        /// </summary>
        /// <value><c>true</c> if this instance is private; otherwise, <c>false</c>.</value>
        bool IsPrivate { get; }

        /// <summary>
        /// Returns true if this type is accessable only by items in its own assembly.
        /// </summary>
        /// <value><c>true</c> if this instance is internal; otherwise, <c>false</c>.</value>
        bool IsInternal { get; }

        /// <summary>
        /// Returns true if this nested type is accessable only by subtypes of its outer type.
        /// </summary>
        /// <value><c>true</c> if this instance is protected; otherwise, <c>false</c>.</value>
        bool IsProtected { get; }

        /// <summary>
        /// Returns true if this class is abstract.
        /// </summary>
        /// <value><c>true</c> if this instance is abstract; otherwise, <c>false</c>.</value>
        bool IsAbstract { get; }

        /// <summary>
        /// Returns true if this class is sealed.
        /// </summary>
        /// <value><c>true</c> if this instance is sealed; otherwise, <c>false</c>.</value>
        bool IsSealed { get; }

        /// <summary>
        /// Returns true if this type is an interface.
        /// </summary>
        /// <value><c>true</c> if this instance is interface; otherwise, <c>false</c>.</value>
        bool IsInterface { get; }

        /// <summary>
        /// Returns all possible fields in this type.   It does not return dynamically typed fields.
        /// Returns an empty list if there are no fields.
        /// </summary>
        /// <value>The fields.</value>
        IList<IClrInstanceField> Fields { get; }

        /// <summary>
        /// Returns a list of static fields on this type.  Returns an empty list if there are no fields.
        /// </summary>
        /// <value>The static fields.</value>
        IList<IClrStaticField> StaticFields { get; }

        /// <summary>
        /// Returns a list of thread static fields on this type.  Returns an empty list if there are no fields.
        /// </summary>
        /// <value>The thread static fields.</value>
        IList<IClrThreadStaticField> ThreadStaticFields { get; }

        /// <summary>
        /// Gets the list of methods this type implements.
        /// </summary>
        /// <value>The methods.</value>
        IList<IClrMethod> Methods { get; }

        /// <summary>
        /// If this type inherits from another type, this is that type.  Can return null if it does not inherit (or is unknown)
        /// </summary>
        /// <value>The type of the base.</value>
        IClrType BaseType { get; }

        /// <summary>
        /// Indicates if the type is in fact a pointer. If so, the pointer operators
        /// may be used.
        /// </summary>
        /// <value><c>true</c> if this instance is pointer; otherwise, <c>false</c>.</value>
        bool IsPointer { get; }

        /// <summary>
        /// Gets the type of the element referenced by the pointer.
        /// </summary>
        /// <value>The type of the component.</value>
        IClrType ComponentType { get; }

        /// <summary>
        /// A type is an array if you can use the array operators below, Abstractly arrays are objects
        /// that whose children are not statically known by just knowing the type.
        /// </summary>
        /// <value><c>true</c> if this instance is array; otherwise, <c>false</c>.</value>
        bool IsArray { get; }

        /// <summary>
        /// Returns the size of individual elements of an array.
        /// </summary>
        /// <value>The size of the element.</value>
        int ElementSize { get; }

        /// <summary>
        /// Returns the base size of the object.
        /// </summary>
        /// <value>The size of the base.</value>
        int BaseSize { get; }

        /// <summary>
        /// Returns true if this type is System.String.
        /// </summary>
        /// <value><c>true</c> if this instance is string; otherwise, <c>false</c>.</value>
        bool IsString { get; }

        /// <summary>
        /// Returns true if this type represents free space on the heap.
        /// </summary>
        /// <value><c>true</c> if this instance is free; otherwise, <c>false</c>.</value>
        bool IsFree { get; }

        /// <summary>
        /// Returns true if this type is an exception (that is, it derives from System.Exception).
        /// </summary>
        /// <value><c>true</c> if this instance is exception; otherwise, <c>false</c>.</value>
        bool IsException { get; }

        /// <summary>
        /// Returns true if this type is an enum.
        /// </summary>
        /// <value><c>true</c> if this instance is enum; otherwise, <c>false</c>.</value>
        bool IsEnum { get; }

        /// <summary>
        /// Returns true if instances of this type have a simple value.
        /// </summary>
        /// <value><c>true</c> if this instance has simple value; otherwise, <c>false</c>.</value>
        bool HasSimpleValue { get; }

        /// <summary>
        /// Enumerates all MethodTable for this type in the process.  MethodTable
        /// are unique to an AppDomain/Type pair, so when there are multiple domains
        /// there may be multiple MethodTable.  Note that even if a type could be
        /// used in an AppDomain, that does not mean we actually have a MethodTable
        /// if the type hasn't been created yet.
        /// </summary>
        /// <returns>An enumeration of MethodTable in the process for this given
        /// type.</returns>
        IEnumerable<ulong> EnumerateMethodTables();

        /// <summary>
        /// GetSize returns the size in bytes for the total overhead of the object 'objRef'.
        /// </summary>
        /// <param name="objRef">The object reference.</param>
        /// <returns>System.UInt64.</returns>
        ulong GetSize(ulong objRef);

        /// <summary>
        /// EnumeationRefsOfObject will call 'action' once for each object reference inside 'objRef'.
        /// 'action' is passed the address of the outgoing refernece as well as an integer that
        /// represents the field offset.  While often this is the physical offset of the outgoing
        /// refernece, abstractly is simply something that can be given to GetFieldForOffset to
        /// return the field information for that object reference
        /// </summary>
        /// <param name="objRef">The object reference.</param>
        /// <param name="action">The action.</param>
        void EnumerateRefsOfObject(ulong objRef, Action<ulong, int> action);

        /// <summary>
        /// Does the same as EnumerateRefsOfObject, but does additional bounds checking to ensure
        /// we don't loop forever with inconsistent data.
        /// </summary>
        /// <param name="objRef">The object reference.</param>
        /// <param name="action">The action.</param>
        void EnumerateRefsOfObjectCarefully(ulong objRef, Action<ulong, int> action);

        /// <summary>
        /// Enumerates all objects that the given object references.
        /// </summary>
        /// <param name="obj">The object in question.</param>
        /// <param name="carefully">Whether to bounds check along the way (useful in cases where
        /// the heap may be in an inconsistent state.)</param>
        /// <returns>IEnumerable&lt;IClrObject&gt;.</returns>
        IEnumerable<IClrObject> EnumerateObjectReferences(ulong obj, bool carefully = false);

        /// <summary>
        /// Returns the concrete type (in the target process) that this RuntimeType represents.
        /// Note you may only call this function if IsRuntimeType returns true.
        /// </summary>
        /// <param name="obj">The RuntimeType object to get the concrete type for.</param>
        /// <returns>The underlying type that this RuntimeType actually represents.  May return null if the
        /// underlying type has not been fully constructed by the runtime, or if the underlying type
        /// is actually a typehandle (which unfortunately ClrMD cannot convert into a ClrType due to
        /// limitations in the underlying APIs.  (So always null-check the return value of this
        /// function.)</returns>
        IClrType GetRuntimeType(ulong obj);

        /// <summary>
        /// Returns true if the finalization is suppressed for an object.  (The user program called
        /// System.GC.SupressFinalize.  The behavior of this function is undefined if the object itself
        /// is not finalizable.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns><c>true</c> if [is finalize suppressed] [the specified object]; otherwise, <c>false</c>.</returns>
        bool IsFinalizeSuppressed(ulong obj);

        /// <summary>
        /// When you enumerate a object, the offset within the object is returned.  This offset might represent
        /// nested fields (obj.Field1.Field2).    GetFieldOffset returns the first of these field (Field1),
        /// and 'remaining' offset with the type of Field1 (which must be a struct type).   Calling
        /// GetFieldForOffset repeatedly until the childFieldOffset is 0 will retrieve the whole chain.
        /// </summary>
        /// <param name="fieldOffset">The field offset.</param>
        /// <param name="inner">if set to <c>true</c> [inner].</param>
        /// <param name="childField">The child field.</param>
        /// <param name="childFieldOffset">The child field offset.</param>
        /// <returns>true if successful.  Will fail if it 'this' is an array type</returns>
        bool GetFieldForOffset(int fieldOffset, bool inner, out IClrInstanceField childField, out int childFieldOffset);

        /// <summary>
        /// Returns the field given by 'name', case sensitive.  Returns NULL if no such field name exists (or on error).
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>IClrInstanceField.</returns>
        IClrInstanceField GetFieldByName(string name);

        /// <summary>
        /// Returns the field given by 'name', case sensitive.  Returns NULL if no such field name exists (or on error).
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>IClrStaticField.</returns>
        IClrStaticField GetStaticFieldByName(string name);

        /// <summary>
        /// Returns true if the given object is a Com-Callable-Wrapper.  This is only supported in v4.5 and later.
        /// </summary>
        /// <param name="obj">The object to check.</param>
        /// <returns>True if this is a CCW.</returns>
        bool IsCCW(ulong obj);

        /// <summary>
        /// Returns the CCWData for the given object.  Note you may only call this function if IsCCW returns true.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>The CCWData associated with the object, undefined result of obj is not a CCW.</returns>
        ICcwData GetCCWData(ulong obj);

        /// <summary>
        /// Returns true if the given object is a Runtime-Callable-Wrapper.  This is only supported in v4.5 and later.
        /// </summary>
        /// <param name="obj">The object to check.</param>
        /// <returns>True if this is an RCW.</returns>
        bool IsRCW(ulong obj);

        /// <summary>
        /// Returns the RCWData for the given object.  Note you may only call this function if IsRCW returns true.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>The RCWData associated with the object, undefined result of obj is not a RCW.</returns>
        IRcwData GetRCWData(ulong obj);

        /// <summary>
        /// If the type is an array, then GetArrayLength returns the number of elements in the array.  Undefined
        /// behavior if this type is not an array.
        /// </summary>
        /// <param name="objRef">The object reference.</param>
        /// <returns>System.Int32.</returns>
        int GetArrayLength(ulong objRef);

        /// <summary>
        /// Returns the absolute address to the given array element.  You may then make a direct memory read out
        /// of the process to get the value if you want.
        /// </summary>
        /// <param name="objRef">The object reference.</param>
        /// <param name="index">The index.</param>
        /// <returns>System.UInt64.</returns>
        ulong GetArrayElementAddress(ulong objRef, int index);

        /// <summary>
        /// Returns the array element value at the given index.  Returns 'null' if the array element is of type
        /// VALUE_CLASS.
        /// </summary>
        /// <param name="objRef">The object reference.</param>
        /// <param name="index">The index.</param>
        /// <returns>System.Object.</returns>
        object GetArrayElementValue(ulong objRef, int index);

        /// <summary>
        /// Returns the element type of this enum.
        /// </summary>
        /// <returns>ClrElementType.</returns>
        ClrElementType GetEnumElementType();

        /// <summary>
        /// Returns a list of names in the enum.
        /// </summary>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        IEnumerable<string> GetEnumNames();

        /// <summary>
        /// Gets the name of the value in the enum, or null if the value doesn't have a name.
        /// This is a convenience function, and has undefined results if the same value appears
        /// twice in the enum.
        /// </summary>
        /// <param name="value">The value to lookup.</param>
        /// <returns>The name of one entry in the enum with this value, or null if none exist.</returns>
        string GetEnumName(object value);

        /// <summary>
        /// Gets the name of the value in the enum, or null if the value doesn't have a name.
        /// This is a convenience function, and has undefined results if the same value appears
        /// twice in the enum.
        /// </summary>
        /// <param name="value">The value to lookup.</param>
        /// <returns>The name of one entry in the enum with this value, or null if none exist.</returns>
        string GetEnumName(int value);

        /// <summary>
        /// Attempts to get the integer value for a given enum entry.  Note you should only call this function if
        /// GetEnumElementType returns ELEMENT_TYPE_I4.
        /// </summary>
        /// <param name="name">The name of the value to get (taken from GetEnumNames).</param>
        /// <param name="value">The value to write out.</param>
        /// <returns>True if we successfully filled value, false if 'name' is not a part of the enumeration.</returns>
        bool TryGetEnumValue(string name, out int value);

        /// <summary>
        /// Attempts to get the value for a given enum entry.  The type of "value" can be determined by the
        /// return value of GetEnumElementType.
        /// </summary>
        /// <param name="name">The name of the value to get (taken from GetEnumNames).</param>
        /// <param name="value">The value to write out.</param>
        /// <returns>True if we successfully filled value, false if 'name' is not a part of the enumeration.</returns>
        bool TryGetEnumValue(string name, out object value);

        /// <summary>
        /// Returns the simple value of an instance of this type.  Undefined behavior if HasSimpleValue returns false.
        /// For example ELEMENT_TYPE_I4 is an "int" and the return value of this function would be an int.
        /// </summary>
        /// <param name="address">The address of an instance of this type.</param>
        /// <returns>System.Object.</returns>
        object GetValue(ulong address);

        /// <summary>
        /// Returns a string representation of this object.
        /// </summary>
        /// <returns>A string representation of this object.</returns>
        string ToString();
    }
}