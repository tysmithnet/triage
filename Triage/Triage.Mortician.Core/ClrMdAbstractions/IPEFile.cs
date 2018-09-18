// ***********************************************************************
// Assembly         : Triage.Mortician.Core
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="IPEFile.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace Triage.Mortician.Core.ClrMdAbstractions
{
    /// <summary>
    /// Interface IPEFile
    /// </summary>
    public interface IPEFile
    {
        /// <summary>
        /// Holds information about the pdb for the current PEFile
        /// </summary>
        /// <value>The PDB information.</value>
        IPdbInfo PdbInfo { get; }

        /// <summary>
        /// Whether this object has been disposed.
        /// </summary>
        /// <value><c>true</c> if disposed; otherwise, <c>false</c>.</value>
        bool Disposed { get; }

        /// <summary>
        /// Looks up the debug signature information in the EXE.   Returns true and sets the parameters if it is found.
        /// If 'first' is true then the first entry is returned, otherwise (by default) the last entry is used
        /// (this is what debuggers do today).   Thus NGEN images put the IL PDB last (which means debuggers
        /// pick up that one), but we can set it to 'first' if we want the NGEN PDB.
        /// </summary>
        /// <param name="pdbName">Name of the PDB.</param>
        /// <param name="pdbGuid">The PDB unique identifier.</param>
        /// <param name="pdbAge">The PDB age.</param>
        /// <param name="first">if set to <c>true</c> [first].</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool GetPdbSignature(out string pdbName, out Guid pdbGuid, out int pdbAge, bool first = false);

        /// <summary>
        /// Gets the File Version Information that is stored as a resource in the PE file.  (This is what the
        /// version tab a file's property page is populated with).
        /// </summary>
        /// <returns>IFileVersionInfo.</returns>
        IFileVersionInfo GetFileVersionInfo();

        /// <summary>
        /// For side by side dlls, the manifest that decribes the binding information is stored as the RT_MANIFEST resource, and it
        /// is an XML string.   This routine returns this.
        /// </summary>
        /// <returns>System.String.</returns>
        string GetSxSManfest();

        /// <summary>
        /// Closes any file handles and cleans up resources.
        /// </summary>
        void Dispose();
    }
}