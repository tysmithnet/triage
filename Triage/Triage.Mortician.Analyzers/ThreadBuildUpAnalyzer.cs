using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using SpreadsheetLight;
using Triage.Mortician.Abstraction;

namespace Triage.Mortician.Analyzers
{
    [Export(typeof(IExcelAnalyzer))]
    public class ThreadBuildUpAnalyzer : IExcelAnalyzer
    {                                 
        public ILog Log { get; set; } = LogManager.GetLogger(typeof(ThreadBuildUpAnalyzer));

        [Import]
        public IDumpThreadRepository DumpThreadRepository { get; set; }
        
        public Task Setup(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void Contribute(SLDocument sharedDocument)
        {
            sharedDocument.AddWorksheet("threads");
            sharedDocument.SelectWorksheet("threads");
            sharedDocument.SetCellValue(1, 1, "hello from build up analyzer");
        }                             
    }  
}
