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
        ///     Gets or sets the event hub.
        /// </summary>
        /// <value>
        ///     The event hub.
        /// </value>
        [Import]
        protected internal IEventHub EventHub { get; set; }

        [Import]
        protected internal IAnalyzerTaskFactory AnalyzerTaskFactory { get; set; }

        /// <summary>
        ///     Processes the analyzers
        /// </summary>
        /// <returns>A Task representing the completion of all the analyzers</returns>
        public async Task Process()
        {
            var internalCts = new CancellationTokenSource();
            var internalToken = internalCts.Token;
            if (Analyzers == null || Analyzers.Length == 0)
            {
                Log.Fatal("No analyzers were found!");
                return;
            }

            Log.Trace("Engine starting...");
            var analyzersTask = AnalyzerTaskFactory.StartAnalyzers(Analyzers, internalToken);

            // analyzer tasks handle the exceptions internally
            await analyzersTask;
            EventHub.Shutdown();
            Log.Trace("Execution complete");
        }
    }
}