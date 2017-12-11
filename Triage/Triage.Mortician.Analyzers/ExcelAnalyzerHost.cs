using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using Triage.Mortician.Abstraction;      
using SpreadsheetLight;

namespace Triage.Mortician.Analyzers
{
    [Export(typeof(IAnalyzer))]
    public class ExcelAnalyzerHost : IAnalyzer
    {
        protected ILog Log = LogManager.GetLogger(typeof(ExcelAnalyzerHost));

        [ImportMany]
        public IExcelAnalyzer[] ExcelAnalyzers { get; set; }

        public Task Setup(CancellationToken cancellationToken)
        {   
            return Task.CompletedTask;
        }

        public async Task Process(CancellationToken cancellationToken)
        {
            if (ExcelAnalyzers == null || ExcelAnalyzers.Length == 0)
            {
                Log.Fatal("No analyzers were found!");
                return;
            }

            Log.Trace("Engine starting...");
            
            var faultedAnalyzers = new ConcurrentBag<IExcelAnalyzer>();
            var tasks = new List<Task>();

            foreach (var analyzer in ExcelAnalyzers)
            {
                var task = Task.Run(() => analyzer.Setup(cancellationToken), cancellationToken)
                    .ContinueWith(t =>
                    {
                        if (t.IsFaulted)
                        {
                            faultedAnalyzers.Add(analyzer);
                            t.Exception?.Handle(exception =>
                            {
                                Log.Error(
                                    $"ExcelAnalyzer {analyzer.GetType().FullName} failed during setup: {exception.GetType().FullName} - {exception.Message}",
                                    exception);
                                return true;
                            });
                        }
                        else if (t.IsCanceled)
                        {
                            Log.Warn($"ExcelAnalyzer {analyzer.GetType().FullName} was cancelled during setup");
                        }
                        else
                        {
                            Log.Trace($"ExcelAnalyzer {analyzer.GetType().FullName} was successfully setup");
                        }
                    }, cancellationToken);
                tasks.Add(task);
            }
            
            await Task.WhenAll(tasks);

            var doc = new SLDocument();
            foreach (var analyzer in ExcelAnalyzers.Except(faultedAnalyzers))
            {
                analyzer.Contribute(doc);
            }
            doc.SaveAs("findme.xls");
        }
    }
}
