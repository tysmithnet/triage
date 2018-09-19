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
        }

        /// <inheritdoc />
        public ulong Address { get; }

        /// <inheritdoc />
        public string ApplicationBase { get; }

        /// <inheritdoc />
        public string ConfigurationFile { get; }

        /// <inheritdoc />
        public int Id { get; }

        /// <inheritdoc />
        public IList<IClrModule> Modules { get; }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public IClrRuntime Runtime { get; }
    }
}
