using System;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class SymbolProviderAdapter : ISymbolProvider
    {
        /// <inheritdoc />
        public SymbolProviderAdapter(Microsoft.Diagnostics.Runtime.ISymbolProvider provider)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        internal Microsoft.Diagnostics.Runtime.ISymbolProvider _provider;

        /// <inheritdoc />
        public ISymbolResolver GetSymbolResolver(string pdbName, Guid guid, int age)
        {
            throw new NotImplementedException();
        }
    }
}