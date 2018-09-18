using System;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class NativeWorkItemAdapter : INativeWorkItem
    {
        /// <inheritdoc />
        public NativeWorkItemAdapter(Microsoft.Diagnostics.Runtime.NativeWorkItem nativeWorkItem)
        {
            _nativeWorkItem = nativeWorkItem ?? throw new ArgumentNullException(nameof(nativeWorkItem));
        }

        internal Microsoft.Diagnostics.Runtime.NativeWorkItem _nativeWorkItem;

        /// <inheritdoc />
        public ulong Callback { get; }

        /// <inheritdoc />
        public ulong Data { get; }

        /// <inheritdoc />
        public WorkItemKind Kind { get; }
    }
}