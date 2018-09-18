using System;
using System.Collections.Generic;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class RcwDataAdapter : IRcwData
    {
        /// <inheritdoc />
        public RcwDataAdapter(Microsoft.Diagnostics.Runtime.RcwData rcwData)
        {
            _rcwData = rcwData ?? throw new ArgumentNullException(nameof(rcwData));
        }

        internal Microsoft.Diagnostics.Runtime.RcwData _rcwData;

        /// <inheritdoc />
        public uint CreatorThread { get; }

        /// <inheritdoc />
        public bool Disconnected { get; }

        /// <inheritdoc />
        public IList<IComInterfaceData> Interfaces { get; }

        /// <inheritdoc />
        public ulong IUnknown { get; }

        /// <inheritdoc />
        public ulong Object { get; }

        /// <inheritdoc />
        public int RefCount { get; }

        /// <inheritdoc />
        public ulong VTablePointer { get; }

        /// <inheritdoc />
        public ulong WinRTObject { get; }
    }
}