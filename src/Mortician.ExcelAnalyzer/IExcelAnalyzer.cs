// ***********************************************************************
// Assembly         : Mortician.ExcelAnalyzer
// Author           : @tysmithnet
// Created          : 01-14-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 01-15-2018
// ***********************************************************************
// <copyright file="IExcelAnalyzer.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Threading;
using System.Threading.Tasks;
using SpreadsheetLight;

namespace Mortician.ExcelAnalyzer
{
    /// <summary>
    ///     Represents an object that is cabable of making an excel report
    /// </summary>
    public interface IExcelAnalyzer
    {
        /// <summary>
        ///     Contributes the specified shared document.
        /// </summary>
        /// <param name="sharedDocument">The shared document.</param>
        void Contribute(SLDocument sharedDocument);

        /// <summary>
        ///     Performs any required setup like number crunching etc.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task, that when complete will signal the setup completion</returns>
        Task Setup(CancellationToken cancellationToken);
    }
}