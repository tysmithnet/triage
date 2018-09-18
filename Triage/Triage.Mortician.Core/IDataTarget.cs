using System;
using System.Collections.Generic;

namespace Triage.Mortician.Core
{
    /// <summary>
    /// While ClrMD provides a managed PDB reader and PDB locator, it would be inefficient to load our own PDB
    /// reader into memory if the user already has one available.  For ClrMD operations which require reading data
    /// from PDBs, you will need to provide this implementation.  (This is currently only required for debugging
    /// .Net Native applications).
    /// </summary>
    public interface ISymbolProvider
    {
        /// <summary>
        /// Loads a PDB by its given guid/age and provides an ISymbolResolver for that PDB.
        /// </summary>
        /// <param name="pdbName">The name of the pdb.  This may be a full path and not just a simple name.</param>
        /// <param name="guid">The guid of the pdb to locate.</param>
        /// <param name="age">The age of the pdb to locate.</param>
        /// <returns>A symbol resolver for the given pdb.  Null if none was found.</returns>
        ISymbolResolver GetSymbolResolver(string pdbName, Guid guid, int age);
    }

    /// <summary>
    /// ISymbolResolver represents a single symbol module (PDB) loaded into the process.
    /// </summary>
    public interface ISymbolResolver
    {
        /// <summary>
        /// Retrieves the given symbol's name based on its RVA.
        /// </summary>
        /// <param name="rva">A relative virtual address in the module.</param>
        /// <returns>The symbol corresponding to RVA.</returns>
        string GetSymbolNameByRVA(uint rva);
    }

    /// <summary>
    /// The result of a VirtualQuery.
    /// </summary>
    [Serializable]
    public struct VirtualQueryData
    {
        /// <summary>
        /// The base address of the allocation.
        /// </summary>
        public ulong BaseAddress;

        /// <summary>
        ///  The size of the allocation.
        /// </summary>
        public ulong Size;


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="addr">Base address of the memory range.</param>
        /// <param name="size">The size of the memory range.</param>
        public VirtualQueryData(ulong addr, ulong size)
        {
            BaseAddress = addr;
            Size = size;
        }
    }

    /// <summary>
    /// An interface for reading data out of the target process.
    /// </summary>
    public interface IDataReader
    {
        /// <summary>
        /// Called when the DataTarget is closing (Disposing).  Used to clean up resources.
        /// </summary>
        void Close();

        /// <summary>
        /// Informs the data reader that the user has requested all data be flushed.
        /// </summary>
        void Flush();

        /// <summary>
        /// Gets the architecture of the target.
        /// </summary>
        /// <returns>The architecture of the target.</returns>
        Architecture GetArchitecture();

        /// <summary>
        /// Gets the size of a pointer in the target process.
        /// </summary>
        /// <returns>The pointer size of the target process.</returns>
        uint GetPointerSize();

        /// <summary>
        /// Enumerates modules in the target process.
        /// </summary>
        /// <returns>A list of the modules in the target process.</returns>
        IList<IModuleInfo> EnumerateModules();

        /// <summary>
        /// Gets the version information for a given module (given by the base address of the module).
        /// </summary>
        /// <param name="baseAddress">The base address of the module to look up.</param>
        /// <param name="version">The version info for the given module.</param>
        void GetVersionInfo(ulong baseAddress, out VersionInfo version);

        /// <summary>
        /// Read memory out of the target process.
        /// </summary>
        /// <param name="address">The address of memory to read.</param>
        /// <param name="buffer">The buffer to write to.</param>
        /// <param name="bytesRequested">The number of bytes to read.</param>
        /// <param name="bytesRead">The number of bytes actually read out of the target process.</param>
        /// <returns>True if any bytes were read at all, false if the read failed (and no bytes were read).</returns>
        bool ReadMemory(ulong address, byte[] buffer, int bytesRequested, out int bytesRead);

        /// <summary>
        /// Read memory out of the target process.
        /// </summary>
        /// <param name="address">The address of memory to read.</param>
        /// <param name="buffer">The buffer to write to.</param>
        /// <param name="bytesRequested">The number of bytes to read.</param>
        /// <param name="bytesRead">The number of bytes actually read out of the target process.</param>
        /// <returns>True if any bytes were read at all, false if the read failed (and no bytes were read).</returns>
        bool ReadMemory(ulong address, IntPtr buffer, int bytesRequested, out int bytesRead);

        /// <summary>
        /// Returns true if the data target is a minidump (or otherwise may not contain full heap data).
        /// </summary>
        /// <returns>True if the data target is a minidump (or otherwise may not contain full heap data).</returns>
        bool IsMinidump { get; }

        /// <summary>
        /// Gets the TEB of the specified thread.
        /// </summary>
        /// <param name="thread">The OS thread ID to get the TEB for.</param>
        /// <returns>The address of the thread's teb.</returns>
        ulong GetThreadTeb(uint thread);

        /// <summary>
        /// Enumerates the OS thread ID of all threads in the process.
        /// </summary>
        /// <returns>An enumeration of all threads in the target process.</returns>
        IEnumerable<uint> EnumerateAllThreads();

        /// <summary>
        /// Gets information about the given memory range.
        /// </summary>
        /// <param name="addr">An arbitrary address in the target process.</param>
        /// <param name="vq">The base address and size of the allocation.</param>
        /// <returns>True if the address was found and vq was filled, false if the address is not valid memory.</returns>
        bool VirtualQuery(ulong addr, out VirtualQueryData vq);

        /// <summary>
        /// Gets the thread context for the given thread.
        /// </summary>
        /// <param name="threadID">The OS thread ID to read the context from.</param>
        /// <param name="contextFlags">The requested context flags, or 0 for default flags.</param>
        /// <param name="contextSize">The size (in bytes) of the context parameter.</param>
        /// <param name="context">A pointer to the buffer to write to.</param>
        bool GetThreadContext(uint threadID, uint contextFlags, uint contextSize, IntPtr context);

        /// <summary>
        /// Gets the thread context for the given thread.
        /// </summary>
        /// <param name="threadID">The OS thread ID to read the context from.</param>
        /// <param name="contextFlags">The requested context flags, or 0 for default flags.</param>
        /// <param name="contextSize">The size (in bytes) of the context parameter.</param>
        /// <param name="context">A pointer to the buffer to write to.</param>
        bool GetThreadContext(uint threadID, uint contextFlags, uint contextSize, byte[] context);

        /// <summary>
        /// Read a pointer out of the target process.
        /// </summary>
        /// <returns>The pointer at the give address, or 0 if that pointer doesn't exist in
        /// the data target.</returns>
        ulong ReadPointerUnsafe(ulong addr);

        /// <summary>
        /// Read an int out of the target process.
        /// </summary>
        /// <returns>The int at the give address, or 0 if that pointer doesn't exist in
        /// the data target.</returns>
        uint ReadDwordUnsafe(ulong addr);
    }

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