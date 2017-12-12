using System.Threading;
using System.Threading.Tasks;

namespace Triage.Mortician.Abstraction
{
    public interface IAnalyzer
    {
        Task Setup(CancellationToken cancellationToken);
        Task Process(CancellationToken cancellationToken);
    }

    public interface ISignature
    {
    }

    public interface IReport
    {
    }
}