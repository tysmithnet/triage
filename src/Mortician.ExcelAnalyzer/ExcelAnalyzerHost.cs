// ***********************************************************************
// Assembly         : Mortician.ExcelAnalyzer
// Author           : @tysmithnet
// Created          : 01-14-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-01-2018
// ***********************************************************************
// <copyright file="ExcelAnalyzerHost.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Serilog;
using SpreadsheetLight;
using Mortician.Core;
using Slog = Serilog.Log;

namespace Mortician.ExcelAnalyzer
{
    /// <summary>
    ///     Represents an analyzer that provides an environment for other excel analyzers to work
    /// </summary>
    /// <seealso cref="Mortician.Core.IAnalyzer" />
    /// <seealso cref="IAnalyzer" />
    /// <inheritdoc />
    /// <seealso cref="T:Mortician.Abstraction.IAnalyzer" />
    [Export(typeof(IAnalyzer))]
    public class ExcelAnalyzerHost : IAnalyzer
    {
        /// <summary>
        ///     Performs the analysis
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task that when complete will signal the completion of the setup procedure</returns>
        /// <inheritdoc />
        // todo: breakup method
        public async Task Process(CancellationToken cancellationToken)
        {
            if (ExcelAnalyzers == null || ExcelAnalyzers.Length == 0)
            {
                Log.Fatal("No analyzers were found!");
                return;
            }

            Log.Information("Engine starting");
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
                            Log.Error(exception,
                                "ExcelAnalyzer {FullName} failed during setup: {FullName1} - {Message}",
                                analyzer.GetType().FullName, exception.GetType().FullName, exception.Message);
                            return true;
                        });
                    }
                    else if (task.IsCanceled)
                    {
                        Log.Warning("ExcelAnalyzer {FullName} was cancelled during setup", analyzer.GetType().FullName);
                    }
                    else
                    {
                        Log.Information("ExcelAnalyzer {FullName} was successfully setup, starting contribution..",
                            analyzer.GetType().FullName);
                        analyzer.Contribute(doc);
                    }

                    analyzerSetupTasks.Remove(task);
                }

                var fileName = DateTime.Now.ToString("yyyy_MM_dd-hh_mm_ss") + ".xlsx";
                try
                {
                    doc.SelectWorksheet("Summary");
                    doc.SaveAs(fileName);
                    Log.Information("Successfully saved report: {FileName}", fileName);
                }
                catch (Exception e)
                {
                    Log.Error(e, "Unable to save excel report: {FullName} - {Message}", e.GetType().FullName,
                        e.Message);
                    throw;
                }

                if (ExcelPostProcessors == null || ExcelPostProcessors.Length == 0)
                {
                    Log.Information("There were no Excel Post Processors registered");
                }
                else
                {
                    Log.Information("Starting excel post processing");
                    foreach (var postProcessor in ExcelPostProcessors)
                        try
                        {
                            var fileInfo = Path.GetFullPath(fileName);
                            postProcessor.PostProcess(new FileInfo(fileInfo));
                        }
                        catch (Exception e)
                        {
                            Log.Error(e, "Excel Post Processor failed: {FullName} - {Message}",
                                postProcessor.GetType().FullName, e.Message);
                        }
                }

                EventHub.Broadcast(new ExcelReportComplete
                {
                    ReportFile = fileName
                });
            }
        }

        /// <summary>
        ///     Performs any necessary setup prior to processing
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task that when complete will signal the completion of the setup procedure</returns>
        /// <inheritdoc />
        public Task Setup(CancellationToken cancellationToken) => Task.CompletedTask;

        /// <summary>
        ///     Gets or sets the event hub.
        /// </summary>
        /// <value>The event hub.</value>
        [Import]
        protected internal IEventHub EventHub { get; set; }

        /// <summary>
        ///     Gets or sets the excel analyzers.
        /// </summary>
        /// <value>The excel analyzers.</value>
        [ImportMany]
        protected internal IExcelAnalyzer[] ExcelAnalyzers { get; set; }

        /// <summary>
        ///     Gets or sets the excel post processors.
        /// </summary>
        /// <value>The excel post processors.</value>
        [ImportMany]
        protected internal IExcelPostProcessor[] ExcelPostProcessors { get; set; }

        /// <summary>
        ///     Gets the log.
        /// </summary>
        /// <value>The log.</value>
        internal ILogger Log { get; } = Slog.ForContext<ExcelAnalyzerHost>();
    }
}