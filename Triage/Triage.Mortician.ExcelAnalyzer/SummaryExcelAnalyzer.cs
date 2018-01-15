using System.ComponentModel.Composition;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using SpreadsheetLight;
using Triage.Mortician.Repository;

namespace Triage.Mortician.ExcelAnalyzer
{
    /// <inheritdoc />
    /// <summary>
    ///     An excel analyzer that will write the summary of the excel report
    /// </summary>
    /// <seealso cref="T:Triage.Mortician.ExcelAnalyzer.IExcelAnalyzer" />
    [Export(typeof(IExcelAnalyzer))]
    public class SummaryExcelAnalyzer : IExcelAnalyzer
    {
        /// <summary>
        ///     Gets or sets the dump information repository.
        /// </summary>
        /// <value>
        ///     The dump information repository.
        /// </value>
        [Import]
        protected internal DumpInformationRepository DumpInformationRepository { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Performs any required setup like number crunching etc.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///     A Task, that when complete will signal the setup completion
        /// </returns>
        public Task Setup(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Contributes the specified shared document.
        /// </summary>
        /// <param name="sharedDocument">The shared document.</param>
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
    }
}