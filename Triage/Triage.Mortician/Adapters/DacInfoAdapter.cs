using System;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class DacInfoAdapter : IDacInfo
    {
        /// <inheritdoc />
        public DacInfoAdapter(Microsoft.Diagnostics.Runtime.DacInfo dacInfo)
        {
            _dacInfo = dacInfo ?? throw new ArgumentNullException(nameof(dacInfo));
        }

        internal Microsoft.Diagnostics.Runtime.DacInfo _dacInfo;

        /// <inheritdoc />
        public string FileName { get; set; }

        /// <inheritdoc />
        public uint FileSize { get; set; }

        /// <inheritdoc />
        public ulong ImageBase { get; set; }

        /// <inheritdoc />
        public bool IsManaged { get; }

        /// <inheritdoc />
        public bool IsRuntime { get; }

        /// <inheritdoc />
        public IPdbInfo Pdb { get; set; }

        /// <inheritdoc />
        public string PlatformAgnosticFileName { get; set; }

        /// <inheritdoc />
        public Architecture TargetArchitecture { get; set; }

        /// <inheritdoc />
        public uint TimeStamp { get; set; }

        /// <inheritdoc />
        public VersionInfo Version { get; set; }
    }
}