using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;

namespace Triage.Mortician
{
    /// <summary>
    ///     Represents the core execution component of the application. It is responsible for executing the analyzers
    ///     in concert with each other.
    /// </summary>
    [Export]
    public class Engine
    {
        /// <summary>
        ///     The log
        /// </summary>
        protected ILog Log = LogManager.GetLogger(typeof(Engine));

        /// <summary>
        ///     Gets or sets the analyzers.
        /// </summary>
        /// <value>
        ///     The analyzers.
        /// </value>
        [ImportMany]
        public IAnalyzer[] Analyzers { get; set; }

        [ImportMany]
        public IAnalysisObserver[] AnalysisObservers { get; set; }

        /// <summary>
        ///     Processes the analyzers
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task representing the completion of all the analyzers</returns>
        public async Task Process(CancellationToken cancellationToken)
        {
            if (Analyzers == null || Analyzers.Length == 0)
            {
                Log.Fatal("No analyzers were found!");
                return;
            }

            CancellationTokenSource cts = new CancellationTokenSource();
            Log.Trace("Engine starting...");
            var analyzerTasks = StartAnalyzers(cancellationToken);
            var analysisObserverTasks = StartAnalysisObservers(cts.Token);
            await Task.WhenAll(analyzerTasks);
            cts.Cancel();
            try
            {
                await Task.WhenAll(analysisObserverTasks);
            }
            catch (TaskCanceledException)
            {
                Log.Trace("Successfully shut down analysis observers");
            }                                  
        }

        private IEnumerable<Task> StartAnalysisObservers(CancellationToken cancellationToken)
        {
            return AnalysisObservers.Select(analysisObserver => Task.Run(async () =>
            {
                cancellationToken.ThrowIfCancellationRequested();
                var isSetup = false;
                try
                {
                    await analysisObserver.Setup(cancellationToken);
                    isSetup = true;
                }
                catch (Exception e)
                {
                    Log.Error(
                        $"AnalysisObserver Setup Exception: {analysisObserver.GetType().FullName} thew {e.GetType().FullName} - {e.Message}",
                        e);
                }

                if (!isSetup)
                    return;

                cancellationToken.ThrowIfCancellationRequested();
                try
                {
                    await analysisObserver.Process(cancellationToken);
                }
                catch (Exception e)
                {
                    Log.Error(
                        $"AnalysisrObserver Process Exception: {analysisObserver.GetType().FullName} threw {e.GetType().FullName} - {e.Message}",
                        e);
                }
            }, cancellationToken));
        }

        private IEnumerable<Task> StartAnalyzers(CancellationToken cancellationToken)
        {
            return Analyzers.Select(analyzer => Task.Run(async () =>
            {
                cancellationToken.ThrowIfCancellationRequested();
                var isSetup = false;
                try
                {
                    await analyzer.Setup(cancellationToken);
                    isSetup = true;
                }
                catch (Exception e)
                {
                    Log.Error(
                        $"Anayler Setup Exception: {analyzer.GetType().FullName} thew {e.GetType().FullName} - {e.Message}",
                        e);
                }

                if (!isSetup)
                    return;

                cancellationToken.ThrowIfCancellationRequested();
                try
                {
                    await analyzer.Process(cancellationToken);
                }
                catch (Exception e)
                {
                    Log.Error(
                        $"Analyzer Process Exception: {analyzer.GetType().FullName} threw {e.GetType().FullName} - {e.Message}",
                        e);
                }
            }, cancellationToken));
        }
    }
}