namespace Triage.Mortician.Core
{
    public interface IDacInfo
    {
        /// <summary>
        /// The platform-agnostice filename of the dac dll
        /// </summary>
        string PlatformAgnosticFileName { get; set; }

        /// <summary>
        /// The architecture (x86 or amd64) being targeted
        /// </summary>
        Architecture TargetArchitecture { get; set; }

        /// <summary>
        /// The base address of the object.
        /// </summary>
        ulong ImageBase { get; set; }

        /// <summary>
        /// The filesize of the image.
        /// </summary>
        uint FileSize { get; set; }

        /// <summary>
        /// The build timestamp of the image.
        /// </summary>
        uint TimeStamp { get; set; }

        /// <summary>
        /// The filename of the module on disk.
        /// </summary>
        string FileName { get; set; }

        /// <summary>
        /// Returns true if this module is a native (non-managed) .Net runtime module.
        /// </summary>
        bool IsRuntime { get; }

        /// <summary>
        /// Whether the module is managed or not.
        /// </summary>
        bool IsManaged { get; }

        /// <summary>
        /// The PDB associated with this module.
        /// </summary>
        IPdbInfo Pdb { get; set; }

        /// <summary>
        /// The version information for this file.
        /// </summary>
        VersionInfo Version { get; set; }
        
        /// <summary>
        /// To string.
        /// </summary>
        /// <returns>The filename of the module.</returns>
        string ToString();
    }
}