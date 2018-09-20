// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-19-2018
// ***********************************************************************
// <copyright file="SymbolLocatorAdapter.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Threading.Tasks;
using Microsoft.Diagnostics.Runtime.Utilities;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    /// <summary>
    ///     Class SymbolLocatorAdapter.
    /// </summary>
    /// <seealso cref="Triage.Mortician.Core.ClrMdAbstractions.ISymbolLocator" />
    internal class SymbolLocatorAdapter : BaseAdapter, ISymbolLocator
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SymbolLocatorAdapter" /> class.
        /// </summary>
        /// <param name="locator">The locator.</param>
        /// <exception cref="ArgumentNullException">locator</exception>
        /// <inheritdoc />
        public SymbolLocatorAdapter(IConverter converter, SymbolLocator locator) : base(converter)
        {
            Locator = locator ?? throw new ArgumentNullException(nameof(locator));
        }
        public override void Setup()
        {

        }
        /// <summary>
        ///     The locator
        /// </summary>
        internal SymbolLocator Locator;

        /// <summary>
        ///     Attempts to locate a binary via the symbol server.  This function will then copy the file
        ///     locally to the symbol cache and return the location of the local file on disk.
        /// </summary>
        /// <param name="fileName">The filename that the binary is indexed under.</param>
        /// <param name="buildTimeStamp">The build timestamp the binary is indexed under.</param>
        /// <param name="imageSize">The image size the binary is indexed under.</param>
        /// <param name="checkProperties">Whether or not to validate the properties of the binary after download.</param>
        /// <returns>A full path on disk (local) of where the binary was copied to, null if it was not found.</returns>
        /// <inheritdoc />
        public string FindBinary(string fileName, uint buildTimeStamp, uint imageSize, bool checkProperties = true) =>
            Locator.FindBinary(fileName, buildTimeStamp, imageSize, checkProperties);

        /// <summary>
        ///     Attempts to locate a binary via the symbol server.  This function will then copy the file
        ///     locally to the symbol cache and return the location of the local file on disk.
        /// </summary>
        /// <param name="fileName">The filename that the binary is indexed under.</param>
        /// <param name="buildTimeStamp">The build timestamp the binary is indexed under.</param>
        /// <param name="imageSize">The image size the binary is indexed under.</param>
        /// <param name="checkProperties">Whether or not to validate the properties of the binary after download.</param>
        /// <returns>A full path on disk (local) of where the binary was copied to, null if it was not found.</returns>
        /// <inheritdoc />
        public string FindBinary(string fileName, int buildTimeStamp, int imageSize, bool checkProperties = true) =>
            Locator.FindBinary(fileName, buildTimeStamp, imageSize, checkProperties);

        /// <summary>
        ///     Attempts to locate a binary via the symbol server.  This function will then copy the file
        ///     locally to the symbol cache and return the location of the local file on disk.
        /// </summary>
        /// <param name="module">The module to locate.</param>
        /// <param name="checkProperties">Whether or not to validate the properties of the binary after download.</param>
        /// <returns>A full path on disk (local) of where the binary was copied to, null if it was not found.</returns>
        /// <inheritdoc />
        public string FindBinary(IModuleInfo module, bool checkProperties = true) =>
            Locator.FindBinary((module as ModuleInfoAdapter)?.ModuleInfo, checkProperties);

        /// <summary>
        ///     Attempts to locate a dac via the symbol server.  This function will then copy the file
        ///     locally to the symbol cache and return the location of the local file on disk.  Note that
        ///     the dac should not validate if the properties of the file match the one it was indexed under.
        /// </summary>
        /// <param name="dac">The dac to locate.</param>
        /// <returns>A full path on disk (local) of where the binary was copied to, null if it was not found.</returns>
        /// <inheritdoc />
        public string FindBinary(IDacInfo dac) => Locator.FindBinary((dac as DacInfoAdapter)?.DacInfo);

        /// <summary>
        ///     Attempts to locate a binary via the symbol server.  This function will then copy the file
        ///     locally to the symbol cache and return the location of the local file on disk.
        /// </summary>
        /// <param name="fileName">The filename that the binary is indexed under.</param>
        /// <param name="buildTimeStamp">The build timestamp the binary is indexed under.</param>
        /// <param name="imageSize">The image size the binary is indexed under.</param>
        /// <param name="checkProperties">Whether or not to validate the properties of the binary after download.</param>
        /// <returns>A full path on disk (local) of where the binary was copied to, null if it was not found.</returns>
        /// <inheritdoc />
        public Task<string> FindBinaryAsync(string fileName, uint buildTimeStamp, uint imageSize,
            bool checkProperties = true) =>
            Locator.FindBinaryAsync(fileName, buildTimeStamp, imageSize, checkProperties);

        /// <summary>
        ///     Attempts to locate a binary via the symbol server.  This function will then copy the file
        ///     locally to the symbol cache and return the location of the local file on disk.
        /// </summary>
        /// <param name="fileName">The filename that the binary is indexed under.</param>
        /// <param name="buildTimeStamp">The build timestamp the binary is indexed under.</param>
        /// <param name="imageSize">The image size the binary is indexed under.</param>
        /// <param name="checkProperties">Whether or not to validate the properties of the binary after download.</param>
        /// <returns>A full path on disk (local) of where the binary was copied to, null if it was not found.</returns>
        /// <inheritdoc />
        public Task<string> FindBinaryAsync(string fileName, int buildTimeStamp, int imageSize,
            bool checkProperties = true) =>
            Locator.FindBinaryAsync(fileName, buildTimeStamp, imageSize, checkProperties);

        /// <summary>
        ///     Attempts to locate a binary via the symbol server.  This function will then copy the file
        ///     locally to the symbol cache and return the location of the local file on disk.
        /// </summary>
        /// <param name="module">The module to locate.</param>
        /// <param name="checkProperties">Whether or not to validate the properties of the binary after download.</param>
        /// <returns>A full path on disk (local) of where the binary was copied to, null if it was not found.</returns>
        /// <inheritdoc />
        public Task<string> FindBinaryAsync(IModuleInfo module, bool checkProperties = true) =>
            Locator.FindBinaryAsync((module as ModuleInfoAdapter)?.ModuleInfo, checkProperties);

        /// <summary>
        ///     Attempts to locate a dac via the symbol server.  This function will then copy the file
        ///     locally to the symbol cache and return the location of the local file on disk.  Note that
        ///     the dac should not validate if the properties of the file match the one it was indexed under.
        /// </summary>
        /// <param name="dac">The dac to locate.</param>
        /// <returns>A full path on disk (local) of where the binary was copied to, null if it was not found.</returns>
        /// <inheritdoc />
        public Task<string> FindBinaryAsync(IDacInfo dac) => Locator.FindBinaryAsync((dac as DacInfoAdapter)?.DacInfo);

        /// <summary>
        ///     Attempts to locate the pdb for a given module.
        /// </summary>
        /// <param name="module">The module to locate the pdb for.</param>
        /// <returns>A full path on disk (local) of where the pdb was copied to.</returns>
        /// <inheritdoc />
        public string FindPdb(IModuleInfo module) => Locator.FindPdb((module as ModuleInfoAdapter)?.ModuleInfo);

        /// <summary>
        ///     Attempts to locate the pdb for a given module.
        /// </summary>
        /// <param name="pdb">The pdb to locate.</param>
        /// <returns>A full path on disk (local) of where the pdb was copied to.</returns>
        /// <inheritdoc />
        public string FindPdb(IPdbInfo pdb) => Locator.FindPdb((pdb as PdbInfoAdapter)?.PdbInfo);

        /// <summary>
        ///     Attempts to locate a pdb based on its name, guid, and revision number.
        /// </summary>
        /// <param name="pdbName">The name the pdb is indexed under.</param>
        /// <param name="pdbIndexGuid">The guid the pdb is indexed under.</param>
        /// <param name="pdbIndexAge">The age of the pdb.</param>
        /// <returns>A full path on disk (local) of where the pdb was copied to.</returns>
        /// <inheritdoc />
        public string FindPdb(string pdbName, Guid pdbIndexGuid, int pdbIndexAge) =>
            Locator.FindPdb(pdbName, pdbIndexGuid, pdbIndexAge);

        /// <summary>
        ///     Attempts to locate the pdb for a given module.
        /// </summary>
        /// <param name="module">The module to locate the pdb for.</param>
        /// <returns>A full path on disk (local) of where the pdb was copied to.</returns>
        /// <inheritdoc />
        public Task<string> FindPdbAsync(IModuleInfo module) =>
            Locator.FindPdbAsync((module as ModuleInfoAdapter)?.ModuleInfo);

        /// <summary>
        ///     Attempts to locate the pdb for a given module.
        /// </summary>
        /// <param name="pdb">The pdb to locate.</param>
        /// <returns>A full path on disk (local) of where the pdb was copied to.</returns>
        /// <inheritdoc />
        public Task<string> FindPdbAsync(IPdbInfo pdb) => Locator.FindPdbAsync((pdb as PdbInfoAdapter)?.PdbInfo);

        /// <summary>
        ///     Attempts to locate a pdb based on its name, guid, and revision number.
        /// </summary>
        /// <param name="pdbName">The name the pdb is indexed under.</param>
        /// <param name="pdbIndexGuid">The guid the pdb is indexed under.</param>
        /// <param name="pdbIndexAge">The age of the pdb.</param>
        /// <returns>A full path on disk (local) of where the pdb was copied to.</returns>
        /// <inheritdoc />
        public Task<string> FindPdbAsync(string pdbName, Guid pdbIndexGuid, int pdbIndexAge) =>
            Locator.FindPdbAsync(pdbName, pdbIndexGuid, pdbIndexAge);

        /// <summary>
        ///     Gets or sets the local symbol file cache.  This is the location that
        ///     all symbol files are downloaded to on your computer.
        /// </summary>
        /// <value>The symbol cache.</value>
        /// <inheritdoc />
        public string SymbolCache => Locator.SymbolCache;

        /// <summary>
        ///     Gets or sets the SymbolPath this object uses to attempt to find PDBs and binaries.
        /// </summary>
        /// <value>The symbol path.</value>
        /// <inheritdoc />
        public string SymbolPath => Locator.SymbolPath;

        /// <summary>
        ///     The timeout (in milliseconds) used when contacting each individual server.  This is not a total timeout for the
        ///     entire
        ///     symbol server operation.
        /// </summary>
        /// <value>The timeout.</value>
        /// <inheritdoc />
        public int Timeout => Locator.Timeout;
    }
}