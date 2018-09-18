namespace Triage.Mortician.Core
{
    public interface IModuleInfo
    {
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
        /// Returns a PEFile from a stream constructed using instance fields of this object.
        /// If the PEFile cannot be constructed correctly, null is returned
        /// </summary>
        /// <returns></returns>
        IPEFile GetPEFile();

        /// <summary>
        /// To string.
        /// </summary>
        /// <returns>The filename of the module.</returns>
        string ToString();
    }
}