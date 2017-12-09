using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using Triage.Mortician.Abstraction;

namespace Triage.Mortician.Api
{
    [Export]
    public class Engine
    {
        [ImportMany]
        public IAnalyzer[] Analyzers { get; set; }

        protected internal ILog Log { get; set; } = LogManager.GetLogger(typeof(Engine));

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
                bool isSetup = false;
                try
                {   
                    await analyzer.Setup(cancellationToken);
                    isSetup = true;
                }
                catch (Exception e)
                {
                    Log.Error($"Anayler Setup Exception: {analyzer.GetType().FullName} thew {e.GetType().FullName} - {e.Message}", e);
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
                    Log.Error($"Analyzer Process Exception: {analyzer.GetType().FullName} threw {e.GetType().FullName} - {e.Message}", e);
                }
            }, cancellationToken));

            return Task.WhenAll(tasks);

        }          
    }
}