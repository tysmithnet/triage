using System;
using Triage.Mortician.Adapters;
using Triage.Mortician.Core.ClrMdAbstractions;
using ClrMd = Microsoft.Diagnostics.Runtime;

namespace Triage.Mortician
{
    public static class Converter
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
                case ClrMd.ClrFlavor.CoreCLR:
                    return ClrFlavor.CoreCLR;
                case ClrMd.ClrFlavor.Native:
                    return ClrFlavor.Native;
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