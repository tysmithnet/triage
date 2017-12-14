using System;
using System.Collections.Generic;
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

            // OBJECTS MUST COME FIRST
            SetupObjects(heapObjectExtractors, rt, objectStore, objectHierarchy);
            SetupThreads(rt, objectStore, threadStore);
        }

        private void SetupThreads(ClrRuntime rt, Dictionary<ulong, DumpObject> objectStore,
            Dictionary<uint, DumpThread> threadStore)
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
                    }).ToList(),
                    StackObjectsInternal = thread.EnumerateStackObjects().Where(o => objectStore.ContainsKey(o.Address))
                        .Select(o => objectStore[o.Address])
                        .ToList()
                };

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
            Dictionary<ulong, DumpObject> objectStore, Dictionary<ulong, List<ulong>> objectHierarchy)
        {
            Log.Trace("Using registered object extractors to process objects on the heap");
            foreach (var obj in rt.Heap.EnumerateObjects().Where(o => !o.IsNull && !o.Type.IsFree))
            {
                var isExtracted = false;
                foreach (var heapObjectExtractor in heapObjectExtractors)
                {
                    // todo: logging
                    if (!heapObjectExtractor.CanExtract(obj, rt))
                        continue;
                    var extracted = heapObjectExtractor.Extract(obj, rt);
                    objectStore.Add(obj.Address, extracted);
                    isExtracted = true;
                    break;
                }
                if (!isExtracted) continue;
                objectHierarchy.Add(obj.Address, new List<ulong>());
                foreach (var objRef in obj.EnumerateObjectReferences())
                    objectHierarchy[obj.Address].Add(objRef.Address);
            }

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
    }
}