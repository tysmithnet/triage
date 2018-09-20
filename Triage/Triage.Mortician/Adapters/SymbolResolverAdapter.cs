// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-19-2018
// ***********************************************************************
// <copyright file="SymbolResolverAdapter.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using Triage.Mortician.Core.ClrMdAbstractions;
using ClrMd = Microsoft.Diagnostics.Runtime;

namespace Triage.Mortician.Adapters
{
    /// <summary>
    ///     Class SymbolResolverAdapter.
    /// </summary>
    /// <seealso cref="Triage.Mortician.Core.ClrMdAbstractions.ISymbolResolver" />
    internal class SymbolResolverAdapter : BaseAdapter, ISymbolResolver
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SymbolResolverAdapter" /> class.
        /// </summary>
        /// <param name="resolver">The resolver.</param>
        /// <exception cref="ArgumentNullException">resolver</exception>
        /// <inheritdoc />
        public SymbolResolverAdapter(IConverter converter, ClrMd.ISymbolResolver resolver) : base(converter)
        {
            Resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
        }

        /// <summary>
        ///     The resolver
        /// </summary>
        internal ClrMd.ISymbolResolver Resolver;

        /// <summary>
        ///     Retrieves the given symbol's name based on its RVA.
        /// </summary>
        /// <param name="rva">A relative virtual address in the module.</param>
        /// <returns>The symbol corresponding to RVA.</returns>
        /// <exception cref="NotImplementedException"></exception>
        /// <inheritdoc />
        public string GetSymbolNameByRVA(uint rva) => Resolver.GetSymbolNameByRVA(rva);
    }
}