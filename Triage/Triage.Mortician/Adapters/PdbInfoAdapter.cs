using System;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class PdbInfoAdapter : IPdbInfo
    {
        /// <inheritdoc />
        public PdbInfoAdapter(Microsoft.Diagnostics.Runtime.PdbInfo pdbInfo)
        {
            PdbInfo = pdbInfo ?? throw new ArgumentNullException(nameof(pdbInfo));
        }

        internal Microsoft.Diagnostics.Runtime.PdbInfo PdbInfo;

        /// <inheritdoc />
        public string FileName => PdbInfo.FileName;

        /// <inheritdoc />
        public Guid Guid => PdbInfo.Guid;

        /// <inheritdoc />
        public int Revision => PdbInfo.Revision;
    }
}