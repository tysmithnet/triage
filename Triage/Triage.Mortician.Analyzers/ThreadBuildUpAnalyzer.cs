using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
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

        [Import]
        public IDumpThreadRepository DumpThreadRepository { get; set; }


        public async Task Setup(CancellationToken cancellationToken)
        {                                                                         
            cancellationToken.ThrowIfCancellationRequested();
            Log.Trace("Hello world");
        }

        public async Task Process(CancellationToken cancellationToken)
        {
            Log.Trace("Processing...");                  
        }
    }  
}
