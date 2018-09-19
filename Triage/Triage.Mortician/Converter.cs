using System;
using Triage.Mortician.Adapters;
using Triage.Mortician.Core.ClrMdAbstractions;
using ClrMd = Microsoft.Diagnostics.Runtime;

namespace Triage.Mortician
{
    internal class Converter
    {
        public static Architecture Convert(ClrMd.Architecture architecture)
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

        public static IClrInstanceField Convert(ClrMd.ClrInstanceField instanceField)
        {
            return new ClrInstanceFieldAdapter(instanceField);
        }

        public static IBlockingObject Convert(ClrMd.BlockingObject blockingObject)
        {
            return new BlockingObjectAdapter(blockingObject);
        }

        public static IClrModule Convert(ClrMd.ClrModule module)
        {
            return new ClrModuleAdapter(module);
        }

        public static IComInterfaceData Convert(ClrMd.ComInterfaceData interfaceData)
        {
            return new ComInterfaceData(interfaceData);
        }

        public static IClrInterface Convert(ClrMd.ClrInterface iface)
        {
            return new ClrInterfaceAdapter(iface);
        }

        public static IClrInfo Convert(ClrMd.ClrInfo info)
        {
            return new ClrInfoAdapter(info);
        }

        public static IClrHeap Convert(ClrMd.ClrHeap heap)
        {
            return new HeapAdapter(heap);
        }

        public static IClrMemoryRegion Convert(ClrMd.ClrMemoryRegion memoryRegion)
        {
            return new MemoryRegionAdapter(memoryRegion);
        }

        public static IClrHandle Convert(ClrMd.ClrHandle handle)
        {
            return new HandleAdapter(handle);
        }

        public static ICcwData Convert(ClrMd.CcwData data)
        {
            return new CcwDataAdapter(data);
        }

        public static IClrAppDomain Convert(ClrMd.ClrAppDomain appDomain)
        {
            return new ClrAppDomainAdapter(appDomain);
        }

        public static IILInfo Convert(ClrMd.ILInfo info)
        {
            return new IlInfoAdapter(info);
        }

        public static IClrMethod Convert(ClrMd.ClrMethod method)
        {
            return new ClrMethodAdapter(method);
        }

        public static IClrStackFrame Convert(ClrMd.ClrStackFrame frame)
        {
            return new StackFrameAdapter(frame);
        }

        public static BlockingReason Convert(ClrMd.BlockingReason blockingReason)
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

        public static ILToNativeMap Convert(ClrMd.ILToNativeMap map)
        {
            return new ILToNativeMap
            {
                EndAddress = map.EndAddress,
                ILOffset = map.ILOffset,
                StartAddress = map.StartAddress
            };
        }

        public static ClrElementType Convert(ClrMd.ClrElementType clrElementType)
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

        public static ClrFlavor Convert(ClrMd.ClrFlavor flavor)
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

        public static ClrMemoryRegionType Convert(ClrMd.ClrMemoryRegionType memoryRegionType)
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

        public static ClrRootStackwalkPolicy Convert(ClrMd.ClrRootStackwalkPolicy policy)
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

        public static ClrStackFrameType Convert(ClrMd.ClrStackFrameType type)
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

        public static GcMode Convert(ClrMd.GcMode mode)
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

        public static GcRootKind Convert(ClrMd.GCRootKind kind)
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

        public static MethodCompilationType Convert(ClrMd.MethodCompilationType compilationType)
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

        public static ISymbolResolver Convert(ClrMd.ISymbolResolver resolver)
        {
            return new SymbolResolverAdapter(resolver);
        }

        public static ISymbolProvider Convert(ClrMd.ISymbolProvider provider)
        {
            return new SymbolProviderAdapter(provider);
        }

        public static ISymbolLocator Convert(ClrMd.Utilities.SymbolLocator locator)
        {
            return new SymbolLocatorAdapter(locator);
        }

        public static IRootPath Convert(ClrMd.RootPath path)
        {
            return new RootPathAdapter(path);
        }

        public static IRcwData Convert(ClrMd.RcwData data)
        {
            return new RcwDataAdapter(data);
        }

        public static IPeFile Convert(ClrMd.Utilities.PEFile peFile)
        {
            return new PeFileAdapter(peFile);
        }

        public static IPdbInfo Convert(ClrMd.PdbInfo info)
        {
            return new PdbInfoAdapter(info);
        }

        public static IObjectSet Convert(ClrMd.ObjectSet objectSet)
        {
            return new ObjectSetAdapter(objectSet);
        }

        public static INativeWorkItem Convert(ClrMd.NativeWorkItem nativeWorkItem)
        {
            return new NativeWorkItemAdapter(nativeWorkItem);
        }

        public static IModuleInfo Convert(ClrMd.ModuleInfo moduleInfo)
        {
            return new ModuleInfoAdapter(moduleInfo);
        }

        public static IManagedWorkItem Convert(ClrMd.ManagedWorkItem workItem)
        {
            return new ManagedWorkItemAdapter(workItem);
        }

        public static IHotColdRegions Convert(ClrMd.HotColdRegions hotColdRegions)
        {
            return new HotColdRegionsAdapter(hotColdRegions);
        }

        public static IFileVersionInfo Convert(ClrMd.Utilities.FileVersionInfo fileVersionInfo)
        {
            return new FileVersionInfoAdapter(fileVersionInfo);
        }

        public static IDataTarget Convert(ClrMd.DataTarget dataTarget)
        {
            return new DataTargetAdapter(dataTarget);
        }

        public static IDataReader Convert(ClrMd.IDataReader dataReader)
        {
            return new DataReaderAdapter(dataReader);
        }

        public static IDacInfo Convert(ClrMd.DacInfo dacInfo)
        {
            return new DacInfoAdapter(dacInfo);
        }

        public static IClrValueClass Convert(ClrMd.ClrValueClass valueClass)
        {
            return new ClrValueClassAdapter(valueClass);
        }

        public static IClrType Convert(ClrMd.ClrType type)
        {
            return new ClrTypeAdapter(type);
        }

        public static IClrThreadStaticField Convert(ClrMd.ClrThreadStaticField field)
        {
            return new ClrThreadStaticFieldAdapter(field);
        }

        public static IClrThreadPool Convert(ClrMd.ClrThreadPool pool)
        {
            return new ClrThreadPoolAdapter(pool);
        }

        public static IClrThread Convert(ClrMd.ClrThread thread)
        {
            return new ClrThreadAdapter(thread);
        }

        public static IClrStaticField Convert(ClrMd.ClrStaticField staticField)
        {
            return new ClrStaticFieldAdapter(staticField);
        }

        public static IClrSegment Convert(ClrMd.ClrSegment segment)
        {
            return new ClrSegmentAdapter(segment);
        }

        public static IClrRuntime Convert(ClrMd.ClrRuntime runtime)
        {
            return new ClrRuntimeAdapter(runtime);
        }

        public static IClrRoot Convert(ClrMd.ClrRoot root)
        {
            return new ClrRootAdapter(root);
        }

        public static IClrObject Convert(ClrMd.ClrObject o)
        {
            return new ClrObjectAdapter(o);
        }

        public static IClrException Convert(ClrMd.ClrException exception)
        {
            return new ClrExceptionAdapter(exception);
        }

        public static VersionInfo Convert(ClrMd.VersionInfo versionInfo)
        {
            return new VersionInfo(versionInfo.Major, versionInfo.Minor, versionInfo.Revision, versionInfo.Patch);
        }

        public static GcRootProgressEvent Convert(ClrMd.GCRootProgressEvent rootProgressEvent)
        {
            return (source, current, total) => rootProgressEvent((source as GcRootAdapter)?._root, current, total);
        }

        public static IGcRoot Convert(ClrMd.GCRoot gcRoot)
        {
            return new GcRootAdapter(gcRoot);
        }

        public static VirtualQueryData Convert(ClrMd.VirtualQueryData data)
        {
            return new VirtualQueryData(data.BaseAddress, data.Size);
        }

        public static WorkItemKind Convert(ClrMd.WorkItemKind kind)
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

        public static HandleType Convert(ClrMd.HandleType handleType)
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