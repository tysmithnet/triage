using System;
using System.Collections.Generic;

namespace Triage.Mortician.Core.ClrMdAbstractions
{
    public interface IClrType
    {
        /// <summary>
        /// Retrieves the first type handle in EnumerateMethodTables().  MethodTables
        /// are unique to an AppDomain/Type pair, so when there are multiple domains
        /// there will be multiple MethodTable for a class.
        /// </summary>
        ulong MethodTable { get; }

        /// <summary>
        /// Returns the metadata token of this type.
        /// </summary>
        uint MetadataToken { get; }

        /// <summary>
        /// Types have names.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Returns true if the type CAN contain references to other objects.  This is used in optimizations 
        /// and 'true' can always be returned safely.  
        /// </summary>
        bool ContainsPointers { get; }

        /// <summary>
        /// All types know the heap they belong to.  
        /// </summary>
        IClrHeap Heap { get; }

        /// <summary>
        /// Returns true if this object is a 'RuntimeType' (that is, the concrete System.RuntimeType class
        /// which is what you get when calling "typeof" in C#).
        /// </summary>
        bool IsRuntimeType { get; }

        /// <summary>
        /// Returns the module this type is defined in.
        /// </summary>
        IClrModule Module { get; }

        /// <summary>
        /// Returns the ElementType of this Type.  Can return ELEMENT_TYPE_VOID on error.
        /// </summary>
        ClrElementType ElementType { get; }

        /// <summary>
        /// Returns true if this type is a primitive (int, float, etc), false otherwise.
        /// </summary>
        /// <returns>True if this type is a primitive (int, float, etc), false otherwise.</returns>
        bool IsPrimitive { get; }

        /// <summary>
        /// Returns true if this type is a ValueClass (struct), false otherwise.
        /// </summary>
        /// <returns>True if this type is a ValueClass (struct), false otherwise.</returns>
        bool IsValueClass { get; }

        /// <summary>
        /// Returns true if this type is an object reference, false otherwise.
        /// </summary>
        /// <returns>True if this type is an object reference, false otherwise.</returns>
        bool IsObjectReference { get; }

        /// <summary>
        /// Returns the list of interfaces this type implements.
        /// </summary>
        IList<IClrInterface> Interfaces { get; }

        /// <summary>
        /// Returns whether objects of this type are finalizable.
        /// </summary>
        bool IsFinalizable { get; }

        /// <summary>
        /// Returns true if this type is marked Public.
        /// </summary>
        bool IsPublic { get; }

        /// <summary>
        /// returns true if this type is marked Private.
        /// </summary>
        bool IsPrivate { get; }

        /// <summary>
        /// Returns true if this type is accessable only by items in its own assembly.
        /// </summary>
        bool IsInternal { get; }

        /// <summary>
        /// Returns true if this nested type is accessable only by subtypes of its outer type.
        /// </summary>
        bool IsProtected { get; }

        /// <summary>
        /// Returns true if this class is abstract.
        /// </summary>
        bool IsAbstract { get; }

        /// <summary>
        /// Returns true if this class is sealed.
        /// </summary>
        bool IsSealed { get; }

        /// <summary>
        /// Returns true if this type is an interface.
        /// </summary>
        bool IsInterface { get; }

        /// <summary>
        /// Returns all possible fields in this type.   It does not return dynamically typed fields.  
        /// Returns an empty list if there are no fields.
        /// </summary>
        IList<IClrInstanceField> Fields { get; }

        /// <summary>
        /// Returns a list of static fields on this type.  Returns an empty list if there are no fields.
        /// </summary>
        IList<IClrStaticField> StaticFields { get; }

        /// <summary>
        /// Returns a list of thread static fields on this type.  Returns an empty list if there are no fields.
        /// </summary>
        IList<IClrThreadStaticField> ThreadStaticFields { get; }

        /// <summary>
        /// Gets the list of methods this type implements.
        /// </summary>
        IList<IClrMethod> Methods { get; }

        /// <summary>
        /// If this type inherits from another type, this is that type.  Can return null if it does not inherit (or is unknown)
        /// </summary>
        IClrType BaseType { get; }

        /// <summary>
        /// Indicates if the type is in fact a pointer. If so, the pointer operators
        /// may be used.
        /// </summary>
        bool IsPointer { get; }

        /// <summary>
        /// Gets the type of the element referenced by the pointer.
        /// </summary>
        IClrType ComponentType { get; }

        /// <summary>
        /// A type is an array if you can use the array operators below, Abstractly arrays are objects 
        /// that whose children are not statically known by just knowing the type.  
        /// </summary>
        bool IsArray { get; }

        /// <summary>
        /// Returns the size of individual elements of an array.
        /// </summary>
        int ElementSize { get; }

        /// <summary>
        /// Returns the base size of the object.
        /// </summary>
        int BaseSize { get; }

        /// <summary>
        /// Returns true if this type is System.String.
        /// </summary>
        bool IsString { get; }

        /// <summary>
        /// Returns true if this type represents free space on the heap.
        /// </summary>
        bool IsFree { get; }

        /// <summary>
        /// Returns true if this type is an exception (that is, it derives from System.Exception).
        /// </summary>
        bool IsException { get; }

        /// <summary>
        /// Returns true if this type is an enum.
        /// </summary>
        bool IsEnum { get; }

        /// <summary>
        /// Returns true if instances of this type have a simple value.
        /// </summary>
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
        ulong GetSize(ulong objRef);

        /// <summary>
        /// EnumeationRefsOfObject will call 'action' once for each object reference inside 'objRef'.  
        /// 'action' is passed the address of the outgoing refernece as well as an integer that
        /// represents the field offset.  While often this is the physical offset of the outgoing
        /// refernece, abstractly is simply something that can be given to GetFieldForOffset to 
        /// return the field information for that object reference  
        /// </summary>
        void EnumerateRefsOfObject(ulong objRef, Action<ulong, int> action);

        /// <summary>
        /// Does the same as EnumerateRefsOfObject, but does additional bounds checking to ensure
        /// we don't loop forever with inconsistent data.
        /// </summary>
        void EnumerateRefsOfObjectCarefully(ulong objRef, Action<ulong, int> action);

        /// <summary>
        /// Enumerates all objects that the given object references.
        /// </summary>
        /// <param name="obj">The object in question.</param>
        /// <param name="carefully">Whether to bounds check along the way (useful in cases where
        /// the heap may be in an inconsistent state.)</param>
        IEnumerable<IClrObject> EnumerateObjectReferences(ulong obj, bool carefully = false);

        /// <summary>
        /// Returns the concrete type (in the target process) that this RuntimeType represents.
        /// Note you may only call this function if IsRuntimeType returns true.
        /// </summary>
        /// <param name="obj">The RuntimeType object to get the concrete type for.</param>
        /// <returns>The underlying type that this RuntimeType actually represents.  May return null if the
        ///          underlying type has not been fully constructed by the runtime, or if the underlying type
        ///          is actually a typehandle (which unfortunately ClrMD cannot convert into a ClrType due to
        ///          limitations in the underlying APIs.  (So always null-check the return value of this
        ///          function.) </returns>
        IClrType GetRuntimeType(ulong obj);

        /// <summary>
        /// Returns true if the finalization is suppressed for an object.  (The user program called
        /// System.GC.SupressFinalize.  The behavior of this function is undefined if the object itself
        /// is not finalizable.
        /// </summary>
        bool IsFinalizeSuppressed(ulong obj);

        /// <summary>
        /// When you enumerate a object, the offset within the object is returned.  This offset might represent
        /// nested fields (obj.Field1.Field2).    GetFieldOffset returns the first of these field (Field1), 
        /// and 'remaining' offset with the type of Field1 (which must be a struct type).   Calling 
        /// GetFieldForOffset repeatedly until the childFieldOffset is 0 will retrieve the whole chain.  
        /// </summary>
        /// <returns>true if successful.  Will fail if it 'this' is an array type</returns>
        bool GetFieldForOffset(int fieldOffset, bool inner, out IClrInstanceField childField, out int childFieldOffset);

        /// <summary>
        /// Returns the field given by 'name', case sensitive.  Returns NULL if no such field name exists (or on error).
        /// </summary>
        IClrInstanceField GetFieldByName(string name);

        /// <summary>
        /// Returns the field given by 'name', case sensitive.  Returns NULL if no such field name exists (or on error).
        /// </summary>
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
        /// <returns>The RCWData associated with the object, undefined result of obj is not a RCW.</returns>
        IRcwData GetRCWData(ulong obj);

        /// <summary>
        /// If the type is an array, then GetArrayLength returns the number of elements in the array.  Undefined
        /// behavior if this type is not an array.
        /// </summary>
        int GetArrayLength(ulong objRef);

        /// <summary>
        /// Returns the absolute address to the given array element.  You may then make a direct memory read out
        /// of the process to get the value if you want.
        /// </summary>
        ulong GetArrayElementAddress(ulong objRef, int index);

        /// <summary>
        /// Returns the array element value at the given index.  Returns 'null' if the array element is of type
        /// VALUE_CLASS.
        /// </summary>
        object GetArrayElementValue(ulong objRef, int index);

        /// <summary>
        /// Returns the element type of this enum.
        /// </summary>
        ClrElementType GetEnumElementType();

        /// <summary>
        /// Returns a list of names in the enum.
        /// </summary>
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
        object GetValue(ulong address);

        /// <summary>
        /// Returns a string representation of this object.
        /// </summary>
        /// <returns>A string representation of this object.</returns>
        string ToString();
    }
}