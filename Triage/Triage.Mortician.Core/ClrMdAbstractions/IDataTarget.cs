// ***********************************************************************
// Assembly         : Triage.Mortician.Core
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="IDataTarget.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;

namespace Triage.Mortician.Core.ClrMdAbstractions
{
    /// <summary>
    ///     Interface IDataTarget
    /// </summary>
    public interface IDataTarget
    {
        /// <summary>
        ///     IDisposable implementation.
        /// </summary>
        void Dispose();

        /// <summary>
        ///     Enumerates information about the loaded modules in the process (both managed and unmanaged).
        /// </summary>
        /// <returns>IEnumerable&lt;IModuleInfo&gt;.</returns>
        IEnumerable<IModuleInfo> EnumerateModules();

        /// <summary>
        ///     Reads memory from the target.
        /// </summary>
        /// <param name="address">The address to read from.</param>
        /// <param name="buffer">
        ///     The buffer to store the data in.  Size must be greator or equal to
        ///     bytesRequested.
        /// </param>
        /// <param name="bytesRequested">The amount of bytes to read from the target process.</param>
        /// <param name="bytesRead">The actual number of bytes read.</param>
        /// <returns>
        ///     True if any bytes were read out of the process (including a partial read).  False
        ///     if no bytes could be read from the address.
        /// </returns>
        bool ReadProcessMemory(ulong address, byte[] buffer, int bytesRequested, out int bytesRead);

        /// <summary>
        ///     Returns the architecture of the target process or crash dump.
        /// </summary>
        /// <value>The architecture.</value>
        Architecture Architecture { get; }

        /// <summary>
        ///     Returns the list of Clr versions loaded into the process.
        /// </summary>
        /// <value>The color versions.</value>
        IList<IClrInfo> ClrVersions { get; }

        /// <summary>
        ///     The data reader for this instance.
        /// </summary>
        /// <value>The data reader.</value>
        IDataReader DataReader { get; }

        /// <summary>
        ///     Returns true if the target process is a minidump, or otherwise might have limited memory.  If IsMinidump
        ///     returns true, a greater range of functions may fail to return data due to the data not being present in
        ///     the application/crash dump you are debugging.
        /// </summary>
        /// <value><c>true</c> if this instance is minidump; otherwise, <c>false</c>.</value>
        bool IsMinidump { get; }

        /// <summary>
        ///     Returns the pointer size for the target process.
        /// </summary>
        /// <value>The size of the pointer.</value>
        uint PointerSize { get; }

        /// <summary>
        ///     Instance to manage the symbol path(s)
        /// </summary>
        /// <value>The symbol locator.</value>
        ISymbolLocator SymbolLocator { get; }

        /// <summary>
        ///     A symbol provider which loads PDBs on behalf of ClrMD.  This should be set so that when ClrMD needs to
        ///     resolve names which can only come from PDBs.  If this is not set, you may have a degraded experience.
        /// </summary>
        /// <value>The symbol provider.</value>
        ISymbolProvider SymbolProvider { get; }
    }
}