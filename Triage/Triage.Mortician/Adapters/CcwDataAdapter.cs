using System;
using System.Collections.Generic;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class CcwDataAdapter : ICcwData
    {
        internal Microsoft.Diagnostics.Runtime.CcwData Data;

        /// <inheritdoc />
        public CcwDataAdapter(Microsoft.Diagnostics.Runtime.CcwData data)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
        }

        /// <inheritdoc />
        public ulong Handle { get; }

        /// <inheritdoc />
        public IList<IComInterfaceData> Interfaces { get; }

        /// <inheritdoc />
        public ulong IUnknown { get; }

        /// <inheritdoc />
        public ulong Object { get; }

        /// <inheritdoc />
        public int RefCount { get; }
    }
}