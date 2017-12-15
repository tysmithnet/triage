using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common.Logging;
using Microsoft.Diagnostics.Runtime;

namespace Triage.Mortician
{
    internal class RepositoryFactory
    {
        public ILog Log = LogManager.GetLogger(typeof(RepositoryFactory));

        public RepositoryFactory(CompositionContainer compositionContainer, DataTarget dataTarget)
        {
            CompositionContainer =
                compositionContainer ?? throw new ArgumentNullException(nameof(compositionContainer));
            DataTarget = dataTarget ?? throw new ArgumentNullException(nameof(dataTarget));
        }

        public CompositionContainer CompositionContainer { get; set; }
        public DataTarget DataTarget { get; set; }

        public void RegisterRepositories()
        {
            var heapObjectExtractors = CompositionContainer.GetExportedValues<IDumpObjectExtractor>().ToList();

            ClrRuntime rt;

            try
            {
                rt = DataTarget.ClrVersions.Single().CreateRuntime();
            }
            catch (Exception)
            {
                Log.Error($"Unable to create CLRMd runtime");
                throw;
            }

            if (DataTarget.IsMinidump)
                Log.Warn(
                    "The provided dump is a mini dump and results will not contain any heap information (objects, etc)");

            if (!rt.Heap.CanWalkHeap)
                Log.Warn("CLRMd reports that the heap is unwalkable, results might vary");

            /*
             * IMPORTANT
             * These are left as thread unsafe collections because they are written to on 1 thread and then
             * READ ONLY accessed from multiple threads
             */
            var objectStore = new Dictionary<ulong, DumpObject>();
            var objectHierarchy = new Dictionary<ulong, List<ulong>>();
            var threadStore = new Dictionary<uint, DumpThread>();
            var appDomainStore = new Dictionary<ulong, DumpAppDomain>();
            var moduleStore = new Dictionary<ulong, DumpModule>();
            var typeStore =
                new Dictionary<(ulong, string), DumpType>(); // same type can be loaded into multiple app domains (think IIS)
            var objectRootsStore = new Dictionary<ulong, DumpObjectRoot>();
            SetupModulesAndTypes(rt, appDomainStore, typeStore, moduleStore);
            SetupObjects(heapObjectExtractors, rt, objectStore, objectHierarchy, typeStore, appDomainStore,
                objectRootsStore);
            EstablishObjectRelationships(objectHierarchy, objectStore);
            objectHierarchy = null;
            GC.Collect();
            SetupThreads(rt, objectStore, threadStore, objectRootsStore);

            var dumpRepo = new DumpObjectRepository(objectStore, objectRootsStore);
            var threadRepo = new DumpThreadRepository(threadStore);
            var appDomainRepo = new DumpAppDomainRepository(appDomainStore);
            var moduleRepo = new DumpModuleRepository(moduleStore);

            CompositionContainer.ComposeExportedValue(dumpRepo);
            CompositionContainer.ComposeExportedValue(threadRepo);
            CompositionContainer.ComposeExportedValue(appDomainRepo);
            CompositionContainer.ComposeExportedValue(moduleRepo);
        }

        private void EstablishObjectRelationships(Dictionary<ulong, List<ulong>> objectHierarchy,
            Dictionary<ulong, DumpObject> objectStore)
        {
            Log.Trace("Setting relationship references on the extracted objects");
            Parallel.ForEach(objectHierarchy, relationship =>
            {
                var parent = objectStore[relationship.Key];

                foreach (var childAddress in relationship.Value)
                {
                    var child = objectStore[childAddress];
                    parent.AddReference(child);
                    child.AddReferencer(parent);
                }
            });
        }

        private void SetupModulesAndTypes(ClrRuntime rt, Dictionary<ulong, DumpAppDomain> appDomainStore,
            Dictionary<(ulong, string), DumpType> typeStore,
            Dictionary<ulong, DumpModule> moduleStore)
        {
            Log.Trace("Extracting Module, AppDomain, and Type information");
            foreach (var clrModule in rt.Modules)
            {
                var dumpModule = new DumpModule();
                dumpModule.Name = clrModule.Name;
                dumpModule.ImageBase = clrModule.ImageBase == 0 ? null : (ulong?) clrModule.ImageBase;

                foreach (var clrAppDomain in clrModule.AppDomains)
                {
                    if (!appDomainStore.ContainsKey(clrAppDomain.Address))
                    {
                        var newDumpAppDomain = new DumpAppDomain();
                        newDumpAppDomain.Address = clrAppDomain.Address;
                        newDumpAppDomain.Name = clrAppDomain.Name;
                        appDomainStore.Add(newDumpAppDomain.Address, newDumpAppDomain);
                    }
                    var dumpAppDomain = appDomainStore[clrAppDomain.Address];
                    dumpModule.AppDomainsInternal.Add(appDomainStore[clrAppDomain.Address]);
                    dumpAppDomain.LoadedModulesInternal.Add(dumpModule);
                }

                foreach (var clrType in clrModule.EnumerateTypes())
                {
                    if (typeStore.ContainsKey((clrType.MethodTable, clrType.Name))) continue;
                    var newDumpType = new DumpType();
                    newDumpType.MethodTable = clrType.MethodTable;
                    newDumpType.Name = clrType.Name;
                    newDumpType.Module = dumpModule;
                    dumpModule.TypesInternal.Add(newDumpType);
                    typeStore.Add((clrType.MethodTable, clrType.Name), newDumpType);
                }

                moduleStore.Add(dumpModule.ImageBase.Value,
                    dumpModule); // todo: possible null, should use image base + name
            }
        }

        private void SetupThreads(ClrRuntime rt, Dictionary<ulong, DumpObject> objectStore,
            Dictionary<uint, DumpThread> threadStore, Dictionary<ulong, DumpObjectRoot> objectRootsStore)
        {
            Log.Trace("Extracting information about the threads");
            foreach (var thread in rt.Threads)
            {
                var extracted = new DumpThread
                {
                    OsId = thread.OSThreadId,
                    StackFrames = thread.StackTrace.Select(f => new DumpStackFrame
                    {
                        DisplayString = f.DisplayString
                    }).ToList()
                };

                extracted.ObjectRoots = thread.EnumerateStackObjects()
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
                        root.Thread = extracted;

                        return root;
                    }).ToList();

                if (!threadStore.ContainsKey(extracted.OsId))
                    threadStore.Add(extracted.OsId, extracted);
                else
                    Log.Error(
                        $"Extracted a thread but there is already an entry with os id: {extracted.OsId}, you should investigate these manually");
            }

            var debuggerProxy = new DebuggerProxy(DataTarget.DebuggerInterface);
            var runawayData = debuggerProxy.Execute("!runaway");
            Log.Debug($"Calling !runaway returned: {runawayData}");

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
                    Log.Debug($"Found thread {id} in runaway data but not in thread repo");
                    continue;
                }
                dumpThread.DebuggerIndex = index;

                if (isUserMode)
                    dumpThread.UserModeTime = timeSpan;
                else if (isKernelMode)
                    dumpThread.KernelModeTime = timeSpan;
            }
        }

        private void SetupObjects(List<IDumpObjectExtractor> heapObjectExtractors, ClrRuntime rt,
            Dictionary<ulong, DumpObject> objectStore, Dictionary<ulong, List<ulong>> objectHierarchy,
            Dictionary<(ulong, string), DumpType> typeStore, Dictionary<ulong, DumpAppDomain> appDomainStore,
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
                    // todo: logging
                    if (!heapObjectExtractor.CanExtract(clrObject, rt))
                        continue;
                    var extracted = heapObjectExtractor.Extract(clrObject, rt);
                    var dumpType = typeStore[(clrObject.Type.MethodTable, clrObject.Type.Name)];
                    // setting object/type mapping
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

                foreach (var clrObjectRef in clrObject.EnumerateObjectReferences())
                    objectHierarchy[clrObject.Address].Add(clrObjectRef.Address);
            }

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

    public class DumpModuleRepository
    {
        protected internal Dictionary<ulong, DumpModule> DumpModules;

        protected internal DumpModuleRepository(Dictionary<ulong, DumpModule> dumpModules)
        {
            DumpModules = dumpModules ?? throw new ArgumentNullException(nameof(dumpModules));
        }
    }

    public class DumpAppDomainRepository
    {
        protected internal Dictionary<ulong, DumpAppDomain> AppDomains;

        protected internal DumpAppDomainRepository(Dictionary<ulong, DumpAppDomain> appDomainStore)
        {
            AppDomains = appDomainStore ?? throw new ArgumentNullException(nameof(appDomainStore));
        }
    }
}