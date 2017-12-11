using System.Threading;
using System.Threading.Tasks;
using SpreadsheetLight;

namespace Triage.Mortician.Analyzers
{
    public interface IExcelAnalyzer
    {
        Task Setup(CancellationToken cancellationToken);
        void Contribute(SLDocument sharedDocument);
    }
}