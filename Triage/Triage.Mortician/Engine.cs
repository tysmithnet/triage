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
    [Export(typeof(IEngine))]
    public class Engine : IEngine
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
        protected internal IAnalyzer[] Analyzers { get; set; }

        /// <summary>
        ///     Gets or sets the analysis observers.
        /// </summary>
        /// <value>
        ///     The analysis observers.
        /// </value>
        [ImportMany]
        protected internal IAnalysisObserver[] AnalysisObservers { get; set; }

        /// <summary>
        ///     Gets or sets the event hub.
        /// </summary>
        /// <value>
        ///     The event hub.
        /// </value>
        [Import]
        protected internal IEventHub EventHub { get; set; }

        /// <summary>
        ///     Processes the analyzers
        /// </summary>
        /// <returns>A Task representing the completion of all the analyzers</returns>
        public async Task Process()
        {
            var internalCts = new CancellationTokenSource();
            var internalToken = internalCts.Token;
            var analysisCts = new CancellationTokenSource();
            var analysisToken = analysisCts.Token;
            if (Analyzers == null || Analyzers.Length == 0)
            {
                Log.Fatal("No analyzers were found!");
                return;
            }

            Log.Trace("Engine starting...");
            var analysisObserverTasks = StartAnalysisObservers(analysisToken);
            var analyzerTasks = StartAnalyzers(internalToken);

            // analyzer tasks handle the exceptions internally
            await Task.WhenAll(analyzerTasks);
            EventHub.Shutdown();
            try
            {
                await Task.WhenAll(analysisObserverTasks);
            }
            catch (TaskCanceledException)
            {
                // we are expecting this
            }
            Log.Trace("Execution complete");
        }

        private Task StartAnalysisObservers(CancellationToken cancellationToken)
        {
            var tasks = AnalysisObservers.Select(analysisObserver => Task.Run(async () =>
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
                        $"AnalysisObserver Process Exception: {analysisObserver.GetType().FullName} threw {e.GetType().FullName} - {e.Message}",
                        e);
                }
            }, cancellationToken));
            return Task.WhenAll(tasks);
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