using System.Threading;
using System.Threading.Tasks;
using SpreadsheetLight;

namespace Triage.Mortician.Analyzers
{
    /// <summary>
    ///     Represents an object that is cabable of making an excel report
    /// </summary>
    public interface IExcelAnalyzer
    {
        /// <summary>
        ///     Performs any required setup like number crunching etc.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task, that when complete will signal the setup completion</returns>
        Task Setup(CancellationToken cancellationToken);
                         
        /// <summary>
        ///     Contributes the specified shared document.
        /// </summary>
        /// <param name="sharedDocument">The shared document.</param>
        void Contribute(SLDocument sharedDocument);
    }
}