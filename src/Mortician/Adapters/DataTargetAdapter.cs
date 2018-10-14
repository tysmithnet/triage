// ***********************************************************************
// Assembly         : Mortician
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-19-2018
// ***********************************************************************
// <copyright file="DataTargetAdapter.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Diagnostics.Runtime;
using Mortician.Core.ClrMdAbstractions;
using Architecture = Mortician.Core.ClrMdAbstractions.Architecture;
using IDataReader = Mortician.Core.ClrMdAbstractions.IDataReader;
using ISymbolProvider = Mortician.Core.ClrMdAbstractions.ISymbolProvider;

namespace Mortician.Adapters
{
    /// <summary>
    ///     Class DataTargetAdapter.
    /// </summary>
    /// <seealso cref="Mortician.Core.ClrMdAbstractions.IDataTarget" />
    internal class DataTargetAdapter : BaseAdapter, IDataTarget
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DataTargetAdapter" /> class.
        /// </summary>
        /// <param name="dataTarget">The data target.</param>
        /// <exception cref="ArgumentNullException">dataTarget</exception>
        /// <inheritdoc />
        public DataTargetAdapter(IConverter converter, DataTarget dataTarget) : base(converter)
        {
            DataTarget = dataTarget ?? throw new ArgumentNullException(nameof(dataTarget));
            IsMinidump = DataTarget.IsMinidump;
            PointerSize = DataTarget.PointerSize;
        }

        /// <summary>
        ///     The data target
        /// </summary>
        internal DataTarget DataTarget;

        /// <summary>
        ///     IDisposable implementation.
        /// </summary>
        /// <inheritdoc />
        public void Dispose() => DataTarget.Dispose();

        /// <summary>
        ///     Enumerates information about the loaded modules in the process (both managed and unmanaged).
        /// </summary>
        /// <returns>IEnumerable&lt;IModuleInfo&gt;.</returns>
        /// <inheritdoc />
        public IEnumerable<IModuleInfo> EnumerateModules() => DataTarget.EnumerateModules().Select(Converter.Convert);

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
        /// <inheritdoc />
        public bool ReadProcessMemory(ulong address, byte[] buffer, int bytesRequested, out int bytesRead) =>
            DataTarget.ReadProcessMemory(address, buffer, bytesRequested, out bytesRead);

        public override void Setup()
        {
            Architecture = Converter.Convert(DataTarget.Architecture);
            ClrVersions = DataTarget.ClrVersions.Select(Converter.Convert).ToList();
            DataReader = Converter.Convert(DataTarget.DataReader);
            SymbolLocator = Converter.Convert(DataTarget.SymbolLocator);
        }

        /// <summary>
        ///     Returns the architecture of the target process or crash dump.
        /// </summary>
        /// <value>The architecture.</value>
        /// <inheritdoc />
        public Architecture Architecture { get; internal set; }

        /// <summary>
        ///     Returns the list of Clr versions loaded into the process.
        /// </summary>
        /// <value>The color versions.</value>
        /// <inheritdoc />
        public IList<IClrInfo> ClrVersions { get; internal set; }

        /// <summary>
        ///     The data reader for this instance.
        /// </summary>
        /// <value>The data reader.</value>
        /// <inheritdoc />
        public IDataReader DataReader { get; internal set; }

        /// <summary>
        ///     Returns true if the target process is a minidump, or otherwise might have limited memory.  If IsMinidump
        ///     returns true, a greater range of functions may fail to return data due to the data not being present in
        ///     the application/crash dump you are debugging.
        /// </summary>
        /// <value><c>true</c> if this instance is minidump; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsMinidump { get; internal set; }

        /// <summary>
        ///     Returns the pointer size for the target process.
        /// </summary>
        /// <value>The size of the pointer.</value>
        /// <inheritdoc />
        public uint PointerSize { get; internal set; }

        /// <summary>
        ///     Instance to manage the symbol path(s)
        /// </summary>
        /// <value>The symbol locator.</value>
        /// <inheritdoc />
        public ISymbolLocator SymbolLocator { get; set; }

        /// <summary>
        ///     A symbol provider which loads PDBs on behalf of ClrMD.  This should be set so that when ClrMD needs to
        ///     resolve names which can only come from PDBs.  If this is not set, you may have a degraded experience.
        /// </summary>
        /// <value>The symbol provider.</value>
        /// <inheritdoc />
        public ISymbolProvider SymbolProvider { get; set; }
    }
}