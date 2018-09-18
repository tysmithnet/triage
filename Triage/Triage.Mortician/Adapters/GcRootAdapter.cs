using System;
using System.Collections.Generic;
using System.Threading;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class GcRootAdapter : IGcRoot
    {
        /// <inheritdoc />
        public GcRootAdapter(Microsoft.Diagnostics.Runtime.GCRoot root)
        {
            _root = root ?? throw new ArgumentNullException(nameof(root));
        }

        /// <inheritdoc />
        public event GcRootProgressEvent ProgressUpdate;

        internal Microsoft.Diagnostics.Runtime.GCRoot _root;

        /// <inheritdoc />
        public void BuildCache(CancellationToken cancelToken)
        {
            _root.BuildCache(cancelToken);
        }

        /// <inheritdoc />
        public void ClearCache()
        {
            _root.ClearCache();
        }

        /// <inheritdoc />
        public IEnumerable<LinkedList<IClrObject>> EnumerateAllPaths(ulong source, ulong target, bool unique,
            CancellationToken cancelToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IEnumerable<IRootPath> EnumerateGCRoots(ulong target, CancellationToken cancelToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IEnumerable<IRootPath> EnumerateGCRoots(ulong target, bool unique, CancellationToken cancelToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public LinkedList<IClrObject> FindSinglePath(ulong source, ulong target, CancellationToken cancelToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool AllowParallelSearch { get; set; }

        /// <inheritdoc />
        public IClrHeap Heap { get; }

        /// <inheritdoc />
        public bool IsFullyCached { get; }

        /// <inheritdoc />
        public int MaximumTasksAllowed { get; set; }
    }
}