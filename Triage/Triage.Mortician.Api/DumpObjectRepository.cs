using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Common.Logging;
using Microsoft.Diagnostics.Runtime;
using Triage.Mortician.Abstraction;

namespace Triage.Mortician.Api
{
    [Export(typeof(IDumpObjectRepository))]
    internal class DumpObjectRepository : IDumpObjectRepository
    {
        protected internal Dictionary<ulong, IDumpObject> HeapObjects = new Dictionary<ulong, IDumpObject>();

        protected internal ILog Log = LogManager.GetLogger(typeof(DumpObjectRepository));

        public DumpObjectRepository(ClrRuntime runtime, List<IDumpObjectExtractor> heapObjectExtractors)
        {
            var heap = runtime.Heap;

            if(!heap.CanWalkHeap)
                Log.Debug("Heap is not walkable - unexpected results might arise");

            foreach (var obj in heap.EnumerateObjects().Where(x => !x.Type.IsFree && !x.IsNull))
            {                                                                                 
                foreach (var extractor in heapObjectExtractors)
                {
                    if (!extractor.CanExtract(obj, runtime)) continue;
                    try
                    {   
                        HeapObjects.Add(obj.Address, extractor.Extract(obj, runtime));
                    }
                    catch (Exception e)
                    {
                        Log.Error(e);
                    }
                }  
                if(HeapObjects.ContainsKey(obj.Address)) continue;
                HeapObjects.Add(obj.Address, new DumpObject(obj.Address, obj.Type.Name, obj.Size));
            }

            foreach (var obj in heap.EnumerateObjects().Where(x => !x.Type.IsFree && !x.IsNull))
            {
                var parent = HeapObjects[obj.Address];

                foreach (var reference in obj.EnumerateObjectReferences())
                {
                    IDumpObject child;
                    if(HeapObjects.TryGetValue(reference.Address, out child))
                        parent.AddReference(child);
                    else
                        Log.Warn($"{parent.Address:x} has a reference to {reference.Address:x}, but it was not found in the heap cache");
                }    
            }
        }

        public IDumpObject Get(ulong address)
        {
            IDumpObject obj;
            if (HeapObjects.TryGetValue(address, out obj))
                return obj;
            throw new IndexOutOfRangeException($"There is no object matching address: {address:x}");
        }

        public IEnumerable<IDumpObject> Get()
        {
            return HeapObjects.Values;
        }
    }
}