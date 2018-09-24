// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-20-2018
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
        public IClrInstanceField Convert(ClrMd.ClrInstanceField instanceField)
        {
            if (instanceField == null) return null;
            var item = new ClrInstanceFieldAdapter(this, instanceField);
            return Cache.GetOrAdd<IClrInstanceField>(instanceField, () => item, () => item.Setup());
        }

        /// <summary>
        ///     Converts the specified blocking object.
        /// </summary>
        /// <param name="blockingObject">The blocking object.</param>
        /// <returns>IBlockingObject.</returns>
        public IBlockingObject Convert(ClrMd.BlockingObject blockingObject)
        {
            if (blockingObject == null) return null;
            var item = new BlockingObjectAdapter(this, blockingObject);
            return Cache.GetOrAdd<IBlockingObject>(blockingObject, () => item, () => item.Setup());
        }

        /// <summary>
        ///     Converts the specified module.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <returns>IClrModule.</returns>
        public IClrModule Convert(ClrMd.ClrModule module)
        {
            if (module == null) return null;
            var item = new ClrModuleAdapter(this, module);
            return Cache.GetOrAdd<IClrModule>(module, () => item, () => item.Setup());
        }

        /// <summary>
        ///     Converts the specified interface data.
        /// </summary>
        /// <param name="interfaceData">The interface data.</param>
        /// <returns>IComInterfaceData.</returns>
        public IComInterfaceData Convert(ClrMd.ComInterfaceData interfaceData)
        {
            if (interfaceData == null) return null;
            var item = new ComInterfaceDataAdapter(this, interfaceData);
            return Cache.GetOrAdd<IComInterfaceData>(interfaceData, () => item, () => item.Setup());
        }

        /// <summary>
        ///     Converts the specified iface.
        /// </summary>
        /// <param name="iface">The iface.</param>
        /// <returns>IClrInterface.</returns>
        public IClrInterface Convert(ClrMd.ClrInterface iface)
        {
            if (iface == null) return null;
            var item = new ClrInterfaceAdapter(this, iface);
            return Cache.GetOrAdd<IClrInterface>(iface, () => item, () => item.Setup());
        }

        /// <summary>
        ///     Converts the specified information.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <returns>IClrInfo.</returns>
        public IClrInfo Convert(ClrMd.ClrInfo info)
        {
            if (info == null) return null;
            var item = new ClrInfoAdapter(this, info);
            return Cache.GetOrAdd<IClrInfo>(info, () => item, () => item.Setup());
        }

        /// <summary>
        ///     Converts the specified heap.
        /// </summary>
        /// <param name="heap">The heap.</param>
        /// <returns>IClrHeap.</returns>
        public IClrHeap Convert(ClrMd.ClrHeap heap)
        {
            if (heap == null) return null;
            var item = new HeapAdapter(this, heap);
            return Cache.GetOrAdd<IClrHeap>(heap, () => item, () => item.Setup());
        }

        /// <summary>
        ///     Converts the specified memory region.
        /// </summary>
        /// <param name="memoryRegion">The memory region.</param>
        /// <returns>IClrMemoryRegion.</returns>
        public IClrMemoryRegion Convert(ClrMd.ClrMemoryRegion memoryRegion)
        {
            if (memoryRegion == null) return null;
            var item = new MemoryRegionAdapter(this, memoryRegion);
            return Cache.GetOrAdd<IClrMemoryRegion>(memoryRegion, () => item, () => item.Setup());
        }

        /// <summary>
        ///     Converts the specified handle.
        /// </summary>
        /// <param name="handle">The handle.</param>
        /// <returns>IClrHandle.</returns>
        public IClrHandle Convert(ClrMd.ClrHandle handle)
        {
            if (handle == null) return null;
            var item = new HandleAdapter(this, handle);
            return Cache.GetOrAdd<IClrHandle>(handle, () => item, () => item.Setup());
        }

        /// <summary>
        ///     Converts the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>ICcwData.</returns>
        public ICcwData Convert(ClrMd.CcwData data)
        {
            if (data == null) return null;
            var item = new CcwDataAdapter(this, data);
            return Cache.GetOrAdd<ICcwData>(data, () => item, () => item.Setup());
        }

        /// <summary>
        ///     Converts the specified application domain.
        /// </summary>
        /// <param name="appDomain">The application domain.</param>
        /// <returns>IClrAppDomain.</returns>
        public IClrAppDomain Convert(ClrMd.ClrAppDomain appDomain)
        {
            if (appDomain == null) return null;
            var item = new ClrAppDomainAdapter(this, appDomain);
            return Cache.GetOrAdd<IClrAppDomain>(appDomain, () => item, () => item.Setup());
        }

        /// <summary>
        ///     Converts the specified information.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <returns>IILInfo.</returns>
        public IILInfo Convert(ClrMd.ILInfo info)
        {
            if (info == null) return null;
            var item = new IlInfoAdapter(this, info);
            return Cache.GetOrAdd<IILInfo>(info, () => item, () => item.Setup());
        }

        /// <summary>
        ///     Converts the specified method.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns>IClrMethod.</returns>
        public IClrMethod Convert(ClrMd.ClrMethod method)
        {
            if (method == null) return null;
            var item = new ClrMethodAdapter(this, method);
            return Cache.GetOrAdd<IClrMethod>(method, () => item, () => item.Setup());
        }

        /// <summary>
        ///     Converts the specified frame.
        /// </summary>
        /// <param name="frame">The frame.</param>
        /// <returns>IClrStackFrame.</returns>
        public IClrStackFrame Convert(ClrMd.ClrStackFrame frame)
        {
            if (frame == null) return null;
            var item = new StackFrameAdapter(this, frame);
            return Cache.GetOrAdd<IClrStackFrame>(frame, () => item, () => item.Setup());
        }

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
        public ILToNativeMap Convert(ClrMd.ILToNativeMap map)
        {
            return Cache.GetOrAdd(map, () => new ILToNativeMap
            {
                EndAddress = map.EndAddress,
                ILOffset = map.ILOffset,
                StartAddress = map.StartAddress
            });
        }

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
        public ISymbolResolver Convert(ClrMd.ISymbolResolver resolver)
        {
            if (resolver == null) return null;
            var item = new SymbolResolverAdapter(this, resolver);
            return Cache.GetOrAdd<ISymbolResolver>(resolver, () => item, () => item.Setup());
        }

        /// <summary>
        ///     Converts the specified provider.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <returns>ISymbolProvider.</returns>
        public ISymbolProvider Convert(ClrMd.ISymbolProvider provider)
        {
            if (provider == null) return null;
            var item = new SymbolProviderAdapter(this, provider);
            return Cache.GetOrAdd<ISymbolProvider>(provider, () => item, () => item.Setup());
        }

        /// <summary>
        ///     Converts the specified locator.
        /// </summary>
        /// <param name="locator">The locator.</param>
        /// <returns>ISymbolLocator.</returns>
        public ISymbolLocator Convert(SymbolLocator locator)
        {
            if (locator == null) return null;
            var item = new SymbolLocatorAdapter(this, locator);
            return Cache.GetOrAdd<ISymbolLocator>(locator, () => item, () => item.Setup());
        }

        /// <summary>
        ///     Converts the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>IRootPath.</returns>
        public IRootPath Convert(ClrMd.RootPath path)
        {
            var item = new RootPathAdapter(this, path);
            return Cache.GetOrAdd<IRootPath>(path, () => item, () => item.Setup());
        }

        /// <summary>
        ///     Converts the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>IRcwData.</returns>
        public IRcwData Convert(ClrMd.RcwData data)
        {
            if (data == null) return null;
            var item = new RcwDataAdapter(this, data);
            return Cache.GetOrAdd<IRcwData>(data, () => item, () => item.Setup());
        }

        /// <summary>
        ///     Converts the specified pe file.
        /// </summary>
        /// <param name="peFile">The pe file.</param>
        /// <returns>IPeFile.</returns>
        public IPeFile Convert(PEFile peFile)
        {
            if (peFile == null) return null;
            var item = new PeFileAdapter(this, peFile);
            return Cache.GetOrAdd<IPeFile>(peFile, () => item, () => item.Setup());
        }

        /// <summary>
        ///     Converts the specified information.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <returns>IPdbInfo.</returns>
        public IPdbInfo Convert(ClrMd.PdbInfo info)
        {
            if (info == null) return null;
            var item = new PdbInfoAdapter(this, info);
            return Cache.GetOrAdd<IPdbInfo>(info, () => item, () => item.Setup());
        }

        /// <summary>
        ///     Converts the specified object set.
        /// </summary>
        /// <param name="objectSet">The object set.</param>
        /// <returns>IObjectSet.</returns>
        public IObjectSet Convert(ClrMd.ObjectSet objectSet)
        {
            if (objectSet == null) return null;
            var item = new ObjectSetAdapter(this, objectSet);
            return Cache.GetOrAdd<IObjectSet>(objectSet, () => item, () => item.Setup());
        }

        /// <summary>
        ///     Converts the specified native work item.
        /// </summary>
        /// <param name="nativeWorkItem">The native work item.</param>
        /// <returns>INativeWorkItem.</returns>
        public INativeWorkItem Convert(ClrMd.NativeWorkItem nativeWorkItem)
        {
            if (nativeWorkItem == null) return null;
            var item = new NativeWorkItemAdapter(this, nativeWorkItem);
            return Cache.GetOrAdd<INativeWorkItem>(nativeWorkItem, () => item, () => item.Setup());
        }

        /// <summary>
        ///     Converts the specified module information.
        /// </summary>
        /// <param name="moduleInfo">The module information.</param>
        /// <returns>IModuleInfo.</returns>
        public IModuleInfo Convert(ClrMd.ModuleInfo moduleInfo)
        {
            if (moduleInfo == null) return null;
            var item = new ModuleInfoAdapter(this, moduleInfo);
            return Cache.GetOrAdd<IModuleInfo>(moduleInfo, () => item, () => item.Setup());
        }

        /// <summary>
        ///     Converts the specified work item.
        /// </summary>
        /// <param name="workItem">The work item.</param>
        /// <returns>IManagedWorkItem.</returns>
        public IManagedWorkItem Convert(ClrMd.ManagedWorkItem workItem)
        {
            if (workItem == null) return null;
            var item = new ManagedWorkItemAdapter(this, workItem);
            return Cache.GetOrAdd<IManagedWorkItem>(workItem, () => item, () => item.Setup());
        }

        /// <summary>
        ///     Converts the specified hot cold regions.
        /// </summary>
        /// <param name="hotColdRegions">The hot cold regions.</param>
        /// <returns>IHotColdRegions.</returns>
        public IHotColdRegions Convert(ClrMd.HotColdRegions hotColdRegions)
        {
            if (hotColdRegions == null) return null;
            var item = new HotColdRegionsAdapter(this, hotColdRegions);
            return Cache.GetOrAdd<IHotColdRegions>(hotColdRegions, () => item, () => item.Setup());
        }

        /// <summary>
        ///     Converts the specified file version information.
        /// </summary>
        /// <param name="fileVersionInfo">The file version information.</param>
        /// <returns>IFileVersionInfo.</returns>
        public IFileVersionInfo Convert(FileVersionInfo fileVersionInfo)
        {
            if (fileVersionInfo == null) return null;
            var item = new FileVersionInfoAdapter(this, fileVersionInfo);
            return Cache.GetOrAdd<IFileVersionInfo>(fileVersionInfo, () => item, () => item.Setup());
        }

        /// <summary>
        ///     Converts the specified data target.
        /// </summary>
        /// <param name="dataTarget">The data target.</param>
        /// <returns>IDataTarget.</returns>
        public IDataTarget Convert(ClrMd.DataTarget dataTarget)
        {
            if (dataTarget == null) return null;
            var item = new DataTargetAdapter(this, dataTarget);
            return Cache.GetOrAdd<IDataTarget>(dataTarget, () => item, () => item.Setup());
        }

        /// <summary>
        ///     Converts the specified data reader.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <returns>IDataReader.</returns>
        public IDataReader Convert(ClrMd.IDataReader dataReader)
        {
            if (dataReader == null) return null;
            var item = new DataReaderAdapter(this, dataReader);
            return Cache.GetOrAdd<IDataReader>(dataReader, () => item, () => item.Setup());
        }

        /// <summary>
        ///     Converts the specified dac information.
        /// </summary>
        /// <param name="dacInfo">The dac information.</param>
        /// <returns>IDacInfo.</returns>
        public IDacInfo Convert(ClrMd.DacInfo dacInfo)
        {
            if (dacInfo == null) return null;
            var item = new DacInfoAdapter(this, dacInfo);
            return Cache.GetOrAdd<IDacInfo>(dacInfo, () => item, () => item.Setup());
        }

        /// <summary>
        ///     Converts the specified value class.
        /// </summary>
        /// <param name="valueClass">The value class.</param>
        /// <returns>IClrValueClass.</returns>
        public IClrValueClass Convert(ClrMd.ClrValueClass valueClass)
        {
            var item = new ClrValueClassAdapter(this, valueClass);
            return Cache.GetOrAdd<IClrValueClass>(valueClass, () => item, () => item.Setup());
        }

        /// <summary>
        ///     Converts the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>IClrType.</returns>
        public IClrType Convert(ClrMd.ClrType type)
        {
            if (type == null) return null;
            var item = new ClrTypeAdapter(this, type);
            return Cache.GetOrAdd<IClrType>(type, () => item, () => item.Setup());
        }

        /// <summary>
        ///     Converts the specified field.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns>IClrThreadStaticField.</returns>
        public IClrThreadStaticField Convert(ClrMd.ClrThreadStaticField field)
        {
            if (field == null) return null;
            var item = new ClrThreadStaticFieldAdapter(this, field);
            return Cache.GetOrAdd<IClrThreadStaticField>(field, () => item, () => item.Setup());
        }

        /// <summary>
        ///     Converts the specified pool.
        /// </summary>
        /// <param name="pool">The pool.</param>
        /// <returns>IClrThreadPool.</returns>
        public IClrThreadPool Convert(ClrMd.ClrThreadPool pool)
        {
            if (pool == null) return null;
            var item = new ClrThreadPoolAdapter(this, pool);
            return Cache.GetOrAdd<IClrThreadPool>(pool, () => item, () => item.Setup());
        }

        /// <summary>
        ///     Converts the specified thread.
        /// </summary>
        /// <param name="thread">The thread.</param>
        /// <returns>IClrThread.</returns>
        public IClrThread Convert(ClrMd.ClrThread thread)
        {
            if (thread == null) return null;
            var item = new ClrThreadAdapter(this, thread);
            return Cache.GetOrAdd<IClrThread>(thread, () => item, () => item.Setup());
        }

        /// <summary>
        ///     Converts the specified field.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns>IClrStaticField.</returns>
        public IClrStaticField Convert(ClrMd.ClrStaticField field)
        {
            if (field == null) return null;
            var item = new ClrStaticFieldAdapter(this, field);
            return Cache.GetOrAdd<IClrStaticField>(field, () => item, () => item.Setup());
        }

        /// <summary>
        ///     Converts the specified segment.
        /// </summary>
        /// <param name="segment">The segment.</param>
        /// <returns>IClrSegment.</returns>
        public IClrSegment Convert(ClrMd.ClrSegment segment)
        {
            if (segment == null) return null;
            var item = new ClrSegmentAdapter(this, segment);
            return Cache.GetOrAdd<IClrSegment>(segment, () => item, () => item.Setup());
        }

        /// <summary>
        ///     Converts the specified runtime.
        /// </summary>
        /// <param name="runtime">The runtime.</param>
        /// <returns>IClrRuntime.</returns>
        public IClrRuntime Convert(ClrMd.ClrRuntime runtime)
        {
            if (runtime == null) return null;
            var item = new ClrRuntimeAdapter(this, runtime);
            return Cache.GetOrAdd<IClrRuntime>(runtime, () => item, () => item.Setup());
        }

        /// <summary>
        ///     Converts the specified root.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <returns>IClrRoot.</returns>
        public IClrRoot Convert(ClrMd.ClrRoot root)
        {
            if (root == null) return null;
            var item = new ClrRootAdapter(this, root);
            return Cache.GetOrAdd<IClrRoot>(root, () => item, () => item.Setup());
        }

        /// <summary>
        ///     Converts the specified o.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <returns>IClrObject.</returns>
        public IClrObject Convert(ClrMd.ClrObject o)
        {
            if (o == null) return null;
            var item = new ClrObjectAdapter(this, o);
            return Cache.GetOrAdd<IClrObject>(o, () => item, () => item.Setup());
        }

        /// <summary>
        ///     Converts the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns>IClrException.</returns>
        public IClrException Convert(ClrMd.ClrException exception)
        {
            if (exception == null) return null;
            var item = new ClrExceptionAdapter(this, exception);
            return Cache.GetOrAdd<IClrException>(exception, () => item, () => item.Setup());
        }

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
        public IGcRoot Convert(ClrMd.GCRoot gcRoot)
        {
            if (gcRoot == null) return null;
            var item = new GcRootAdapter(this, gcRoot);
            return Cache.GetOrAdd<IGcRoot>(gcRoot, () => item, () => item.Setup());
        }

        /// <summary>
        ///     Converts the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>VirtualQueryData.</returns>
        public VirtualQueryData Convert(ClrMd.VirtualQueryData data)
        {
            return Cache.GetOrAdd(data, () => new VirtualQueryData(data.BaseAddress, data.Size));
        }

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

        /// <summary>
        ///     Gets or sets the cache.
        /// </summary>
        /// <value>The cache.</value>
        [Import]
        internal IConverterCache Cache { get; set; } = new DefaultConverterCache();
    }
}