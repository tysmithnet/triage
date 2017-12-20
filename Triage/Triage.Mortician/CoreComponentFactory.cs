using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common.Logging;
using Microsoft.Diagnostics.Runtime;
using Triage.Mortician.Domain;
using Triage.Mortician.Repository;

namespace Triage.Mortician
{
    /// <summary>
    ///     Factory responsible for populating the repositories and registering them with the container
    /// </summary>
    internal class CoreComponentFactory
    {
        /// <summary>
        ///     The log
        /// </summary>
        public ILog Log = LogManager.GetLogger(typeof(CoreComponentFactory));

        /// <summary>
        ///     Initializes a new instance of the <see cref="CoreComponentFactory" /> class.
        /// </summary>
        /// <param name="compositionContainer">The composition container.</param>
        /// <param name="dumpFile">Dump file to analzye</param>
        /// <exception cref="ArgumentNullException">
        ///     compositionContainer
        ///     or
        ///     dataTarget
        /// </exception>
        public CoreComponentFactory(CompositionContainer compositionContainer, FileInfo dumpFile)
        {
            CompositionContainer =
                compositionContainer ?? throw new ArgumentNullException(nameof(compositionContainer));
            DumpFile = dumpFile ?? throw new ArgumentNullException(nameof(dumpFile));
            try
            {
                DataTarget = DataTarget.LoadCrashDump(dumpFile.FullName);
            }
            catch (Exception e)
            {
                // todo: should check this ahead of time
                Log.Fatal(
                    $"Unable to open crash dump: {e.Message}, Does the dump file exist and do you have the x64 folder of the Windows Debugging Kit in your path?");
            }
        }

        /// <summary>
        ///     Gets or sets the location of the dump file
        /// </summary>
        /// <value>
        ///     The dump file location.
        /// </value>
        public FileInfo DumpFile { get; protected set; }

        /// <summary>
        ///     Gets or sets the composition container.
        /// </summary>
        /// <value>
        ///     The composition container.
        /// </value>
        public CompositionContainer CompositionContainer { get; set; }

        /// <summary>
        ///     Gets or sets the data target.
        /// </summary>
        /// <value>
        ///     The data target.
        /// </value>
        public DataTarget DataTarget { get; set; }

        /// <summary>
        ///     Registers the repositories.
        /// </summary>
        // todo: this is too big
        public void RegisterRepositories()
        {
            // todo: check symbols
            var heapObjectExtractors = CompositionContainer.GetExportedValues<IDumpObjectExtractor>().ToList();

            ClrRuntime runtime;

            try
            {
                Log.Trace($"Attempting to create the CLRMd runtime");
                runtime = DataTarget.ClrVersions.Single().CreateRuntime();
            }
            catch (Exception)
            {
                Log.Error($"Unable to create CLRMd runtime");
                throw;
            }

            if (DataTarget.IsMinidump)
                Log.Warn(
                    "The provided dump is a mini dump and results will not contain any heap information (objects, etc)");

            if (!runtime.Heap.CanWalkHeap)
                Log.Warn("CLRMd reports that the heap is unwalkable, results might vary");

            var dumpInformationRepository = new DumpInformationRepository(DataTarget, runtime, DumpFile);
            var settingsRepository = new SettingsRepository(Settings.GetSettings());
            var eventHub = new EventHub();
            /*
             * IMPORTANT
             * These are left as thread unsafe collections because they are written to on 1 thread and then
             * READ ONLY accessed from multiple threads
             */
            var objectStore = new Dictionary<ulong, DumpObject>();
            var objectHierarchy = new Dictionary<ulong, List<ulong>>();
            var threadStore = new Dictionary<uint, DumpThread>();
            var appDomainStore = new Dictionary<ulong, DumpAppDomain>();
            var moduleStore = new Dictionary<(ulong, string), DumpModule>();
            //var dynamicModuleStore = new Dictionary<ulong>();
            var typeStore =
                new Dictionary<DumpTypeKey, DumpType>(); // same type can be loaded into multiple app domains (think IIS)
            var objectRootsStore = new Dictionary<ulong, DumpObjectRoot>();
            SetupModulesAndTypes(runtime, appDomainStore, typeStore, moduleStore);
            SetupObjects(heapObjectExtractors, runtime, objectStore, objectHierarchy, typeStore, appDomainStore,
                objectRootsStore);
            EstablishObjectRelationships(objectHierarchy, objectStore);
            objectHierarchy = null;
            GC.Collect();
            SetupThreads(runtime, threadStore, objectRootsStore);

            var dumpRepo = new DumpObjectRepository(objectStore, objectRootsStore);
            var threadRepo = new DumpThreadRepository(threadStore);
            var appDomainRepo = new DumpAppDomainRepository(appDomainStore);

            var moduleRepo = new DumpModuleRepository(moduleStore);
            var typeRepo = new DumpTypeRepository(typeStore);

            CompositionContainer.ComposeExportedValue(eventHub);
            CompositionContainer.ComposeExportedValue(dumpInformationRepository);
            CompositionContainer.ComposeExportedValue(settingsRepository);
            CompositionContainer.ComposeExportedValue(dumpRepo);
            CompositionContainer.ComposeExportedValue(threadRepo);
            CompositionContainer.ComposeExportedValue(appDomainRepo);
            CompositionContainer.ComposeExportedValue(moduleRepo);
            CompositionContainer.ComposeExportedValue(typeRepo);
        }

        private void EstablishObjectRelationships(Dictionary<ulong, List<ulong>> objectHierarchy,
            Dictionary<ulong, DumpObject> objectStore)
        {
            Log.Trace("Setting relationship references on the extracted objects");
            Parallel.ForEach(objectHierarchy, relationship =>
            {
                if (!objectStore.ContainsKey(relationship.Key))
                {
                    Log.Error(
                        $"Object relationship says that there is a parent-child relationship between {relationship.Key} and {relationship.Value}, but cannot find the parent");
                    return;
                }
                var parent = objectStore[relationship.Key];

                foreach (var childAddress in relationship.Value)
                {
                    if (!objectStore.ContainsKey(childAddress))
                    {
                        Log.Error(
                            $"Object relationship says that there is a parent-child relationship between {relationship.Key} and {childAddress}, but cannot find the child");
                        return;
                    }
                    var child = objectStore[childAddress];
                    parent.AddReference(child);
                    child.AddReferencer(parent);
                }
            });
        }

        private void SetupModulesAndTypes(ClrRuntime rt, Dictionary<ulong, DumpAppDomain> appDomainStore,
            Dictionary<DumpTypeKey, DumpType> typeStore,
            Dictionary<(ulong, string), DumpModule> moduleStore)
        {
            Log.Trace("Extracting Module, AppDomain, and Type information");
            var baseClassMapping = new Dictionary<DumpTypeKey, DumpTypeKey>();
            foreach (var clrModule in rt.Modules)
            {
                var dumpModule = new DumpModule
                {
                    Name = clrModule.Name,
                    ImageBase = clrModule.ImageBase == 0 ? null : (ulong?) clrModule.ImageBase,
                    AssemblyId = clrModule.AssemblyId,
                    AssemblyName = clrModule.AssemblyName,
                    DebuggingMode = clrModule.DebuggingMode,
                    FileName = clrModule.FileName,
                    IsDynamic = clrModule.IsDynamic
                };

                foreach (var clrAppDomain in clrModule.AppDomains)
                {
                    if (!appDomainStore.ContainsKey(clrAppDomain.Address))
                    {
                        var newDumpAppDomain = new DumpAppDomain
                        {
                            Address = clrAppDomain.Address,
                            Name = clrAppDomain.Name,
                            ApplicationBase = clrAppDomain.ApplicationBase,
                            ConfigFile = clrAppDomain.ConfigurationFile
                        };

                        appDomainStore.Add(newDumpAppDomain.Address, newDumpAppDomain);
                    }
                    var dumpAppDomain = appDomainStore[clrAppDomain.Address];
                    dumpModule.AppDomainsInternal.Add(appDomainStore[clrAppDomain.Address]);
                    dumpAppDomain.LoadedModulesInternal.Add(dumpModule);
                }

                foreach (var clrType in clrModule.EnumerateTypes())
                {
                    var key = new DumpTypeKey(clrType.MethodTable, clrType.Name);
                    if (typeStore.ContainsKey(key)) continue;
                    baseClassMapping.Add(key, new DumpTypeKey(clrType.MethodTable, clrType.Name));

                    var newDumpType = new DumpType
                    {
                        DumpTypeKey = key,
                        MethodTable = clrType.MethodTable,
                        Name = clrType.Name,
                        Module = dumpModule,
                        BaseSize = clrType.BaseSize,
                        IsInternal = clrType.IsInternal,
                        IsString = clrType.IsString,
                        IsInterface = clrType.IsInterface,
                        ContainsPointers = clrType.ContainsPointers,
                        IsAbstract = clrType.IsAbstract,
                        IsArray = clrType.IsArray,
                        IsEnum = clrType.IsEnum,
                        IsException = clrType.IsException,
                        IsFinalizable = clrType.IsFinalizable,
                        IsPointer = clrType.IsPointer,
                        IsPrimitive = clrType.IsPrimitive,
                        IsPrivate = clrType.IsPrivate,
                        IsProtected = clrType.IsProtected,
                        IsRuntimeType = clrType.IsRuntimeType,
                        IsSealed = clrType.IsSealed
                    };
                    dumpModule.TypesInternal.Add(newDumpType);
                    typeStore.Add(new DumpTypeKey(clrType.MethodTable, clrType.Name), newDumpType);
                }

                moduleStore.Add((dumpModule.AssemblyId, dumpModule.Name),
                    dumpModule);
            }
            foreach (var pair in baseClassMapping)
                typeStore[pair.Key].BaseDumpType = typeStore[pair.Value];
        }

        /// <summary>
        ///     Setups the threads.
        /// </summary>
        /// <param name="rt">The rt.</param>
        /// <param name="threadStore">The thread store.</param>
        /// <param name="objectRootsStore">The object roots store.</param>
        private void SetupThreads(ClrRuntime rt,
            Dictionary<uint, DumpThread> threadStore, Dictionary<ulong, DumpObjectRoot> objectRootsStore)
        {
            Log.Trace("Extracting information about the threads");
            foreach (var thread in rt.Threads)
            {
                var dumpThread = new DumpThread
                {
                    OsId = thread.OSThreadId,
                    ManagedStackFrames = thread.StackTrace.Select(f => new DumpStackFrame
                    {
                        IsManaged = f.Kind == ClrStackFrameType.ManagedMethod,
                        InstructionPointer = f.InstructionPointer,
                        ModuleName = f.ModuleName,
                        StackPointer = f.StackPointer,
                        DisplayString =
                            f.ToString() // todo: I've seen where this throws a null reference exception - look out
                    }).ToList()
                };
                foreach (var extractedStackFrame in dumpThread.ManagedStackFrames)
                    extractedStackFrame.Thread = dumpThread;

                dumpThread.ObjectRoots = thread.EnumerateStackObjects()
                    .Where(o =>
                    {
                        if (objectRootsStore.ContainsKey(o.Address))
                            return true;
                        Log.Warn(
                            $"Thread {thread.OSThreadId} claims to have an object root at {o.Address} but there is no corresponding object at {o.Object}");
                        return false;
                    }).Select(o =>
                    {
                        var root = objectRootsStore[o.Address];
                        root.Thread = dumpThread;
                        root.StackFrame =
                            dumpThread.ManagedStackFrames.FirstOrDefault(f =>
                                f.StackPointer == o.StackFrame.StackPointer);
                        return root;
                    }).ToList();

                if (!threadStore.ContainsKey(dumpThread.OsId))
                    threadStore.Add(dumpThread.OsId, dumpThread);
                else
                    Log.Error(
                        $"Extracted a thread but there is already an entry with os id: {dumpThread.OsId}, you should investigate these manually");
            }

            var debuggerProxy = new DebuggerProxy(DataTarget.DebuggerInterface);
            Log.Trace("Loading debugger extensions");
            debuggerProxy.Execute(".load sosex");
            debuggerProxy.Execute(".load mex");
            debuggerProxy.Execute(".load netext");
            var res = debuggerProxy.Execute("!mu"); // forces sosex to load the appropriate SOS.dll
            Log.Trace("Calling !runaway");
            var runawayData = debuggerProxy.Execute("!runaway");
            var isUserMode = false;
            var isKernelMode = false;
            foreach (var line in runawayData.Split('\n'))
            {
                if (Regex.IsMatch(line, "User Mode Time"))
                {
                    isUserMode = true;
                    continue;
                }
                if (Regex.IsMatch(line, "Kernel Mode Time"))
                {
                    isUserMode = false;
                    isKernelMode = true;
                }
                var match = Regex.Match(line,
                    @"(?<index>\d+):(?<id>[a-zA-Z0-9]+)\s*(?<days>\d+) days (?<time>\d+:\d{2}:\d{2}.\d{3})");
                if (!match.Success) continue;
                var index = uint.Parse(match.Groups["index"].Value);
                var id = Convert.ToUInt32(match.Groups["id"].Value, 16);
                var days = uint.Parse(match.Groups["days"].Value);
                var time = match.Groups["time"].Value;
                var timeSpan = TimeSpan.Parse(time, CultureInfo.CurrentCulture);
                timeSpan = timeSpan.Add(TimeSpan.FromDays(days));
                var dumpThread = threadStore.Values.SingleOrDefault(x => x.OsId == id);
                if (dumpThread == null)
                {
                    dumpThread = new DumpThread {OsId = id};
                    threadStore.Add(dumpThread.OsId, dumpThread);
                }
                dumpThread.DebuggerIndex = index;

                if (isUserMode)
                    dumpThread.UserModeTime = timeSpan;
                else if (isKernelMode)
                    dumpThread.KernelModeTime = timeSpan;
            }

            // todo: save !runaway, !eestack to disk and zip up and send to s3
            Log.Trace("Calling !EEStack");
            var eestackCommandResult = debuggerProxy.Execute("!eestack");
            var eeStacks = Regex.Split(eestackCommandResult, "---------------------------------------------")
                .Select(threadInfo => threadInfo.Trim()).Skip(1).ToArray();
            foreach (var eeStack in eeStacks)
            {
                var lines = eeStack.Split('\n');
                var header = lines.Take(3).ToArray();
                var stackFrames = lines.Skip(3).Select(x => x.Substring("0000000000000000 0000000000000000 ".Length));

                var threadIndex =
                    Convert.ToUInt32(Regex.Match(header[0], @"Thread\s+(?<index>\d+)").Groups["index"].Value);
                var currentFrame = header[1].Substring("Current frame: ".Length);

                var existingThread = threadStore.Values.FirstOrDefault(t => t.DebuggerIndex == threadIndex);
                if (existingThread == null)
                {
                    Log.Error(
                        $"Found thread in !eestack that wasn't in !runaway: {threadIndex}, consider investigating the dump manually");
                    continue;
                }

                existingThread.CurrentFrame = currentFrame;
                existingThread.EEStackFrames = stackFrames.ToList();
            }
        }

        private void SetupObjects(List<IDumpObjectExtractor> heapObjectExtractors, ClrRuntime rt,
            Dictionary<ulong, DumpObject> objectStore, Dictionary<ulong, List<ulong>> objectHierarchy,
            Dictionary<DumpTypeKey, DumpType> typeStore, Dictionary<ulong, DumpAppDomain> appDomainStore,
            Dictionary<ulong, DumpObjectRoot> objectRootStore)
        {
            Log.Trace("Using registered object extractors to process objects on the heap");
            var defaultExtractor = new DefaultObjectExtractor();
            foreach (var clrObject in rt.Heap.EnumerateObjects()
                .Where(o => !o.IsNull && !o.Type.IsFree))
            {
                var isExtracted = false;
                foreach (var heapObjectExtractor in heapObjectExtractors)
                {
                    if (!heapObjectExtractor.CanExtract(clrObject, rt))
                        continue;
                    var extracted = heapObjectExtractor.Extract(clrObject, rt);
                    var dumpType = typeStore[new DumpTypeKey(clrObject.Type.MethodTable, clrObject.Type.Name)];
                    extracted.DumpType = dumpType;
                    dumpType.ObjectsInternal.Add(extracted.Address, extracted);
                    objectStore.Add(clrObject.Address, extracted);
                    isExtracted = true;
                    break;
                }
                if (!isExtracted)
                {
                    var newDumpObject = defaultExtractor.Extract(clrObject, rt);
                    objectStore.Add(newDumpObject.Address, newDumpObject);
                }
                objectHierarchy.Add(clrObject.Address, new List<ulong>());

                foreach (var clrObjectRef in clrObject.EnumerateObjectReferences(true))
                    objectHierarchy[clrObject.Address].Add(clrObjectRef.Address);
            }

            Log.Trace("Extracting object roots from all threads");
            foreach (var clrRoot in rt.Threads.SelectMany(t => t.EnumerateStackObjects()))
            {
                var dumpRootObject = new DumpObjectRoot
                {
                    Address = clrRoot.Address,
                    Name = clrRoot.Name,
                    IsAsyncIoPinning = clrRoot.Kind == GCRootKind.AsyncPinning,
                    IsFinalizerQueue = clrRoot.Kind == GCRootKind.Finalizer,
                    IsInteriorPointer = clrRoot.IsInterior,
                    IsLocalVar = clrRoot.Kind == GCRootKind.LocalVar,
                    IsPossibleFalsePositive = clrRoot.IsPossibleFalsePositive,
                    IsPinned = clrRoot.IsPinned,
                    IsStaticVariable = clrRoot.Kind == GCRootKind.StaticVar,
                    IsStrongHandle = clrRoot.Kind == GCRootKind.Strong,
                    IsThreadStaticVariable = clrRoot.Kind == GCRootKind.ThreadStaticVar,
                    IsStrongPinningHandle = clrRoot.Kind == GCRootKind.Pinning,
                    IsWeakHandle = clrRoot.Kind == GCRootKind.Weak
                };
                var appDomainAddress = clrRoot.AppDomain?.Address;
                if (appDomainAddress.HasValue && appDomainStore.ContainsKey(appDomainAddress.Value))
                    dumpRootObject.AppDomain = appDomainStore[appDomainAddress.Value];

                if (objectStore.ContainsKey(clrRoot.Object))
                    dumpRootObject.RootedObject = objectStore[clrRoot.Object];

                objectRootStore.Add(dumpRootObject.Address, dumpRootObject);
            }
        }
    }
}