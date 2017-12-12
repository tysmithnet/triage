using System.Threading;
using System.Threading.Tasks;

namespace Triage.Mortician.Abstraction
{
    /// <summary>
    ///     Represents an object that is capable of doing some intelligent analysis
    ///     and reporting on it
    /// </summary>
    public interface IAnalyzer
    {
        /// <summary>
        ///     Performs any necessary setup prior to processing
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task that when complete will signal the completion of the setup procedure</returns>
        Task Setup(CancellationToken cancellationToken);

        /// <summary>
        ///     Performs the analysis
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task that when complete will signal the completion of the setup procedure</returns>
        Task Process(CancellationToken cancellationToken);
    }
}