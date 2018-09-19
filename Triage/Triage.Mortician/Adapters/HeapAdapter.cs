using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class HeapAdapter : IClrHeap
    {
        internal Microsoft.Diagnostics.Runtime.ClrHeap Heap;

        /// <inheritdoc />
        public HeapAdapter(Microsoft.Diagnostics.Runtime.ClrHeap heap)
        {
            Heap = heap ?? throw new ArgumentNullException(nameof(heap));
            Runtime = Converter.Convert(heap.Runtime);
            Free = Converter.Convert(heap.Free);
            Segments = heap.Segments.Select(Converter.Convert).ToList();
            StackwalkPolicy = Converter.Convert(heap.StackwalkPolicy);
        }

        /// <inheritdoc />
        public void CacheHeap(CancellationToken cancelToken) => Heap.CacheHeap(cancelToken);

        /// <inheritdoc />
        public void CacheRoots(CancellationToken cancelToken) => Heap.CacheRoots(cancelToken);

        /// <inheritdoc />
        public void ClearHeapCache() => Heap.ClearHeapCache();

        /// <inheritdoc />
        public void ClearRootCache() => Heap.ClearRootCache();

        /// <inheritdoc />
        public IEnumerable<IBlockingObject> EnumerateBlockingObjects() => Heap.EnumerateBlockingObjects().Select(Converter.Convert);

        /// <inheritdoc />
        public IEnumerable<ulong> EnumerateFinalizableObjectAddresses() => Heap.EnumerateFinalizableObjectAddresses();

        /// <inheritdoc />
        public IEnumerable<ulong> EnumerateObjectAddresses() => Heap.EnumerateObjectAddresses();

        /// <inheritdoc />
        public IEnumerable<IClrObject> EnumerateObjects() => Heap.EnumerateObjects().Select(Converter.Convert);

        /// <inheritdoc />
        public IEnumerable<IClrRoot> EnumerateRoots() => Heap.EnumerateRoots().Select(Converter.Convert);

        /// <inheritdoc />
        public IEnumerable<IClrRoot> EnumerateRoots(bool enumerateStatics) => Heap.EnumerateRoots(enumerateStatics).Select(Converter.Convert);

        /// <inheritdoc />
        public IEnumerable<IClrType> EnumerateTypes() => Heap.EnumerateTypes().Select(Converter.Convert);

        /// <inheritdoc />
        public ulong GetEEClassByMethodTable(ulong methodTable) => Heap.GetEEClassByMethodTable(methodTable);

        /// <inheritdoc />
        public IClrException GetExceptionObject(ulong objRef) => Converter.Convert(Heap.GetExceptionObject(objRef));

        /// <inheritdoc />
        public int GetGeneration(ulong obj) => Heap.GetGeneration(obj);

        /// <inheritdoc />
        public ulong GetMethodTable(ulong obj) => Heap.GetMethodTable(obj);

        /// <inheritdoc />
        public ulong GetMethodTableByEEClass(ulong eeclass) => Heap.GetMethodTableByEEClass(eeclass);

        /// <inheritdoc />
        public IClrType GetObjectType(ulong objRef) => Converter.Convert(Heap.GetObjectType(objRef));

        /// <inheritdoc />
        public IClrSegment GetSegmentByAddress(ulong objRef) => Converter.Convert(Heap.GetSegmentByAddress(objRef));

        /// <inheritdoc />
        public ulong GetSizeByGen(int gen) => Heap.GetSizeByGen(gen);

        /// <inheritdoc />
        public IClrType GetTypeByMethodTable(ulong methodTable, ulong componentMethodTable) =>
            Converter.Convert(Heap.GetTypeByMethodTable(methodTable, componentMethodTable));

        /// <inheritdoc />
        public IClrType GetTypeByMethodTable(ulong methodTable) => Converter.Convert(Heap.GetTypeByMethodTable(methodTable));

        /// <inheritdoc />
        public IClrType GetTypeByName(string name) => Converter.Convert(Heap.GetTypeByName(name));

        /// <inheritdoc />
        public bool IsInHeap(ulong address) => Heap.IsInHeap(address);

        /// <inheritdoc />
        public ulong NextObject(ulong obj) => Heap.NextObject(obj);

        /// <inheritdoc />
        public int ReadMemory(ulong address, byte[] buffer, int offset, int count) => Heap.ReadMemory(address, buffer, offset, count);

        /// <inheritdoc />
        public bool ReadPointer(ulong addr, out ulong value) => Heap.ReadPointer(addr, out value);

        /// <inheritdoc />
        public bool TryGetMethodTable(ulong obj, out ulong methodTable, out ulong componentMethodTable) => Heap.TryGetMethodTable(obj, out methodTable, out componentMethodTable);

        /// <inheritdoc />
        public bool AreRootsCached => Heap.AreRootsCached;

        /// <inheritdoc />
        public bool CanWalkHeap => Heap.CanWalkHeap;

        /// <inheritdoc />
        public IClrType Free { get; }

        /// <inheritdoc />
        public bool HasComponentMethodTables => Heap.HasComponentMethodTables;

        /// <inheritdoc />
        public bool IsHeapCached => Heap.IsHeapCached;

        /// <inheritdoc />
        public int PointerSize => Heap.PointerSize;

        /// <inheritdoc />
        public IClrRuntime Runtime { get; }

        /// <inheritdoc />
        public IList<IClrSegment> Segments { get; }

        /// <inheritdoc />
        public ClrRootStackwalkPolicy StackwalkPolicy { get; set; }

        /// <inheritdoc />
        public ulong TotalHeapSize => Heap.TotalHeapSize;
    }
}