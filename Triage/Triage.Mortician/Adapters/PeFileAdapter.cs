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
            PeFile = peFile ?? throw new ArgumentNullException(nameof(peFile));
            PdbInfo = Converter.Convert(peFile.PdbInfo);
        }

        internal PEFile PeFile;

        /// <inheritdoc />
        public void Dispose() => PeFile.Dispose();

        /// <inheritdoc />
        public IFileVersionInfo GetFileVersionInfo() => Converter.Convert(PeFile.GetFileVersionInfo());

        /// <inheritdoc />
        public bool GetPdbSignature(out string pdbName, out Guid pdbGuid, out int pdbAge, bool first = false) =>
            PeFile.GetPdbSignature(out pdbName, out pdbGuid, out pdbAge, first);

        /// <inheritdoc />
        public string GetSxSManfest() => PeFile.GetSxSManfest();

        /// <inheritdoc />
        public bool Disposed => PeFile.Disposed;

        /// <inheritdoc />
        public IPdbInfo PdbInfo { get; }
    }
}