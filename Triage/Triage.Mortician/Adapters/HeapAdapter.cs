using System;
using System.Collections.Generic;
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
        }

        /// <inheritdoc />
        public void CacheHeap(CancellationToken cancelToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void CacheRoots(CancellationToken cancelToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void ClearHeapCache()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void ClearRootCache()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IEnumerable<IBlockingObject> EnumerateBlockingObjects()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IEnumerable<ulong> EnumerateFinalizableObjectAddresses()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IEnumerable<ulong> EnumerateObjectAddresses()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IEnumerable<IClrObject> EnumerateObjects()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IEnumerable<IClrRoot> EnumerateRoots()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IEnumerable<IClrRoot> EnumerateRoots(bool enumerateStatics)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IEnumerable<IClrType> EnumerateTypes()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ulong GetEEClassByMethodTable(ulong methodTable)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IClrException GetExceptionObject(ulong objRef)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public int GetGeneration(ulong obj)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ulong GetMethodTable(ulong obj)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ulong GetMethodTableByEEClass(ulong eeclass)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IClrType GetObjectType(ulong objRef)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IClrSegment GetSegmentByAddress(ulong objRef)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ulong GetSizeByGen(int gen)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IClrType GetTypeByMethodTable(ulong methodTable, ulong componentMethodTable)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IClrType GetTypeByMethodTable(ulong methodTable)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IClrType GetTypeByName(string name)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool IsInHeap(ulong address)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ulong NextObject(ulong obj)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public int ReadMemory(ulong address, byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool ReadPointer(ulong addr, out ulong value)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool TryGetMethodTable(ulong obj, out ulong methodTable, out ulong componentMethodTable)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool AreRootsCached { get; }

        /// <inheritdoc />
        public bool CanWalkHeap { get; }

        /// <inheritdoc />
        public IClrType Free { get; }

        /// <inheritdoc />
        public bool HasComponentMethodTables { get; }

        /// <inheritdoc />
        public bool IsHeapCached { get; }

        /// <inheritdoc />
        public int PointerSize { get; }

        /// <inheritdoc />
        public IClrRuntime Runtime { get; }

        /// <inheritdoc />
        public IList<IClrSegment> Segments { get; }

        /// <inheritdoc />
        public ClrRootStackwalkPolicy StackwalkPolicy { get; set; }

        /// <inheritdoc />
        public ulong TotalHeapSize { get; }
    }
}