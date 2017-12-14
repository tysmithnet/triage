using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common.Logging;
using Microsoft.Diagnostics.Runtime;
using Triage.Mortician.Abstraction;

namespace Triage.Mortician
{
    internal class RepositoryFactory
    {
        public ILog Log = LogManager.GetLogger(typeof(RepositoryFactory));
        public CompositionContainer CompositionContainer { get; set; }
        public DataTarget DataTarget { get; set; }

        public RepositoryFactory(CompositionContainer compositionContainer, DataTarget dataTarget)
        {
            CompositionContainer = compositionContainer ?? throw new ArgumentNullException(nameof(compositionContainer));
            DataTarget = dataTarget ?? throw new ArgumentNullException(nameof(dataTarget));
        }

        public void RegisterRepositories()
        {
            var heapObjectExtractors = CompositionContainer.GetExportedValues<IDumpObjectExtractor>().ToList();

            var rt = DataTarget.ClrVersions.Single().CreateRuntime();

            if (DataTarget.IsMinidump)
                Log.Warn("The provided dump is a mini dump and results will not contain any heap information (objects, etc)");

            if (!rt.Heap.CanWalkHeap)
                Log.Warn("CLRMd reports that the heap is unwalkable, results might vary");

            var objectStore = new Dictionary<ulong, DumpObject>();
            var objectHierarchy = new Dictionary<ulong, List<ulong>>();
            var threadStore = new Dictionary<uint, DumpThread>();
            
            SetupObjects(heapObjectExtractors, rt, objectStore, objectHierarchy);
            SetupThreads(rt, objectStore, threadStore);
            
        }

        private void SetupThreads(ClrRuntime rt, Dictionary<ulong, DumpObject> objectStore, Dictionary<uint, DumpThread> threadStore)
        {
            foreach (var thread in rt.Threads)
            {
                var extracted = new DumpThread
                {
                    OsId = thread.OSThreadId,
                    StackFrames = thread.StackTrace.Select(f => new DumpStackFrame
                    {
                        DisplayString = f.DisplayString
                    }).Cast<IDumpStackFrame>().ToList(),
                    StackObjectsInternal = thread.EnumerateStackObjects().Select(o => objectStore[o.Address])
                        .Cast<IDumpObject>().ToList()
                };
                threadStore.Add(extracted.OsId, extracted);
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

        private static void SetupObjects(List<IDumpObjectExtractor> heapObjectExtractors, ClrRuntime rt, Dictionary<ulong, DumpObject> objectStore, Dictionary<ulong, List<ulong>> objectHierarchy)
        {
            foreach (var obj in rt.Heap.EnumerateObjects().Where(o => !o.IsNull && !o.Type.IsFree))
            {
                bool isExtracted = false;
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
                {
                    objectHierarchy[obj.Address].Add(objRef.Address);
                }
            }

            foreach (var relationship in objectHierarchy)
            {
                var parent = objectStore[relationship.Key];

                foreach (var childAddress in relationship.Value)
                {
                    var child = objectStore[childAddress];
                    parent.AddReference(child);
                    child.AddReferencer(parent);
                }
            }
        }
    }
}
