// ***********************************************************************
// Assembly         : Triage.Mortician.Core
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="IModuleInfo.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Triage.Mortician.Core.ClrMdAbstractions
{
    /// <summary>
    ///     Interface IModuleInfo
    /// </summary>
    public interface IModuleInfo
    {
        /// <summary>
        ///     Returns a PEFile from a stream constructed using instance fields of this object.
        ///     If the PEFile cannot be constructed correctly, null is returned
        /// </summary>
        /// <returns>IPEFile.</returns>
        IPeFile GetPEFile();

        /// <summary>
        ///     To string.
        /// </summary>
        /// <returns>The filename of the module.</returns>
        string ToString();

        /// <summary>
        ///     The filename of the module on disk.
        /// </summary>
        /// <value>The name of the file.</value>
        string FileName { get; }

        /// <summary>
        ///     The filesize of the image.
        /// </summary>
        /// <value>The size of the file.</value>
        uint FileSize { get; }

        /// <summary>
        ///     The base address of the object.
        /// </summary>
        /// <value>The image base.</value>
        ulong ImageBase { get; }

        /// <summary>
        ///     Whether the module is managed or not.
        /// </summary>
        /// <value><c>true</c> if this instance is managed; otherwise, <c>false</c>.</value>
        bool IsManaged { get; }

        /// <summary>
        ///     Returns true if this module is a native (non-managed) .Net runtime module.
        /// </summary>
        /// <value><c>true</c> if this instance is runtime; otherwise, <c>false</c>.</value>
        bool IsRuntime { get; }

        /// <summary>
        ///     The PDB associated with this module.
        /// </summary>
        /// <value>The PDB.</value>
        IPdbInfo Pdb { get; }

        /// <summary>
        ///     The build timestamp of the image.
        /// </summary>
        /// <value>The time stamp.</value>
        uint TimeStamp { get; }

        /// <summary>
        ///     The version information for this file.
        /// </summary>
        /// <value>The version.</value>
        VersionInfo Version { get; }
    }
}