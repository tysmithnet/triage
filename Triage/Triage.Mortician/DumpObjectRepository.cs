using System;
using System.Collections.Generic;
using System.Linq;
using Common.Logging;
using Microsoft.Diagnostics.Runtime;
using Triage.Mortician.Abstraction;

namespace Triage.Mortician
{
    /// <summary>
    ///     Repository for objects that were extracted from the managed heap
    /// </summary>
    /// <seealso cref="Triage.Mortician.Abstraction.IDumpObjectRepository" />
    internal class DumpObjectRepository : IDumpObjectRepository
    {
        /// <summary>
        ///     The heap objects
        /// </summary>
        protected internal Dictionary<ulong, IDumpObject> HeapObjects = new Dictionary<ulong, IDumpObject>();

        /// <summary>
        ///     The log
        /// </summary>
        protected internal ILog Log = LogManager.GetLogger(typeof(DumpObjectRepository));

        /// <summary>
        ///     Initializes a new instance of the <see cref="DumpObjectRepository" /> class.
        /// </summary>
        /// <param name="runtime">The runtime.</param>
        /// <param name="heapObjectExtractors">The heap object extractors.</param>
        public DumpObjectRepository(ClrRuntime runtime, IReadOnlyCollection<IDumpObjectExtractor> heapObjectExtractors)
        {
            var heap = runtime.Heap;

            if (!heap.CanWalkHeap)
                Log.Debug("Heap is not walkable - unexpected results might arise");

            // loop over each object in the heap and try to extract value from it
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
                if (HeapObjects.ContainsKey(obj.Address)) continue;
                HeapObjects.Add(obj.Address,
                    new DumpObject(obj.Address, obj.Type.Name, obj.Size, runtime.Heap.GetGeneration(obj.Address)));
            }

            if (!HeapObjects.Any())
                Log.Error("No object data was collected from the heap. Is this a mini dump?");

            // loop over the objects again and extract the reference graph
            foreach (var obj in heap.EnumerateObjects().Where(x => !x.Type.IsFree && !x.IsNull))
            {
                var parent = HeapObjects[obj.Address];

                foreach (var reference in obj.EnumerateObjectReferences())
                    if (HeapObjects.TryGetValue(reference.Address, out var child))
                        if (parent is DumpObject parentAsDumpObject)
                            parentAsDumpObject.AddReference(child);
                        else
                            Log.Error(
                                $"Expecting to find a dump object while generating the reference graph but instead found: {parent.GetType().FullName}");
                    else
                        Log.Warn(
                            $"{parent.Address:x} has a reference to {reference.Address:x}, but it was not found in the heap cache");
            }
        }

        /// <summary>
        ///     Gets the object at the specified address
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns>The object at the specified address</returns>
        /// <exception cref="IndexOutOfRangeException">The provided address is not a valid object address</exception>
        public IDumpObject Get(ulong address)
        {
            if (HeapObjects.TryGetValue(address, out var obj))
                return obj;
            throw new IndexOutOfRangeException($"There is no object matching address: {address:x}");
        }

        /// <summary>
        ///     Get all dump objects extracted from the heap
        /// </summary>
        /// <returns>All dump objects extracted from the heap</returns>
        public IEnumerable<IDumpObject> Get()
        {
            return HeapObjects.Values;
        }
    }
}