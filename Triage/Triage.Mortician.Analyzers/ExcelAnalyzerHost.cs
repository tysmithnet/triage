using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using SpreadsheetLight;
using Triage.Mortician.Abstraction;

namespace Triage.Mortician.Analyzers
{
    /// <inheritdoc />
    /// <summary>
    /// Represents an analyzer that provides an environment for other excel analyzers to work
    /// </summary>
    /// <seealso cref="T:Triage.Mortician.Abstraction.IAnalyzer" />
    [Export(typeof(IAnalyzer))]
    public class ExcelAnalyzerHost : IAnalyzer
    {
        /// <summary>
        /// The log
        /// </summary>
        protected ILog Log = LogManager.GetLogger(typeof(ExcelAnalyzerHost));

        /// <summary>
        /// Gets or sets the excel analyzers.
        /// </summary>
        /// <value>
        /// The excel analyzers.
        /// </value>
        [ImportMany]
        public IExcelAnalyzer[] ExcelAnalyzers { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Performs any necessary setup prior to processing
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// A Task that when complete will signal the completion of the setup procedure
        /// </returns>
        public Task Setup(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        /// <summary>
        /// Performs the analysis
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// A Task that when complete will signal the completion of the setup procedure
        /// </returns>
        public async Task Process(CancellationToken cancellationToken)
        {
            if (ExcelAnalyzers == null || ExcelAnalyzers.Length == 0)
            {
                Log.Fatal("No analyzers were found!");
                return;
            }

            Log.Trace("Engine starting...");

            var faultedAnalyzers = new ConcurrentBag<IExcelAnalyzer>();
            var analyzerSetupTasks = new Dictionary<Task, IExcelAnalyzer>();
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
                analyzerSetupTasks.Add(task, analyzer);
            }
            using (var stream = File.OpenRead("template.xlsx"))
            {
                var doc = new SLDocument(stream);
                while (analyzerSetupTasks.Keys.Any())
                {
                    var task = await Task.WhenAny(analyzerSetupTasks.Keys);
                    if (task.IsFaulted || task.IsCanceled)
                    {
                        analyzerSetupTasks.Remove(task);
                        continue;
                    }
                    var analyzer = analyzerSetupTasks[task];
                    analyzer.Contribute(doc);
                    analyzerSetupTasks.Remove(task);
                }
                doc.SaveAs("findme.xlsx");
            }
        }
    }
}