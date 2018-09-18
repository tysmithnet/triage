using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Triage.Mortician
{
    public interface IAnalyzerTaskFactory
    {
        Task StartAnalyzers(IEnumerable<IAnalyzer> analyzers, CancellationToken cancellationToken);
    }
}