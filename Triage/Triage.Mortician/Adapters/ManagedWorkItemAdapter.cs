using System;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class ManagedWorkItemAdapter : IManagedWorkItem
    {
        /// <inheritdoc />
        public ManagedWorkItemAdapter(Microsoft.Diagnostics.Runtime.ManagedWorkItem workItem)
        {
            _workItem = workItem ?? throw new ArgumentNullException(nameof(workItem));
        }

        internal Microsoft.Diagnostics.Runtime.ManagedWorkItem _workItem;

        /// <inheritdoc />
        public ulong Object { get; }

        /// <inheritdoc />
        public IClrType Type { get; }
    }
}