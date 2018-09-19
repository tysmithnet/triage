using System;
using System.ComponentModel.Composition;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class ClrInfoAdapter : IClrInfo
    {
        [Import]
        internal IConverter Converter { get; set; }
        internal Microsoft.Diagnostics.Runtime.ClrInfo Info;

        /// <inheritdoc />
        public ClrInfoAdapter(Microsoft.Diagnostics.Runtime.ClrInfo info)
        {
            Info = info ?? throw new ArgumentNullException(nameof(info));
            DacInfo = Converter.Convert(info.DacInfo);
            Flavor = Converter.Convert(info.Flavor);
            ModuleInfo = Converter.Convert(info.ModuleInfo);
            Version = Converter.Convert(info.Version);
        }

        /// <inheritdoc />
        public int CompareTo(object obj) => Info.CompareTo(obj);

        /// <inheritdoc />
        public IClrRuntime CreateRuntime() => Converter.Convert(Info.CreateRuntime());


        /// <inheritdoc />
        public IClrRuntime CreateRuntime(object clrDataProcess) => Converter.Convert(Info.CreateRuntime(clrDataProcess));


        /// <inheritdoc />
        public IClrRuntime CreateRuntime(string dacFilename, bool ignoreMismatch = false) => Converter.Convert(Info.CreateRuntime(dacFilename, ignoreMismatch));

        /// <inheritdoc />
        public IDacInfo DacInfo { get; }

        /// <inheritdoc />
        public ClrFlavor Flavor { get; }

        /// <inheritdoc />
        public string LocalMatchingDac => Info.LocalMatchingDac;

        /// <inheritdoc />
        public IModuleInfo ModuleInfo { get; }

        /// <inheritdoc />
        public VersionInfo Version { get; }
    }
}