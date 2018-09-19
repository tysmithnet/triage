using System;
using System.ComponentModel.Composition;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class NativeWorkItemAdapter : INativeWorkItem
    {
        [Import]
        internal IConverter Converter { get; set; }

        /// <inheritdoc />
        public NativeWorkItemAdapter(Microsoft.Diagnostics.Runtime.NativeWorkItem nativeWorkItem)
        {
            NativeWorkItem = nativeWorkItem ?? throw new ArgumentNullException(nameof(nativeWorkItem));
            Kind = Converter.Convert(nativeWorkItem.Kind);
        }

        internal Microsoft.Diagnostics.Runtime.NativeWorkItem NativeWorkItem;

        /// <inheritdoc />
        public ulong Callback => NativeWorkItem.Callback;

        /// <inheritdoc />
        public ulong Data => NativeWorkItem.Data;

        /// <inheritdoc />
        public WorkItemKind Kind { get; }
    }
}