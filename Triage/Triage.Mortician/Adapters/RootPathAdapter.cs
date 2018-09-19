using System.ComponentModel.Composition;
using System.Linq;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class RootPathAdapter : IRootPath
    {
        [Import]
        internal IConverter Converter { get; set; }
        /// <inheritdoc />
        public RootPathAdapter(Microsoft.Diagnostics.Runtime.RootPath rootPath)
        {
            RootPath = rootPath;
            Path = rootPath.Path.Select(Converter.Convert).ToArray();
            Root = Converter.Convert(rootPath.Root);
        }

        internal Microsoft.Diagnostics.Runtime.RootPath RootPath;

        /// <inheritdoc />
        public IClrObject[] Path { get; }

        /// <inheritdoc />
        public IClrRoot Root { get; }
    }
}