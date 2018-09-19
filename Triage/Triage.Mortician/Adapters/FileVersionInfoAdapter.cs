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
            FileVersionInfo = fileVersionInfo ?? throw new ArgumentNullException(nameof(fileVersionInfo));
        }

        internal FileVersionInfo FileVersionInfo;

        /// <inheritdoc />
        public string Comments => FileVersionInfo.Comments;

        /// <inheritdoc />
        public string FileVersion => FileVersionInfo.FileVersion;
    }
}