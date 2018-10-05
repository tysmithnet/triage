// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-27-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-01-2018
// ***********************************************************************
// <copyright file="CoreComponentFactory.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using Triage.Mortician.Core;
using Triage.Mortician.Core.ClrMdAbstractions;
using Triage.Mortician.Reports;
using Triage.Mortician.Repositories;
using Slog = Serilog.Log;

namespace Triage.Mortician
{
    /// <summary>
    ///     Class CoreComponentFactory.
    /// </summary>
    internal class CoreComponentFactory
    {
        /// <summary>
        ///     The error type
        /// </summary>
        private const string ERROR_TYPE = "ERROR";

        /// <summary>
        ///     Initializes a new instance of the <see cref="CoreComponentFactory" /> class.
        /// </summary>
        /// <param name="compositionContainer">The composition container.</param>
        /// <param name="dumpFile">The dump file.</param>
        /// <exception cref="ArgumentNullException">
        ///     compositionContainer
        ///     or
        ///     dumpFile
        /// </exception>
        /// <exception cref="ApplicationException">Memory dump was not found. Is the path correct? Is it read only?</exception>
        /// <exception cref="System.ArgumentNullException">
        ///     compositionContainer
        ///     or
        ///     dumpFile
        /// </exception>
        /// <exception cref="System.ApplicationException">Memory dump was not found. Is the path correct? Is it read only?</exception>
        /// <inheritdoc />
        public CoreComponentFactory(CompositionContainer compositionContainer, FileInfo dumpFile)
        {
            CompositionContainer =
                compositionContainer ?? throw new ArgumentNullException(nameof(compositionContainer));
            DumpFile = dumpFile ?? throw new ArgumentNullException(nameof(dumpFile));

            try
            {
                var dt = Microsoft.Diagnostics.Runtime.DataTarget.LoadCrashDump(dumpFile.FullName);
                DataTarget =
                    Converter.Convert(dt);
                DebuggerProxy = new DebuggerProxy(dt.DebuggerInterface);
                try
                {
                    CompositionContainer.ComposeExportedValue(DebuggerProxy);
                }
                catch (Exception e)
                {
                    Log.Error(e, "Could not create DebuggerProxy. Verify the dump file can be opened in windbg.");
                    throw;
                }
            }
            catch (FileNotFoundException e)
            {
                throw new ApplicationException("Memory dump was not found. Is the path correct? Is it read only?", e);
            }

            Runtime = DataTarget.ClrVersions.Single().CreateRuntime();
            DumpObjectExtractors = CompositionContainer.GetExportedValues<IDumpObjectExtractor>()
                .Concat(new[] {new DefaultObjectExtractor()});
        }

        /// <summary>
        ///     Connects the handles.
        /// </summary>
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

        /// <summary>
        ///     Connects the threads.
        /// </summary>
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

            foreach (var kvp in ThreadToObjectMapping)
            {
                var thread = Threads[kvp.Key];
                foreach (var addr in kvp.Value)
                    if (Roots.TryGetValue(addr, out var o))
                    {
                        thread.AddRoot(o);
                        o.AddThread(thread);
                    }
                    else
                    {
                        ;
                    }
            }
        }

        /// <summary>
        ///     Registers the repositories.
        /// </summary>
        /// <param name="options">The options.</param>
        public void RegisterRepositories(DefaultOptions options)
        {
            var objRepo = new DumpObjectRepository(Objects, Roots, BlockingObjects, FinalizerQueueObjects);
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

        /// <summary>
        ///     Setups this instance.
        /// </summary>
        public void Setup()
        {
            ObjectAddressesInFinalizerQueue = new HashSet<ulong>(Runtime.EnumerateFinalizerQueueObjectAddresses());
            FinalizableObjectAddresses = new HashSet<ulong>(Runtime.Heap.EnumerateFinalizableObjectAddresses());
            GcThreads = Runtime.EnumerateGCThreads().Select(Convert.ToUInt64).ToList();

            CreateReports();
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
            ManagedWorkItems = Runtime.ThreadPool.EnumerateManagedWorkItems().Select(x => x.Object).ToList();
            NativeWorkitems = Runtime.ThreadPool.EnumerateNativeWorkItems().ToList();
            CreateMemoryRegions();
            ConnectObjects();
            ConnectObjectsToTypes();
            ConnectTypes();
            ConnectThreads();
            ConnectAppDomainsAndModules();
            ConnectBlockingObjects();
            ConnectHandles();
            ConnectRoots();
        }

        /// <summary>
        ///     Connects the application domains and modules.
        /// </summary>
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

        /// <summary>
        ///     Connects the blocking objects.
        /// </summary>
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

        /// <summary>
        ///     Connects the objects.
        /// </summary>
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

        /// <summary>
        ///     Connects the objects to types.
        /// </summary>
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

        /// <summary>
        ///     Connects the roots.
        /// </summary>
        internal void ConnectRoots()
        {
            foreach (var kvp in RootToTypeMapping)
            {
                var root = Roots[kvp.Key];
                if (Types.TryGetValue(kvp.Value, out var type))
                    root.Type = type;
            }
        }

        /// <summary>
        ///     Connects the types.
        /// </summary>
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
                if (Types.TryGetValue(kvp.Value, out var componentType))
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
                if (Types.TryGetValue(kvp.Value, out var type))
                    field.Type = type;
            }

            foreach (var kvp in StaticFieldToTypeMapping)
            {
                var field = kvp.Key;
                if (Types.TryGetValue(kvp.Value, out var type))
                    field.Type = type;
            }
        }

        /// <summary>
        ///     Creates the application domains.
        /// </summary>
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

        /// <summary>
        ///     Creates the blocking objects.
        /// </summary>
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

                if (!BlockingObjects.ContainsKey(dumpBlockingObject.Address))
                    BlockingObjects.Add(dumpBlockingObject.Address, dumpBlockingObject);

                if (BlockingObjectToThreadMapping.ContainsKey(blockingObject.Object)) continue;
                var owners = new List<uint>();
                if (blockingObject.Owner != null) owners.Add(blockingObject.Owner.OSThreadId);

                if (blockingObject.Owners != null)
                    owners.AddRange(blockingObject.Owners.Where(x => x != null).Select(x => x.OSThreadId));

                BlockingObjectToThreadMapping.Add(blockingObject.Object, owners);
            }
        }

        /// <summary>
        ///     Creates the color modules.
        /// </summary>
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

        /// <summary>
        ///     Creates the dump module information.
        /// </summary>
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

        /// <summary>
        ///     Creates the handles.
        /// </summary>
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

                if (!Handles.ContainsKey(newHandle.Address))
                    Handles.Add(newHandle.Address, newHandle);

                if (!HandleToTypeMapping.ContainsKey(newHandle.Address))
                    HandleToTypeMapping.Add(newHandle.Address, handle.Type.ToKeyType());

                if (!HandleToAppDomainMapping.ContainsKey(newHandle.Address))
                    HandleToAppDomainMapping.Add(newHandle.Address, handle.AppDomain.Address);

                if (handle.DependentType != null && !HandleToDependentTypeMapping.ContainsKey(handle.Address))
                    HandleToDependentTypeMapping.Add(handle.Address, handle.DependentType.ToKeyType());
            }
        }

        /// <summary>
        ///     Creates the heap segments.
        /// </summary>
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

        /// <summary>
        ///     Creates the memory regions.
        /// </summary>
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
                if (!regions.ContainsKey(region.Address))
                    regions.Add(region.Address, newRegion);
            }

            MemoryRegions = regions;
        }

        /// <summary>
        ///     Creates the object.
        /// </summary>
        /// <param name="cur">The current.</param>
        internal void CreateObject(IClrObject cur)
        {
            var handler = DumpObjectExtractors.FirstOrDefault(h => h.CanExtract(cur, Runtime));
            if (handler == null)
                return;
            var toAdd = handler.Extract(cur, Runtime);
            Objects.Add(toAdd.Address, toAdd);
            ObjectGraph.Add(toAdd.Address, cur.EnumerateObjectReferences().Select(x => x.Address).ToList());
            ObjectToTypeMapping.Add(toAdd.Address, cur.Type.ToKeyType());
        }

        /// <summary>
        ///     Creates the objects.
        /// </summary>
        internal void CreateObjects()
        {
            Objects = new Dictionary<ulong, DumpObject>();
            ObjectGraph = new Dictionary<ulong, IList<ulong>>();
            ObjectToTypeMapping = new Dictionary<ulong, DumpTypeKey>();
            FinalizerQueueObjects = new Dictionary<ulong, DumpObject>();
            foreach (var cur in Runtime.Heap.EnumerateObjects())
            {
                CreateObject(cur);
                if (!ObjectAddressesInFinalizerQueue.Contains(cur.Address) ||
                    FinalizerQueueObjects.ContainsKey(cur.Address)) continue;
                if (Objects.TryGetValue(cur.Address, out var o))
                {
                    FinalizerQueueObjects.Add(cur.Address, o);
                }
            };
        }

        public Dictionary<ulong, DumpObject> FinalizerQueueObjects { get; set; }

        /// <summary>
        ///     Creates the reports.
        /// </summary>
        internal void CreateReports()
        {
            var factories = CompositionContainer.GetExportedValues<IReportFactory>().ToList();
            if (!factories.Any())
                return;

            foreach (var reportFactory in factories) reportFactory.Setup(DebuggerProxy);

            var tasks = factories.Select(x => Task.Run(() => x.Process())).ToList();
            var batch = new CompositionBatch();

            for (var i = 0; i < tasks.Count; i++)
            {
                var task = tasks[i];
                try
                {
                    batch.AddPart(task.Result);
                }
                catch (Exception e)
                {
                    Log.Error(e, "Unable to process report for {ReportFactory}", factories[i].GetType().FullName);
                }
            }

            CompositionContainer.Compose(batch);
        }

        /// <summary>
        ///     Creates the roots.
        /// </summary>
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

                if (!roots.ContainsKey(newRoot.Address))
                    roots.Add(newRoot.Address, newRoot);

                if (!RootToTypeMapping.ContainsKey(root.Address) && root.Type != null)
                    RootToTypeMapping.Add(root.Address, root.Type.ToKeyType());
            }

            Roots = roots;
        }

        /// <summary>
        ///     Creates the threads.
        /// </summary>
        internal void CreateThreads()
        {
            Threads = new Dictionary<uint, DumpThread>();
            ThreadToExceptionMapping = new Dictionary<uint, ulong>();
            ThreadToRootMapping = new Dictionary<uint, IList<ulong>>();
            ThreadToObjectMapping = new Dictionary<uint, IList<ulong>>();
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

                if (!Threads.ContainsKey(newThread.OsId))
                    Threads.Add(newThread.OsId, newThread);

                if (thread.CurrentException != null && !ThreadToExceptionMapping.ContainsKey(newThread.OsId))
                    ThreadToExceptionMapping.Add(newThread.OsId, thread.CurrentException.Address);

                if (!ThreadToObjectMapping.ContainsKey(newThread.OsId))
                {
                    var objects = thread.EnumerateStackObjects().Select(x => x.Address).ToList();
                    ThreadToObjectMapping.Add(newThread.OsId, objects);
                }
            }
        }

        /// <summary>
        ///     Creates the types.
        /// </summary>
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
                    if (cur.BaseType != null && !TypeToBaseTypeMapping.ContainsKey(key))
                        TypeToBaseTypeMapping.Add(key, cur.BaseType.ToKeyType());
                }

                {
                    var key = new DumpTypeKey(t.AssemblyId, t.Name);
                    if (cur.ComponentType != null && !TypeToComponentTypeMapping.ContainsKey(key))
                        TypeToComponentTypeMapping.Add(key,
                            cur.ComponentType.ToKeyType());
                }

                {
                    var key = new DumpTypeKey(t.AssemblyId, t.Name);
                    if (cur.Module != null && !TypeToModuleMapping.ContainsKey(key))
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
                    if (field.Type != null && field.Type?.Name != ERROR_TYPE && !InstanceFieldToTypeMapping.ContainsKey(newField))
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
                    if (field.Type != null && !StaticFieldToTypeMapping.ContainsKey(newField))
                        StaticFieldToTypeMapping.Add(newField, field.Type.ToKeyType());
                }
            }
        }

        /// <summary>
        ///     Gets or sets the application domains.
        /// </summary>
        /// <value>The application domains.</value>
        public Dictionary<ulong, DumpAppDomain> AppDomains { get; set; }

        /// <summary>
        ///     Gets or sets the application domain to module mapping.
        /// </summary>
        /// <value>The application domain to module mapping.</value>
        public Dictionary<ulong, IList<DumpModuleKey>> AppDomainToModuleMapping { get; set; }

        /// <summary>
        ///     Gets or sets the blocking objects.
        /// </summary>
        /// <value>The blocking objects.</value>
        public Dictionary<ulong, DumpBlockingObject> BlockingObjects { get; set; }

        /// <summary>
        ///     Gets or sets the blocking object to thread mapping.
        /// </summary>
        /// <value>The blocking object to thread mapping.</value>
        public Dictionary<ulong, IList<uint>> BlockingObjectToThreadMapping { get; set; }

        /// <summary>
        ///     Gets or sets the composition container.
        /// </summary>
        /// <value>The composition container.</value>
        public CompositionContainer CompositionContainer { get; set; }

        /// <summary>
        ///     Gets or sets the converter.
        /// </summary>
        /// <value>The converter.</value>
        public IConverter Converter { get; set; } = new Converter(); // todo: doesn't feel great

        /// <summary>
        ///     Gets or sets the data target.
        /// </summary>
        /// <value>The data target.</value>
        public IDataTarget DataTarget { get; set; }

        /// <summary>
        ///     Gets or sets the debugger proxy.
        /// </summary>
        /// <value>The debugger proxy.</value>
        public IDebuggerProxy DebuggerProxy { get; set; }

        /// <summary>
        ///     Gets or sets the dump file.
        /// </summary>
        /// <value>The dump file.</value>
        public FileInfo DumpFile { get; set; }

        /// <summary>
        ///     Gets or sets the dump object extractors.
        /// </summary>
        /// <value>The dump object extractors.</value>
        public IEnumerable<IDumpObjectExtractor> DumpObjectExtractors { get; set; }

        /// <summary>
        ///     Gets or sets the finalizable object addresses.
        /// </summary>
        /// <value>The finalizable object addresses.</value>
        public ISet<ulong> FinalizableObjectAddresses { get; set; } // todo: cleanup

        /// <summary>
        ///     Gets or sets the gc threads.
        /// </summary>
        /// <value>The gc threads.</value>
        public List<ulong> GcThreads { get; set; }

        /// <summary>
        ///     Gets or sets the handles.
        /// </summary>
        /// <value>The handles.</value>
        public Dictionary<ulong, DumpHandle> Handles { get; set; }

        /// <summary>
        ///     Gets or sets the handle to application domain mapping.
        /// </summary>
        /// <value>The handle to application domain mapping.</value>
        public Dictionary<ulong, ulong> HandleToAppDomainMapping { get; set; }

        /// <summary>
        ///     Gets or sets the handle to dependent type mapping.
        /// </summary>
        /// <value>The handle to dependent type mapping.</value>
        public Dictionary<ulong, DumpTypeKey> HandleToDependentTypeMapping { get; set; }

        /// <summary>
        ///     Gets or sets the handle to type mapping.
        /// </summary>
        /// <value>The handle to type mapping.</value>
        public Dictionary<ulong, DumpTypeKey> HandleToTypeMapping { get; set; }

        /// <summary>
        ///     Gets or sets the instance field to type mapping.
        /// </summary>
        /// <value>The instance field to type mapping.</value>
        public Dictionary<DumpTypeField, DumpTypeKey> InstanceFieldToTypeMapping { get; set; }

        /// <summary>
        ///     Gets or sets the managed work items.
        /// </summary>
        /// <value>The managed work items.</value>
        public List<ulong> ManagedWorkItems { get; set; }

        /// <summary>
        ///     Gets or sets the memory regions.
        /// </summary>
        /// <value>The memory regions.</value>
        public Dictionary<ulong, DumpMemoryRegion> MemoryRegions { get; set; }

        /// <summary>
        ///     Gets or sets the module infos.
        /// </summary>
        /// <value>The module infos.</value>
        public Dictionary<string, DumpModuleInfo> ModuleInfos { get; set; }

        /// <summary>
        ///     Gets or sets the modules.
        /// </summary>
        /// <value>The modules.</value>
        public Dictionary<DumpModuleKey, DumpModule> Modules { get; set; }

        /// <summary>
        ///     Gets or sets the native workitems.
        /// </summary>
        /// <value>The native workitems.</value>
        public List<INativeWorkItem> NativeWorkitems { get; set; }

        /// <summary>
        ///     Gets or sets the object addresses in finalizer queue.
        /// </summary>
        /// <value>The object addresses in finalizer queue.</value>
        public ISet<ulong> ObjectAddressesInFinalizerQueue { get; set; }

        /// <summary>
        ///     Gets or sets the object graph.
        /// </summary>
        /// <value>The object graph.</value>
        public Dictionary<ulong, IList<ulong>> ObjectGraph { get; set; }

        /// <summary>
        ///     Gets or sets the objects.
        /// </summary>
        /// <value>The objects.</value>
        public Dictionary<ulong, DumpObject> Objects { get; set; }

        /// <summary>
        ///     Gets or sets the object to type mapping.
        /// </summary>
        /// <value>The object to type mapping.</value>
        public Dictionary<ulong, DumpTypeKey> ObjectToTypeMapping { get; set; }

        /// <summary>
        ///     Gets or sets the roots.
        /// </summary>
        /// <value>The roots.</value>
        public Dictionary<ulong, DumpObjectRoot> Roots { get; set; }

        /// <summary>
        ///     Gets or sets the root to type mapping.
        /// </summary>
        /// <value>The root to type mapping.</value>
        public Dictionary<ulong, DumpTypeKey> RootToTypeMapping { get; set; }

        /// <summary>
        ///     Gets or sets the runtime.
        /// </summary>
        /// <value>The runtime.</value>
        public IClrRuntime Runtime { get; set; }

        /// <summary>
        ///     Gets or sets the segments.
        /// </summary>
        /// <value>The segments.</value>
        public Dictionary<ulong, DumpHeapSegment> Segments { get; set; }

        /// <summary>
        ///     Gets or sets the static field to type mapping.
        /// </summary>
        /// <value>The static field to type mapping.</value>
        public Dictionary<DumpTypeField, DumpTypeKey> StaticFieldToTypeMapping { get; set; }

        /// <summary>
        ///     Gets or sets the threads.
        /// </summary>
        /// <value>The threads.</value>
        public Dictionary<uint, DumpThread> Threads { get; set; }

        /// <summary>
        ///     Gets or sets the thread to exception mapping.
        /// </summary>
        /// <value>The thread to exception mapping.</value>
        public Dictionary<uint, ulong> ThreadToExceptionMapping { get; set; }

        /// <summary>
        ///     Gets or sets the thread to object mapping.
        /// </summary>
        /// <value>The thread to object mapping.</value>
        public Dictionary<uint, IList<ulong>> ThreadToObjectMapping { get; set; }

        /// <summary>
        ///     Gets or sets the thread to root mapping.
        /// </summary>
        /// <value>The thread to root mapping.</value>
        public Dictionary<uint, IList<ulong>> ThreadToRootMapping { get; set; }

        /// <summary>
        ///     Gets or sets the types.
        /// </summary>
        /// <value>The types.</value>
        public Dictionary<DumpTypeKey, DumpType> Types { get; set; }

        /// <summary>
        ///     Gets or sets the type to base type mapping.
        /// </summary>
        /// <value>The type to base type mapping.</value>
        public Dictionary<DumpTypeKey, DumpTypeKey> TypeToBaseTypeMapping { get; set; }

        /// <summary>
        ///     Gets or sets the type to component type mapping.
        /// </summary>
        /// <value>The type to component type mapping.</value>
        public Dictionary<DumpTypeKey, DumpTypeKey> TypeToComponentTypeMapping { get; set; }

        /// <summary>
        ///     Gets or sets the type to module mapping.
        /// </summary>
        /// <value>The type to module mapping.</value>
        public Dictionary<DumpTypeKey, DumpTypeKey> TypeToModuleMapping { get; set; }

        /// <summary>
        ///     Gets the log.
        /// </summary>
        /// <value>The log.</value>
        internal ILogger Log { get; } = Slog.ForContext<CoreComponentFactory>();
    }
}