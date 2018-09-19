using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class GcRootAdapter : IGcRoot
    {
        /// <inheritdoc />
        public GcRootAdapter(Microsoft.Diagnostics.Runtime.GCRoot root)
        {
            Root = root ?? throw new ArgumentNullException(nameof(root));
            Heap = Converter.Convert(root.Heap);
            root.ProgressUpdate += (source, current, total) =>
            {
                var convertedSource = Converter.Convert(source);
                ProgressUpdate?.Invoke(convertedSource, current, total);
            };
        }

        /// <inheritdoc />
        public event GcRootProgressEvent ProgressUpdate;

        internal Microsoft.Diagnostics.Runtime.GCRoot Root;

        /// <inheritdoc />
        public void BuildCache(CancellationToken cancelToken) => Root.BuildCache(cancelToken);


        /// <inheritdoc />
        public void ClearCache() => Root.ClearCache();


        /// <inheritdoc />
        public IEnumerable<IList<IClrObject>> EnumerateAllPaths(ulong source, ulong target, bool unique, CancellationToken cancelToken) =>
            Root.EnumerateAllPaths(source, target, unique, cancelToken).Select(ll => ll.Select(Converter.Convert).ToList());

        /// <inheritdoc />
        public IEnumerable<IRootPath> EnumerateGCRoots(ulong target, CancellationToken cancelToken) => Root.EnumerateGCRoots(target, cancelToken).Select(Converter.Convert);

        /// <inheritdoc />
        public IEnumerable<IRootPath> EnumerateGCRoots(ulong target, bool unique, CancellationToken cancelToken) => Root.EnumerateGCRoots(target, unique, cancelToken).Select(Converter.Convert);

        /// <inheritdoc />
        public IList<IClrObject> FindSinglePath(ulong source, ulong target, CancellationToken cancelToken) => Root.FindSinglePath(source, target, cancelToken).Select(Converter.Convert).ToList();

        /// <inheritdoc />
        public bool AllowParallelSearch => Root.AllowParallelSearch;

        /// <inheritdoc />
        public IClrHeap Heap { get; }

        /// <inheritdoc />
        public bool IsFullyCached => Root.IsFullyCached;

        /// <inheritdoc />
        public int MaximumTasksAllowed => Root.MaximumTasksAllowed;
    }
}