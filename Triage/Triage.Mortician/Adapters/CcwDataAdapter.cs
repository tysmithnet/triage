using System;
using System.Collections.Generic;
using System.Linq;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class CcwDataAdapter : ICcwData
    {
        internal Microsoft.Diagnostics.Runtime.CcwData CcwData;

        /// <inheritdoc />
        public CcwDataAdapter(Microsoft.Diagnostics.Runtime.CcwData data)
        {
            CcwData = data ?? throw new ArgumentNullException(nameof(data));
            Interfaces = data.Interfaces.Select(Converter.Convert).ToList();
        }

        /// <inheritdoc />
        public ulong Handle => CcwData.Handle;

        /// <inheritdoc />
        public IList<IComInterfaceData> Interfaces { get; }

        /// <inheritdoc />
        public ulong IUnknown => CcwData.IUnknown;

        /// <inheritdoc />
        public ulong Object => CcwData.Object;

        /// <inheritdoc />
        public int RefCount => CcwData.RefCount;
    }
}