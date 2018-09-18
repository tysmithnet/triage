namespace Triage.Mortician.Core
{
    public interface IFileVersionInfo
    {
        /// <summary>
        /// The verison string 
        /// </summary>
        string FileVersion { get; }

        /// <summary>
        /// Comments to supplement the file version
        /// </summary>
        string Comments { get; }
    }
}