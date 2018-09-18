using System;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class SymbolResolverAdapter : ISymbolResolver
    {
        /// <inheritdoc />
        public SymbolResolverAdapter(Microsoft.Diagnostics.Runtime.ISymbolResolver resolver)
        {
            _resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
        }

        internal Microsoft.Diagnostics.Runtime.ISymbolResolver _resolver;

        /// <inheritdoc />
        public string GetSymbolNameByRVA(uint rva)
        {
            throw new NotImplementedException();
        }
    }
}