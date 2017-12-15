using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using SpreadsheetLight;

namespace Triage.Mortician.Analyzers
{
    /// <inheritdoc />
    /// <summary>
    ///     Represents an analyzer that provides an environment for other excel analyzers to work
    /// </summary>
    /// <seealso cref="T:Triage.Mortician.Abstraction.IAnalyzer" />
    [Export(typeof(IAnalyzer))]
    public class ExcelAnalyzerHost : IAnalyzer
    {
        /// <summary>
        ///     The log
        /// </summary>
        protected ILog Log = LogManager.GetLogger(typeof(ExcelAnalyzerHost));

        /// <summary>
        ///     Gets or sets the excel analyzers.
        /// </summary>
        /// <value>
        ///     The excel analyzers.
        /// </value>
        [ImportMany]
        public IExcelAnalyzer[] ExcelAnalyzers { get; set; }

        [ImportMany]
        public IExcelPostProcessor[] ExcelPostProcessors { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Performs any necessary setup prior to processing
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///     A Task that when complete will signal the completion of the setup procedure
        /// </returns>
        public Task Setup(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Performs the analysis
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///     A Task that when complete will signal the completion of the setup procedure
        /// </returns>
        public async Task Process(CancellationToken cancellationToken)
        {
            if (ExcelAnalyzers == null || ExcelAnalyzers.Length == 0)
            {
                Log.Fatal("No analyzers were found!");
                return;
            }

            Log.Trace("Engine starting...");

            var analyzerSetupTasks = new Dictionary<Task, IExcelAnalyzer>();
            foreach (var analyzer in ExcelAnalyzers)
            {
                var task = Task.Run(() => analyzer.Setup(cancellationToken), cancellationToken);
                analyzerSetupTasks.Add(task, analyzer);
            }
            using (var stream = File.OpenRead("template.xlsx"))
            {
                var doc = new SLDocument(stream);
                while (analyzerSetupTasks.Keys.Any())
                {
                    var task = await Task.WhenAny(analyzerSetupTasks.Keys);
                    var analyzer = analyzerSetupTasks[task];
                    if (task.IsFaulted)
                    {
                        task.Exception?.Handle(exception =>
                        {
                            Log.Error(
                                $"ExcelAnalyzer {analyzer.GetType().FullName} failed during setup: {exception.GetType().FullName} - {exception.Message}",
                                exception);
                            return true;
                        });
                    }
                    else if (task.IsCanceled)
                    {
                        Log.Warn($"ExcelAnalyzer {analyzer.GetType().FullName} was cancelled during setup");
                    }
                    else
                    {
                        Log.Trace(
                            $"ExcelAnalyzer {analyzer.GetType().FullName} was successfully setup, starting contribution..");
                        analyzer.Contribute(doc);
                    }
                    analyzerSetupTasks.Remove(task);
                }
                var fileName = DateTime.Now.ToString("yyyy_MM_dd-hh_mm_ss") + ".xlsx";
                try
                {
                    doc.SaveAs(fileName);
                }
                catch (Exception e)
                {
                    Log.Error($"Unable to save excel report: {e.GetType().FullName} - {e.Message}", e);
                    throw;
                }
                                     
                if (ExcelPostProcessors == null || ExcelPostProcessors.Length == 0)
                {
                    Log.Warn($"There were no Excel Post Processors registered");
                    return;
                }

                foreach (var postProcessor in ExcelPostProcessors)
                {
                    try
                    {
                        var fileInfo = Path.GetFullPath(fileName);
                        postProcessor.PostProcess(new FileInfo(fileInfo));
                    }
                    catch (Exception e)
                    {
                        Log.Error($"Excel Post Processor failed: {postProcessor.GetType().FullName} - {e.Message}", e);
                    }
                }
            }
        }
    }
}