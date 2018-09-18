using System.Collections.Generic;

namespace Triage.Mortician.Core.ClrMdAbstractions
{
    public interface IDataTarget
    {
        /// <summary>
        /// The data reader for this instance.
        /// </summary>
        IDataReader DataReader { get; }

        /// <summary>
        /// Instance to manage the symbol path(s)
        /// </summary>
        ISymbolLocator SymbolLocator { get; set; }

        /// <summary>
        /// A symbol provider which loads PDBs on behalf of ClrMD.  This should be set so that when ClrMD needs to
        /// resolve names which can only come from PDBs.  If this is not set, you may have a degraded experience.
        /// </summary>
        ISymbolProvider SymbolProvider { get; set; }

        /// <summary>
        /// Returns true if the target process is a minidump, or otherwise might have limited memory.  If IsMinidump
        /// returns true, a greater range of functions may fail to return data due to the data not being present in
        /// the application/crash dump you are debugging.
        /// </summary>
        bool IsMinidump { get; }

        /// <summary>
        /// Returns the architecture of the target process or crash dump.
        /// </summary>
        Architecture Architecture { get; }

        /// <summary>
        /// Returns the list of Clr versions loaded into the process.
        /// </summary>
        IList<IClrInfo> ClrVersions { get; }

        /// <summary>
        /// Returns the pointer size for the target process.
        /// </summary>
        uint PointerSize { get; }
        
        /// <summary>
        /// Reads memory from the target.
        /// </summary>
        /// <param name="address">The address to read from.</param>
        /// <param name="buffer">The buffer to store the data in.  Size must be greator or equal to
        /// bytesRequested.</param>
        /// <param name="bytesRequested">The amount of bytes to read from the target process.</param>
        /// <param name="bytesRead">The actual number of bytes read.</param>
        /// <returns>True if any bytes were read out of the process (including a partial read).  False
        /// if no bytes could be read from the address.</returns>
        bool ReadProcessMemory(ulong address, byte[] buffer, int bytesRequested, out int bytesRead);

        /// <summary>
        /// Enumerates information about the loaded modules in the process (both managed and unmanaged).
        /// </summary>
        IEnumerable<IModuleInfo> EnumerateModules();

        /// <summary>
        /// IDisposable implementation.
        /// </summary>
        void Dispose();
    }
}