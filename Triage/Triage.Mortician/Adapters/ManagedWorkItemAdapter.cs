using System;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class ManagedWorkItemAdapter : IManagedWorkItem
    {
        /// <inheritdoc />
        public ManagedWorkItemAdapter(Microsoft.Diagnostics.Runtime.ManagedWorkItem workItem)
        {
            WorkItem = workItem ?? throw new ArgumentNullException(nameof(workItem));
            Type = Converter.Convert(workItem.Type);
        }

        internal Microsoft.Diagnostics.Runtime.ManagedWorkItem WorkItem;

        /// <inheritdoc />
        public ulong Object => WorkItem.Object;

        /// <inheritdoc />
        public IClrType Type { get; }
    }
}