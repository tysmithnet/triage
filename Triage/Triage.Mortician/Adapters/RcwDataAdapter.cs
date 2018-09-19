using System;
using System.Collections.Generic;
using System.Linq;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class RcwDataAdapter : IRcwData
    {
        /// <inheritdoc />
        public RcwDataAdapter(Microsoft.Diagnostics.Runtime.RcwData rcwData)
        {
            RcwData = rcwData ?? throw new ArgumentNullException(nameof(rcwData));
            Interfaces = rcwData.Interfaces.Select(Converter.Convert).ToList();
        }

        internal Microsoft.Diagnostics.Runtime.RcwData RcwData;

        /// <inheritdoc />
        public uint CreatorThread => RcwData.CreatorThread;

        /// <inheritdoc />
        public bool Disconnected => RcwData.Disconnected;

        /// <inheritdoc />
        public IList<IComInterfaceData> Interfaces { get; }

        /// <inheritdoc />
        public ulong IUnknown => RcwData.IUnknown;

        /// <inheritdoc />
        public ulong Object => RcwData.Object;

        /// <inheritdoc />
        public int RefCount => RcwData.RefCount;

        /// <inheritdoc />
        public ulong VTablePointer => RcwData.VTablePointer;

        /// <inheritdoc />
        public ulong WinRTObject => RcwData.WinRTObject;
    }
}