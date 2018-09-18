using System;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class ModuleInfoAdapter : IModuleInfo
    {
        /// <inheritdoc />
        public ModuleInfoAdapter(Microsoft.Diagnostics.Runtime.ModuleInfo moduleInfo)
        {
            _moduleInfo = moduleInfo ?? throw new ArgumentNullException(nameof(moduleInfo));
        }

        internal Microsoft.Diagnostics.Runtime.ModuleInfo _moduleInfo;

        /// <inheritdoc />
        public IPeFile GetPEFile()
        {
            throw new NotImplementedException();
        }

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
        public uint TimeStamp { get; set; }

        /// <inheritdoc />
        public VersionInfo Version { get; set; }
    }
}