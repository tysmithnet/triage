using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;

namespace Triage.Mortician
{
    [Export(typeof(IAnalyzerTaskFactory))]
    public class AnalyzerTaskFactory : IAnalyzerTaskFactory
    {
        private ILog Log { get; } = LogManager.GetLogger<AnalyzerTaskFactory>();

        public Task StartAnalyzers(IEnumerable<IAnalyzer> analyzers, CancellationToken cancellationToken)
        {
            var tasks = analyzers.Select(analyzer => Task.Run(async () =>
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
