using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class RootPathAdapter : IRootPath
    {
        /// <inheritdoc />
        public RootPathAdapter(Microsoft.Diagnostics.Runtime.RootPath rootPath)
        {
            _rootPath = rootPath;
        }

        internal Microsoft.Diagnostics.Runtime.RootPath _rootPath;

        /// <inheritdoc />
        public IClrObject[] Path { get; set; }

        /// <inheritdoc />
        public IClrRoot Root { get; set; }
    }
}