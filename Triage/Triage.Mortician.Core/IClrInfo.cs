namespace Triage.Mortician.Core
{
    public interface IClrInfo
    {
        /// <summary>
        /// The version number of this runtime.
        /// </summary>
        VersionInfo Version { get; }

        /// <summary>
        /// The type of CLR this module represents.
        /// </summary>
        ClrFlavor Flavor { get; }

        /// <summary>
        /// Returns module information about the Dac needed create a ClrRuntime instance for this runtime.
        /// </summary>
        IDacInfo DacInfo { get; }

        /// <summary>
        /// Returns module information about the ClrInstance.
        /// </summary>
        IModuleInfo ModuleInfo { get; }

        /// <summary>
        /// Returns the location of the local dac on your machine which matches this version of Clr, or null
        /// if one could not be found.
        /// </summary>
        string LocalMatchingDac { get; }

        /// <summary>
        /// Creates a runtime from the given Dac file on disk.
        /// </summary>
        IClrRuntime CreateRuntime();

        /// <summary>
        /// Creates a runtime from a given IXClrDataProcess interface.  Used for debugger plugins.
        /// </summary>
        IClrRuntime CreateRuntime(object clrDataProcess);

        /// <summary>
        /// Creates a runtime from the given Dac file on disk.
        /// </summary>
        /// <param name="dacFilename">A full path to the matching mscordacwks for this process.</param>
        /// <param name="ignoreMismatch">Whether or not to ignore mismatches between </param>
        /// <returns></returns>
        IClrRuntime CreateRuntime(string dacFilename, bool ignoreMismatch = false);

        /// <summary>
        /// To string.
        /// </summary>
        /// <returns>A version string for this Clr runtime.</returns>
        string ToString();

        /// <summary>
        /// IComparable.  Sorts the object by version.
        /// </summary>
        /// <param name="obj">The object to compare to.</param>
        /// <returns>-1 if less, 0 if equal, 1 if greater.</returns>
        int CompareTo(object obj);
    }
}