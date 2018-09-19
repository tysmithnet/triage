using System;
using System.ComponentModel.Composition;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class ModuleInfoAdapter : IModuleInfo
    {
        [Import]
        internal IConverter Converter { get; set; }
        /// <inheritdoc />
        public ModuleInfoAdapter(Microsoft.Diagnostics.Runtime.ModuleInfo moduleInfo)
        {
            ModuleInfo = moduleInfo ?? throw new ArgumentNullException(nameof(moduleInfo));
            Pdb = Converter.Convert(moduleInfo.Pdb);
            Version = Converter.Convert(moduleInfo.Version);
        }

        internal Microsoft.Diagnostics.Runtime.ModuleInfo ModuleInfo;

        /// <inheritdoc />
        public IPeFile GetPEFile() => Converter.Convert(ModuleInfo.GetPEFile());

        /// <inheritdoc />
        public string FileName => ModuleInfo.FileName;

        /// <inheritdoc />
        public uint FileSize => ModuleInfo.FileSize;

        /// <inheritdoc />
        public ulong ImageBase => ModuleInfo.ImageBase;

        /// <inheritdoc />
        public bool IsManaged => ModuleInfo.IsManaged;

        /// <inheritdoc />
        public bool IsRuntime => ModuleInfo.IsRuntime;

        /// <inheritdoc />
        public IPdbInfo Pdb { get; set; }

        /// <inheritdoc />
        public uint TimeStamp => ModuleInfo.TimeStamp;

        /// <inheritdoc />
        public VersionInfo Version { get; set; }
    }
}