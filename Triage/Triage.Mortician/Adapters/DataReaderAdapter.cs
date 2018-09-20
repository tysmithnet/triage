// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-19-2018
// ***********************************************************************
// <copyright file="DataReaderAdapter.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    /// <summary>
    ///     Class DataReaderAdapter.
    /// </summary>
    /// <seealso cref="Triage.Mortician.Core.ClrMdAbstractions.IDataReader" />
    internal class DataReaderAdapter : BaseAdapter, IDataReader
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DataReaderAdapter" /> class.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <exception cref="ArgumentNullException">dataReader</exception>
        /// <inheritdoc />
        public DataReaderAdapter(IConverter converter, Microsoft.Diagnostics.Runtime.IDataReader dataReader) : base(converter)
        {
            DataReader = dataReader ?? throw new ArgumentNullException(nameof(dataReader));
        }
        public override void Setup()
        {

        }
        /// <summary>
        ///     The data reader
        /// </summary>
        internal Microsoft.Diagnostics.Runtime.IDataReader DataReader;

        /// <summary>
        ///     Called when the DataTarget is closing (Disposing).  Used to clean up resources.
        /// </summary>
        /// <inheritdoc />
        public void Close() => DataReader.Close();

        /// <summary>
        ///     Enumerates the OS thread ID of all threads in the process.
        /// </summary>
        /// <returns>An enumeration of all threads in the target process.</returns>
        /// <inheritdoc />
        public IEnumerable<uint> EnumerateAllThreads() => DataReader.EnumerateAllThreads();

        /// <summary>
        ///     Enumerates modules in the target process.
        /// </summary>
        /// <returns>A list of the modules in the target process.</returns>
        /// <inheritdoc />
        public IList<IModuleInfo> EnumerateModules() =>
            DataReader.EnumerateModules().Select(Converter.Convert).ToList();

        /// <summary>
        ///     Informs the data reader that the user has requested all data be flushed.
        /// </summary>
        /// <inheritdoc />
        public void Flush() => DataReader.Flush();

        /// <summary>
        ///     Gets the architecture of the target.
        /// </summary>
        /// <returns>The architecture of the target.</returns>
        /// <inheritdoc />
        public Architecture GetArchitecture() => Converter.Convert(DataReader.GetArchitecture());

        /// <summary>
        ///     Gets the size of a pointer in the target process.
        /// </summary>
        /// <returns>The pointer size of the target process.</returns>
        /// <inheritdoc />
        public uint GetPointerSize() => DataReader.GetPointerSize();

        /// <summary>
        ///     Gets the thread context for the given thread.
        /// </summary>
        /// <param name="threadID">The OS thread ID to read the context from.</param>
        /// <param name="contextFlags">The requested context flags, or 0 for default flags.</param>
        /// <param name="contextSize">The size (in bytes) of the context parameter.</param>
        /// <param name="context">A pointer to the buffer to write to.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <inheritdoc />
        public bool GetThreadContext(uint threadID, uint contextFlags, uint contextSize, IntPtr context) =>
            DataReader.GetThreadContext(threadID, contextFlags, contextSize, context);

        /// <summary>
        ///     Gets the thread context for the given thread.
        /// </summary>
        /// <param name="threadID">The OS thread ID to read the context from.</param>
        /// <param name="contextFlags">The requested context flags, or 0 for default flags.</param>
        /// <param name="contextSize">The size (in bytes) of the context parameter.</param>
        /// <param name="context">A pointer to the buffer to write to.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <inheritdoc />
        public bool GetThreadContext(uint threadID, uint contextFlags, uint contextSize, byte[] context) =>
            DataReader.GetThreadContext(threadID, contextFlags, contextSize, context);

        /// <summary>
        ///     Gets the TEB of the specified thread.
        /// </summary>
        /// <param name="thread">The OS thread ID to get the TEB for.</param>
        /// <returns>The address of the thread's teb.</returns>
        /// <inheritdoc />
        public ulong GetThreadTeb(uint thread) => GetThreadTeb(thread);

        /// <summary>
        ///     Gets the version information for a given module (given by the base address of the module).
        /// </summary>
        /// <param name="baseAddress">The base address of the module to look up.</param>
        /// <param name="version">The version info for the given module.</param>
        /// <inheritdoc />
        public void GetVersionInfo(ulong baseAddress, out VersionInfo version) =>
            GetVersionInfo(baseAddress, out version);

        /// <summary>
        ///     Read an int out of the target process.
        /// </summary>
        /// <param name="addr">The addr.</param>
        /// <returns>
        ///     The int at the give address, or 0 if that pointer doesn't exist in
        ///     the data target.
        /// </returns>
        /// <inheritdoc />
        public uint ReadDwordUnsafe(ulong addr) => ReadDwordUnsafe(addr);

        /// <summary>
        ///     Read memory out of the target process.
        /// </summary>
        /// <param name="address">The address of memory to read.</param>
        /// <param name="buffer">The buffer to write to.</param>
        /// <param name="bytesRequested">The number of bytes to read.</param>
        /// <param name="bytesRead">The number of bytes actually read out of the target process.</param>
        /// <returns>True if any bytes were read at all, false if the read failed (and no bytes were read).</returns>
        /// <inheritdoc />
        public bool ReadMemory(ulong address, byte[] buffer, int bytesRequested, out int bytesRead) =>
            DataReader.ReadMemory(address, buffer, bytesRequested, out bytesRead);

        /// <summary>
        ///     Read memory out of the target process.
        /// </summary>
        /// <param name="address">The address of memory to read.</param>
        /// <param name="buffer">The buffer to write to.</param>
        /// <param name="bytesRequested">The number of bytes to read.</param>
        /// <param name="bytesRead">The number of bytes actually read out of the target process.</param>
        /// <returns>True if any bytes were read at all, false if the read failed (and no bytes were read).</returns>
        /// <inheritdoc />
        public bool ReadMemory(ulong address, IntPtr buffer, int bytesRequested, out int bytesRead) =>
            DataReader.ReadMemory(address, buffer, bytesRequested, out bytesRead);

        /// <summary>
        ///     Read a pointer out of the target process.
        /// </summary>
        /// <param name="addr">The addr.</param>
        /// <returns>
        ///     The pointer at the give address, or 0 if that pointer doesn't exist in
        ///     the data target.
        /// </returns>
        /// <inheritdoc />
        public ulong ReadPointerUnsafe(ulong addr) => DataReader.ReadPointerUnsafe(addr);

        /// <summary>
        ///     Gets information about the given memory range.
        /// </summary>
        /// <param name="addr">An arbitrary address in the target process.</param>
        /// <param name="virtualQuery">The base address and size of the allocation.</param>
        /// <returns>True if the address was found and vq was filled, false if the address is not valid memory.</returns>
        /// <inheritdoc />
        public bool VirtualQuery(ulong addr, out VirtualQueryData virtualQuery)
        {
            var res = DataReader.VirtualQuery(addr, out var outVar);
            virtualQuery = Converter.Convert(outVar);
            return res;
        }

        /// <summary>
        ///     Returns true if the data target is a minidump (or otherwise may not contain full heap data).
        /// </summary>
        /// <value><c>true</c> if this instance is minidump; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsMinidump => DataReader.IsMinidump;
        
    }
}