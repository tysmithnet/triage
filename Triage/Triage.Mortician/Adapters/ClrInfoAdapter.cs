using System;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class ClrInfoAdapter : IClrInfo
    {
        internal Microsoft.Diagnostics.Runtime.ClrInfo Info;

        /// <inheritdoc />
        public ClrInfoAdapter(Microsoft.Diagnostics.Runtime.ClrInfo info)
        {
            Info = info ?? throw new ArgumentNullException(nameof(info));
        }

        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IClrRuntime CreateRuntime()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IClrRuntime CreateRuntime(object clrDataProcess)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IClrRuntime CreateRuntime(string dacFilename, bool ignoreMismatch = false)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IDacInfo DacInfo { get; }

        /// <inheritdoc />
        public ClrFlavor Flavor { get; }

        /// <inheritdoc />
        public string LocalMatchingDac { get; }

        /// <inheritdoc />
        public IModuleInfo ModuleInfo { get; }

        /// <inheritdoc />
        public VersionInfo Version { get; }
    }
}