using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Diagnostics.Runtime.Utilities;
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

    internal class ClrExceptionAdapter : IClrException
    {
        /// <inheritdoc />
        public ClrExceptionAdapter(ClrMd.ClrException exception)
        {
            _exception = exception ?? throw new ArgumentNullException(nameof(exception));
        }

        internal ClrMd.ClrException _exception;

        /// <inheritdoc />
        public ulong Address { get; }

        /// <inheritdoc />
        public int HResult { get; }

        /// <inheritdoc />
        public IClrException Inner { get; }

        /// <inheritdoc />
        public string Message { get; }

        /// <inheritdoc />
        public IList<IClrStackFrame> StackTrace { get; }

        /// <inheritdoc />
        public IClrType Type { get; }
    }

    internal class SymbolResolverAdapter : ISymbolResolver
    {
        /// <inheritdoc />
        public SymbolResolverAdapter(ClrMd.ISymbolResolver resolver)
        {
            _resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
        }

        internal ClrMd.ISymbolResolver _resolver;

        /// <inheritdoc />
        public string GetSymbolNameByRVA(uint rva)
        {
            throw new NotImplementedException();
        }
    }

    internal class SymbolProviderAdapter : ISymbolProvider
    {
        /// <inheritdoc />
        public SymbolProviderAdapter(ClrMd.ISymbolProvider provider)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        internal ClrMd.ISymbolProvider _provider;

        /// <inheritdoc />
        public ISymbolResolver GetSymbolResolver(string pdbName, Guid guid, int age)
        {
            throw new NotImplementedException();
        }
    }

    internal class SymbolLocatorAdapter : ISymbolLocator
    {
        /// <inheritdoc />
        public SymbolLocatorAdapter(SymbolLocator locator)
        {
            _locator = locator ?? throw new ArgumentNullException(nameof(locator));
        }

        internal SymbolLocator _locator;

        /// <inheritdoc />
        public string FindBinary(string fileName, uint buildTimeStamp, uint imageSize, bool checkProperties = true)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public string FindBinary(string fileName, int buildTimeStamp, int imageSize, bool checkProperties = true)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public string FindBinary(IModuleInfo module, bool checkProperties = true)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public string FindBinary(IDacInfo dac)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<string> FindBinaryAsync(string fileName, uint buildTimeStamp, uint imageSize,
            bool checkProperties = true)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<string> FindBinaryAsync(string fileName, int buildTimeStamp, int imageSize,
            bool checkProperties = true)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<string> FindBinaryAsync(IModuleInfo module, bool checkProperties = true)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<string> FindBinaryAsync(IDacInfo dac)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public string FindPdb(IModuleInfo module)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public string FindPdb(IPdbInfo pdb)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public string FindPdb(string pdbName, Guid pdbIndexGuid, int pdbIndexAge)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<string> FindPdbAsync(IModuleInfo module)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<string> FindPdbAsync(IPdbInfo pdb)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<string> FindPdbAsync(string pdbName, Guid pdbIndexGuid, int pdbIndexAge)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public string SymbolCache { get; set; }

        /// <inheritdoc />
        public string SymbolPath { get; set; }

        /// <inheritdoc />
        public int Timeout { get; set; }
    }

    internal class RootPathAdapter : IRootPath
    {
        /// <inheritdoc />
        public RootPathAdapter(ClrMd.RootPath rootPath)
        {
            _rootPath = rootPath;
        }

        internal ClrMd.RootPath _rootPath;

        /// <inheritdoc />
        public IClrObject[] Path { get; set; }

        /// <inheritdoc />
        public IClrRoot Root { get; set; }
    }

    internal class RcwDataAdapter : IRcwData
    {
        /// <inheritdoc />
        public RcwDataAdapter(ClrMd.RcwData rcwData)
        {
            _rcwData = rcwData ?? throw new ArgumentNullException(nameof(rcwData));
        }

        internal ClrMd.RcwData _rcwData;

        /// <inheritdoc />
        public uint CreatorThread { get; }

        /// <inheritdoc />
        public bool Disconnected { get; }

        /// <inheritdoc />
        public IList<IComInterfaceData> Interfaces { get; }

        /// <inheritdoc />
        public ulong IUnknown { get; }

        /// <inheritdoc />
        public ulong Object { get; }

        /// <inheritdoc />
        public int RefCount { get; }

        /// <inheritdoc />
        public ulong VTablePointer { get; }

        /// <inheritdoc />
        public ulong WinRTObject { get; }
    }

    internal class PeFileAdapter : IPeFile
    {
        /// <inheritdoc />
        public PeFileAdapter(PEFile peFile)
        {
            _peFile = peFile ?? throw new ArgumentNullException(nameof(peFile));
        }

        internal PEFile _peFile;

        /// <inheritdoc />
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IFileVersionInfo GetFileVersionInfo()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool GetPdbSignature(out string pdbName, out Guid pdbGuid, out int pdbAge, bool first = false)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public string GetSxSManfest()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool Disposed { get; }

        /// <inheritdoc />
        public IPdbInfo PdbInfo { get; }
    }

    internal class PdbInfoAdapter : IPdbInfo
    {
        /// <inheritdoc />
        public PdbInfoAdapter(ClrMd.PdbInfo pdbInfo)
        {
            _pdbInfo = pdbInfo ?? throw new ArgumentNullException(nameof(pdbInfo));
        }

        internal ClrMd.PdbInfo _pdbInfo;

        /// <inheritdoc />
        public string FileName { get; set; }

        /// <inheritdoc />
        public Guid Guid { get; set; }

        /// <inheritdoc />
        public int Revision { get; set; }
    }

    internal class ObjectSetAdapter : IObjectSet
    {
        /// <inheritdoc />
        public ObjectSetAdapter(ClrMd.ObjectSet objectSet)
        {
            _objectSet = objectSet ?? throw new ArgumentNullException(nameof(objectSet));
        }

        internal ClrMd.ObjectSet _objectSet;

        /// <inheritdoc />
        public bool Add(ulong obj)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void Clear()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool Contains(ulong obj)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool Remove(ulong obj)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public int Count { get; }
    }

    internal class NativeWorkItemAdapter : INativeWorkItem
    {
        /// <inheritdoc />
        public NativeWorkItemAdapter(ClrMd.NativeWorkItem nativeWorkItem)
        {
            _nativeWorkItem = nativeWorkItem ?? throw new ArgumentNullException(nameof(nativeWorkItem));
        }

        internal ClrMd.NativeWorkItem _nativeWorkItem;

        /// <inheritdoc />
        public ulong Callback { get; }

        /// <inheritdoc />
        public ulong Data { get; }

        /// <inheritdoc />
        public WorkItemKind Kind { get; }
    }

    internal class ModuleInfoAdapter : IModuleInfo
    {
        /// <inheritdoc />
        public ModuleInfoAdapter(ClrMd.ModuleInfo moduleInfo)
        {
            _moduleInfo = moduleInfo ?? throw new ArgumentNullException(nameof(moduleInfo));
        }

        internal ClrMd.ModuleInfo _moduleInfo;

        /// <inheritdoc />
        public IPeFile GetPEFile()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public string FileName { get; set; }

        /// <inheritdoc />
        public uint FileSize { get; set; }

        /// <inheritdoc />
        public ulong ImageBase { get; set; }

        /// <inheritdoc />
        public bool IsManaged { get; }

        /// <inheritdoc />
        public bool IsRuntime { get; }

        /// <inheritdoc />
        public IPdbInfo Pdb { get; set; }

        /// <inheritdoc />
        public uint TimeStamp { get; set; }

        /// <inheritdoc />
        public VersionInfo Version { get; set; }
    }

    internal class ManagedWorkItemAdapter : IManagedWorkItem
    {
        /// <inheritdoc />
        public ManagedWorkItemAdapter(ClrMd.ManagedWorkItem workItem)
        {
            _workItem = workItem ?? throw new ArgumentNullException(nameof(workItem));
        }

        internal ClrMd.ManagedWorkItem _workItem;

        /// <inheritdoc />
        public ulong Object { get; }

        /// <inheritdoc />
        public IClrType Type { get; }
    }

    internal class IlInfoAdapter : IILInfo
    {
        /// <inheritdoc />
        public IlInfoAdapter(ClrMd.ILInfo info)
        {
            _info = info ?? throw new ArgumentNullException(nameof(info));
        }

        internal ClrMd.ILInfo _info;

        /// <inheritdoc />
        public ulong Address { get; }

        /// <inheritdoc />
        public uint Flags { get; }

        /// <inheritdoc />
        public int Length { get; }

        /// <inheritdoc />
        public uint LocalVarSignatureToken { get; }

        /// <inheritdoc />
        public int MaxStack { get; }
    }

    internal class HotColdRegionsAdapter : IHotColdRegions
    {
        /// <inheritdoc />
        public HotColdRegionsAdapter(ClrMd.HotColdRegions hotColdRegions)
        {
            _hotColdRegions = hotColdRegions ?? throw new ArgumentNullException(nameof(hotColdRegions));
        }

        internal ClrMd.HotColdRegions _hotColdRegions;

        /// <inheritdoc />
        public uint ColdSize { get; }

        /// <inheritdoc />
        public ulong ColdStart { get; }

        /// <inheritdoc />
        public uint HotSize { get; }

        /// <inheritdoc />
        public ulong HotStart { get; }
    }

    internal class FileVersionInfoAdapter : IFileVersionInfo
    {
        /// <inheritdoc />
        public FileVersionInfoAdapter(FileVersionInfo fileVersionInfo)
        {
            _fileVersionInfo = fileVersionInfo ?? throw new ArgumentNullException(nameof(fileVersionInfo));
        }

        private FileVersionInfo _fileVersionInfo;

        /// <inheritdoc />
        public string Comments { get; }

        /// <inheritdoc />
        public string FileVersion { get; }
    }

    internal class DataTargetAdapter : IDataTarget
    {
        /// <inheritdoc />
        public DataTargetAdapter(ClrMd.DataTarget dataTarget)
        {
            _dataTarget = dataTarget ?? throw new ArgumentNullException(nameof(dataTarget));
        }

        internal ClrMd.DataTarget _dataTarget;

        /// <inheritdoc />
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IEnumerable<IModuleInfo> EnumerateModules()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool ReadProcessMemory(ulong address, byte[] buffer, int bytesRequested, out int bytesRead)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Architecture Architecture { get; }

        /// <inheritdoc />
        public IList<IClrInfo> ClrVersions { get; }

        /// <inheritdoc />
        public IDataReader DataReader { get; }

        /// <inheritdoc />
        public bool IsMinidump { get; }

        /// <inheritdoc />
        public uint PointerSize { get; }

        /// <inheritdoc />
        public ISymbolLocator SymbolLocator { get; set; }

        /// <inheritdoc />
        public ISymbolProvider SymbolProvider { get; set; }
    }

    internal class DataReaderAdapter : IDataReader
    {
        /// <inheritdoc />
        public DataReaderAdapter(ClrMd.IDataReader dataReader)
        {
            _dataReader = dataReader ?? throw new ArgumentNullException(nameof(dataReader));
        }

        internal ClrMd.IDataReader _dataReader;

        /// <inheritdoc />
        public void Close()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IEnumerable<uint> EnumerateAllThreads()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IList<IModuleInfo> EnumerateModules()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void Flush()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Architecture GetArchitecture()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public uint GetPointerSize()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool GetThreadContext(uint threadID, uint contextFlags, uint contextSize, IntPtr context)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool GetThreadContext(uint threadID, uint contextFlags, uint contextSize, byte[] context)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ulong GetThreadTeb(uint thread)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void GetVersionInfo(ulong baseAddress, out VersionInfo version)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public uint ReadDwordUnsafe(ulong addr)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool ReadMemory(ulong address, byte[] buffer, int bytesRequested, out int bytesRead)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool ReadMemory(ulong address, IntPtr buffer, int bytesRequested, out int bytesRead)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ulong ReadPointerUnsafe(ulong addr)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool VirtualQuery(ulong addr, out VirtualQueryData vq)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool IsMinidump { get; }
    }

    internal class DacInfoAdapter : IDacInfo
    {
        /// <inheritdoc />
        public DacInfoAdapter(ClrMd.DacInfo dacInfo)
        {
            _dacInfo = dacInfo ?? throw new ArgumentNullException(nameof(dacInfo));
        }

        internal ClrMd.DacInfo _dacInfo;

        /// <inheritdoc />
        public string FileName { get; set; }

        /// <inheritdoc />
        public uint FileSize { get; set; }

        /// <inheritdoc />
        public ulong ImageBase { get; set; }

        /// <inheritdoc />
        public bool IsManaged { get; }

        /// <inheritdoc />
        public bool IsRuntime { get; }

        /// <inheritdoc />
        public IPdbInfo Pdb { get; set; }

        /// <inheritdoc />
        public string PlatformAgnosticFileName { get; set; }

        /// <inheritdoc />
        public Architecture TargetArchitecture { get; set; }

        /// <inheritdoc />
        public uint TimeStamp { get; set; }

        /// <inheritdoc />
        public VersionInfo Version { get; set; }
    }

    internal class ComInterfaceData : IComInterfaceData
    {
        /// <inheritdoc />
        public ComInterfaceData(ClrMd.ComInterfaceData comInterfaceData)
        {
            _comInterfaceData = comInterfaceData ?? throw new ArgumentNullException(nameof(comInterfaceData));
        }

        internal ClrMd.ComInterfaceData _comInterfaceData;

        /// <inheritdoc />
        public ulong InterfacePointer { get; }

        /// <inheritdoc />
        public IClrType Type { get; }
    }

    internal class ClrValueClassAdapter : IClrValueClass
    {
        /// <inheritdoc />
        public ClrValueClassAdapter(ClrMd.ClrValueClass valueClass)
        {
            _valueClass = valueClass;
        }

        internal ClrMd.ClrValueClass _valueClass;

        /// <inheritdoc />
        public T GetField<T>(string fieldName) where T : struct
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IClrObject GetObjectField(string fieldName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public string GetStringField(string fieldName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IClrValueClass GetValueClassField(string fieldName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ulong Address { get; }

        /// <inheritdoc />
        public string HexAddress { get; }

        /// <inheritdoc />
        public IClrType Type { get; }
    }

    internal class ClrTypeAdapter : IClrType
    {
        /// <inheritdoc />
        public ClrTypeAdapter(ClrMd.ClrThread clrThread)
        {
            _clrThread = clrThread ?? throw new ArgumentNullException(nameof(clrThread));
        }

        internal ClrMd.ClrThread _clrThread;

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

    internal class ClrThreadStaticFieldAdapter : IClrThreadStaticField
    {
        /// <inheritdoc />
        public ClrThreadStaticFieldAdapter(ClrMd.ClrThreadStaticField threadStaticField)
        {
            _threadStaticField = threadStaticField ?? throw new ArgumentNullException(nameof(threadStaticField));
        }

        internal ClrMd.ClrThreadStaticField _threadStaticField;

        /// <inheritdoc />
        public ulong GetAddress(IClrAppDomain appDomain, IClrThread thread)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public object GetValue(IClrAppDomain appDomain, IClrThread thread)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public object GetValue(IClrAppDomain appDomain, IClrThread thread, bool convertStrings)
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

    internal class ClrThreadPoolAdapter : IClrThreadPool
    {
        /// <inheritdoc />
        public ClrThreadPoolAdapter(ClrMd.ClrThreadPool threadPool)
        {
            _threadPool = threadPool ?? throw new ArgumentNullException(nameof(threadPool));
        }

        internal ClrMd.ClrThreadPool _threadPool;

        /// <inheritdoc />
        public IEnumerable<IManagedWorkItem> EnumerateManagedWorkItems()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IEnumerable<INativeWorkItem> EnumerateNativeWorkItems()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public int CpuUtilization { get; }

        /// <inheritdoc />
        public int FreeCompletionPortCount { get; }

        /// <inheritdoc />
        public int IdleThreads { get; }

        /// <inheritdoc />
        public int MaxCompletionPorts { get; }

        /// <inheritdoc />
        public int MaxFreeCompletionPorts { get; }

        /// <inheritdoc />
        public int MaxThreads { get; }

        /// <inheritdoc />
        public int MinCompletionPorts { get; }

        /// <inheritdoc />
        public int MinThreads { get; }

        /// <inheritdoc />
        public int RunningThreads { get; }

        /// <inheritdoc />
        public int TotalThreads { get; }
    }

    internal class ClrThreadAdapter : IClrThread
    {
        /// <inheritdoc />
        public ClrThreadAdapter(ClrMd.ClrThread thread)
        {
            _thread = thread ?? throw new ArgumentNullException(nameof(thread));
        }

        internal ClrMd.ClrThread _thread;

        /// <inheritdoc />
        public IEnumerable<IClrRoot> EnumerateStackObjects()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IEnumerable<IClrRoot> EnumerateStackObjects(bool includePossiblyDead)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IEnumerable<IClrStackFrame> EnumerateStackTrace()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ulong Address { get; }

        /// <inheritdoc />
        public ulong AppDomain { get; }

        /// <inheritdoc />
        public IList<IBlockingObject> BlockingObjects { get; }

        /// <inheritdoc />
        public IClrException CurrentException { get; }

        /// <inheritdoc />
        public GcMode GcMode { get; }

        /// <inheritdoc />
        public bool IsAborted { get; }

        /// <inheritdoc />
        public bool IsAbortRequested { get; }

        /// <inheritdoc />
        public bool IsAlive { get; }

        /// <inheritdoc />
        public bool IsBackground { get; }

        /// <inheritdoc />
        public bool IsCoInitialized { get; }

        /// <inheritdoc />
        public bool IsDebuggerHelper { get; }

        /// <inheritdoc />
        public bool IsDebugSuspended { get; }

        /// <inheritdoc />
        public bool IsFinalizer { get; }

        /// <inheritdoc />
        public bool IsGC { get; }

        /// <inheritdoc />
        public bool IsGCSuspendPending { get; }

        /// <inheritdoc />
        public bool IsMTA { get; }

        /// <inheritdoc />
        public bool IsShutdownHelper { get; }

        /// <inheritdoc />
        public bool IsSTA { get; }

        /// <inheritdoc />
        public bool IsSuspendingEE { get; }

        /// <inheritdoc />
        public bool IsThreadpoolCompletionPort { get; }

        /// <inheritdoc />
        public bool IsThreadpoolGate { get; }

        /// <inheritdoc />
        public bool IsThreadpoolTimer { get; }

        /// <inheritdoc />
        public bool IsThreadpoolWait { get; }

        /// <inheritdoc />
        public bool IsThreadpoolWorker { get; }

        /// <inheritdoc />
        public bool IsUnstarted { get; }

        /// <inheritdoc />
        public bool IsUserSuspended { get; }

        /// <inheritdoc />
        public uint LockCount { get; }

        /// <inheritdoc />
        public int ManagedThreadId { get; }

        /// <inheritdoc />
        public uint OSThreadId { get; }

        /// <inheritdoc />
        public IClrRuntime Runtime { get; }

        /// <inheritdoc />
        public ulong StackBase { get; }

        /// <inheritdoc />
        public ulong StackLimit { get; }

        /// <inheritdoc />
        public IList<IClrStackFrame> StackTrace { get; }

        /// <inheritdoc />
        public ulong Teb { get; }
    }

    internal class ClrStaticFieldAdapter : IClrStaticField
    {
        /// <inheritdoc />
        public ClrStaticFieldAdapter(ClrMd.ClrStaticField staticField)
        {
            _staticField = staticField ?? throw new ArgumentNullException(nameof(staticField));
        }

        internal ClrMd.ClrStaticField _staticField;

        /// <inheritdoc />
        public ulong GetAddress(IClrAppDomain appDomain)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public object GetDefaultValue()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public object GetValue(IClrAppDomain appDomain)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public object GetValue(IClrAppDomain appDomain, bool convertStrings)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool IsInitialized(IClrAppDomain appDomain)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ClrElementType ElementType { get; }

        /// <inheritdoc />
        public bool HasDefaultValue { get; }

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

    internal class ClrStackFrame : IClrStackFrame
    {
        /// <inheritdoc />
        public ClrStackFrame(ClrMd.ClrStackFrame stackFrame)
        {
            _stackFrame = stackFrame ?? throw new ArgumentNullException(nameof(stackFrame));
        }

        internal ClrMd.ClrStackFrame _stackFrame;

        /// <inheritdoc />
        public string DisplayString { get; }

        /// <inheritdoc />
        public ulong InstructionPointer { get; }

        /// <inheritdoc />
        public ClrStackFrameType Kind { get; }

        /// <inheritdoc />
        public IClrMethod Method { get; }

        /// <inheritdoc />
        public string ModuleName { get; }

        /// <inheritdoc />
        public ulong StackPointer { get; }

        /// <inheritdoc />
        public IClrThread Thread { get; }
    }

    internal class ClrSegmentAdapter : IClrSegment
    {
        /// <inheritdoc />
        public ClrSegmentAdapter(ClrMd.ClrSegment segment)
        {
            _segment = segment ?? throw new ArgumentNullException(nameof(segment));
        }

        internal ClrMd.ClrSegment _segment;

        /// <inheritdoc />
        public IEnumerable<ulong> EnumerateObjectAddresses()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ulong GetFirstObject(out IClrType type)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public int GetGeneration(ulong obj)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ulong NextObject(ulong objRef)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ulong NextObject(ulong objRef, out IClrType type)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ulong CommittedEnd { get; }

        /// <inheritdoc />
        public ulong End { get; }

        /// <inheritdoc />
        public ulong FirstObject { get; }

        /// <inheritdoc />
        public ulong Gen0Length { get; }

        /// <inheritdoc />
        public ulong Gen0Start { get; }

        /// <inheritdoc />
        public ulong Gen1Length { get; }

        /// <inheritdoc />
        public ulong Gen1Start { get; }

        /// <inheritdoc />
        public ulong Gen2Length { get; }

        /// <inheritdoc />
        public ulong Gen2Start { get; }

        /// <inheritdoc />
        public IClrHeap Heap { get; }

        /// <inheritdoc />
        public bool IsEphemeral { get; }

        /// <inheritdoc />
        public bool IsLarge { get; }

        /// <inheritdoc />
        public ulong Length { get; }

        /// <inheritdoc />
        public int ProcessorAffinity { get; }

        /// <inheritdoc />
        public ulong ReservedEnd { get; }

        /// <inheritdoc />
        public ulong Start { get; }
    }

    internal class ClrRuntimeAdapter : IClrRuntime
    {
        /// <inheritdoc />
        public ClrRuntimeAdapter(ClrMd.ClrRuntime runtime)
        {
            _runtime = runtime ?? throw new ArgumentNullException(nameof(runtime));
        }

        internal ClrMd.ClrRuntime _runtime;

        /// <inheritdoc />
        public IEnumerable<ulong> EnumerateFinalizerQueueObjectAddresses()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IEnumerable<int> EnumerateGCThreads()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IEnumerable<IClrHandle> EnumerateHandles()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IEnumerable<IClrMemoryRegion> EnumerateMemoryRegions()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IEnumerable<IClrException> EnumerateSerializedExceptions()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void Flush()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ICcwData GetCcwDataByAddress(ulong addr)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IClrHeap GetHeap()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IClrMethod GetMethodByAddress(ulong ip)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IClrMethod GetMethodByHandle(ulong methodHandle)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IClrThreadPool GetThreadPool()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool ReadMemory(ulong address, byte[] buffer, int bytesRequested, out int bytesRead)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool ReadPointer(ulong address, out ulong value)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IList<IClrAppDomain> AppDomains { get; }

        /// <inheritdoc />
        public IClrInfo ClrInfo { get; }

        /// <inheritdoc />
        public IDataTarget DataTarget { get; }

        /// <inheritdoc />
        public IClrHeap Heap { get; }

        /// <inheritdoc />
        public int HeapCount { get; }

        /// <inheritdoc />
        public IList<IClrModule> Modules { get; }

        /// <inheritdoc />
        public int PointerSize { get; }

        /// <inheritdoc />
        public bool ServerGC { get; }

        /// <inheritdoc />
        public IClrAppDomain SharedDomain { get; }

        /// <inheritdoc />
        public IClrAppDomain SystemDomain { get; }

        /// <inheritdoc />
        public IClrThreadPool ThreadPool { get; }

        /// <inheritdoc />
        public IList<IClrThread> Threads { get; }
    }

    internal class ClrRootAdapter : IClrRoot
    {
        /// <inheritdoc />
        public ClrRootAdapter(ClrMd.ClrRoot root)
        {
            _root = root ?? throw new ArgumentNullException(nameof(root));
        }

        internal ClrMd.ClrRoot _root;

        /// <inheritdoc />
        public ulong Address { get; }

        /// <inheritdoc />
        public IClrAppDomain AppDomain { get; }

        /// <inheritdoc />
        public bool IsInterior { get; }

        /// <inheritdoc />
        public bool IsPinned { get; }

        /// <inheritdoc />
        public bool IsPossibleFalsePositive { get; }

        /// <inheritdoc />
        public GcRootKind Kind { get; }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public ulong Object { get; }

        /// <inheritdoc />
        public IClrStackFrame StackFrame { get; }

        /// <inheritdoc />
        public IClrThread Thread { get; }

        /// <inheritdoc />
        public IClrType Type { get; }
    }

    internal class ClrObjectAdapter : IClrObject
    {
        /// <inheritdoc />
        public ClrObjectAdapter(ClrMd.ClrObject o)
        {
            _object = o;
        }

        internal ClrMd.ClrObject _object;

        /// <inheritdoc />
        public IEnumerable<IClrObject> EnumerateObjectReferences(bool carefully = false)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool Equals(IClrObject other)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public T GetField<T>(string fieldName) where T : struct
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IClrObject GetObjectField(string fieldName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public string GetStringField(string fieldName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IClrValueClass GetValueClassField(string fieldName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ulong Address { get; }

        /// <inheritdoc />
        public bool ContainsPointers { get; }

        /// <inheritdoc />
        public string HexAddress { get; }

        /// <inheritdoc />
        public bool IsArray { get; }

        /// <inheritdoc />
        public bool IsBoxed { get; }

        /// <inheritdoc />
        public bool IsNull { get; }

        /// <inheritdoc />
        public int Length { get; }

        /// <inheritdoc />
        public ulong Size { get; }

        /// <inheritdoc />
        public IClrType Type { get; }
    }

    internal class GcRootAdapter : IGcRoot
    {
        /// <inheritdoc />
        public GcRootAdapter(ClrMd.GCRoot root)
        {
            _root = root ?? throw new ArgumentNullException(nameof(root));
        }

        /// <inheritdoc />
        public event GcRootProgressEvent ProgressUpdate;

        internal ClrMd.GCRoot _root;

        /// <inheritdoc />
        public void BuildCache(CancellationToken cancelToken)
        {
            _root.BuildCache(cancelToken);
        }

        /// <inheritdoc />
        public void ClearCache()
        {
            _root.ClearCache();
        }

        /// <inheritdoc />
        public IEnumerable<LinkedList<IClrObject>> EnumerateAllPaths(ulong source, ulong target, bool unique,
            CancellationToken cancelToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IEnumerable<IRootPath> EnumerateGCRoots(ulong target, CancellationToken cancelToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IEnumerable<IRootPath> EnumerateGCRoots(ulong target, bool unique, CancellationToken cancelToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public LinkedList<IClrObject> FindSinglePath(ulong source, ulong target, CancellationToken cancelToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool AllowParallelSearch { get; set; }

        /// <inheritdoc />
        public IClrHeap Heap { get; }

        /// <inheritdoc />
        public bool IsFullyCached { get; }

        /// <inheritdoc />
        public int MaximumTasksAllowed { get; set; }
    }
}