namespace Triage.Mortician.Abstraction
{
    /// <summary>
    /// Represents an object that is capable of communicating with the windows debugger com server
    /// </summary>
    public interface IDebuggerProxy
    {
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        void Dispose();

        /// <summary>
        /// Executes the specified command.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <returns>The results of the command</returns>
        string Execute(string cmd);
    }                  
}