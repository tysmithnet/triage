using System;
using System.Collections.Generic;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class ClrTypeAdapter : IClrType
    {
        /// <inheritdoc />
        public ClrTypeAdapter(Microsoft.Diagnostics.Runtime.ClrType type)
        {
            _clrType = type ?? throw new ArgumentNullException(nameof(type));
        }

        internal Microsoft.Diagnostics.Runtime.ClrType _clrType;

        /// <inheritdoc />
        public IEnumerable<ulong> EnumerateMethodTables()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IEnumerable<IClrObject> EnumerateObjectReferences(ulong obj, bool carefully = false)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void EnumerateRefsOfObject(ulong objRef, Action<ulong, int> action)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void EnumerateRefsOfObjectCarefully(ulong objRef, Action<ulong, int> action)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ulong GetArrayElementAddress(ulong objRef, int index)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public object GetArrayElementValue(ulong objRef, int index)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public int GetArrayLength(ulong objRef)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ICcwData GetCCWData(ulong obj)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ClrElementType GetEnumElementType()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public string GetEnumName(object value)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public string GetEnumName(int value)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IEnumerable<string> GetEnumNames()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IClrInstanceField GetFieldByName(string name)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool GetFieldForOffset(int fieldOffset, bool inner, out IClrInstanceField childField,
            out int childFieldOffset)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IRcwData GetRCWData(ulong obj)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IClrType GetRuntimeType(ulong obj)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ulong GetSize(ulong objRef)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IClrStaticField GetStaticFieldByName(string name)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public object GetValue(ulong address)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool IsCCW(ulong obj)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool IsFinalizeSuppressed(ulong obj)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool IsRCW(ulong obj)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool TryGetEnumValue(string name, out int value)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool TryGetEnumValue(string name, out object value)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public int BaseSize { get; }

        /// <inheritdoc />
        public IClrType BaseType { get; }

        /// <inheritdoc />
        public IClrType ComponentType { get; }

        /// <inheritdoc />
        public bool ContainsPointers { get; }

        /// <inheritdoc />
        public int ElementSize { get; }

        /// <inheritdoc />
        public ClrElementType ElementType { get; }

        /// <inheritdoc />
        public IList<IClrInstanceField> Fields { get; }

        /// <inheritdoc />
        public bool HasSimpleValue { get; }

        /// <inheritdoc />
        public IClrHeap Heap { get; }

        /// <inheritdoc />
        public IList<IClrInterface> Interfaces { get; }

        /// <inheritdoc />
        public bool IsAbstract { get; }

        /// <inheritdoc />
        public bool IsArray { get; }

        /// <inheritdoc />
        public bool IsEnum { get; }

        /// <inheritdoc />
        public bool IsException { get; }

        /// <inheritdoc />
        public bool IsFinalizable { get; }

        /// <inheritdoc />
        public bool IsFree { get; }

        /// <inheritdoc />
        public bool IsInterface { get; }

        /// <inheritdoc />
        public bool IsInternal { get; }

        /// <inheritdoc />
        public bool IsObjectReference { get; }

        /// <inheritdoc />
        public bool IsPointer { get; }

        /// <inheritdoc />
        public bool IsPrimitive { get; }

        /// <inheritdoc />
        public bool IsPrivate { get; }

        /// <inheritdoc />
        public bool IsProtected { get; }

        /// <inheritdoc />
        public bool IsPublic { get; }

        /// <inheritdoc />
        public bool IsRuntimeType { get; }

        /// <inheritdoc />
        public bool IsSealed { get; }

        /// <inheritdoc />
        public bool IsString { get; }

        /// <inheritdoc />
        public bool IsValueClass { get; }

        /// <inheritdoc />
        public uint MetadataToken { get; }

        /// <inheritdoc />
        public IList<IClrMethod> Methods { get; }

        /// <inheritdoc />
        public ulong MethodTable { get; }

        /// <inheritdoc />
        public IClrModule Module { get; }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public IList<IClrStaticField> StaticFields { get; }

        /// <inheritdoc />
        public IList<IClrThreadStaticField> ThreadStaticFields { get; }
    }
}