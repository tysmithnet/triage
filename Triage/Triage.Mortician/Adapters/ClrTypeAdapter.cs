// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-19-2018
// ***********************************************************************
// <copyright file="ClrTypeAdapter.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Diagnostics.Runtime;
using Triage.Mortician.Core.ClrMdAbstractions;
using ClrElementType = Triage.Mortician.Core.ClrMdAbstractions.ClrElementType;

namespace Triage.Mortician.Adapters
{
    /// <summary>
    ///     Class ClrTypeAdapter.
    /// </summary>
    /// <seealso cref="Triage.Mortician.Core.ClrMdAbstractions.IClrType" />
    internal class ClrTypeAdapter : BaseAdapter, IClrType
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ClrTypeAdapter" /> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <exception cref="ArgumentNullException">type</exception>
        /// <inheritdoc />
        public ClrTypeAdapter(IConverter converter, ClrType type) : base(converter)
        {
            ClrType = type ?? throw new ArgumentNullException(nameof(type));
            BaseType = Converter.Convert(type.BaseType);
            ComponentType = Converter.Convert(type.ComponentType);
            ElementType = Converter.Convert(type.ElementType);
            Fields = type.Fields.Select(Converter.Convert).ToList();
            Heap = Converter.Convert(type.Heap);
            Interfaces = type.Interfaces.Select(Converter.Convert).ToList();
            Methods = type.Methods.Select(Converter.Convert).ToList();
            StaticFields = type.StaticFields.Select(Converter.Convert).ToList();
            ThreadStaticFields = type.ThreadStaticFields.Select(Converter.Convert).ToList();
            Module = Converter.Convert(type.Module);
        }

        /// <summary>
        ///     The color type
        /// </summary>
        internal ClrType ClrType;

        /// <summary>
        ///     Enumerates all MethodTable for this type in the process.  MethodTable
        ///     are unique to an AppDomain/Type pair, so when there are multiple domains
        ///     there may be multiple MethodTable.  Note that even if a type could be
        ///     used in an AppDomain, that does not mean we actually have a MethodTable
        ///     if the type hasn't been created yet.
        /// </summary>
        /// <returns>
        ///     An enumeration of MethodTable in the process for this given
        ///     type.
        /// </returns>
        /// <inheritdoc />
        public IEnumerable<ulong> EnumerateMethodTables() => ClrType.EnumerateMethodTables();

        /// <summary>
        ///     Enumerates all objects that the given object references.
        /// </summary>
        /// <param name="obj">The object in question.</param>
        /// <param name="carefully">
        ///     Whether to bounds check along the way (useful in cases where
        ///     the heap may be in an inconsistent state.)
        /// </param>
        /// <returns>IEnumerable&lt;IClrObject&gt;.</returns>
        public IEnumerable<IClrObject> EnumerateObjectReferences(ulong obj, bool carefully = false) =>
            ClrType.EnumerateObjectReferences(obj, carefully).Select(Converter.Convert);

        /// <summary>
        ///     EnumeationRefsOfObject will call 'action' once for each object reference inside 'objRef'.
        ///     'action' is passed the address of the outgoing refernece as well as an integer that
        ///     represents the field offset.  While often this is the physical offset of the outgoing
        ///     refernece, abstractly is simply something that can be given to GetFieldForOffset to
        ///     return the field information for that object reference
        /// </summary>
        /// <param name="objRef">The object reference.</param>
        /// <param name="action">The action.</param>
        public void EnumerateRefsOfObject(ulong objRef, Action<ulong, int> action)
        {
            ClrType.EnumerateRefsOfObject(objRef, action);
        }

        /// <summary>
        ///     Does the same as EnumerateRefsOfObject, but does additional bounds checking to ensure
        ///     we don't loop forever with inconsistent data.
        /// </summary>
        /// <param name="objRef">The object reference.</param>
        /// <param name="action">The action.</param>
        public void EnumerateRefsOfObjectCarefully(ulong objRef, Action<ulong, int> action)
        {
            ClrType.EnumerateRefsOfObjectCarefully(objRef, action);
        }

        /// <summary>
        ///     Returns the absolute address to the given array element.  You may then make a direct memory read out
        ///     of the process to get the value if you want.
        /// </summary>
        /// <param name="objRef">The object reference.</param>
        /// <param name="index">The index.</param>
        /// <returns>System.UInt64.</returns>
        public ulong GetArrayElementAddress(ulong objRef, int index) => ClrType.GetArrayElementAddress(objRef, index);

        /// <summary>
        ///     Returns the array element value at the given index.  Returns 'null' if the array element is of type
        ///     VALUE_CLASS.
        /// </summary>
        /// <param name="objRef">The object reference.</param>
        /// <param name="index">The index.</param>
        /// <returns>System.Object.</returns>
        public object GetArrayElementValue(ulong objRef, int index) => ClrType.GetArrayElementValue(objRef, index);

        /// <summary>
        ///     If the type is an array, then GetArrayLength returns the number of elements in the array.  Undefined
        ///     behavior if this type is not an array.
        /// </summary>
        /// <param name="objRef">The object reference.</param>
        /// <returns>System.Int32.</returns>
        public int GetArrayLength(ulong objRef) => ClrType.GetArrayLength(objRef);

        /// <summary>
        ///     Returns the CCWData for the given object.  Note you may only call this function if IsCCW returns true.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>The CCWData associated with the object, undefined result of obj is not a CCW.</returns>
        public ICcwData GetCCWData(ulong obj) => Converter.Convert(ClrType.GetCCWData(obj));

        /// <summary>
        ///     Returns the element type of this enum.
        /// </summary>
        /// <returns>ClrElementType.</returns>
        public ClrElementType GetEnumElementType() => Converter.Convert(ClrType.GetEnumElementType());

        /// <summary>
        ///     Gets the name of the value in the enum, or null if the value doesn't have a name.
        ///     This is a convenience function, and has undefined results if the same value appears
        ///     twice in the enum.
        /// </summary>
        /// <param name="value">The value to lookup.</param>
        /// <returns>The name of one entry in the enum with this value, or null if none exist.</returns>
        public string GetEnumName(object value) => ClrType.GetEnumName(value);

        /// <summary>
        ///     Gets the name of the value in the enum, or null if the value doesn't have a name.
        ///     This is a convenience function, and has undefined results if the same value appears
        ///     twice in the enum.
        /// </summary>
        /// <param name="value">The value to lookup.</param>
        /// <returns>The name of one entry in the enum with this value, or null if none exist.</returns>
        public string GetEnumName(int value) => ClrType.GetEnumName(value);

        /// <summary>
        ///     Returns a list of names in the enum.
        /// </summary>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        public IEnumerable<string> GetEnumNames() => ClrType.GetEnumNames();

        /// <summary>
        ///     Returns the field given by 'name', case sensitive.  Returns NULL if no such field name exists (or on error).
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>IClrInstanceField.</returns>
        public IClrInstanceField GetFieldByName(string name) => Converter.Convert(ClrType.GetFieldByName(name));

        /// <summary>
        ///     When you enumerate a object, the offset within the object is returned.  This offset might represent
        ///     nested fields (obj.Field1.Field2).    GetFieldOffset returns the first of these field (Field1),
        ///     and 'remaining' offset with the type of Field1 (which must be a struct type).   Calling
        ///     GetFieldForOffset repeatedly until the childFieldOffset is 0 will retrieve the whole chain.
        /// </summary>
        /// <param name="fieldOffset">The field offset.</param>
        /// <param name="inner">if set to <c>true</c> [inner].</param>
        /// <param name="childField">The child field.</param>
        /// <param name="childFieldOffset">The child field offset.</param>
        /// <returns>true if successful.  Will fail if it 'this' is an array type</returns>
        public bool GetFieldForOffset(int fieldOffset, bool inner, out IClrInstanceField childField,
            out int childFieldOffset)
        {
            var res = ClrType.GetFieldForOffset(fieldOffset, inner, out var outChildField, out childFieldOffset);
            childField = Converter.Convert(outChildField);
            return res;
        }

        /// <summary>
        ///     Returns the RCWData for the given object.  Note you may only call this function if IsRCW returns true.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>The RCWData associated with the object, undefined result of obj is not a RCW.</returns>
        public IRcwData GetRCWData(ulong obj) => Converter.Convert(ClrType.GetRCWData(obj));

        /// <summary>
        ///     Returns the concrete type (in the target process) that this RuntimeType represents.
        ///     Note you may only call this function if IsRuntimeType returns true.
        /// </summary>
        /// <param name="obj">The RuntimeType object to get the concrete type for.</param>
        /// <returns>
        ///     The underlying type that this RuntimeType actually represents.  May return null if the
        ///     underlying type has not been fully constructed by the runtime, or if the underlying type
        ///     is actually a typehandle (which unfortunately ClrMD cannot convert into a ClrType due to
        ///     limitations in the underlying APIs.  (So always null-check the return value of this
        ///     function.)
        /// </returns>
        public IClrType GetRuntimeType(ulong obj) => Converter.Convert(ClrType.GetRuntimeType(obj));

        /// <summary>
        ///     GetSize returns the size in bytes for the total overhead of the object 'objRef'.
        /// </summary>
        /// <param name="objRef">The object reference.</param>
        /// <returns>System.UInt64.</returns>
        public ulong GetSize(ulong objRef) => ClrType.GetSize(objRef);

        /// <summary>
        ///     Returns the field given by 'name', case sensitive.  Returns NULL if no such field name exists (or on error).
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>IClrStaticField.</returns>
        public IClrStaticField GetStaticFieldByName(string name) =>
            Converter.Convert(ClrType.GetStaticFieldByName(name));

        /// <summary>
        ///     Returns the simple value of an instance of this type.  Undefined behavior if HasSimpleValue returns false.
        ///     For example ELEMENT_TYPE_I4 is an "int" and the return value of this function would be an int.
        /// </summary>
        /// <param name="address">The address of an instance of this type.</param>
        /// <returns>System.Object.</returns>
        public object GetValue(ulong address) => ClrType.GetValue(address);

        /// <summary>
        ///     Returns true if the given object is a Com-Callable-Wrapper.  This is only supported in v4.5 and later.
        /// </summary>
        /// <param name="obj">The object to check.</param>
        /// <returns>True if this is a CCW.</returns>
        public bool IsCCW(ulong obj) => ClrType.IsCCW(obj);

        /// <summary>
        ///     Returns true if the finalization is suppressed for an object.  (The user program called
        ///     System.GC.SupressFinalize.  The behavior of this function is undefined if the object itself
        ///     is not finalizable.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns><c>true</c> if [is finalize suppressed] [the specified object]; otherwise, <c>false</c>.</returns>
        public bool IsFinalizeSuppressed(ulong obj) => ClrType.IsFinalizeSuppressed(obj);

        /// <summary>
        ///     Returns true if the given object is a Runtime-Callable-Wrapper.  This is only supported in v4.5 and later.
        /// </summary>
        /// <param name="obj">The object to check.</param>
        /// <returns>True if this is an RCW.</returns>
        public bool IsRCW(ulong obj) => ClrType.IsRCW(obj);

        /// <summary>
        ///     Attempts to get the integer value for a given enum entry.  Note you should only call this function if
        ///     GetEnumElementType returns ELEMENT_TYPE_I4.
        /// </summary>
        /// <param name="name">The name of the value to get (taken from GetEnumNames).</param>
        /// <param name="value">The value to write out.</param>
        /// <returns>True if we successfully filled value, false if 'name' is not a part of the enumeration.</returns>
        public bool TryGetEnumValue(string name, out int value) => ClrType.TryGetEnumValue(name, out value);

        /// <summary>
        ///     Attempts to get the value for a given enum entry.  The type of "value" can be determined by the
        ///     return value of GetEnumElementType.
        /// </summary>
        /// <param name="name">The name of the value to get (taken from GetEnumNames).</param>
        /// <param name="value">The value to write out.</param>
        /// <returns>True if we successfully filled value, false if 'name' is not a part of the enumeration.</returns>
        public bool TryGetEnumValue(string name, out object value) => ClrType.TryGetEnumValue(name, out value);

        /// <summary>
        ///     Returns the base size of the object.
        /// </summary>
        /// <value>The size of the base.</value>
        /// <inheritdoc />
        public int BaseSize => ClrType.BaseSize;

        /// <summary>
        ///     If this type inherits from another type, this is that type.  Can return null if it does not inherit (or is unknown)
        /// </summary>
        /// <value>The type of the base.</value>
        /// <inheritdoc />
        public IClrType BaseType { get; }

        /// <summary>
        ///     Gets the type of the element referenced by the pointer.
        /// </summary>
        /// <value>The type of the component.</value>
        /// <inheritdoc />
        public IClrType ComponentType { get; }

        /// <summary>
        ///     Returns true if the type CAN contain references to other objects.  This is used in optimizations
        ///     and 'true' can always be returned safely.
        /// </summary>
        /// <value><c>true</c> if [contains pointers]; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool ContainsPointers => ClrType.ContainsPointers;

        /// <summary>
        ///     Returns the size of individual elements of an array.
        /// </summary>
        /// <value>The size of the element.</value>
        /// <inheritdoc />
        public int ElementSize => ClrType.ElementSize;

        /// <summary>
        ///     Returns the ElementType of this Type.  Can return ELEMENT_TYPE_VOID on error.
        /// </summary>
        /// <value>The type of the element.</value>
        /// <inheritdoc />
        public ClrElementType ElementType { get; }

        /// <summary>
        ///     Returns all possible fields in this type.   It does not return dynamically typed fields.
        ///     Returns an empty list if there are no fields.
        /// </summary>
        /// <value>The fields.</value>
        /// <inheritdoc />
        public IList<IClrInstanceField> Fields { get; }

        /// <summary>
        ///     Returns true if instances of this type have a simple value.
        /// </summary>
        /// <value><c>true</c> if this instance has simple value; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool HasSimpleValue => ClrType.HasSimpleValue;

        /// <summary>
        ///     All types know the heap they belong to.
        /// </summary>
        /// <value>The heap.</value>
        /// <inheritdoc />
        public IClrHeap Heap { get; }

        /// <summary>
        ///     Returns the list of interfaces this type implements.
        /// </summary>
        /// <value>The interfaces.</value>
        /// <inheritdoc />
        public IList<IClrInterface> Interfaces { get; }

        /// <summary>
        ///     Returns true if this class is abstract.
        /// </summary>
        /// <value><c>true</c> if this instance is abstract; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsAbstract => ClrType.IsAbstract;

        /// <summary>
        ///     A type is an array if you can use the array operators below, Abstractly arrays are objects
        ///     that whose children are not statically known by just knowing the type.
        /// </summary>
        /// <value><c>true</c> if this instance is array; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsArray => ClrType.IsArray;

        /// <summary>
        ///     Returns true if this type is an enum.
        /// </summary>
        /// <value><c>true</c> if this instance is enum; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsEnum => ClrType.IsEnum;

        /// <summary>
        ///     Returns true if this type is an exception (that is, it derives from System.Exception).
        /// </summary>
        /// <value><c>true</c> if this instance is exception; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsException => ClrType.IsException;

        /// <summary>
        ///     Returns whether objects of this type are finalizable.
        /// </summary>
        /// <value><c>true</c> if this instance is finalizable; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsFinalizable => ClrType.IsFinalizable;

        /// <summary>
        ///     Returns true if this type represents free space on the heap.
        /// </summary>
        /// <value><c>true</c> if this instance is free; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsFree => ClrType.IsFree;

        /// <summary>
        ///     Returns true if this type is an interface.
        /// </summary>
        /// <value><c>true</c> if this instance is interface; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsInterface => ClrType.IsInterface;

        /// <summary>
        ///     Returns true if this type is accessable only by items in its own assembly.
        /// </summary>
        /// <value><c>true</c> if this instance is internal; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsInternal => ClrType.IsInternal;

        /// <summary>
        ///     Returns true if this type is an object reference, false otherwise.
        /// </summary>
        /// <value><c>true</c> if this instance is object reference; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsObjectReference => ClrType.IsObjectReference;

        /// <summary>
        ///     Indicates if the type is in fact a pointer. If so, the pointer operators
        ///     may be used.
        /// </summary>
        /// <value><c>true</c> if this instance is pointer; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsPointer => ClrType.IsPointer;

        /// <summary>
        ///     Returns true if this type is a primitive (int, float, etc), false otherwise.
        /// </summary>
        /// <value><c>true</c> if this instance is primitive; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsPrimitive => ClrType.IsPrimitive;

        /// <summary>
        ///     returns true if this type is marked Private.
        /// </summary>
        /// <value><c>true</c> if this instance is private; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsPrivate => ClrType.IsPrivate;

        /// <summary>
        ///     Returns true if this nested type is accessable only by subtypes of its outer type.
        /// </summary>
        /// <value><c>true</c> if this instance is protected; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsProtected => ClrType.IsProtected;

        /// <summary>
        ///     Returns true if this type is marked Public.
        /// </summary>
        /// <value><c>true</c> if this instance is public; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsPublic => ClrType.IsPublic;

        /// <summary>
        ///     Returns true if this object is a 'RuntimeType' (that is, the concrete System.RuntimeType class
        ///     which is what you get when calling "typeof" in C#).
        /// </summary>
        /// <value><c>true</c> if this instance is runtime type; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsRuntimeType => ClrType.IsRuntimeType;

        /// <summary>
        ///     Returns true if this class is sealed.
        /// </summary>
        /// <value><c>true</c> if this instance is sealed; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsSealed => ClrType.IsSealed;

        /// <summary>
        ///     Returns true if this type is System.String.
        /// </summary>
        /// <value><c>true</c> if this instance is string; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsString => ClrType.IsString;

        /// <summary>
        ///     Returns true if this type is a ValueClass (struct), false otherwise.
        /// </summary>
        /// <value><c>true</c> if this instance is value class; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsValueClass => ClrType.IsValueClass;

        /// <summary>
        ///     Returns the metadata token of this type.
        /// </summary>
        /// <value>The metadata token.</value>
        /// <inheritdoc />
        public uint MetadataToken => ClrType.MetadataToken;

        /// <summary>
        ///     Gets the list of methods this type implements.
        /// </summary>
        /// <value>The methods.</value>
        /// <inheritdoc />
        public IList<IClrMethod> Methods { get; }

        /// <summary>
        ///     Retrieves the first type handle in EnumerateMethodTables().  MethodTables
        ///     are unique to an AppDomain/Type pair, so when there are multiple domains
        ///     there will be multiple MethodTable for a class.
        /// </summary>
        /// <value>The method table.</value>
        /// <inheritdoc />
        public ulong MethodTable => ClrType.MethodTable;

        /// <summary>
        ///     Returns the module this type is defined in.
        /// </summary>
        /// <value>The module.</value>
        /// <inheritdoc />
        public IClrModule Module { get; }

        /// <summary>
        ///     Types have names.
        /// </summary>
        /// <value>The name.</value>
        /// <inheritdoc />
        public string Name => ClrType.Name;

        /// <summary>
        ///     Returns a list of static fields on this type.  Returns an empty list if there are no fields.
        /// </summary>
        /// <value>The static fields.</value>
        /// <inheritdoc />
        public IList<IClrStaticField> StaticFields { get; }

        /// <summary>
        ///     Returns a list of thread static fields on this type.  Returns an empty list if there are no fields.
        /// </summary>
        /// <value>The thread static fields.</value>
        /// <inheritdoc />
        public IList<IClrThreadStaticField> ThreadStaticFields { get; }
        
    }
}