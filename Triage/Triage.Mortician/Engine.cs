using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using Triage.Mortician.Abstraction;

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

        /// <summary>
        ///     Processes the analyzers
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task representing the completion of all the analyzers</returns>
        public Task Process(CancellationToken cancellationToken)
        {
            if (Analyzers == null || Analyzers.Length == 0)
            {
                Log.Fatal("No analyzers were found!");
                return Task.FromResult(0);
            }

            Log.Trace("Engine starting...");
            var tasks = Analyzers.Select(analyzer => Task.Run(async () =>
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

            return Task.WhenAll(tasks);
        }
    }
}