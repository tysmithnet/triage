using System;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class PdbInfoAdapter : IPdbInfo
    {
        /// <inheritdoc />
        public PdbInfoAdapter(Microsoft.Diagnostics.Runtime.PdbInfo pdbInfo)
        {
            _pdbInfo = pdbInfo ?? throw new ArgumentNullException(nameof(pdbInfo));
        }

        internal Microsoft.Diagnostics.Runtime.PdbInfo _pdbInfo;

        /// <inheritdoc />
        public string FileName { get; set; }

        /// <inheritdoc />
        public Guid Guid { get; set; }

        /// <inheritdoc />
        public int Revision { get; set; }
    }
}