using System;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class DacInfoAdapter : IDacInfo
    {
        /// <inheritdoc />
        public DacInfoAdapter(Microsoft.Diagnostics.Runtime.DacInfo dacInfo)
        {
            DacInfo = dacInfo ?? throw new ArgumentNullException(nameof(dacInfo));
            Pdb = Converter.Convert(dacInfo.Pdb);
            TargetArchitecture = Converter.Convert(dacInfo.TargetArchitecture);
            Version = Converter.Convert(dacInfo.Version);
        }

        internal Microsoft.Diagnostics.Runtime.DacInfo DacInfo;

        /// <inheritdoc />
        public string FileName => DacInfo.FileName;

        /// <inheritdoc />
        public uint FileSize => DacInfo.FileSize;

        /// <inheritdoc />
        public ulong ImageBase => DacInfo.ImageBase;

        /// <inheritdoc />
        public bool IsManaged => DacInfo.IsManaged;

        /// <inheritdoc />
        public bool IsRuntime => DacInfo.IsRuntime;

        /// <inheritdoc />
        public IPdbInfo Pdb { get; set; }

        /// <inheritdoc />
        public string PlatformAgnosticFileName { get; set; }

        /// <inheritdoc />
        public Architecture TargetArchitecture { get; set; }

        /// <inheritdoc />
        public uint TimeStamp => DacInfo.TimeStamp;

        /// <inheritdoc />
        public VersionInfo Version { get; set; }
    }
}