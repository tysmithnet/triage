using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using Triage.Mortician.Abstraction;

namespace Triage.Mortician.Analyzers
{

    public class ThreadBuildUpAnalyzer : IAnalyzer
    {                                 
        public ILog Log { get; set; } = LogManager.GetLogger(typeof(ThreadBuildUpAnalyzer));

        public Task Setup(CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public Task Process(CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
    }
}
