// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-19-2018
// ***********************************************************************
// <copyright file="SymbolProviderAdapter.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.ComponentModel.Composition;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    /// <summary>
    ///     Class SymbolProviderAdapter.
    /// </summary>
    /// <seealso cref="Triage.Mortician.Core.ClrMdAbstractions.ISymbolProvider" />
    internal class SymbolProviderAdapter : BaseAdapter, ISymbolProvider
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SymbolProviderAdapter" /> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <exception cref="ArgumentNullException">provider</exception>
        /// <inheritdoc />
        public SymbolProviderAdapter(IConverter converter, Microsoft.Diagnostics.Runtime.ISymbolProvider provider) : base(converter)
        {
            Provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }
        public override void Setup()
        {

        }
        /// <summary>
        ///     The provider
        /// </summary>
        internal Microsoft.Diagnostics.Runtime.ISymbolProvider Provider;

        /// <summary>
        ///     Loads a PDB by its given guid/age and provides an ISymbolResolver for that PDB.
        /// </summary>
        /// <param name="pdbName">The name of the pdb.  This may be a full path and not just a simple name.</param>
        /// <param name="guid">The guid of the pdb to locate.</param>
        /// <param name="age">The age of the pdb to locate.</param>
        /// <returns>A symbol resolver for the given pdb.  Null if none was found.</returns>
        /// <inheritdoc />
        public ISymbolResolver GetSymbolResolver(string pdbName, Guid guid, int age) =>
            Converter.Convert(Provider.GetSymbolResolver(pdbName, guid, age));
        
    }
}