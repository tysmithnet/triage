using System;
using Microsoft.Diagnostics.Runtime.Utilities;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class PeFileAdapter : IPeFile
    {
        /// <inheritdoc />
        public PeFileAdapter(PEFile peFile)
        {
            _peFile = peFile ?? throw new ArgumentNullException(nameof(peFile));
        }

        internal PEFile _peFile;

        /// <inheritdoc />
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IFileVersionInfo GetFileVersionInfo()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool GetPdbSignature(out string pdbName, out Guid pdbGuid, out int pdbAge, bool first = false)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public string GetSxSManfest()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool Disposed { get; }

        /// <inheritdoc />
        public IPdbInfo PdbInfo { get; }
    }
}