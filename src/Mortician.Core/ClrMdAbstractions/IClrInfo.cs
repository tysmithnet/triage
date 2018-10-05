// ***********************************************************************
// Assembly         : Mortician.Core
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="IClrInfo.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Mortician.Core.ClrMdAbstractions
{
    /// <summary>
    ///     Interface IClrInfo
    /// </summary>
    public interface IClrInfo
    {
        /// <summary>
        ///     IComparable.  Sorts the object by version.
        /// </summary>
        /// <param name="obj">The object to compare to.</param>
        /// <returns>-1 if less, 0 if equal, 1 if greater.</returns>
        int CompareTo(object obj);

        /// <summary>
        ///     Creates a runtime from the given Dac file on disk.
        /// </summary>
        /// <returns>IClrRuntime.</returns>
        IClrRuntime CreateRuntime();

        /// <summary>
        ///     Creates a runtime from a given IXClrDataProcess interface.  Used for debugger plugins.
        /// </summary>
        /// <param name="clrDataProcess">The color data process.</param>
        /// <returns>IClrRuntime.</returns>
        IClrRuntime CreateRuntime(object clrDataProcess);

        /// <summary>
        ///     Creates a runtime from the given Dac file on disk.
        /// </summary>
        /// <param name="dacFilename">A full path to the matching mscordacwks for this process.</param>
        /// <param name="ignoreMismatch">Whether or not to ignore mismatches between</param>
        /// <returns>IClrRuntime.</returns>
        IClrRuntime CreateRuntime(string dacFilename, bool ignoreMismatch = false);

        /// <summary>
        ///     To string.
        /// </summary>
        /// <returns>A version string for this Clr runtime.</returns>
        string ToString();

        /// <summary>
        ///     Returns module information about the Dac needed create a ClrRuntime instance for this runtime.
        /// </summary>
        /// <value>The dac information.</value>
        IDacInfo DacInfo { get; }

        /// <summary>
        ///     The type of CLR this module represents.
        /// </summary>
        /// <value>The flavor.</value>
        ClrFlavor Flavor { get; }

        /// <summary>
        ///     Returns the location of the local dac on your machine which matches this version of Clr, or null
        ///     if one could not be found.
        /// </summary>
        /// <value>The local matching dac.</value>
        string LocalMatchingDac { get; }

        /// <summary>
        ///     Returns module information about the ClrInstance.
        /// </summary>
        /// <value>The module information.</value>
        IModuleInfo ModuleInfo { get; }

        /// <summary>
        ///     The version number of this runtime.
        /// </summary>
        /// <value>The version.</value>
        VersionInfo Version { get; }
    }
}