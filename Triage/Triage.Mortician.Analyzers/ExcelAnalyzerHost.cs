using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;     
using Triage.Mortician.Abstraction;      
using SpreadsheetLight;

namespace Triage.Mortician.Analyzers
{
    [Export(typeof(IAnalyzer))]
    public class ExcelAnalyzerHost : IAnalyzer
    {
        [ImportMany]
        public IExcelAnalyzer[] ExcelAnalyzers { get; set; }

        public Task Setup(CancellationToken cancellationToken)
        {                         
            var doc = new SLDocument();
            foreach (var excelAnalyzer in ExcelAnalyzers)
            {
                excelAnalyzer.Contribute(doc);
            }

            doc.SaveAs("findme.xls");
            return Task.CompletedTask;
        }

        public Task Process(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }

    public interface IExcelAnalyzer
    {
        void Contribute(SLDocument sharedDocument);
    }

    [Export(typeof(IExcelAnalyzer))]
    public class HelloExcel : IExcelAnalyzer
    {
        public void Contribute(SLDocument sharedDocument)
        {
            sharedDocument.AddWorksheet("Hello world");
        }
    }
}
