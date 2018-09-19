using System;
using System.Collections.Generic;
using System.Diagnostics;
using Triage.Mortician.Core.ClrMdAbstractions;
using ClrMd = Microsoft.Diagnostics.Runtime;
namespace Triage.Mortician.Adapters
{
    internal class ClrInstanceFieldAdapter : IClrInstanceField
    {
        internal ClrMd.ClrInstanceField InstanceField;

        /// <inheritdoc />
        public ClrInstanceFieldAdapter(ClrMd.ClrInstanceField instanceField)
        {
            InstanceField = instanceField ?? throw new ArgumentNullException(nameof(instanceField));
        }

        /// <inheritdoc />
        public ulong GetAddress(ulong objRef)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ulong GetAddress(ulong objRef, bool interior)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public object GetValue(ulong objRef)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public object GetValue(ulong objRef, bool interior)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public object GetValue(ulong objRef, bool interior, bool convertStrings)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ClrElementType ElementType { get; }

        /// <inheritdoc />
        public bool HasSimpleValue { get; }

        /// <inheritdoc />
        public bool IsInternal { get; }

        /// <inheritdoc />
        public bool IsObjectReference { get; }

        /// <inheritdoc />
        public bool IsPrimitive { get; }

        /// <inheritdoc />
        public bool IsPrivate { get; }

        /// <inheritdoc />
        public bool IsProtected { get; }

        /// <inheritdoc />
        public bool IsPublic { get; }

        /// <inheritdoc />
        public bool IsValueClass { get; }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public int Offset { get; }

        /// <inheritdoc />
        public int Size { get; }

        /// <inheritdoc />
        public uint Token { get; }

        /// <inheritdoc />
        public IClrType Type { get; }
    }

    internal class BlockingObjectAdapter : IBlockingObject
    {
        internal ClrMd.BlockingObject BlockingObject;

        /// <inheritdoc />
        public BlockingObjectAdapter(ClrMd.BlockingObject blockingObject)
        {
            BlockingObject = blockingObject ?? throw new ArgumentNullException(nameof(blockingObject));
        }

        /// <inheritdoc />
        public bool HasSingleOwner { get; }

        /// <inheritdoc />
        public ulong Object { get; }

        /// <inheritdoc />
        public IClrThread Owner { get; }

        /// <inheritdoc />
        public IList<IClrThread> Owners { get; }

        /// <inheritdoc />
        public BlockingReason Reason { get; }

        /// <inheritdoc />
        public int RecursionCount { get; }

        /// <inheritdoc />
        public bool Taken { get; }

        /// <inheritdoc />
        public IList<IClrThread> Waiters { get; }
    }

    internal class ClrModuleAdapter : IClrModule
    {
        internal ClrMd.ClrModule Module;

        /// <inheritdoc />
        public ClrModuleAdapter(ClrMd.ClrModule module)
        {
            Module = module ?? throw new ArgumentNullException(nameof(module));
        }

        /// <inheritdoc />
        public IEnumerable<IClrType> EnumerateTypes()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IClrType GetTypeByName(string name)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IList<IClrAppDomain> AppDomains { get; }

        /// <inheritdoc />
        public ulong AssemblyId { get; }

        /// <inheritdoc />
        public string AssemblyName { get; }

        /// <inheritdoc />
        public DebuggableAttribute.DebuggingModes DebuggingMode { get; }

        /// <inheritdoc />
        public string FileName { get; }

        /// <inheritdoc />
        public ulong ImageBase { get; }

        /// <inheritdoc />
        public bool IsDynamic { get; }

        /// <inheritdoc />
        public bool IsFile { get; }

        /// <inheritdoc />
        public ulong MetadataAddress { get; }

        /// <inheritdoc />
        public object MetadataImport { get; }

        /// <inheritdoc />
        public ulong MetadataLength { get; }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public IPdbInfo Pdb { get; }

        /// <inheritdoc />
        public IClrRuntime Runtime { get; }

        /// <inheritdoc />
        public ulong Size { get; }
    }

    internal class ComInterfaceDataAdapter : IComInterfaceData
    {
        internal ClrMd.ComInterfaceData Data;

        /// <inheritdoc />
        public ComInterfaceDataAdapter(ClrMd.ComInterfaceData data)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
        }

        /// <inheritdoc />
        public ulong InterfacePointer { get; }

        /// <inheritdoc />
        public IClrType Type { get; }
    }

    internal class ClrInterfaceAdapter : IClrInterface
    {
        internal ClrMd.ClrInterface Interface;

        /// <inheritdoc />
        public ClrInterfaceAdapter(ClrMd.ClrInterface @interface)
        {
            Interface = @interface ?? throw new ArgumentNullException(nameof(@interface));
        }

        /// <inheritdoc />
        public IClrInterface BaseInterface { get; }

        /// <inheritdoc />
        public string Name { get; }
    }

    internal class SymbolResolverAdapter : ISymbolResolver
    {
        /// <inheritdoc />
        public SymbolResolverAdapter(Microsoft.Diagnostics.Runtime.ISymbolResolver resolver)
        {
            _resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
        }

        internal Microsoft.Diagnostics.Runtime.ISymbolResolver _resolver;

        /// <inheritdoc />
        public string GetSymbolNameByRVA(uint rva)
        {
            throw new NotImplementedException();
        }
    }
}