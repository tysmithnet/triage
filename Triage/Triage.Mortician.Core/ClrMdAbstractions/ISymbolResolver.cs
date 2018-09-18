// ***********************************************************************
// Assembly         : Triage.Mortician.Core
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="ISymbolResolver.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Triage.Mortician.Core.ClrMdAbstractions
{
    /// <summary>
    /// ISymbolResolver represents a single symbol module (PDB) loaded into the process.
    /// </summary>
    public interface ISymbolResolver
    {
        /// <summary>
        /// Retrieves the given symbol's name based on its RVA.
        /// </summary>
        /// <param name="rva">A relative virtual address in the module.</param>
        /// <returns>The symbol corresponding to RVA.</returns>
        string GetSymbolNameByRVA(uint rva);
    }
}