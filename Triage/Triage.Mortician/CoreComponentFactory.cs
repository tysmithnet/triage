using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using Triage.Mortician.Core;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician
{
    public class DumpHeapSegment
    {
        public ulong CommittedEnd { get; set; }
        public ulong End { get; set; }
        public ulong FirstObject { get; set; }
        public ulong Gen0Length { get; set; }
        public ulong Gen0Start { get; set; }
        public ulong Gen1Length { get; set; }
        public ulong Gen1Start { get; set; }
        public ulong Gen2Length { get; set; }
        public ulong Gen2Start { get; set; }
        public IClrHeap Heap { get; set; }
        public bool IsEphemeral { get; set; }
        public bool IsLarge { get; set; }
        public ulong Length { get; set; }
        public int ProcessorAffinity { get; set; }
        public ulong ReservedEnd { get; set; }
        public ulong Start { get; set; }
    }

    public class DumpMemoryRegion
    {
        public ulong Address { get; set; }
        public GcSegmentType GcSegmentType { get; set; }
        public int HeapNumber { get; set; }
        public ClrMemoryRegionType MemoryRegionType { get; set; }
        public ulong Size { get; set; }
    }

    internal class CoreComponentFactory
    {
        /// <inheritdoc />
        public CoreComponentFactory(CompositionContainer compositionContainer, FileInfo dumpFile)
        {
            CompositionContainer =
                compositionContainer ?? throw new ArgumentNullException(nameof(compositionContainer));
            DumpFile = dumpFile ?? throw new ArgumentNullException(nameof(dumpFile));
            DataTarget = Converter.Convert(Microsoft.Diagnostics.Runtime.DataTarget.LoadCrashDump(dumpFile.FullName));
            Runtime = DataTarget.ClrVersions.Single().CreateRuntime();
        }

        public void RegisterRepositories(DefaultOptions options)
        {
        }

        public void Setup()
        {
            CreateObjects();
            CreateTypes();
            CreateAppDomains();
            CreateClrModules();
            CreateBlockingObjects();
            CreateRoots();
            CreateThreads();
            CreateHandles();
            CreateHeapSegments();
            CreateDumpModuleInfo();
            FinalizableObjects = Runtime.Heap.EnumerateFinalizableObjectAddresses().ToList();
            ManagedWorkItems = Runtime.ThreadPool.EnumerateManagedWorkItems().Select(x => x.Object).ToList();
            NativeWorkitems = Runtime.ThreadPool.EnumerateNativeWorkItems().ToList();
            CreateMemoryRegions();
            ObjectsInFinalizerQueue = Runtime.EnumerateFinalizerQueueObjectAddresses().ToList();
            GcThreads = Runtime.EnumerateGCThreads().Select(Convert.ToUInt64).ToList();
        }

        internal void ConnectObjects()
        {
            foreach (var kvp in ObjectGraph)
            {
                var address = kvp.Key;
                var references = kvp.Value;
                var current = Objects[address];

                foreach (var refAddr in references)
                {
                    var reference = Objects[refAddr];
                    current.AddReference(reference);
                    reference.AddReferencer(current);
                }
            }
        }

        internal void CreateAppDomains()
        {
            var appDomains = new Dictionary<ulong, DumpAppDomain>();
            foreach (var runtimeAppDomain in Runtime.AppDomains)
            {
                var dumpAppDomain = new DumpAppDomain
                {
                    Address = runtimeAppDomain.Address,
                    Name = runtimeAppDomain.Name,
                    ApplicationBase = runtimeAppDomain.ApplicationBase,
                    ConfigFile = runtimeAppDomain.ConfigurationFile
                };
                appDomains.Add(dumpAppDomain.Address, dumpAppDomain);
            }

            AppDomains = appDomains;
        }

        internal void CreateBlockingObjects()
        {
            var blockingObjects = new Dictionary<ulong, DumpBlockingObject>();

            foreach (var blockingObject in Runtime.Heap.EnumerateBlockingObjects())
            {
                var dumpBlockingObject = new DumpBlockingObject
                {
                    Address = blockingObject.Object,
                    BlockingReason = blockingObject.Reason,
                    HasSingleOwner = blockingObject.HasSingleOwner,
                    IsLocked = blockingObject.Taken,
                    RecursionCount = blockingObject.RecursionCount
                };

                blockingObjects.Add(dumpBlockingObject.Address, dumpBlockingObject);
            }

            BlockingObjects = blockingObjects;
        }

        internal void CreateClrModules()
        {
            var modules = new Dictionary<ulong, DumpModule>();

            foreach (var module in Runtime.Modules)
            {
                var dumpModule = new DumpModule
                {
                    AssemblyId = module.AssemblyId,
                    Name = module.Name,
                    Size = module.Size,
                    AssemblyName = module.AssemblyName,
                    IsDynamic = module.IsDynamic,
                    IsFile = module.IsFile,
                    FileName = module.FileName,
                    ImageBase = module.ImageBase,
                    DebuggingMode = module.DebuggingMode,
                    PdbInfo = module.PdbInfo
                };
                modules.Add(module.AssemblyId, dumpModule);
            }

            Modules = modules;
        }

        internal void CreateDumpModuleInfo()
        {
            var res = new Dictionary<string, DumpModuleInfo>();

            foreach (var info in Runtime.DataTarget.EnumerateModules())
            {
                var moduleInfo = new DumpModuleInfo
                {
                    Pdb = info.Pdb,
                    Version = info.Version,
                    IsManaged = info.IsManaged,
                    FileName = info.FileName,
                    ImageBase = info.ImageBase,
                    FileSize = info.FileSize,
                    PeFile = info.GetPEFile(),
                    IsRuntime = info.IsRuntime,
                    TimeStamp = info.TimeStamp
                };
                res.Add(info.FileName, moduleInfo);
            }

            ModuleInfos = res;
        }

        internal void CreateHandles()
        {
            var handles = new Dictionary<ulong, DumpHandle>();

            foreach (var handle in Runtime.EnumerateHandles())
            {
                var newHandle = new DumpHandle
                {
                    Address = handle.Address,
                    HandleType = handle.HandleType,
                    DependentTarget = handle.DependentTarget,
                    IsPinned = handle.IsPinned,
                    IsStrong = handle.IsStrong,
                    ObjectAddress = handle.Object,
                    RefCount = handle.RefCount
                };

                handles.Add(newHandle.Address, newHandle);
            }

            Handles = handles;
        }

        internal void CreateHeapSegments()
        {
            var segments = new Dictionary<ulong, DumpHeapSegment>();

            foreach (var heapSegment in Runtime.Heap.Segments)
            {
                var segment = new DumpHeapSegment
                {
                    ReservedEnd = heapSegment.ReservedEnd,
                    Start = heapSegment.Start,
                    CommittedEnd = heapSegment.CommittedEnd,
                    End = heapSegment.End,
                    FirstObject = heapSegment.FirstObject,
                    Gen0Length = heapSegment.Gen0Length,
                    Gen0Start = heapSegment.Gen0Start,
                    Gen1Length = heapSegment.Gen1Length,
                    Gen1Start = heapSegment.Gen1Start,
                    Gen2Length = heapSegment.Gen2Length,
                    Gen2Start = heapSegment.Gen2Start,
                    Heap = heapSegment.Heap,
                    IsEphemeral = heapSegment.IsEphemeral,
                    IsLarge = heapSegment.IsLarge,
                    Length = heapSegment.Length,
                    ProcessorAffinity = heapSegment.ProcessorAffinity
                };

                segments.Add(heapSegment.Start, segment);
            }

            Segments = segments;
        }

        internal void CreateMemoryRegions()
        {
            var regions = new Dictionary<ulong, DumpMemoryRegion>();
            foreach (var region in Runtime.EnumerateMemoryRegions())
            {
                var newRegion = new DumpMemoryRegion
                {
                    Address = region.Address,
                    GcSegmentType = region.GcSegmentType,
                    HeapNumber = region.HeapNumber,
                    MemoryRegionType = region.MemoryRegionType,
                    Size = region.Size
                };
                regions.Add(region.Address, newRegion);
            }

            MemoryRegions = regions;
        }

        internal void CreateObjects()
        {
            var objects = new Dictionary<ulong, DumpObject>();
            var objectGraph = new Dictionary<ulong, IList<ulong>>();

            foreach (var cur in Runtime.Heap.EnumerateObjects())
            {
                var o = new DumpObject(cur.Address)
                {
                    Address = cur.Address,
                    FullTypeName = cur.Type?.Name,
                    Size = cur.Size,
                    IsNull = cur.IsNull,
                    IsBoxed = cur.IsBoxed,
                    IsArray = cur.IsArray,
                    ContainsPointers = cur.ContainsPointers
                };
                objects.Add(o.Address, o);
                objectGraph.Add(o.Address, cur.EnumerateObjectReferences().Select(x => x.Address).ToList());
            }

            Objects = objects;
            ObjectGraph = objectGraph;
        }

        internal void CreateRoots()
        {
            var roots = new Dictionary<ulong, DumpObjectRoot>();

            foreach (var root in Runtime.Heap.EnumerateRoots())
            {
                var newRoot = new DumpObjectRoot
                {
                    Address = root.Address,
                    Name = root.Name,
                    IsPinned = root.IsPinned,
                    IsInteriorPointer = root.IsInterior,
                    GcRootKind = root.Kind,
                    IsPossibleFalsePositive = root.IsPossibleFalsePositive
                };

                roots.Add(newRoot.Address, newRoot);
            }

            Roots = roots;
        }

        internal void CreateThreads()
        {
            var threads = new Dictionary<uint, DumpThread>();

            foreach (var thread in Runtime.Threads)
            {
                var newThread = new DumpThread
                {
                    Address = thread.Address,
                    AppDomainAddress = thread.AppDomain,
                    GcMode = thread.GcMode,
                    IsAborted = thread.IsAborted,
                    IsAbortRequested = thread.IsAbortRequested,
                    IsAlive = thread.IsAlive,
                    IsBackground = thread.IsBackground,
                    IsCoinitialized = thread.IsCoInitialized,
                    IsCreatedButNotStarted = thread.IsUnstarted,
                    IsDebuggerHelper = thread.IsDebuggerHelper,
                    IsDebugSuspended = thread.IsDebugSuspended,
                    IsFinalizer = thread.IsFinalizer,
                    IsGc = thread.IsGC,
                    IsGcSuspendPending = thread.IsGCSuspendPending,
                    IsMta = thread.IsMTA,
                    IsShutdownHelper = thread.IsShutdownHelper,
                    IsSta = thread.IsSTA,
                    IsSuspendingEe = thread.IsSuspendingEE,
                    IsThreadpoolCompletionPort = thread.IsThreadpoolCompletionPort,
                    IsThreadpoolGate = thread.IsThreadpoolGate,
                    IsThreadpoolTimer = thread.IsThreadpoolTimer,
                    IsThreadpoolWait = thread.IsThreadpoolWait,
                    IsThreadpoolWorker = thread.IsThreadpoolWorker,
                    IsUserSuspended = thread.IsUserSuspended,
                    LockCount = thread.LockCount,
                    StackLimit = thread.StackLimit,
                    Teb = thread.Teb,
                    OsId = thread.OSThreadId
                };
                newThread.ManagedStackFramesInternal = thread.StackTrace.Select(x => new DumpStackFrame
                {
                    InstructionPointer = x.InstructionPointer,
                    Kind = x.Kind,
                    ModuleName = x.ModuleName,
                    StackPointer = x.StackPointer,
                    Thread = newThread,
                    DisplayString = x.DisplayString
                }).ToList();

                threads.Add(newThread.OsId, newThread);
            }

            Threads = threads;
        }

        internal void CreateTypes()
        {
            Types = new Dictionary<(ulong, string), DumpType>();
            TypeToBaseTypeMapping = new Dictionary<(ulong, string), (ulong, string)>();
            TypeToModuleMapping = new Dictionary<(ulong, string), (ulong, string)>();
            TypeToComponentTypeMapping = new Dictionary<(ulong, string), (ulong, string)>();
            TypeToInterfacesImplementedMapping = new Dictionary<(ulong, string), string>();
            foreach (var cur in Runtime.Heap.EnumerateTypes())
            {
                var t = new DumpType
                {
                    AssemblyId = cur.Module.AssemblyId,
                    BaseSize = cur.BaseSize,
                    ContainsPointers = cur.ContainsPointers,
                    HasSimpleValue = cur.HasSimpleValue,
                    IsAbstract = cur.IsAbstract,
                    IsArray = cur.IsArray,
                    IsEnum = cur.IsEnum,
                    IsException = cur.IsException,
                    IsFinalizable = cur.IsFinalizable,
                    IsFree = cur.IsFree,
                    IsInterface = cur.IsInterface,
                    IsInternal = cur.IsInternal,
                    IsObjectReference = cur.IsObjectReference,
                    IsPointer = cur.IsPointer,
                    IsPrimitive = cur.IsPrimitive,
                    IsPrivate = cur.IsPrivate,
                    IsProtected = cur.IsProtected,
                    IsPublic = cur.IsPublic,
                    IsRuntimeType = cur.IsRuntimeType,
                    IsSealed = cur.IsSealed,
                    IsString = cur.IsString,
                    IsValueClass = cur.IsValueClass,
                    Key = new DumpTypeKey(cur.Module.AssemblyId, cur.Name),
                    MetaDataToken = cur.MetadataToken,
                    MethodTable = cur.MethodTable,
                    Name = cur.Name,
                    ElementSize = cur.ElementSize,
                    ElementType = cur.ElementType,
                };
                Types.Add((t.AssemblyId, t.Name), t);
                TypeToBaseTypeMapping.Add((t.AssemblyId, t.Name), (cur.BaseType.Module.AssemblyId, cur.BaseType.Name));
                TypeToModuleMapping.Add((t.AssemblyId, t.Name), (t.Module.AssemblyId, t.Module.Name));
                foreach (var curInterface in cur.Interfaces)
                {
                    // unfortunately, the API for CLRMd provides this as a string
                    TypeToInterfacesImplementedMapping.Add((t.AssemblyId, t.Name), curInterface.Name);
                }
            }
        }

        public Dictionary<(ulong, string), string> TypeToInterfacesImplementedMapping { get; set; }

        public Dictionary<(ulong, string), (ulong, string)> TypeToComponentTypeMapping { get; set; }

        public Dictionary<(ulong, string), (ulong, string)> TypeToModuleMapping { get; set; }

        public Dictionary<ulong, DumpAppDomain> AppDomains { get; set; }
        public Dictionary<(ulong, string), (ulong, string)> TypeToBaseTypeMapping { get; set; }
        public Dictionary<ulong, DumpBlockingObject> BlockingObjects { get; set; }
        public CompositionContainer CompositionContainer { get; set; }
        public IConverter Converter { get; set; } = new Converter(); // todo: doesn't feel great
        public IDataTarget DataTarget { get; set; }
        public IDebuggerProxy DebuggerProxy { get; set; }
        public FileInfo DumpFile { get; set; }

        public List<ulong> FinalizableObjects { get; set; }
        public List<ulong> GcThreads { get; set; }

        public Dictionary<ulong, DumpHandle> Handles { get; set; }

        public List<ulong> ManagedWorkItems { get; set; }

        public Dictionary<ulong, DumpMemoryRegion> MemoryRegions { get; set; }

        public Dictionary<string, DumpModuleInfo> ModuleInfos { get; set; }

        public Dictionary<ulong, DumpModule> Modules { get; set; }

        public List<INativeWorkItem> NativeWorkitems { get; set; }
        public Dictionary<ulong, IList<ulong>> ObjectGraph { get; set; }

        public Dictionary<ulong, DumpObject> Objects { get; set; }

        public List<ulong> ObjectsInFinalizerQueue { get; set; }

        public Dictionary<ulong, DumpObjectRoot> Roots { get; set; }
        public IClrRuntime Runtime { get; set; }

        public Dictionary<ulong, DumpHeapSegment> Segments { get; set; }

        public Dictionary<uint, DumpThread> Threads { get; set; }

        public Dictionary<(ulong, string), DumpType> Types { get; set; }
    }

    public class DumpModuleInfo
    {
        /// <inheritdoc />
        public string FileName { get; set; }

        /// <inheritdoc />
        public uint FileSize { get; set; }

        /// <inheritdoc />
        public ulong ImageBase { get; set; }

        /// <inheritdoc />
        public bool IsManaged { get; set; }

        /// <inheritdoc />
        public bool IsRuntime { get; set; }

        /// <inheritdoc />
        public IPdbInfo Pdb { get; set; }

        /// <inheritdoc />
        public IPeFile PeFile { get; set; }

        /// <inheritdoc />
        public uint TimeStamp { get; set; }

        /// <inheritdoc />
        public VersionInfo Version { get; set; }
    }
}