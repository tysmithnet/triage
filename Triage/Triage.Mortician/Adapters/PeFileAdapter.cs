// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-19-2018
// ***********************************************************************
// <copyright file="PeFileAdapter.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.ComponentModel.Composition;
using Microsoft.Diagnostics.Runtime.Utilities;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    /// <summary>
    ///     Class PeFileAdapter.
    /// </summary>
    /// <seealso cref="Triage.Mortician.Core.ClrMdAbstractions.IPeFile" />
    internal class PeFileAdapter : BaseAdapter, IPeFile
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PeFileAdapter" /> class.
        /// </summary>
        /// <param name="peFile">The pe file.</param>
        /// <exception cref="ArgumentNullException">peFile</exception>
        /// <inheritdoc />
        public PeFileAdapter(IConverter converter, PEFile peFile) : base(converter)
        {
            PeFile = peFile ?? throw new ArgumentNullException(nameof(peFile));
            PdbInfo = Converter.Convert(peFile.PdbInfo);
        }
        public override void Setup()
        {

        }
        /// <summary>
        ///     The pe file
        /// </summary>
        internal PEFile PeFile;

        /// <summary>
        ///     Closes any file handles and cleans up resources.
        /// </summary>
        /// <inheritdoc />
        public void Dispose() => PeFile.Dispose();

        /// <summary>
        ///     Gets the File Version Information that is stored as a resource in the PE file.  (This is what the
        ///     version tab a file's property page is populated with).
        /// </summary>
        /// <returns>IFileVersionInfo.</returns>
        /// <inheritdoc />
        public IFileVersionInfo GetFileVersionInfo() => Converter.Convert(PeFile.GetFileVersionInfo());

        /// <summary>
        ///     Looks up the debug signature information in the EXE.   Returns true and sets the parameters if it is found.
        ///     If 'first' is true then the first entry is returned, otherwise (by default) the last entry is used
        ///     (this is what debuggers do today).   Thus NGEN images put the IL PDB last (which means debuggers
        ///     pick up that one), but we can set it to 'first' if we want the NGEN PDB.
        /// </summary>
        /// <param name="pdbName">Name of the PDB.</param>
        /// <param name="pdbGuid">The PDB unique identifier.</param>
        /// <param name="pdbAge">The PDB age.</param>
        /// <param name="first">if set to <c>true</c> [first].</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <inheritdoc />
        public bool GetPdbSignature(out string pdbName, out Guid pdbGuid, out int pdbAge, bool first = false) =>
            PeFile.GetPdbSignature(out pdbName, out pdbGuid, out pdbAge, first);

        /// <summary>
        ///     For side by side dlls, the manifest that decribes the binding information is stored as the RT_MANIFEST resource,
        ///     and it
        ///     is an XML string.   This routine returns this.
        /// </summary>
        /// <returns>System.String.</returns>
        /// <inheritdoc />
        public string GetSxSManfest() => PeFile.GetSxSManfest();

        /// <summary>
        ///     Whether this object has been disposed.
        /// </summary>
        /// <value><c>true</c> if disposed; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool Disposed => PeFile.Disposed;

        /// <summary>
        ///     Holds information about the pdb for the current PEFile
        /// </summary>
        /// <value>The PDB information.</value>
        /// <inheritdoc />
        public IPdbInfo PdbInfo { get; internal set; }
        
    }
}