using System;
using System.ComponentModel.Composition;
using Triage.Mortician.Adapters;
using Triage.Mortician.Core.ClrMdAbstractions;
using ClrMd = Microsoft.Diagnostics.Runtime;

namespace Triage.Mortician
{
    [Export(typeof(IConverter))]
    internal class Converter : IConverter
    {
        public  Architecture Convert(ClrMd.Architecture architecture)
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

        public  GcSegmentType Convert(ClrMd.GCSegmentType segmentType)
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

        public  IClrInstanceField Convert(ClrMd.ClrInstanceField instanceField)
        {
            return new ClrInstanceFieldAdapter(instanceField);
        }

        public  IBlockingObject Convert(ClrMd.BlockingObject blockingObject)
        {
            return new BlockingObjectAdapter(blockingObject);
        }

        public  IClrModule Convert(ClrMd.ClrModule module)
        {
            return new ClrModuleAdapter(module);
        }

        public  IComInterfaceData Convert(ClrMd.ComInterfaceData interfaceData)
        {
            return new ComInterfaceData(interfaceData);
        }

        public  IClrInterface Convert(ClrMd.ClrInterface iface)
        {
            return new ClrInterfaceAdapter(iface);
        }

        public  IClrInfo Convert(ClrMd.ClrInfo info)
        {
            return new ClrInfoAdapter(info);
        }

        public  IClrHeap Convert(ClrMd.ClrHeap heap)
        {
            return new HeapAdapter(heap);
        }

        public  IClrMemoryRegion Convert(ClrMd.ClrMemoryRegion memoryRegion)
        {
            return new MemoryRegionAdapter(memoryRegion);
        }

        public  IClrHandle Convert(ClrMd.ClrHandle handle)
        {
            return new HandleAdapter(handle);
        }

        public  ICcwData Convert(ClrMd.CcwData data)
        {
            return new CcwDataAdapter(data);
        }

        public  IClrAppDomain Convert(ClrMd.ClrAppDomain appDomain)
        {
            return new ClrAppDomainAdapter(appDomain);
        }

        public  IILInfo Convert(ClrMd.ILInfo info)
        {
            return new IlInfoAdapter(info);
        }

        public  IClrMethod Convert(ClrMd.ClrMethod method)
        {
            return new ClrMethodAdapter(method);
        }

        public  IClrStackFrame Convert(ClrMd.ClrStackFrame frame)
        {
            return new StackFrameAdapter(frame);
        }

        public  BlockingReason Convert(ClrMd.BlockingReason blockingReason)
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

        public  ILToNativeMap Convert(ClrMd.ILToNativeMap map)
        {
            return new ILToNativeMap
            {
                EndAddress = map.EndAddress,
                ILOffset = map.ILOffset,
                StartAddress = map.StartAddress
            };
        }

        public  ClrElementType Convert(ClrMd.ClrElementType clrElementType)
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
        
        public  ClrFlavor Convert(ClrMd.ClrFlavor flavor)
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

        public  ClrMemoryRegionType Convert(ClrMd.ClrMemoryRegionType memoryRegionType)
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

        public  ClrRootStackwalkPolicy Convert(ClrMd.ClrRootStackwalkPolicy policy)
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

        public  ClrStackFrameType Convert(ClrMd.ClrStackFrameType type)
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

        public  GcMode Convert(ClrMd.GcMode mode)
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

        public  GcRootKind Convert(ClrMd.GCRootKind kind)
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

        public  MethodCompilationType Convert(ClrMd.MethodCompilationType compilationType)
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

        public  ISymbolResolver Convert(ClrMd.ISymbolResolver resolver)
        {
            return new SymbolResolverAdapter(resolver);
        }

        public  ISymbolProvider Convert(ClrMd.ISymbolProvider provider)
        {
            return new SymbolProviderAdapter(provider);
        }

        public  ISymbolLocator Convert(ClrMd.Utilities.SymbolLocator locator)
        {
            return new SymbolLocatorAdapter(locator);
        }

        public  IRootPath Convert(ClrMd.RootPath path)
        {
            return new RootPathAdapter(path);
        }

        public  IRcwData Convert(ClrMd.RcwData data)
        {
            return new RcwDataAdapter(data);
        }

        public  IPeFile Convert(ClrMd.Utilities.PEFile peFile)
        {
            return new PeFileAdapter(peFile);
        }

        public  IPdbInfo Convert(ClrMd.PdbInfo info)
        {
            return new PdbInfoAdapter(info);
        }

        public  IObjectSet Convert(ClrMd.ObjectSet objectSet)
        {
            return new ObjectSetAdapter(objectSet);
        }

        public  INativeWorkItem Convert(ClrMd.NativeWorkItem nativeWorkItem)
        {
            return new NativeWorkItemAdapter(nativeWorkItem);
        }

        public  IModuleInfo Convert(ClrMd.ModuleInfo moduleInfo)
        {
            return new ModuleInfoAdapter(moduleInfo);
        }

        public  IManagedWorkItem Convert(ClrMd.ManagedWorkItem workItem)
        {
            return new ManagedWorkItemAdapter(workItem);
        }

        public  IHotColdRegions Convert(ClrMd.HotColdRegions hotColdRegions)
        {
            return new HotColdRegionsAdapter(hotColdRegions);
        }

        public  IFileVersionInfo Convert(ClrMd.Utilities.FileVersionInfo fileVersionInfo)
        {
            return new FileVersionInfoAdapter(fileVersionInfo);
        }

        public  IDataTarget Convert(ClrMd.DataTarget dataTarget)
        {
            return new DataTargetAdapter(dataTarget);
        }

        public  IDataReader Convert(ClrMd.IDataReader dataReader)
        {
            return new DataReaderAdapter(dataReader);
        }

        public  IDacInfo Convert(ClrMd.DacInfo dacInfo)
        {
            return new DacInfoAdapter(dacInfo);
        }

        public  IClrValueClass Convert(ClrMd.ClrValueClass valueClass)
        {
            return new ClrValueClassAdapter(valueClass);
        }

        public  IClrType Convert(ClrMd.ClrType type)
        {
            return new ClrTypeAdapter(type);
        }

        public  IClrThreadStaticField Convert(ClrMd.ClrThreadStaticField field)
        {
            return new ClrThreadStaticFieldAdapter(field);
        }

        public  IClrThreadPool Convert(ClrMd.ClrThreadPool pool)
        {
            return new ClrThreadPoolAdapter(pool);
        }

        public  IClrThread Convert(ClrMd.ClrThread thread)
        {
            return new ClrThreadAdapter(thread);
        }

        public IClrStaticField Convert(ClrMd.ClrStaticField field)
        {
            return new ClrStaticFieldAdapter(field);
        }

        public  IClrSegment Convert(ClrMd.ClrSegment segment)
        {
            return new ClrSegmentAdapter(segment);
        }

        public  IClrRuntime Convert(ClrMd.ClrRuntime runtime)
        {
            return new ClrRuntimeAdapter(runtime);
        }

        public  IClrRoot Convert(ClrMd.ClrRoot root)
        {
            return new ClrRootAdapter(root);
        }

        public  IClrObject Convert(ClrMd.ClrObject o)
        {
            return new ClrObjectAdapter(o);
        }

        public  IClrException Convert(ClrMd.ClrException exception)
        {
            return new ClrExceptionAdapter(exception);
        }

        public  VersionInfo Convert(ClrMd.VersionInfo versionInfo)
        {
            return new VersionInfo(versionInfo.Major, versionInfo.Minor, versionInfo.Revision, versionInfo.Patch);
        }

        public  GcRootProgressEvent Convert(ClrMd.GCRootProgressEvent rootProgressEvent)
        {
            return (source, current, total) => rootProgressEvent((source as GcRootAdapter)?.Root, current, total);
        }

        public  IGcRoot Convert(ClrMd.GCRoot gcRoot)
        {
            return new GcRootAdapter(gcRoot);
        }

        public  VirtualQueryData Convert(ClrMd.VirtualQueryData data)
        {
            return new VirtualQueryData(data.BaseAddress, data.Size);
        }

        public  WorkItemKind Convert(ClrMd.WorkItemKind kind)
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

        public  HandleType Convert(ClrMd.HandleType handleType)
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
    }
}