// ***********************************************************************
// Assembly         : Mortician.Core
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="ISymbolLocator.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Threading.Tasks;

namespace Mortician.Core.ClrMdAbstractions
{
    /// <summary>
    ///     Interface ISymbolLocator
    /// </summary>
    public interface ISymbolLocator
    {
        /// <summary>
        ///     Attempts to locate a binary via the symbol server.  This function will then copy the file
        ///     locally to the symbol cache and return the location of the local file on disk.
        /// </summary>
        /// <param name="fileName">The filename that the binary is indexed under.</param>
        /// <param name="buildTimeStamp">The build timestamp the binary is indexed under.</param>
        /// <param name="imageSize">The image size the binary is indexed under.</param>
        /// <param name="checkProperties">Whether or not to validate the properties of the binary after download.</param>
        /// <returns>A full path on disk (local) of where the binary was copied to, null if it was not found.</returns>
        string FindBinary(string fileName, uint buildTimeStamp, uint imageSize, bool checkProperties = true);

        /// <summary>
        ///     Attempts to locate a binary via the symbol server.  This function will then copy the file
        ///     locally to the symbol cache and return the location of the local file on disk.
        /// </summary>
        /// <param name="fileName">The filename that the binary is indexed under.</param>
        /// <param name="buildTimeStamp">The build timestamp the binary is indexed under.</param>
        /// <param name="imageSize">The image size the binary is indexed under.</param>
        /// <param name="checkProperties">Whether or not to validate the properties of the binary after download.</param>
        /// <returns>A full path on disk (local) of where the binary was copied to, null if it was not found.</returns>
        string FindBinary(string fileName, int buildTimeStamp, int imageSize, bool checkProperties = true);

        /// <summary>
        ///     Attempts to locate a binary via the symbol server.  This function will then copy the file
        ///     locally to the symbol cache and return the location of the local file on disk.
        /// </summary>
        /// <param name="module">The module to locate.</param>
        /// <param name="checkProperties">Whether or not to validate the properties of the binary after download.</param>
        /// <returns>A full path on disk (local) of where the binary was copied to, null if it was not found.</returns>
        string FindBinary(IModuleInfo module, bool checkProperties = true);

        /// <summary>
        ///     Attempts to locate a dac via the symbol server.  This function will then copy the file
        ///     locally to the symbol cache and return the location of the local file on disk.  Note that
        ///     the dac should not validate if the properties of the file match the one it was indexed under.
        /// </summary>
        /// <param name="dac">The dac to locate.</param>
        /// <returns>A full path on disk (local) of where the binary was copied to, null if it was not found.</returns>
        string FindBinary(IDacInfo dac);

        /// <summary>
        ///     Attempts to locate a binary via the symbol server.  This function will then copy the file
        ///     locally to the symbol cache and return the location of the local file on disk.
        /// </summary>
        /// <param name="fileName">The filename that the binary is indexed under.</param>
        /// <param name="buildTimeStamp">The build timestamp the binary is indexed under.</param>
        /// <param name="imageSize">The image size the binary is indexed under.</param>
        /// <param name="checkProperties">Whether or not to validate the properties of the binary after download.</param>
        /// <returns>A full path on disk (local) of where the binary was copied to, null if it was not found.</returns>
        Task<string> FindBinaryAsync(string fileName, uint buildTimeStamp, uint imageSize, bool checkProperties = true);

        /// <summary>
        ///     Attempts to locate a binary via the symbol server.  This function will then copy the file
        ///     locally to the symbol cache and return the location of the local file on disk.
        /// </summary>
        /// <param name="fileName">The filename that the binary is indexed under.</param>
        /// <param name="buildTimeStamp">The build timestamp the binary is indexed under.</param>
        /// <param name="imageSize">The image size the binary is indexed under.</param>
        /// <param name="checkProperties">Whether or not to validate the properties of the binary after download.</param>
        /// <returns>A full path on disk (local) of where the binary was copied to, null if it was not found.</returns>
        Task<string> FindBinaryAsync(string fileName, int buildTimeStamp, int imageSize, bool checkProperties = true);

        /// <summary>
        ///     Attempts to locate a binary via the symbol server.  This function will then copy the file
        ///     locally to the symbol cache and return the location of the local file on disk.
        /// </summary>
        /// <param name="module">The module to locate.</param>
        /// <param name="checkProperties">Whether or not to validate the properties of the binary after download.</param>
        /// <returns>A full path on disk (local) of where the binary was copied to, null if it was not found.</returns>
        Task<string> FindBinaryAsync(IModuleInfo module, bool checkProperties = true);

        /// <summary>
        ///     Attempts to locate a dac via the symbol server.  This function will then copy the file
        ///     locally to the symbol cache and return the location of the local file on disk.  Note that
        ///     the dac should not validate if the properties of the file match the one it was indexed under.
        /// </summary>
        /// <param name="dac">The dac to locate.</param>
        /// <returns>A full path on disk (local) of where the binary was copied to, null if it was not found.</returns>
        Task<string> FindBinaryAsync(IDacInfo dac);

        /// <summary>
        ///     Attempts to locate the pdb for a given module.
        /// </summary>
        /// <param name="module">The module to locate the pdb for.</param>
        /// <returns>A full path on disk (local) of where the pdb was copied to.</returns>
        string FindPdb(IModuleInfo module);

        /// <summary>
        ///     Attempts to locate the pdb for a given module.
        /// </summary>
        /// <param name="pdb">The pdb to locate.</param>
        /// <returns>A full path on disk (local) of where the pdb was copied to.</returns>
        string FindPdb(IPdbInfo pdb);

        /// <summary>
        ///     Attempts to locate a pdb based on its name, guid, and revision number.
        /// </summary>
        /// <param name="pdbName">The name the pdb is indexed under.</param>
        /// <param name="pdbIndexGuid">The guid the pdb is indexed under.</param>
        /// <param name="pdbIndexAge">The age of the pdb.</param>
        /// <returns>A full path on disk (local) of where the pdb was copied to.</returns>
        string FindPdb(string pdbName, Guid pdbIndexGuid, int pdbIndexAge);

        /// <summary>
        ///     Attempts to locate the pdb for a given module.
        /// </summary>
        /// <param name="module">The module to locate the pdb for.</param>
        /// <returns>A full path on disk (local) of where the pdb was copied to.</returns>
        Task<string> FindPdbAsync(IModuleInfo module);

        /// <summary>
        ///     Attempts to locate the pdb for a given module.
        /// </summary>
        /// <param name="pdb">The pdb to locate.</param>
        /// <returns>A full path on disk (local) of where the pdb was copied to.</returns>
        Task<string> FindPdbAsync(IPdbInfo pdb);

        /// <summary>
        ///     Attempts to locate a pdb based on its name, guid, and revision number.
        /// </summary>
        /// <param name="pdbName">The name the pdb is indexed under.</param>
        /// <param name="pdbIndexGuid">The guid the pdb is indexed under.</param>
        /// <param name="pdbIndexAge">The age of the pdb.</param>
        /// <returns>A full path on disk (local) of where the pdb was copied to.</returns>
        Task<string> FindPdbAsync(string pdbName, Guid pdbIndexGuid, int pdbIndexAge);

        /// <summary>
        ///     Gets or sets the local symbol file cache.  This is the location that
        ///     all symbol files are downloaded to on your computer.
        /// </summary>
        /// <value>The symbol cache.</value>
        string SymbolCache { get; }

        /// <summary>
        ///     Gets or sets the SymbolPath this object uses to attempt to find PDBs and binaries.
        /// </summary>
        /// <value>The symbol path.</value>
        string SymbolPath { get; }

        /// <summary>
        ///     The timeout (in milliseconds) used when contacting each individual server.  This is not a total timeout for the
        ///     entire
        ///     symbol server operation.
        /// </summary>
        /// <value>The timeout.</value>
        int Timeout { get; }
    }
}