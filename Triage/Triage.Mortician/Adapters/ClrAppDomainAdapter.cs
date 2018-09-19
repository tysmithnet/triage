using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triage.Mortician.Core.ClrMdAbstractions;
using ClrMd = Microsoft.Diagnostics.Runtime;

namespace Triage.Mortician.Adapters
{
    internal class ClrAppDomainAdapter : IClrAppDomain
    {
        internal ClrMd.ClrAppDomain AppDomain;

        /// <inheritdoc />
        public ClrAppDomainAdapter(ClrMd.ClrAppDomain appDomain)
        {
            AppDomain = appDomain ?? throw new ArgumentNullException(nameof(appDomain));
            Modules = appDomain.Modules.Select(Converter.Convert).ToList();
            Runtime = Converter.Convert(appDomain.Runtime);
        }

        /// <inheritdoc />
        public ulong Address => AppDomain.Address;

        /// <inheritdoc />
        public string ApplicationBase => AppDomain.ApplicationBase;

        /// <inheritdoc />
        public string ConfigurationFile => AppDomain.ConfigurationFile;

        /// <inheritdoc />
        public int Id => AppDomain.Id;

        /// <inheritdoc />
        public IList<IClrModule> Modules { get; }

        /// <inheritdoc />
        public string Name => AppDomain.Name;

        /// <inheritdoc />
        public IClrRuntime Runtime { get; }
    }
}
