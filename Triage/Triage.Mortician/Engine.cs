// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 12-17-2017
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="Engine.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.ComponentModel.Composition;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;

namespace Triage.Mortician
{
    /// <summary>
    ///     Represents the core execution component of the application. It is responsible for executing the analyzers
    ///     in concert with each other.
    /// </summary>
    /// <seealso cref="Triage.Mortician.IEngine" />
    [Export(typeof(IEngine))]
    public class Engine : IEngine
    {
        /// <summary>
        ///     The log
        /// </summary>
        protected ILog Log = LogManager.GetLogger(typeof(Engine));

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
            var analysisObserversTask = AnalyzerTaskFactory.StartAnalyzers(AnalysisObservers, analysisToken);
            var analyzersTask = AnalyzerTaskFactory.StartAnalyzers(Analyzers, internalToken);

            // analyzer tasks handle the exceptions internally
            await analyzersTask;
            EventHub.Shutdown();
            try
            {
                await analysisObserversTask;
            }
            catch (TaskCanceledException)
            {
                // we are expecting this
            }

            Log.Trace("Execution complete");
        }

        /// <summary>
        ///     Gets or sets the analysis observers.
        /// </summary>
        /// <value>The analysis observers.</value>
        [ImportMany]
        protected internal IAnalysisObserver[] AnalysisObservers { get; set; }

        /// <summary>
        ///     Gets or sets the analyzers.
        /// </summary>
        /// <value>The analyzers.</value>
        [ImportMany]
        protected internal IAnalyzer[] Analyzers { get; set; }

        /// <summary>
        ///     Gets or sets the analyzer task factory.
        /// </summary>
        /// <value>The analyzer task factory.</value>
        [Import]
        protected internal IAnalyzerTaskFactory AnalyzerTaskFactory { get; set; }

        /// <summary>
        ///     Gets or sets the event hub.
        /// </summary>
        /// <value>The event hub.</value>
        [Import]
        protected internal IEventHub EventHub { get; set; }
    }
}