using System;
using Microsoft.Diagnostics.Runtime.Utilities;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class FileVersionInfoAdapter : IFileVersionInfo
    {
        /// <inheritdoc />
        public FileVersionInfoAdapter(FileVersionInfo fileVersionInfo)
        {
            _fileVersionInfo = fileVersionInfo ?? throw new ArgumentNullException(nameof(fileVersionInfo));
        }

        private FileVersionInfo _fileVersionInfo;

        /// <inheritdoc />
        public string Comments { get; }

        /// <inheritdoc />
        public string FileVersion { get; }
    }
}