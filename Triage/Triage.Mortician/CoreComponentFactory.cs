using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using Serilog;
using Triage.Mortician.Core;
using Triage.Mortician.Core.ClrMdAbstractions;
using Triage.Mortician.Repositories;

namespace Triage.Mortician
{
    internal class CoreComponentFactory
    {
        private const string ERROR_TYPE = "ERROR";

        /// <inheritdoc />
        public CoreComponentFactory(CompositionContainer compositionContainer, FileInfo dumpFile)
        {
            CompositionContainer =
                compositionContainer ?? throw new ArgumentNullException(nameof(compositionContainer));
            DumpFile = dumpFile ?? throw new ArgumentNullException(nameof(dumpFile));

            try
            {
                DataTarget = Converter.Convert(Microsoft.Diagnostics.Runtime.DataTarget.LoadCrashDump(dumpFile.FullName));
            }
            catch (FileNotFoundException e)
            {
                throw new ApplicationException("Memory dump was not found. Is the path correct? Is it read only?", e);
            }
            Runtime = DataTarget.ClrVersions.Single().CreateRuntime();
            DumpObjectExtractors = CompositionContainer.GetExportedValues<IDumpObjectExtractor>();
        }

        public IEnumerable<IDumpObjectExtractor> DumpObjectExtractors { get; set; }

        public void ConnectHandles()
        {
            foreach (var kvp in HandleToAppDomainMapping)
            {
                var handle = Handles[kvp.Key];
                var appDomain = AppDomains[kvp.Value];
                handle.AppDomain = appDomain;
                appDomain.AddHandle(handle);
            }

            foreach (var kvp in HandleToDependentTypeMapping)
            {
                var handle = Handles[kvp.Key];
                var type = Types[kvp.Value];
                handle.DependentType = type;
            }

            foreach (var kvp in HandleToTypeMapping)
            {
                var handle = Handles[kvp.Key];
                var type = Types[kvp.Value];
                handle.ObjectType = type;
            }
        }

        public void ConnectThreads()
        {
            foreach (var kvp in ThreadToExceptionMapping)
            {
                var thread = Threads[kvp.Key];
                var exception = Objects[kvp.Value];
                thread.CurrentException = exception;
            }

            foreach (var kvp in ThreadToRootMapping)
            {
                var thread = Threads[kvp.Key];
                var roots = Roots.Where(r => kvp.Value.Contains(r.Key)).Select(x => x.Value).ToList();
                foreach (var dumpObjectRoot in roots)
                {
                    thread.AddRoot(dumpObjectRoot);
                    dumpObjectRoot.AddThread(thread);
                }
            }
        }

        public void RegisterRepositories(DefaultOptions options)
        {
            var objRepo = new DumpObjectRepository(Objects, Roots, BlockingObjects);
            var typeRepo = new DumpTypeRepository(Types);
            var threadRepo = new DumpThreadRepository(Threads);
            var appDomainRepo = new DumpAppDomainRepository(AppDomains);
            var moduleRepo = new DumpModuleRepository(Modules);
            var handleRepo = new DumpHandleRepository(Handles);
            var infoRepo = new DumpInformationRepository(DataTarget, Runtime, DumpFile);

            CompositionContainer.ComposeExportedValue<IDumpObjectRepository>(objRepo);
            CompositionContainer.ComposeExportedValue<IDumpTypeRepository>(typeRepo);
            CompositionContainer.ComposeExportedValue<IDumpThreadRepository>(threadRepo);
            CompositionContainer.ComposeExportedValue<IDumpAppDomainRepository>(appDomainRepo);
            CompositionContainer.ComposeExportedValue<IDumpModuleRepository>(moduleRepo);
            CompositionContainer.ComposeExportedValue<IDumpHandleRepository>(handleRepo);
            CompositionContainer.ComposeExportedValue<IDumpInformationRepository>(infoRepo);
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
            FinalizableObjectAddresses = Runtime.Heap.EnumerateFinalizableObjectAddresses().ToList();
            ManagedWorkItems = Runtime.ThreadPool.EnumerateManagedWorkItems().Select(x => x.Object).ToList();
            NativeWorkitems = Runtime.ThreadPool.EnumerateNativeWorkItems().ToList();
            CreateMemoryRegions();
            ObjectAddressesInFinalizerQueue = Runtime.EnumerateFinalizerQueueObjectAddresses().ToList();
            GcThreads = Runtime.EnumerateGCThreads().Select(Convert.ToUInt64).ToList();
            ConnectObjects();
            ConnectObjectsToTypes();
            ConnectTypes();
            ConnectThreads();
            ConnectAppDomainsAndModules();
            ConnectBlockingObjects();
            ConnectHandles();
            ConnectRoots();
        }

        internal void ConnectAppDomainsAndModules()
        {
            foreach (var kvp in AppDomainToModuleMapping)
            {
                var domain = AppDomains[kvp.Key];
                var modules = kvp.Value.Select(x => Modules[x]).ToList();
                modules.ForEach(domain.AddModule);
                modules.ForEach(x => x.AddAppDomain(domain));
            }
        }

        internal void ConnectBlockingObjects()
        {
            foreach (var kvp in BlockingObjectToThreadMapping)
            {
                var o = BlockingObjects[kvp.Key];
                var threads = Threads.Where(x => kvp.Value.Contains(x.Key)).Select(x => x.Value).ToList();
                o.Owners = threads;
                foreach (var dumpThread in threads) dumpThread.AddBlockingObject(o);
            }
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

        internal void ConnectObjectsToTypes()
        {
            foreach (var kvp in ObjectToTypeMapping)
            {
                var o = Objects[kvp.Key];
                var type = Types[kvp.Value];
                o.Type = type;
                type.ObjectsInternal.Add(o.Address, o);
            }
        }

        internal void ConnectRoots()
        {
            foreach (var kvp in RootToTypeMapping)
            {
                var root = Roots[kvp.Key];
                if(Types.TryGetValue(kvp.Value, out var type))
                    root.Type = type;
            }
        }

        internal void ConnectTypes()
        {
            foreach (var kvp in TypeToBaseTypeMapping)
            {
                var type = Types[kvp.Key];

                if (Types.TryGetValue(kvp.Value, out var baseType))
                {
                    type.BaseType = baseType;
                    baseType.InheritingTypes.Add(type);
                }
            }

            foreach (var kvp in TypeToComponentTypeMapping)
            {
                var type = Types[kvp.Key];
                if(Types.TryGetValue(kvp.Value, out var componentType))
                    type.ComponentType = componentType;
            }

            foreach (var kvp in TypeToModuleMapping)
            {
                var type = Types[kvp.Key];
                var module =
                    Modules.First(x =>
                            x.Value.Key.AssemblyId == kvp.Value.AssemblyId && x.Value.Name == kvp.Value.TypeName)
                        .Value;
                type.Module = module;
                module.AddType(type);
            }

            foreach (var kvp in InstanceFieldToTypeMapping)
            {
                var field = kvp.Key;
                if(Types.TryGetValue(kvp.Value, out var type))
                    field.Type = type;
            }

            foreach (var kvp in StaticFieldToTypeMapping)
            {
                var field = kvp.Key;
                if (Types.TryGetValue(kvp.Value, out var type))
                    field.Type = type;
            }
        }

        internal void CreateAppDomains()
        {
            AppDomains = new Dictionary<ulong, DumpAppDomain>();
            AppDomainToModuleMapping = new Dictionary<ulong, IList<DumpModuleKey>>();
            var runtimeAppDomains = Runtime.AppDomains.Concat(new[] {Runtime.SharedDomain, Runtime.SystemDomain});
            foreach (var domain in runtimeAppDomains)
            {
                var dumpAppDomain = new DumpAppDomain
                {
                    Address = domain.Address,
                    Name = domain.Name,
                    ApplicationBase = domain.ApplicationBase,
                    ConfigFile = domain.ConfigurationFile
                };
                AppDomains.Add(dumpAppDomain.Address, dumpAppDomain);
                AppDomainToModuleMapping.Add(domain.Address, domain.Modules.Select(x => x.ToKeyType()).ToList());
            }
        }

        internal void CreateBlockingObjects()
        {
            BlockingObjects = new Dictionary<ulong, DumpBlockingObject>();
            BlockingObjectToThreadMapping = new Dictionary<ulong, IList<uint>>();
            foreach (var blockingObject in Runtime.Heap.EnumerateBlockingObjects())
            {
                var dumpBlockingObject = new DumpBlockingObject
                {
                    Address = blockingObject.Object,
                    BlockingReason = blockingObject.Reason,
                    IsLocked = blockingObject.Taken,
                    RecursionCount = blockingObject.RecursionCount
                };

                BlockingObjects.Add(dumpBlockingObject.Address, dumpBlockingObject);
                try
                {
                    BlockingObjectToThreadMapping.Add(blockingObject.Object,
                        blockingObject.HasSingleOwner
                            ? new List<uint> {blockingObject.Owner.OSThreadId}
                            : blockingObject.Owners.Select(x => x.OSThreadId).ToList());
                }
                catch (Exception e)
                {
                    // todo: something
                }
            }
        }

        internal void CreateClrModules()
        {
            var modules = new Dictionary<DumpModuleKey, DumpModule>();

            foreach (var module in Runtime.Modules)
            {
                var dumpModule = new DumpModule
                {
                    Key = module.ToKeyType(),
                    Size = module.Size,
                    IsDynamic = module.IsDynamic,
                    IsFile = module.IsFile,
                    FileName = module.FileName,
                    ImageBase = module.ImageBase,
                    DebuggingMode = module.DebuggingMode,
                    PdbInfo = module.PdbInfo
                };
                modules.Add(dumpModule.Key, dumpModule);
            }

            Modules = modules;
        }

        internal void CreateDumpModuleInfo()
        {
            ModuleInfos = new Dictionary<string, DumpModuleInfo>();

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
                ModuleInfos.Add(info.FileName, moduleInfo);
            }
        }

        internal void CreateHandles()
        {
            Handles = new Dictionary<ulong, DumpHandle>();
            HandleToTypeMapping = new Dictionary<ulong, DumpTypeKey>();
            HandleToDependentTypeMapping = new Dictionary<ulong, DumpTypeKey>();
            HandleToAppDomainMapping = new Dictionary<ulong, ulong>();
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

                try
                {
                    Handles.Add(newHandle.Address, newHandle);
                }
                catch (Exception)
                {
                }

                try
                {
                    HandleToTypeMapping.Add(newHandle.Address, handle.Type.ToKeyType());
                }
                catch (Exception)
                {
                }

                try
                {
                    HandleToAppDomainMapping.Add(newHandle.Address, handle.AppDomain.Address);
                }
                catch (Exception)
                {
                }

                try
                {
                    HandleToDependentTypeMapping.Add(handle.Address, handle.DependentType.ToKeyType());
                }
                catch (Exception)
                {
                }
            }
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
                try
                {
                    regions.Add(region.Address, newRegion);
                }
                catch (Exception)
                {
                    // todo: something
                }
            }

            MemoryRegions = regions;
        }

        internal void CreateObjects()
        {
            var objects = new Dictionary<ulong, DumpObject>();
            var objectGraph = new Dictionary<ulong, IList<ulong>>();
            ObjectToTypeMapping = new Dictionary<ulong, DumpTypeKey>();
            foreach (var cur in Runtime.Heap.EnumerateObjects())
            {
                DumpObject toAdd;
                var handler = DumpObjectExtractors.FirstOrDefault(h => h.CanExtract(cur, Runtime));
                if (handler != null)
                {
                    toAdd = handler.Extract(cur, Runtime);
                }
                else
                {
                    toAdd = new DumpObject(cur.Address)
                    {
                        Address = cur.Address,
                        FullTypeName = cur.Type?.Name,
                        Size = cur.Size,
                        IsNull = cur.IsNull,
                        IsBoxed = cur.IsBoxed,
                        IsArray = cur.IsArray,
                        ContainsPointers = cur.ContainsPointers
                    };
                }
                objects.Add(toAdd.Address, toAdd);
                objectGraph.Add(toAdd.Address, cur.EnumerateObjectReferences().Select(x => x.Address).ToList());
                ObjectToTypeMapping.Add(toAdd.Address, cur.Type.ToKeyType());
            }

            Objects = objects;
            ObjectGraph = objectGraph;
        }

        internal void CreateRoots()
        {
            var roots = new Dictionary<ulong, DumpObjectRoot>();
            RootToTypeMapping = new Dictionary<ulong, DumpTypeKey>();
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

                try
                {
                    roots.Add(newRoot.Address, newRoot);
                }
                catch (Exception)
                {
                    // todo: something
                }

                try
                {
                    RootToTypeMapping.Add(root.Address, root.Type.ToKeyType());
                }
                catch (Exception)
                {
                    // todo: do something
                }
            }

            Roots = roots;
        }

        internal void CreateThreads()
        {
            Threads = new Dictionary<uint, DumpThread>();
            ThreadToExceptionMapping = new Dictionary<uint, ulong>();
            ThreadToRootMapping = new Dictionary<uint, IList<ulong>>();
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

                try
                {
                    Threads.Add(newThread.OsId, newThread);
                }
                catch (Exception)
                {
                    Log.Error("Duplicate exceptions for thread: {OsId}", newThread.OsId);
                }

                try
                {
                    if(thread.CurrentException != null)
                        ThreadToExceptionMapping.Add(newThread.OsId, thread.CurrentException.Address);
                }
                catch (Exception)
                {
                    Log.Error("Multiple exceptions for thread: {OsId}", newThread.OsId);
                }
            }
        }

        internal void CreateTypes()
        {
            Types = new Dictionary<DumpTypeKey, DumpType>();
            TypeToBaseTypeMapping = new Dictionary<DumpTypeKey, DumpTypeKey>();
            TypeToModuleMapping = new Dictionary<DumpTypeKey, DumpTypeKey>();
            TypeToComponentTypeMapping = new Dictionary<DumpTypeKey, DumpTypeKey>();
            InstanceFieldToTypeMapping = new Dictionary<DumpTypeField, DumpTypeKey>();
            StaticFieldToTypeMapping = new Dictionary<DumpTypeField, DumpTypeKey>();
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
                    Interfaces = cur.Interfaces.Select(x => x.Name).ToList()
                };
                {
                    var key = new DumpTypeKey(t.AssemblyId, t.Name);
                    if (!Types.ContainsKey(key))
                        Types.Add(key, t);
                }
                {
                    var key = new DumpTypeKey(t.AssemblyId, t.Name);
                    if(cur.BaseType != null && !TypeToBaseTypeMapping.ContainsKey(key))
                        TypeToBaseTypeMapping.Add(key, cur.BaseType.ToKeyType());
                }

                {
                    var key = new DumpTypeKey(t.AssemblyId, t.Name);
                    if(cur.ComponentType != null && !TypeToComponentTypeMapping.ContainsKey(key))
                        TypeToComponentTypeMapping.Add(key,
                            cur.ComponentType.ToKeyType());
                }

                {
                    var key = new DumpTypeKey(t.AssemblyId, t.Name);
                    if(cur.Module != null && !TypeToModuleMapping.ContainsKey(key))
                        TypeToModuleMapping.Add(key,
                            new DumpTypeKey(cur.Module.AssemblyId, cur.Module.Name));
                }

                t.InstanceFields = new List<DumpTypeField>();
                foreach (var field in cur.Fields)
                {
                    var newField = new DumpTypeField
                    {
                        HasSimpleValue = field.HasSimpleValue,
                        IsInternal = field.IsInternal,
                        IsObjectReference = field.IsObjectReference,
                        IsPrimitive = field.IsPrimitive,
                        IsPrivate = field.IsPrivate,
                        IsProtected = field.IsProtected,
                        IsPublic = field.IsPublic,
                        IsValueClass = field.IsValueClass,
                        Name = field.Name,
                        Offset = field.Offset,
                        Size = field.Size,
                        Token = field.Token,
                        ElementType = field.ElementType
                    };
                    t.InstanceFields.Add(newField);
                    if (field.Type.Name != ERROR_TYPE)
                        InstanceFieldToTypeMapping.Add(newField, field.Type.ToKeyType());
                }

                foreach (var field in cur.StaticFields)
                {
                    var newField = new DumpTypeField
                    {
                        HasSimpleValue = field.HasSimpleValue,
                        IsInternal = field.IsInternal,
                        IsObjectReference = field.IsObjectReference,
                        IsPrimitive = field.IsPrimitive,
                        IsPrivate = field.IsPrivate,
                        IsProtected = field.IsProtected,
                        IsPublic = field.IsPublic,
                        IsValueClass = field.IsValueClass,
                        Name = field.Name,
                        Offset = field.Offset,
                        Size = field.Size,
                        Token = field.Token,
                        ElementType = field.ElementType
                    };
                    t.StaticFields.Add(newField);
                    StaticFieldToTypeMapping.Add(newField, field.Type.ToKeyType());
                }
            }
        }

        public Dictionary<ulong, DumpAppDomain> AppDomains { get; set; }

        public Dictionary<ulong, IList<DumpModuleKey>> AppDomainToModuleMapping { get; set; }
        public Dictionary<ulong, DumpBlockingObject> BlockingObjects { get; set; }

        public Dictionary<ulong, IList<uint>> BlockingObjectToThreadMapping { get; set; }
        public CompositionContainer CompositionContainer { get; set; }
        public IConverter Converter { get; set; } = new Converter(); // todo: doesn't feel great
        public IDataTarget DataTarget { get; set; }
        public IDebuggerProxy DebuggerProxy { get; set; }
        public FileInfo DumpFile { get; set; }
        public List<ulong> FinalizableObjectAddresses { get; set; } // todo: cleanup
        public List<ulong> GcThreads { get; set; }
        public Dictionary<ulong, DumpHandle> Handles { get; set; }

        public Dictionary<ulong, ulong> HandleToAppDomainMapping { get; set; }

        public Dictionary<ulong, DumpTypeKey> HandleToDependentTypeMapping { get; set; }

        public Dictionary<ulong, DumpTypeKey> HandleToTypeMapping { get; set; }

        public Dictionary<DumpTypeField, DumpTypeKey> InstanceFieldToTypeMapping { get; set; }
        public List<ulong> ManagedWorkItems { get; set; }
        public Dictionary<ulong, DumpMemoryRegion> MemoryRegions { get; set; }
        public Dictionary<string, DumpModuleInfo> ModuleInfos { get; set; }
        public Dictionary<DumpModuleKey, DumpModule> Modules { get; set; }
        public List<INativeWorkItem> NativeWorkitems { get; set; }
        public List<ulong> ObjectAddressesInFinalizerQueue { get; set; }
        public Dictionary<ulong, IList<ulong>> ObjectGraph { get; set; }
        public Dictionary<ulong, DumpObject> Objects { get; set; }

        public Dictionary<ulong, DumpTypeKey> ObjectToTypeMapping { get; set; }
        public Dictionary<ulong, DumpObjectRoot> Roots { get; set; }

        public Dictionary<ulong, DumpTypeKey> RootToTypeMapping { get; set; }
        public IClrRuntime Runtime { get; set; }
        public Dictionary<ulong, DumpHeapSegment> Segments { get; set; }
        public Dictionary<DumpTypeField, DumpTypeKey> StaticFieldToTypeMapping { get; set; }
        public Dictionary<uint, DumpThread> Threads { get; set; }

        public Dictionary<uint, ulong> ThreadToExceptionMapping { get; set; }

        public Dictionary<uint, IList<ulong>> ThreadToRootMapping { get; set; }
        public Dictionary<DumpTypeKey, DumpType> Types { get; set; }
        public Dictionary<DumpTypeKey, DumpTypeKey> TypeToBaseTypeMapping { get; set; }
        public Dictionary<DumpTypeKey, DumpTypeKey> TypeToComponentTypeMapping { get; set; }
        public Dictionary<DumpTypeKey, DumpTypeKey> TypeToModuleMapping { get; set; }
    }

    public class DumpModuleInfo
    {
        public string FileName { get; set; }
        public uint FileSize { get; set; }
        public ulong ImageBase { get; set; }
        public bool IsManaged { get; set; }
        public bool IsRuntime { get; set; }
        public IPdbInfo Pdb { get; set; }
        public IPeFile PeFile { get; set; }
        public uint TimeStamp { get; set; }
        public VersionInfo Version { get; set; }
    }

    internal static class ClrMdExtensionMethods
    {
        public static DumpModuleKey ToKeyType(this IClrModule module) =>
            new DumpModuleKey(module.AssemblyId, module.Name);

        public static DumpTypeKey ToKeyType(this IClrType type) => new DumpTypeKey(type.Module?.AssemblyId ?? 0, type.Name);
    }
}