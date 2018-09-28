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

        /// <inheritdoc />
        public ulong ReservedEnd {get; set;}
        public ulong Start { get; set; }

        public ulong CommittedEnd { get; set; }

        /// <inheritdoc />
        public ulong End {get; set;}

        /// <inheritdoc />
        public ulong FirstObject {get; set;}

        /// <inheritdoc />
        public ulong Gen0Length {get; set;}

        /// <inheritdoc />
        public ulong Gen0Start {get; set;}

        /// <inheritdoc />
        public ulong Gen1Length {get; set;}

        /// <inheritdoc />
        public ulong Gen1Start {get; set;}

        /// <inheritdoc />
        public ulong Gen2Length {get; set;}

        /// <inheritdoc />
        public ulong Gen2Start {get; set;}

        /// <inheritdoc />
        public IClrHeap Heap {get; set;}

        /// <inheritdoc />
        public bool IsEphemeral {get; set;}

        /// <inheritdoc />
        public bool IsLarge {get; set;}

        /// <inheritdoc />
        public ulong Length {get; set;}

        /// <inheritdoc />
        public int ProcessorAffinity {get; set;}
    }

    internal class CoreComponentFactory
    {
        public CompositionContainer CompositionContainer { get; set; }
        public FileInfo DumpFile { get; set; }
        public IDataTarget DataTarget { get; set; }
        public IClrRuntime Runtime { get; set; }
        public IDebuggerProxy DebuggerProxy { get; set; }
        public IConverter Converter { get; set; } = new Converter(); // todo: doesn't feel great

        /// <inheritdoc />
        public CoreComponentFactory(CompositionContainer compositionContainer, FileInfo dumpFile)
        {
            CompositionContainer =
                compositionContainer ?? throw new ArgumentNullException(nameof(compositionContainer));
            DumpFile = dumpFile ?? throw new ArgumentNullException(nameof(dumpFile));
            DataTarget = Converter.Convert(Microsoft.Diagnostics.Runtime.DataTarget.LoadCrashDump(dumpFile.FullName));
            Runtime = DataTarget.ClrVersions.Single().CreateRuntime();
        }

        public void Setup()
        {
            var objects = CreateObjects();
            var types = CreateTypes();
            var appDomains = CreateAppDomains();
            var modules = CreateModules();
            var blockingObjects = CreateBlockingObjects();
            var roots = CreateRoots();
            var threads = CreateThreads();
            var handles = CreateHandles();
            var segments = CreateHeapSegments();
        }

        public void EstablishRelationships()
        {

        }

        private Dictionary<ulong, DumpHeapSegment> CreateHeapSegments()
        {
            var heaps = new Dictionary<ulong, DumpHeapSegment>();
            
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
                    ProcessorAffinity = heapSegment.ProcessorAffinity,
                };

                heaps.Add(heapSegment.Start, segment);
            }

            return heaps;
        }

        private Dictionary<ulong, DumpHandle> CreateHandles()
        {
            var handles = new Dictionary<ulong, DumpHandle>();

            foreach (var handle in Runtime.EnumerateHandles())
            {
                var newHandle = new DumpHandle()
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

            return handles;
        }

        private Dictionary<uint, DumpThread> CreateThreads()
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
                    OsId = thread.OSThreadId,
                };
                newThread.ManagedStackFramesInternal = thread.StackTrace.Select(x => new DumpStackFrame()
                {
                    InstructionPointer = x.InstructionPointer,
                    Kind = x.Kind,
                    ModuleName = x.ModuleName,
                    StackPointer = x.StackPointer,
                    Thread = newThread,
                    DisplayString = x.DisplayString,
                }).ToList();
                
                threads.Add(newThread.OsId, newThread);
            }

            return threads;
        }

        private Dictionary<ulong, DumpObjectRoot> CreateRoots()
        {
            var roots = new Dictionary<ulong, DumpObjectRoot>();

            foreach (var root in Runtime.Heap.EnumerateRoots())
            {
                var newRoot = new DumpObjectRoot()
                {
                    Address = root.Address,
                    Name = root.Name,
                    IsPinned = root.IsPinned,
                    IsInteriorPointer = root.IsInterior,
                    GcRootKind = root.Kind,
                    IsPossibleFalsePositive = root.IsPossibleFalsePositive,
                };

                roots.Add(newRoot.Address, newRoot);
            }

            return roots;
        }

        private Dictionary<ulong, DumpBlockingObject> CreateBlockingObjects()
        {
            var blockingObjects = new Dictionary<ulong, DumpBlockingObject>();

            foreach (var blockingObject in Runtime.Heap.EnumerateBlockingObjects())
            {
                var dumpBlockingObject = new DumpBlockingObject()
                {
                    Address = blockingObject.Object,
                    BlockingReason = blockingObject.Reason,
                    HasSingleOwner = blockingObject.HasSingleOwner,
                    IsLocked = blockingObject.Taken,
                    RecursionCount = blockingObject.RecursionCount
                };

                blockingObjects.Add(dumpBlockingObject.Address, dumpBlockingObject);
            }

            return blockingObjects;
        }

        private Dictionary<ulong, DumpModule> CreateModules()
        {
            var modules = new Dictionary<ulong, DumpModule>();

            foreach (var module in Runtime.Modules)
            {
                var dumpModule = new DumpModule()
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

            return modules;
        }

        private Dictionary<ulong, DumpAppDomain> CreateAppDomains()
        {
            var appDomains = new Dictionary<ulong, DumpAppDomain>();
            foreach (var runtimeAppDomain in Runtime.AppDomains)
            {
                var dumpAppDomain = new DumpAppDomain()
                {
                    Address = runtimeAppDomain.Address,
                    Name = runtimeAppDomain.Name,
                    ApplicationBase = runtimeAppDomain.ApplicationBase,
                    ConfigFile = runtimeAppDomain.ConfigurationFile
                };
                appDomains.Add(dumpAppDomain.Address, dumpAppDomain);
            }

            return appDomains;
        }

        private Dictionary<(ulong, string), DumpType> CreateTypes()
        {
            var types = new Dictionary<(ulong, string), DumpType>();

            foreach (var cur in Runtime.Heap.EnumerateTypes())
            {
                var t = new DumpType()
                {
                    Key = new DumpTypeKey(cur.MethodTable, cur.Name),
                    MethodTable = cur.MethodTable,
                    Name = cur.Name,
                    BaseSize = cur.BaseSize,
                    ContainsPointers = cur.ContainsPointers,
                    IsAbstract = cur.IsAbstract,
                    IsArray = cur.IsArray,
                    IsEnum = cur.IsEnum,
                    IsException = cur.IsException,
                    IsFinalizable = cur.IsFinalizable,
                    IsInterface = cur.IsInterface,
                    IsInternal = cur.IsInternal,
                    IsPointer = cur.IsPointer,
                    IsPrimitive = cur.IsPrimitive,
                    IsPrivate = cur.IsPrivate,
                    IsProtected = cur.IsProtected,
                    IsRuntimeType = cur.IsRuntimeType,
                    IsSealed = cur.IsSealed,
                    IsString = cur.IsString
                };
                types.Add((t.MethodTable, t.Name), t);
            }

            return types;
        }

        private Dictionary<ulong, DumpObject> CreateObjects()
        {
            var objects = new Dictionary<ulong, DumpObject>();

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
                    ContainsPointers = cur.ContainsPointers,
                };
                objects.Add(o.Address, o);
            }

            return objects;
        }



        public void RegisterRepositories(DefaultOptions options)
        {
            
        }
    }
}