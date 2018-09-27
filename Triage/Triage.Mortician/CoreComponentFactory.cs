// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 12-12-2017
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-26-2018
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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Common.Logging;
using Microsoft.Diagnostics.Runtime;
using Newtonsoft.Json;
using Triage.Mortician.Core;
using Triage.Mortician.Reports;
using Triage.Mortician.Reports.DumpDomain;
using Triage.Mortician.Reports.EeStack;
using Triage.Mortician.Reports.Runaway;
using Triage.Mortician.Repositories;

namespace Triage.Mortician
{
    /// <summary>
    ///     Factory responsible for populating the repositories and registering them with the container
    /// </summary>
    internal class CoreComponentFactory
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CoreComponentFactory" /> class.
        /// </summary>
        /// <param name="compositionContainer">The composition container.</param>
        /// <param name="dumpFile">Dump file to analzye</param>
        /// <exception cref="System.ArgumentNullException">
        ///     compositionContainer
        ///     or
        ///     dumpFile
        /// </exception>
        /// <exception cref="System.ApplicationException">
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     compositionContainer
        ///     or
        ///     dumpFile
        /// </exception>
        /// <exception cref="ApplicationException"></exception>
        public CoreComponentFactory(CompositionContainer compositionContainer, FileInfo dumpFile)
        {
            CompositionContainer =
                compositionContainer ?? throw new ArgumentNullException(nameof(compositionContainer));
            Converter = compositionContainer.GetExportedValue<IConverter>();
            DumpFile = dumpFile ?? throw new ArgumentNullException(nameof(dumpFile));
            try
            {
                DataTarget = DataTarget.LoadCrashDump(dumpFile.FullName);
            }
            catch (IOException e)
            {
                var message =
                    $"Unable to open crash dump: {e.Message}, Does the dump file exist?";
                Log.Fatal(message);
                throw new ApplicationException(message, e);
            }
            catch (Exception e)
            {
                // todo: support x86
                var message =
                    $"Unable to open crash dump: {e.Message}, Do you have x64 folder of the Windows Debugging Kit in your path?";
                Log.Fatal(message);
                throw new ApplicationException(message, e);
            }

            DebuggerProxy = new DebuggerProxy(DataTarget.DebuggerInterface);
            LoadPlugins();
            ProcessReports();
        }

        /// <summary>
        ///     The log
        /// </summary>
        public ILog Log = LogManager.GetLogger(typeof(CoreComponentFactory));

        /// <summary>
        ///     Registers the repositories.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="settings">The settings.</param>
        // todo: this is too big
        public void RegisterRepositories(DefaultOptions options, IEnumerable<ISettings> settings = null)
        {
            var heapObjectExtractors = CompositionContainer.GetExportedValues<IDumpObjectExtractor>().ToList();

            ClrRuntime runtime;
            
            try
            {
                Log.Trace($"Attempting to create the CLRMd runtime");
                // todo: handle multiple clrs? does anyone still do that?
                runtime = DataTarget.ClrVersions.Single().CreateRuntime();
            }
            catch (Exception)
            {
                Log.Error($"Unable to create CLRMd runtime");
                throw;
            }

            // todo: do we even know if we handle mini dumps? write an integration test for it
            if (DataTarget.IsMinidump)
                Log.Warn(
                    "The provided dump is a mini dump and results will not contain any heap information (objects, etc)");

            if (!runtime.Heap.CanWalkHeap)
                Log.Warn("CLRMd reports that the heap is unwalkable, results might vary");

            var dumpInformationRepository = new DumpInformationRepository(DataTarget, runtime, DumpFile);
            var eventHub = new EventHub();
            /*
             * IMPORTANT
             * These are left as thread unsafe collections because they must execute on the same thread
             * READ ONLY accessed from multiple threads
             */
            var objectStore = new Dictionary<ulong, DumpObject>();
            var objectHierarchy = new Dictionary<ulong, List<ulong>>();
            var threadStore = new Dictionary<uint, DumpThread>();
            var appDomainStore = new Dictionary<ulong, DumpAppDomain>();
            var moduleStore = new Dictionary<(ulong, string), DumpModule>();
            var handleStore = new Dictionary<ulong, DumpHandle>();
            var typeStore =
                new Dictionary<DumpTypeKey, DumpType>(); // same type can be loaded into multiple app domains (think IIS)
            var objectRootsStore = new Dictionary<ulong, DumpObjectRoot>();
            var finalizerStore = new Dictionary<ulong, DumpObject>();
            var blockingObjectStore = new Dictionary<ulong, DumpBlockingObject>();

            SetupAppDomains(appDomainStore);
            SetupModulesAndTypes(runtime, appDomainStore, typeStore, moduleStore);
            SetupObjects(heapObjectExtractors, runtime, objectStore, objectHierarchy, typeStore, appDomainStore,
                objectRootsStore, finalizerStore, blockingObjectStore);
            SetupHandles(runtime, appDomainStore, typeStore, handleStore);
            EstablishObjectRelationships(objectHierarchy, objectStore);
            GC.Collect();
            SetupThreads(runtime, threadStore, objectRootsStore);

            var dumpRepo = new DumpObjectRepository(objectStore, objectRootsStore, finalizerStore, blockingObjectStore);
            var threadRepo = new DumpThreadRepository(threadStore);
            var appDomainRepo = new DumpAppDomainRepository(appDomainStore);
            var moduleRepo = new DumpModuleRepository(moduleStore);
            var typeRepo = new DumpTypeRepository(typeStore);
            var handleRepo = new DumpHandleRepository(handleStore);
            CompositionContainer.ComposeExportedValue<IEventHub>(eventHub);
            CompositionContainer.ComposeExportedValue<IDumpInformationRepository>(dumpInformationRepository);
            CompositionContainer.ComposeExportedValue<IDumpObjectRepository>(dumpRepo);
            CompositionContainer.ComposeExportedValue<IDumpThreadRepository>(threadRepo);
            CompositionContainer.ComposeExportedValue<IDumpAppDomainRepository>(appDomainRepo);
            CompositionContainer.ComposeExportedValue<IDumpModuleRepository>(moduleRepo);
            CompositionContainer.ComposeExportedValue<IDumpTypeRepository>(typeRepo);
            CompositionContainer.ComposeExportedValue<IDumpHandleRepository>(handleRepo);

            var settingsToAdd = settings ?? GetSettings(options.SettingsFile);
            foreach (var setting in settingsToAdd) CompositionContainer.ComposeExportedValue(setting);
        }

        private void SetupHandles(ClrRuntime runtime, Dictionary<ulong, DumpAppDomain> appDomainStore, Dictionary<DumpTypeKey, DumpType> typeStore, Dictionary<ulong, DumpHandle> handleStore)
        {
            foreach (var handle in runtime.EnumerateHandles())
            {
                var dependentTypeKey = new DumpTypeKey(handle?.DependentType?.MethodTable ?? 0, handle?.DependentType?.Name);
                var objectTypeKey = new DumpTypeKey(handle?.Type?.MethodTable ?? 0, handle?.Type?.Name);
                var dumpHandle = new DumpHandle()
                {
                    Address = handle.Address,
                    AppDomain = appDomainStore.Values.FirstOrDefault(a => a.Address == handle?.AppDomain?.Address),
                    DependentTarget = handle.DependentTarget,
                    DependentType = typeStore.ContainsKey(dependentTypeKey) ? typeStore[dependentTypeKey] : null,
                    HandleType = Converter.Convert(handle.HandleType),
                    IsPinned = handle.IsPinned,
                    IsStrong = handle.IsStrong,
                    ObjectAddress = handle.Object,
                    ObjectType = typeStore.ContainsKey(objectTypeKey) ? typeStore[objectTypeKey] : null,
                    RefCount = handle.RefCount
                };
                handleStore[dumpHandle.Address] = dumpHandle;
            }
        }

        /// <summary>
        ///     Establishes the object relationships.
        /// </summary>
        /// <param name="objectHierarchy">The object hierarchy.</param>
        /// <param name="objectStore">The object store.</param>
        internal void EstablishObjectRelationships(Dictionary<ulong, List<ulong>> objectHierarchy,
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

        /// <summary>
        ///     Collects the ee stack information.
        /// </summary>
        /// <param name="threadStore">The thread store.</param>
        private void ApplyEeStackInformation(Dictionary<uint, DumpThread> threadStore)
        {
            var report = CompositionContainer.GetExportedValue<EeStackReport>();
            foreach (var eeStackThread in report.Threads)
            {
                var existing = threadStore.Values.FirstOrDefault(t => t.DebuggerIndex == eeStackThread.Index);
                if (existing == null)
                {
                    Log.Error(
                        $"!eestack indicates that there is a thread with index {eeStackThread.Index}, but it was not found in the thread store. This should not happen. Investigate dump and check why this happened -it is probably a bug.");
                    continue;
                }

                existing.CurrentFrame = eeStackThread.CurrentLocation;
            }
        }

        /// <summary>
        ///     Collects the runaway information.
        /// </summary>
        /// <param name="threadStore">The thread store.</param>
        private void ApplyRunawayInformation(Dictionary<uint, DumpThread> threadStore)
        {
            Log.Trace("Calling !runaway");
            var runawayReport = CompositionContainer.GetExportedValue<RunawayReport>();
            foreach (var runawayReportRunawayLine in runawayReport.RunawayLines)
            {
                if (!threadStore.TryGetValue(runawayReportRunawayLine.ThreadId, out var dumpThread))
                    continue; // todo: log?
                dumpThread.UserModeTime = runawayReportRunawayLine.UserModeTime;
                dumpThread.KernelModeTime = runawayReportRunawayLine.KernelModeTime;
            }
        }

        /// <summary>
        ///     Extracts the heap objects.
        /// </summary>
        /// <param name="heapObjectExtractors">The heap object extractors.</param>
        /// <param name="runtime">The rt.</param>
        /// <param name="objectStore">The object store.</param>
        /// <param name="objectHierarchy">The object hierarchy.</param>
        /// <param name="typeStore">The type store.</param>
        private void ExtractHeapObjects(List<IDumpObjectExtractor> heapObjectExtractors, ClrRuntime runtime,
            Dictionary<ulong, DumpObject> objectStore,
            Dictionary<ulong, List<ulong>> objectHierarchy, Dictionary<DumpTypeKey, DumpType> typeStore, Dictionary<ulong, DumpObject> finalizerQueue, Dictionary<ulong, DumpBlockingObject> blockingObjects)
        {
            Log.Trace("Using registered object extractors to process objects on the heap");
            var defaultExtractor = new DefaultObjectExtractor();
            foreach (var clrObject in runtime.Heap.EnumerateObjects()
                .Where(o => !o.IsNull && !o.Type.IsFree))
            {
                var isExtracted = false;
                var convertedClrObject = Converter.Convert(clrObject);
                var convertedRuntime = Converter.Convert(runtime);
                foreach (var heapObjectExtractor in heapObjectExtractors)
                {
                    if (!heapObjectExtractor.CanExtract(convertedClrObject, convertedRuntime))
                        continue;
                    var extracted = heapObjectExtractor.Extract(convertedClrObject, convertedRuntime);
                    var dumpType = typeStore[new DumpTypeKey(clrObject.Type.MethodTable, clrObject.Type.Name)];
                    extracted.DumpType = dumpType;
                    dumpType.ObjectsInternal.Add(extracted.Address, extracted);
                    objectStore.Add(clrObject.Address, extracted);
                    isExtracted = true;
                    break;
                }

                if (!isExtracted)
                {
                    var newDumpObject = defaultExtractor.Extract(convertedClrObject, convertedRuntime);
                    objectStore.Add(newDumpObject.Address, newDumpObject);
                }

                objectHierarchy.Add(clrObject.Address, new List<ulong>());

                foreach (var clrObjectRef in clrObject.EnumerateObjectReferences(true))
                    objectHierarchy[clrObject.Address].Add(clrObjectRef.Address);
            }

            foreach (var address in runtime.EnumerateFinalizerQueueObjectAddresses())
            {
                if (objectStore.TryGetValue(address, out var o))
                {
                    o.IsInFinalizerQueue = true;
                    finalizerQueue.Add(address, o);
                }
            }

            foreach (var blockingObject in runtime.Heap.EnumerateBlockingObjects())
            {
                blockingObjects.Add(blockingObject.Object, new DumpBlockingObject()
                {
                    Address = blockingObject.Object,
                    BlockingReason = Converter.Convert(blockingObject.Reason),
                    HasSingleOwner = blockingObject.HasSingleOwner,
                    IsLocked = blockingObject.Taken,
                    RecursionCount = blockingObject.RecursionCount,
                    // todo: owners
                });
            }
        }

        /// <summary>
        ///     Extracts the stack objects.
        /// </summary>
        /// <param name="rt">The rt.</param>
        /// <param name="objectStore">The object store.</param>
        /// <param name="appDomainStore">The application domain store.</param>
        /// <param name="objectRootStore">The object root store.</param>
        private void ExtractStackObjects(ClrRuntime rt, Dictionary<ulong, DumpObject> objectStore,
            Dictionary<ulong, DumpAppDomain> appDomainStore,
            Dictionary<ulong, DumpObjectRoot> objectRootStore)
        {
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

        /// <summary>
        ///     Gets the settings.
        /// </summary>
        /// <param name="settingsPath">The settings path.</param>
        /// <returns>IEnumerable&lt;ISettings&gt;.</returns>
        private IEnumerable<ISettings> GetSettings(string settingsPath = null)
        {
            string settingsText;
            if (settingsPath != null)
                settingsText = File.ReadAllText(settingsPath);
            else
                settingsText = File.ReadAllText("settings.json");
            var converter = new SettingsJsonConverter();
            return JsonConvert.DeserializeObject<IEnumerable<ISettings>>(settingsText, converter);
        }

        /// <summary>
        ///     Loads the plugins.
        /// </summary>
        private void LoadPlugins()
        {
            string cwd = Environment.CurrentDirectory;
            string extPath = DebuggerProxy.Execute(".extpath");
            DebuggerProxy.Execute(@".sympath srv*https://msdl.microsoft.com/download/symbols");
            string suffix = IntPtr.Size == 4 ? "32" : "64";
            var loadSosex = DebuggerProxy.Execute($".load {cwd}\\sosex{suffix}");
            var loadMex = DebuggerProxy.Execute($".load {cwd}\\mex{suffix}");
            DebuggerProxy
                .Execute("!mu"); // forces sosex to load the appropriate SOS.dll // todo: should be possible from API
            DebuggerProxy.Execute("!eestack"); // todo: figure out a better way to force symbol loading
        }

        /// <summary>
        /// Processes the reports.
        /// </summary>
        private void ProcessReports()
        {
            var reportFactories = CompositionContainer.GetExportedValues<IReportFactory>().ToArray();
            var failedSetup = new List<IReportFactory>();
            // setup must happen serially on the main thread because it uses COM
            foreach (var reportFactory in reportFactories)
                try
                {
                    reportFactory.Setup(DebuggerProxy);
                }
                catch (Exception e)
                {
                    failedSetup.Add(reportFactory);
                }

            var setupFactories = reportFactories.Except(failedSetup).ToArray();
            var tasks = setupFactories.Select(f => Task.Run(() => f.Process())).ToArray();
            try
            {
                Task.WaitAll(tasks);
            }
            catch (AggregateException e)
            {
                e.Flatten().Handle(exception =>
                {
                    Log.Error($"Error occurred during report processing");
                    return true;
                });
            }

            foreach (var (task, factory) in tasks.Zip(setupFactories, (task, factory) => (task, factory)))
            {
                if (task.IsFaulted)
                {
                    Log.Error(
                        $"There was a problem processing the report for {factory.DisplayName}, it will not be available for this execution. Make sure that your report factory can correctly handle all variations of output.",
                        task.Exception);
                    continue;
                }

                CompositionContainer.ComposeExportedValue(task.Result);
            }
        }

        /// <summary>
        ///     Setups the application domains.
        /// </summary>
        /// <param name="appDomainStore">The application domain store.</param>
        private void SetupAppDomains(Dictionary<ulong, DumpAppDomain> appDomainStore)
        {
            var results = CompositionContainer.GetExportedValues<IReport>();
            var report = results.OfType<DumpDomainReport>().First();
            foreach (var current in report.AppDomainsInternal)
                if (!appDomainStore.ContainsKey(current.Address))
                    appDomainStore.Add(current.Address, new DumpAppDomain
                    {
                        Address = current.Address,
                        Name = current.Name
                    });
        }

        /// <summary>
        ///     Setups the modules and types.
        /// </summary>
        /// <param name="rt">The rt.</param>
        /// <param name="appDomainStore">The application domain store.</param>
        /// <param name="typeStore">The type store.</param>
        /// <param name="moduleStore">The module store.</param>
        private void SetupModulesAndTypes(ClrRuntime rt, Dictionary<ulong, DumpAppDomain> appDomainStore,
            Dictionary<DumpTypeKey, DumpType> typeStore,
            Dictionary<(ulong, string), DumpModule> moduleStore)
        {
            Log.Trace("Extracting Module, AppDomain, and Type information");
            var baseClassMapping = new Dictionary<DumpTypeKey, DumpTypeKey>();
            ExtractModuleInformation(rt, appDomainStore, typeStore, moduleStore, baseClassMapping);
            foreach (var pair in baseClassMapping)
                typeStore[pair.Key].BaseDumpType = typeStore[pair.Value];
        }

        private static void ExtractModuleInformation(ClrRuntime rt, Dictionary<ulong, DumpAppDomain> appDomainStore, Dictionary<DumpTypeKey, DumpType> typeStore,
            Dictionary<(ulong, string), DumpModule> moduleStore, Dictionary<DumpTypeKey, DumpTypeKey> baseClassMapping)
        {
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

                UpdateAppDomains(appDomainStore, clrModule, dumpModule);
                AddTypesToTypeStore(typeStore, clrModule, baseClassMapping, dumpModule);
                moduleStore.Add((dumpModule.AssemblyId, dumpModule.Name),
                    dumpModule);
            }
        }

        private static void AddTypesToTypeStore(Dictionary<DumpTypeKey, DumpType> typeStore, ClrModule clrModule, Dictionary<DumpTypeKey, DumpTypeKey> baseClassMapping,
            DumpModule dumpModule)
        {
            foreach (var clrType in clrModule.EnumerateTypes())
            {
                if (clrType.MethodTable == 0 || clrType.Name == null)
                    continue;
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
        }

        private static void UpdateAppDomains(Dictionary<ulong, DumpAppDomain> appDomainStore, ClrModule clrModule, DumpModule dumpModule)
        {
            foreach (var clrAppDomain in clrModule.AppDomains)
            {
                var dumpAppDomain = appDomainStore[clrAppDomain.Address];
                dumpAppDomain.ApplicationBase = clrAppDomain.ApplicationBase;
                dumpAppDomain.ConfigFile = clrAppDomain.ConfigurationFile;
                dumpModule.AppDomainsInternal.Add(appDomainStore[clrAppDomain.Address]);
                dumpAppDomain.LoadedModulesInternal.Add(dumpModule);
            }
        }

        /// <summary>
        ///     Setups the objects.
        /// </summary>
        /// <param name="heapObjectExtractors">The heap object extractors.</param>
        /// <param name="runtime">The rt.</param>
        /// <param name="objectStore">The object store.</param>
        /// <param name="objectHierarchy">The object hierarchy.</param>
        /// <param name="typeStore">The type store.</param>
        /// <param name="appDomainStore">The application domain store.</param>
        /// <param name="objectRootStore">The object root store.</param>
        private void SetupObjects(List<IDumpObjectExtractor> heapObjectExtractors, ClrRuntime runtime,
            Dictionary<ulong, DumpObject> objectStore, Dictionary<ulong, List<ulong>> objectHierarchy,
            Dictionary<DumpTypeKey, DumpType> typeStore, Dictionary<ulong, DumpAppDomain> appDomainStore,
            Dictionary<ulong, DumpObjectRoot> objectRootStore, Dictionary<ulong, DumpObject> finalizerQueue, Dictionary<ulong, DumpBlockingObject> blockingObjects)
        {
            ExtractHeapObjects(heapObjectExtractors, runtime, objectStore, objectHierarchy, typeStore, finalizerQueue, blockingObjects);
            ExtractStackObjects(runtime, objectStore, appDomainStore, objectRootStore);
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
            // todo: priority: rewrite this garbage
            Log.Trace("Extracting information about the threads");
            foreach (var thread in rt.Threads)
            {
                var dumpThread = new DumpThread
                {
                    OsId = thread.OSThreadId,
                    ManagedStackFramesInternal = thread.StackTrace.Select(f => new DumpStackFrame
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
                                f.StackPointer == o?.StackFrame?.StackPointer);
                        return root;
                    }).ToList();

                if (!threadStore.ContainsKey(dumpThread.OsId))
                    threadStore.Add(dumpThread.OsId, dumpThread);
                else
                    Log.Error(
                        $"Extracted a thread but there is already an entry with os id: {dumpThread.OsId}, you should investigate these manually");
            }

            foreach (var gcThreadOsId in rt.EnumerateGCThreads())
            {
                var osId = Convert.ToUInt32(gcThreadOsId);
                if (threadStore.ContainsKey(osId))
                {
                    threadStore[osId].IsGcThread = true;
                }
            }

            Log.Trace("Loading debugger extensions");
            ApplyRunawayInformation(threadStore);
            ApplyEeStackInformation(threadStore);
        }

        /// <summary>
        ///     Gets or sets the composition container.
        /// </summary>
        /// <value>The composition container.</value>
        public CompositionContainer CompositionContainer { get; set; }

        /// <summary>
        ///     Gets or sets the data target.
        /// </summary>
        /// <value>The data target.</value>
        public DataTarget DataTarget { get; set; }

        /// <summary>
        ///     Gets or sets the debugger proxy.
        /// </summary>
        /// <value>The debugger proxy.</value>
        public DebuggerProxy DebuggerProxy { get; internal set; }

        /// <summary>
        ///     Gets or sets the location of the dump file
        /// </summary>
        /// <value>The dump file location.</value>
        public FileInfo DumpFile { get; protected set; }

        /// <summary>
        ///     Gets or sets the converter.
        /// </summary>
        /// <value>The converter.</value>
        internal IConverter Converter { get; set; }
    }
}