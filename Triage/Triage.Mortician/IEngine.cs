using System.Threading.Tasks;

namespace Triage.Mortician
{
    public interface IEngine
    {
        /// <summary>
        ///     Processes the analyzers
        /// </summary>
        /// <returns>A Task representing the completion of all the analyzers</returns>
        Task Process();
    }
}