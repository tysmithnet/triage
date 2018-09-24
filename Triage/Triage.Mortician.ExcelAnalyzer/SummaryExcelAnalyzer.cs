// ***********************************************************************
// Assembly         : Triage.Mortician.ExcelAnalyzer
// Author           : @tysmithnet
// Created          : 01-14-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="SummaryExcelAnalyzer.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.ComponentModel.Composition;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using SpreadsheetLight;
using Triage.Mortician.Core;
using Triage.Mortician.Repository;

namespace Triage.Mortician.ExcelAnalyzer
{
    /// <summary>
    ///     An excel analyzer that will write the summary of the excel report
    /// </summary>
    /// <seealso cref="Triage.Mortician.ExcelAnalyzer.IExcelAnalyzer" />
    /// <inheritdoc />
    /// <seealso cref="T:Triage.Mortician.ExcelAnalyzer.IExcelAnalyzer" />
    [Export(typeof(IExcelAnalyzer))]
    public class SummaryExcelAnalyzer : IExcelAnalyzer
    {
        /// <summary>
        ///     Contributes the specified shared document.
        /// </summary>
        /// <param name="sharedDocument">The shared document.</param>
        /// <inheritdoc />
        public void Contribute(SLDocument sharedDocument)
        {
            sharedDocument.SelectWorksheet("SummaryData");
            sharedDocument.SetCellValue(1, 2, Assembly.GetEntryAssembly().GetName().Version.ToString());
            sharedDocument.SetCellValue(2, 2, DumpInformationRepository.StartTimeUtc.ToString());
            sharedDocument.SetCellValue(3, 2, DumpInformationRepository.DumpFile.Name);
            sharedDocument.SetCellValue(4, 2, DumpInformationRepository.DumpFile.Length);
            sharedDocument.SetCellValue(5, 2, DumpInformationRepository.IsMiniDump ? "Mini" : "Full");
            sharedDocument.SetCellValue(6, 2, DumpInformationRepository.CpuUtilization);
            sharedDocument.SetCellValue(7, 2, DumpInformationRepository.TotalHeapSize);
            sharedDocument.SetCellValue(8, 2, DumpInformationRepository.NumRunningThreads);
            sharedDocument.SetCellValue(9, 2, DumpInformationRepository.TotalThreads);
            sharedDocument.HideWorksheet("SummaryData");
        }

        /// <summary>
        ///     Performs any required setup like number crunching etc.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task, that when complete will signal the setup completion</returns>
        /// <inheritdoc />
        public Task Setup(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        ///     Gets or sets the dump information repository.
        /// </summary>
        /// <value>The dump information repository.</value>
        [Import]
        protected internal IDumpInformationRepository DumpInformationRepository { get; set; }
    }
}