// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-19-2018
// ***********************************************************************
// <copyright file="Converter.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.ComponentModel.Composition;
using Microsoft.Diagnostics.Runtime.Utilities;
using Triage.Mortician.Adapters;
using Triage.Mortician.Core.ClrMdAbstractions;
using ClrMd = Microsoft.Diagnostics.Runtime;

namespace Triage.Mortician
{
    /// <summary>
    ///     Class Converter.
    /// </summary>
    /// <seealso cref="Triage.Mortician.IConverter" />
    [Export(typeof(IConverter))]
    internal class Converter : IConverter
    {
        /// <summary>
        ///     Converts the specified architecture.
        /// </summary>
        /// <param name="architecture">The architecture.</param>
        /// <returns>Architecture.</returns>
        /// <exception cref="ArgumentOutOfRangeException">architecture - The Architecture contract does not match CLRMd.</exception>
        public Architecture Convert(ClrMd.Architecture architecture)
        {
            switch (architecture)
            {
                case ClrMd.Architecture.Amd64:
                    return Architecture.Amd64;
                case ClrMd.Architecture.Arm:
                    return Architecture.Arm;
                case ClrMd.Architecture.Unknown:
                    return Architecture.Unknown;
                case ClrMd.Architecture.X86:
                    return Architecture.X86;
                default:
                    throw new ArgumentOutOfRangeException(nameof(architecture), architecture,
                        "The Architecture contract does not match CLRMd.");
            }
        }

        /// <summary>
        ///     Converts the specified segment type.
        /// </summary>
        /// <param name="segmentType">Type of the segment.</param>
        /// <returns>GcSegmentType.</returns>
        /// <exception cref="ArgumentOutOfRangeException">segmentType - null</exception>
        public GcSegmentType Convert(ClrMd.GCSegmentType segmentType)
        {
            switch (segmentType)
            {
                case ClrMd.GCSegmentType.Ephemeral:
                    return GcSegmentType.Ephemeral;
                case ClrMd.GCSegmentType.Regular:
                    return GcSegmentType.Regular;
                case ClrMd.GCSegmentType.LargeObject:
                    return GcSegmentType.LargeObject;
                default:
                    throw new ArgumentOutOfRangeException(nameof(segmentType), segmentType, null);
            }
        }

        /// <summary>
        ///     Converts the specified instance field.
        /// </summary>
        /// <param name="instanceField">The instance field.</param>
        /// <returns>IClrInstanceField.</returns>
        public IClrInstanceField Convert(ClrMd.ClrInstanceField instanceField) =>
            Cache.GetOrAdd<IClrInstanceField>(instanceField, () => new ClrInstanceFieldAdapter(instanceField));

        /// <summary>
        ///     Converts the specified blocking object.
        /// </summary>
        /// <param name="blockingObject">The blocking object.</param>
        /// <returns>IBlockingObject.</returns>
        public IBlockingObject Convert(ClrMd.BlockingObject blockingObject) =>
            Cache.GetOrAdd<IBlockingObject>(blockingObject, () => new BlockingObjectAdapter(blockingObject));

        /// <summary>
        ///     Converts the specified module.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <returns>IClrModule.</returns>
        public IClrModule Convert(ClrMd.ClrModule module) =>
            Cache.GetOrAdd<IClrModule>(module, () => new ClrModuleAdapter(module));

        /// <summary>
        ///     Converts the specified interface data.
        /// </summary>
        /// <param name="interfaceData">The interface data.</param>
        /// <returns>IComInterfaceData.</returns>
        public IComInterfaceData Convert(ClrMd.ComInterfaceData interfaceData) =>
            Cache.GetOrAdd<IComInterfaceData>(interfaceData, () => new ComInterfaceData(interfaceData));

        /// <summary>
        ///     Converts the specified iface.
        /// </summary>
        /// <param name="iface">The iface.</param>
        /// <returns>IClrInterface.</returns>
        public IClrInterface Convert(ClrMd.ClrInterface iface) =>
            Cache.GetOrAdd<IClrInterface>(iface, () => new ClrInterfaceAdapter(iface));

        /// <summary>
        ///     Converts the specified information.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <returns>IClrInfo.</returns>
        public IClrInfo Convert(ClrMd.ClrInfo info) => Cache.GetOrAdd<IClrInfo>(info, () => new ClrInfoAdapter(info));

        /// <summary>
        ///     Converts the specified heap.
        /// </summary>
        /// <param name="heap">The heap.</param>
        /// <returns>IClrHeap.</returns>
        public IClrHeap Convert(ClrMd.ClrHeap heap) => Cache.GetOrAdd<IClrHeap>(heap, () => new HeapAdapter(heap));

        /// <summary>
        ///     Converts the specified memory region.
        /// </summary>
        /// <param name="memoryRegion">The memory region.</param>
        /// <returns>IClrMemoryRegion.</returns>
        public IClrMemoryRegion Convert(ClrMd.ClrMemoryRegion memoryRegion) =>
            Cache.GetOrAdd<IClrMemoryRegion>(memoryRegion, () => new MemoryRegionAdapter(memoryRegion));

        /// <summary>
        ///     Converts the specified handle.
        /// </summary>
        /// <param name="handle">The handle.</param>
        /// <returns>IClrHandle.</returns>
        public IClrHandle Convert(ClrMd.ClrHandle handle) =>
            Cache.GetOrAdd<IClrHandle>(handle, () => new HandleAdapter(handle));

        /// <summary>
        ///     Converts the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>ICcwData.</returns>
        public ICcwData Convert(ClrMd.CcwData data) => Cache.GetOrAdd<ICcwData>(data, () => new CcwDataAdapter(data));

        /// <summary>
        ///     Converts the specified application domain.
        /// </summary>
        /// <param name="appDomain">The application domain.</param>
        /// <returns>IClrAppDomain.</returns>
        public IClrAppDomain Convert(ClrMd.ClrAppDomain appDomain) =>
            Cache.GetOrAdd<IClrAppDomain>(appDomain, () => new ClrAppDomainAdapter(appDomain));

        /// <summary>
        ///     Converts the specified information.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <returns>IILInfo.</returns>
        public IILInfo Convert(ClrMd.ILInfo info) => Cache.GetOrAdd<IILInfo>(info, () => new IlInfoAdapter(info));

        /// <summary>
        ///     Converts the specified method.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns>IClrMethod.</returns>
        public IClrMethod Convert(ClrMd.ClrMethod method) =>
            Cache.GetOrAdd<IClrMethod>(method, () => new ClrMethodAdapter(method));

        /// <summary>
        ///     Converts the specified frame.
        /// </summary>
        /// <param name="frame">The frame.</param>
        /// <returns>IClrStackFrame.</returns>
        public IClrStackFrame Convert(ClrMd.ClrStackFrame frame) =>
            Cache.GetOrAdd<IClrStackFrame>(frame, () => new StackFrameAdapter(frame));

        /// <summary>
        ///     Converts the specified blocking reason.
        /// </summary>
        /// <param name="blockingReason">The blocking reason.</param>
        /// <returns>BlockingReason.</returns>
        /// <exception cref="ArgumentOutOfRangeException">blockingReason - The BlockingReason contract does not match CLRMd.</exception>
        public BlockingReason Convert(ClrMd.BlockingReason blockingReason)
        {
            switch (blockingReason)
            {
                case ClrMd.BlockingReason.None:
                    return BlockingReason.None;
                case ClrMd.BlockingReason.Unknown:
                    return BlockingReason.Unknown;
                case ClrMd.BlockingReason.Monitor:
                    return BlockingReason.Monitor;
                case ClrMd.BlockingReason.MonitorWait:
                    return BlockingReason.MonitorWait;
                case ClrMd.BlockingReason.WaitOne:
                    return BlockingReason.WaitOne;
                case ClrMd.BlockingReason.WaitAll:
                    return BlockingReason.WaitAll;
                case ClrMd.BlockingReason.WaitAny:
                    return BlockingReason.WaitAny;
                case ClrMd.BlockingReason.ThreadJoin:
                    return BlockingReason.ThreadJoin;
                case ClrMd.BlockingReason.ReaderAcquired:
                    return BlockingReason.ReaderAcquired;
                case ClrMd.BlockingReason.WriterAcquired:
                    return BlockingReason.WriterAcquired;
                default:
                    throw new ArgumentOutOfRangeException(nameof(blockingReason), blockingReason,
                        "The BlockingReason contract does not match CLRMd.");
            }
        }

        /// <summary>
        ///     Converts the specified map.
        /// </summary>
        /// <param name="map">The map.</param>
        /// <returns>ILToNativeMap.</returns>
        public ILToNativeMap Convert(ClrMd.ILToNativeMap map) => Cache.GetOrAdd(map, () => new ILToNativeMap
        {
            EndAddress = map.EndAddress,
            ILOffset = map.ILOffset,
            StartAddress = map.StartAddress
        });

        /// <summary>
        ///     Converts the specified color element type.
        /// </summary>
        /// <param name="clrElementType">Type of the color element.</param>
        /// <returns>ClrElementType.</returns>
        /// <exception cref="ArgumentOutOfRangeException">clrElementType - null</exception>
        public ClrElementType Convert(ClrMd.ClrElementType clrElementType)
        {
            switch (clrElementType)
            {
                case ClrMd.ClrElementType.Unknown:
                    return ClrElementType.Unknown;
                case ClrMd.ClrElementType.Boolean:
                    return ClrElementType.Boolean;
                case ClrMd.ClrElementType.Char:
                    return ClrElementType.Char;
                case ClrMd.ClrElementType.Int8:
                    return ClrElementType.Int8;
                case ClrMd.ClrElementType.UInt8:
                    return ClrElementType.UInt8;
                case ClrMd.ClrElementType.Int16:
                    return ClrElementType.Int16;
                case ClrMd.ClrElementType.UInt16:
                    return ClrElementType.UInt16;
                case ClrMd.ClrElementType.Int32:
                    return ClrElementType.Int32;
                case ClrMd.ClrElementType.UInt32:
                    return ClrElementType.UInt32;
                case ClrMd.ClrElementType.Int64:
                    return ClrElementType.Int64;
                case ClrMd.ClrElementType.UInt64:
                    return ClrElementType.UInt64;
                case ClrMd.ClrElementType.Float:
                    return ClrElementType.Float;
                case ClrMd.ClrElementType.Double:
                    return ClrElementType.Double;
                case ClrMd.ClrElementType.String:
                    return ClrElementType.String;
                case ClrMd.ClrElementType.Pointer:
                    return ClrElementType.Pointer;
                case ClrMd.ClrElementType.Struct:
                    return ClrElementType.Struct;
                case ClrMd.ClrElementType.Class:
                    return ClrElementType.Class;
                case ClrMd.ClrElementType.Array:
                    return ClrElementType.Array;
                case ClrMd.ClrElementType.NativeInt:
                    return ClrElementType.NativeInt;
                case ClrMd.ClrElementType.NativeUInt:
                    return ClrElementType.NativeUInt;
                case ClrMd.ClrElementType.FunctionPointer:
                    return ClrElementType.FunctionPointer;
                case ClrMd.ClrElementType.Object:
                    return ClrElementType.Object;
                case ClrMd.ClrElementType.SZArray:
                    return ClrElementType.SZArray;
                default:
                    throw new ArgumentOutOfRangeException(nameof(clrElementType), clrElementType, null);
            }
        }

        /// <summary>
        ///     Converts the specified flavor.
        /// </summary>
        /// <param name="flavor">The flavor.</param>
        /// <returns>ClrFlavor.</returns>
        /// <exception cref="ArgumentOutOfRangeException">flavor - null</exception>
        public ClrFlavor Convert(ClrMd.ClrFlavor flavor)
        {
            switch (flavor)
            {
                case ClrMd.ClrFlavor.Desktop:
                    return ClrFlavor.Desktop;
#pragma warning disable CS0612 // Type or member is obsolete
                case ClrMd.ClrFlavor.CoreCLR:
#pragma warning restore CS0612 // Type or member is obsolete
#pragma warning disable CS0612 // Type or member is obsolete
                    return ClrFlavor.CoreCLR;
#pragma warning restore CS0612 // Type or member is obsolete
#pragma warning disable CS0618 // Type or member is obsolete
                case ClrMd.ClrFlavor.Native:
#pragma warning restore CS0618 // Type or member is obsolete
#pragma warning disable 618
                    return ClrFlavor.Native;
#pragma warning restore 618
                case ClrMd.ClrFlavor.Core:
                    return ClrFlavor.Core;
                default:
                    throw new ArgumentOutOfRangeException(nameof(flavor), flavor, null);
            }
        }

        /// <summary>
        ///     Converts the specified memory region type.
        /// </summary>
        /// <param name="memoryRegionType">Type of the memory region.</param>
        /// <returns>ClrMemoryRegionType.</returns>
        /// <exception cref="ArgumentOutOfRangeException">memoryRegionType - null</exception>
        public ClrMemoryRegionType Convert(ClrMd.ClrMemoryRegionType memoryRegionType)
        {
            switch (memoryRegionType)
            {
                case ClrMd.ClrMemoryRegionType.LowFrequencyLoaderHeap:
                    return ClrMemoryRegionType.LowFrequencyLoaderHeap;
                case ClrMd.ClrMemoryRegionType.HighFrequencyLoaderHeap:
                    return ClrMemoryRegionType.HighFrequencyLoaderHeap;
                case ClrMd.ClrMemoryRegionType.StubHeap:
                    return ClrMemoryRegionType.StubHeap;
                case ClrMd.ClrMemoryRegionType.IndcellHeap:
                    return ClrMemoryRegionType.IndcellHeap;
                case ClrMd.ClrMemoryRegionType.LookupHeap:
                    return ClrMemoryRegionType.LookupHeap;
                case ClrMd.ClrMemoryRegionType.ResolveHeap:
                    return ClrMemoryRegionType.ResolveHeap;
                case ClrMd.ClrMemoryRegionType.DispatchHeap:
                    return ClrMemoryRegionType.DispatchHeap;
                case ClrMd.ClrMemoryRegionType.CacheEntryHeap:
                    return ClrMemoryRegionType.CacheEntryHeap;
                case ClrMd.ClrMemoryRegionType.JitHostCodeHeap:
                    return ClrMemoryRegionType.JitHostCodeHeap;
                case ClrMd.ClrMemoryRegionType.JitLoaderCodeHeap:
                    return ClrMemoryRegionType.JitLoaderCodeHeap;
                case ClrMd.ClrMemoryRegionType.ModuleThunkHeap:
                    return ClrMemoryRegionType.ModuleThunkHeap;
                case ClrMd.ClrMemoryRegionType.ModuleLookupTableHeap:
                    return ClrMemoryRegionType.ModuleLookupTableHeap;
                case ClrMd.ClrMemoryRegionType.GCSegment:
                    return ClrMemoryRegionType.GCSegment;
                case ClrMd.ClrMemoryRegionType.ReservedGCSegment:
                    return ClrMemoryRegionType.ReservedGCSegment;
                case ClrMd.ClrMemoryRegionType.HandleTableChunk:
                    return ClrMemoryRegionType.HandleTableChunk;
                default:
                    throw new ArgumentOutOfRangeException(nameof(memoryRegionType), memoryRegionType, null);
            }
        }

        /// <summary>
        ///     Converts the specified policy.
        /// </summary>
        /// <param name="policy">The policy.</param>
        /// <returns>ClrRootStackwalkPolicy.</returns>
        /// <exception cref="ArgumentOutOfRangeException">policy - null</exception>
        public ClrRootStackwalkPolicy Convert(ClrMd.ClrRootStackwalkPolicy policy)
        {
            switch (policy)
            {
                case ClrMd.ClrRootStackwalkPolicy.Automatic:
                    return ClrRootStackwalkPolicy.Automatic;
                case ClrMd.ClrRootStackwalkPolicy.Exact:
                    return ClrRootStackwalkPolicy.Exact;
                case ClrMd.ClrRootStackwalkPolicy.Fast:
                    return ClrRootStackwalkPolicy.Fast;
                case ClrMd.ClrRootStackwalkPolicy.SkipStack:
                    return ClrRootStackwalkPolicy.SkipStack;
                default:
                    throw new ArgumentOutOfRangeException(nameof(policy), policy, null);
            }
        }

        /// <summary>
        ///     Converts the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>ClrStackFrameType.</returns>
        /// <exception cref="ArgumentOutOfRangeException">type - null</exception>
        public ClrStackFrameType Convert(ClrMd.ClrStackFrameType type)
        {
            switch (type)
            {
                case ClrMd.ClrStackFrameType.Unknown:
                    return ClrStackFrameType.Unknown;
                case ClrMd.ClrStackFrameType.ManagedMethod:
                    return ClrStackFrameType.ManagedMethod;
                case ClrMd.ClrStackFrameType.Runtime:
                    return ClrStackFrameType.Runtime;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        /// <summary>
        ///     Converts the specified mode.
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <returns>GcMode.</returns>
        /// <exception cref="ArgumentOutOfRangeException">mode - null</exception>
        public GcMode Convert(ClrMd.GcMode mode)
        {
            switch (mode)
            {
                case ClrMd.GcMode.Cooperative:
                    return GcMode.Cooperative;
                case ClrMd.GcMode.Preemptive:
                    return GcMode.Preemptive;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }

        /// <summary>
        ///     Converts the specified kind.
        /// </summary>
        /// <param name="kind">The kind.</param>
        /// <returns>GcRootKind.</returns>
        /// <exception cref="ArgumentOutOfRangeException">kind - null</exception>
        public GcRootKind Convert(ClrMd.GCRootKind kind)
        {
            switch (kind)
            {
                case ClrMd.GCRootKind.StaticVar:
                    return GcRootKind.StaticVar;
                case ClrMd.GCRootKind.ThreadStaticVar:
                    return GcRootKind.ThreadStaticVar;
                case ClrMd.GCRootKind.LocalVar:
                    return GcRootKind.LocalVar;
                case ClrMd.GCRootKind.Strong:
                    return GcRootKind.Strong;
                case ClrMd.GCRootKind.Weak:
                    return GcRootKind.Weak;
                case ClrMd.GCRootKind.Pinning:
                    return GcRootKind.Pinning;
                case ClrMd.GCRootKind.Finalizer:
                    return GcRootKind.Finalizer;
                case ClrMd.GCRootKind.AsyncPinning:
                    return GcRootKind.AsyncPinning;
                default:
                    throw new ArgumentOutOfRangeException(nameof(kind), kind, null);
            }
        }

        /// <summary>
        ///     Converts the specified compilation type.
        /// </summary>
        /// <param name="compilationType">Type of the compilation.</param>
        /// <returns>MethodCompilationType.</returns>
        /// <exception cref="ArgumentOutOfRangeException">compilationType - null</exception>
        public MethodCompilationType Convert(ClrMd.MethodCompilationType compilationType)
        {
            switch (compilationType)
            {
                case ClrMd.MethodCompilationType.None:
                    return MethodCompilationType.None;
                case ClrMd.MethodCompilationType.Jit:
                    return MethodCompilationType.Jit;
                case ClrMd.MethodCompilationType.Ngen:
                    return MethodCompilationType.Ngen;
                default:
                    throw new ArgumentOutOfRangeException(nameof(compilationType), compilationType, null);
            }
        }

        /// <summary>
        ///     Converts the specified resolver.
        /// </summary>
        /// <param name="resolver">The resolver.</param>
        /// <returns>ISymbolResolver.</returns>
        public ISymbolResolver Convert(ClrMd.ISymbolResolver resolver) =>
            Cache.GetOrAdd<ISymbolResolver>(resolver, () => new SymbolResolverAdapter(resolver));

        /// <summary>
        ///     Converts the specified provider.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <returns>ISymbolProvider.</returns>
        public ISymbolProvider Convert(ClrMd.ISymbolProvider provider) =>
            Cache.GetOrAdd<ISymbolProvider>(provider, () => new SymbolProviderAdapter(provider));

        /// <summary>
        ///     Converts the specified locator.
        /// </summary>
        /// <param name="locator">The locator.</param>
        /// <returns>ISymbolLocator.</returns>
        public ISymbolLocator Convert(SymbolLocator locator) =>
            Cache.GetOrAdd<ISymbolLocator>(locator, () => new SymbolLocatorAdapter(locator));

        /// <summary>
        ///     Converts the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>IRootPath.</returns>
        public IRootPath Convert(ClrMd.RootPath path) =>
            Cache.GetOrAdd<IRootPath>(path, () => new RootPathAdapter(path));

        /// <summary>
        ///     Converts the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>IRcwData.</returns>
        public IRcwData Convert(ClrMd.RcwData data) => Cache.GetOrAdd<IRcwData>(data, () => new RcwDataAdapter(data));

        /// <summary>
        ///     Converts the specified pe file.
        /// </summary>
        /// <param name="peFile">The pe file.</param>
        /// <returns>IPeFile.</returns>
        public IPeFile Convert(PEFile peFile) => Cache.GetOrAdd<IPeFile>(peFile, () => new PeFileAdapter(peFile));

        /// <summary>
        ///     Converts the specified information.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <returns>IPdbInfo.</returns>
        public IPdbInfo Convert(ClrMd.PdbInfo info) => Cache.GetOrAdd<IPdbInfo>(info, () => new PdbInfoAdapter(info));

        /// <summary>
        ///     Converts the specified object set.
        /// </summary>
        /// <param name="objectSet">The object set.</param>
        /// <returns>IObjectSet.</returns>
        public IObjectSet Convert(ClrMd.ObjectSet objectSet) =>
            Cache.GetOrAdd<IObjectSet>(objectSet, () => new ObjectSetAdapter(objectSet));

        /// <summary>
        ///     Converts the specified native work item.
        /// </summary>
        /// <param name="nativeWorkItem">The native work item.</param>
        /// <returns>INativeWorkItem.</returns>
        public INativeWorkItem Convert(ClrMd.NativeWorkItem nativeWorkItem) =>
            Cache.GetOrAdd<INativeWorkItem>(nativeWorkItem, () => new NativeWorkItemAdapter(nativeWorkItem));

        /// <summary>
        ///     Converts the specified module information.
        /// </summary>
        /// <param name="moduleInfo">The module information.</param>
        /// <returns>IModuleInfo.</returns>
        public IModuleInfo Convert(ClrMd.ModuleInfo moduleInfo) =>
            Cache.GetOrAdd<IModuleInfo>(moduleInfo, () => new ModuleInfoAdapter(moduleInfo));

        /// <summary>
        ///     Converts the specified work item.
        /// </summary>
        /// <param name="workItem">The work item.</param>
        /// <returns>IManagedWorkItem.</returns>
        public IManagedWorkItem Convert(ClrMd.ManagedWorkItem workItem) =>
            Cache.GetOrAdd<IManagedWorkItem>(workItem, () => new ManagedWorkItemAdapter(workItem));

        /// <summary>
        ///     Converts the specified hot cold regions.
        /// </summary>
        /// <param name="hotColdRegions">The hot cold regions.</param>
        /// <returns>IHotColdRegions.</returns>
        public IHotColdRegions Convert(ClrMd.HotColdRegions hotColdRegions) =>
            Cache.GetOrAdd<IHotColdRegions>(hotColdRegions, () => new HotColdRegionsAdapter(hotColdRegions));

        /// <summary>
        ///     Converts the specified file version information.
        /// </summary>
        /// <param name="fileVersionInfo">The file version information.</param>
        /// <returns>IFileVersionInfo.</returns>
        public IFileVersionInfo Convert(FileVersionInfo fileVersionInfo) =>
            Cache.GetOrAdd<IFileVersionInfo>(fileVersionInfo, () => new FileVersionInfoAdapter(fileVersionInfo));

        /// <summary>
        ///     Converts the specified data target.
        /// </summary>
        /// <param name="dataTarget">The data target.</param>
        /// <returns>IDataTarget.</returns>
        public IDataTarget Convert(ClrMd.DataTarget dataTarget) =>
            Cache.GetOrAdd<IDataTarget>(dataTarget, () => new DataTargetAdapter(dataTarget));

        /// <summary>
        ///     Converts the specified data reader.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <returns>IDataReader.</returns>
        public IDataReader Convert(ClrMd.IDataReader dataReader) =>
            Cache.GetOrAdd<IDataReader>(dataReader, () => new DataReaderAdapter(dataReader));

        /// <summary>
        ///     Converts the specified dac information.
        /// </summary>
        /// <param name="dacInfo">The dac information.</param>
        /// <returns>IDacInfo.</returns>
        public IDacInfo Convert(ClrMd.DacInfo dacInfo) =>
            Cache.GetOrAdd<IDacInfo>(dacInfo, () => new DacInfoAdapter(dacInfo));

        /// <summary>
        ///     Converts the specified value class.
        /// </summary>
        /// <param name="valueClass">The value class.</param>
        /// <returns>IClrValueClass.</returns>
        public IClrValueClass Convert(ClrMd.ClrValueClass valueClass) =>
            Cache.GetOrAdd<IClrValueClass>(valueClass, () => new ClrValueClassAdapter(valueClass));

        /// <summary>
        ///     Converts the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>IClrType.</returns>
        public IClrType Convert(ClrMd.ClrType type) => Cache.GetOrAdd<IClrType>(type, () => new ClrTypeAdapter(type));

        /// <summary>
        ///     Converts the specified field.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns>IClrThreadStaticField.</returns>
        public IClrThreadStaticField Convert(ClrMd.ClrThreadStaticField field) =>
            Cache.GetOrAdd<IClrThreadStaticField>(field, () => new ClrThreadStaticFieldAdapter(field));

        /// <summary>
        ///     Converts the specified pool.
        /// </summary>
        /// <param name="pool">The pool.</param>
        /// <returns>IClrThreadPool.</returns>
        public IClrThreadPool Convert(ClrMd.ClrThreadPool pool) =>
            Cache.GetOrAdd<IClrThreadPool>(pool, () => new ClrThreadPoolAdapter(pool));

        /// <summary>
        ///     Converts the specified thread.
        /// </summary>
        /// <param name="thread">The thread.</param>
        /// <returns>IClrThread.</returns>
        public IClrThread Convert(ClrMd.ClrThread thread) =>
            Cache.GetOrAdd<IClrThread>(thread, () => new ClrThreadAdapter(thread));

        /// <summary>
        ///     Converts the specified field.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns>IClrStaticField.</returns>
        public IClrStaticField Convert(ClrMd.ClrStaticField field) =>
            Cache.GetOrAdd<IClrStaticField>(field, () => new ClrStaticFieldAdapter(field));

        /// <summary>
        ///     Converts the specified segment.
        /// </summary>
        /// <param name="segment">The segment.</param>
        /// <returns>IClrSegment.</returns>
        public IClrSegment Convert(ClrMd.ClrSegment segment) =>
            Cache.GetOrAdd<IClrSegment>(segment, () => new ClrSegmentAdapter(segment));

        /// <summary>
        ///     Converts the specified runtime.
        /// </summary>
        /// <param name="runtime">The runtime.</param>
        /// <returns>IClrRuntime.</returns>
        public IClrRuntime Convert(ClrMd.ClrRuntime runtime) =>
            Cache.GetOrAdd<IClrRuntime>(runtime, () => new ClrRuntimeAdapter(runtime));

        /// <summary>
        ///     Converts the specified root.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <returns>IClrRoot.</returns>
        public IClrRoot Convert(ClrMd.ClrRoot root) => Cache.GetOrAdd<IClrRoot>(root, () => new ClrRootAdapter(root));

        /// <summary>
        ///     Converts the specified o.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <returns>IClrObject.</returns>
        public IClrObject Convert(ClrMd.ClrObject o) => Cache.GetOrAdd<IClrObject>(o, () => new ClrObjectAdapter(o));

        /// <summary>
        ///     Converts the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns>IClrException.</returns>
        public IClrException Convert(ClrMd.ClrException exception) =>
            Cache.GetOrAdd<IClrException>(exception, () => new ClrExceptionAdapter(exception));

        /// <summary>
        ///     Converts the specified version information.
        /// </summary>
        /// <param name="versionInfo">The version information.</param>
        /// <returns>VersionInfo.</returns>
        public VersionInfo Convert(ClrMd.VersionInfo versionInfo) => new VersionInfo(versionInfo.Major,
            versionInfo.Minor, versionInfo.Revision, versionInfo.Patch);

        /// <summary>
        ///     Converts the specified root progress event.
        /// </summary>
        /// <param name="rootProgressEvent">The root progress event.</param>
        /// <returns>GcRootProgressEvent.</returns>
        public GcRootProgressEvent Convert(ClrMd.GCRootProgressEvent rootProgressEvent)
        {
            return (source, current, total) => rootProgressEvent((source as GcRootAdapter)?.Root, current, total);
        }

        /// <summary>
        ///     Converts the specified gc root.
        /// </summary>
        /// <param name="gcRoot">The gc root.</param>
        /// <returns>IGcRoot.</returns>
        public IGcRoot Convert(ClrMd.GCRoot gcRoot) => Cache.GetOrAdd<IGcRoot>(gcRoot, () => new GcRootAdapter(gcRoot));

        /// <summary>
        ///     Converts the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>VirtualQueryData.</returns>
        public VirtualQueryData Convert(ClrMd.VirtualQueryData data) =>
            Cache.GetOrAdd(data, () => new VirtualQueryData(data.BaseAddress, data.Size));

        /// <summary>
        ///     Converts the specified kind.
        /// </summary>
        /// <param name="kind">The kind.</param>
        /// <returns>WorkItemKind.</returns>
        /// <exception cref="ArgumentOutOfRangeException">kind - null</exception>
        public WorkItemKind Convert(ClrMd.WorkItemKind kind)
        {
            switch (kind)
            {
                case ClrMd.WorkItemKind.Unknown:
                    return WorkItemKind.Unknown;
                case ClrMd.WorkItemKind.AsyncTimer:
                    return WorkItemKind.AsyncTimer;
                case ClrMd.WorkItemKind.AsyncCallback:
                    return WorkItemKind.AsyncCallback;
                case ClrMd.WorkItemKind.QueueUserWorkItem:
                    return WorkItemKind.QueueUserWorkItem;
                case ClrMd.WorkItemKind.TimerDelete:
                    return WorkItemKind.TimerDelete;
                default:
                    throw new ArgumentOutOfRangeException(nameof(kind), kind, null);
            }
        }

        /// <summary>
        ///     Converts the specified handle type.
        /// </summary>
        /// <param name="handleType">Type of the handle.</param>
        /// <returns>HandleType.</returns>
        /// <exception cref="ArgumentOutOfRangeException">handleType - null</exception>
        public HandleType Convert(ClrMd.HandleType handleType)
        {
            switch (handleType)
            {
                case ClrMd.HandleType.WeakShort:
                    return HandleType.WeakShort;
                case ClrMd.HandleType.WeakLong:
                    return HandleType.WeakLong;
                case ClrMd.HandleType.Strong:
                    return HandleType.Strong;
                case ClrMd.HandleType.Pinned:
                    return HandleType.Pinned;
                case ClrMd.HandleType.RefCount:
                    return HandleType.RefCount;
                case ClrMd.HandleType.Dependent:
                    return HandleType.Dependent;
                case ClrMd.HandleType.AsyncPinned:
                    return HandleType.AsyncPinned;
                case ClrMd.HandleType.SizedRef:
                    return HandleType.SizedRef;
                default:
                    throw new ArgumentOutOfRangeException(nameof(handleType), handleType, null);
            }
        }

        [Import]
        internal IConverterCache Cache { get; set; }
    }
}