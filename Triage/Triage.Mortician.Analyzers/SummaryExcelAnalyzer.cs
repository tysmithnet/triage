using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SpreadsheetLight;

namespace Triage.Mortician.Analyzers
{
    [Export(typeof(IExcelAnalyzer))]
    public class SummaryExcelAnalyzer : IExcelAnalyzer
    {
        [Import]
        public DumpInformationRepository DumpInformationRepository { get; set; }

        public Task Setup(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

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
