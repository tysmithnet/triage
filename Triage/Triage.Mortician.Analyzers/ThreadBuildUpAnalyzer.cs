using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using Triage.Mortician.Abstraction;

namespace Triage.Mortician.Analyzers
{
    [Export(typeof(IAnalyzer))]
    public class ThreadBuildUpAnalyzer : IAnalyzer
    {                                 
        public ILog Log { get; set; } = LogManager.GetLogger(typeof(ThreadBuildUpAnalyzer));

        public async Task Setup(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Log.Trace("Hello world");
            await Task.Delay(5000, cancellationToken);
        }

        public async Task Process(CancellationToken cancellationToken)
        {
            Log.Trace("Processing...");
            await Task.Delay(5000, cancellationToken);
        }
    }  
}
