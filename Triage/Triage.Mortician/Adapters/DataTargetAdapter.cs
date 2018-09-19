using System;
using System.Collections.Generic;
using System.Linq;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class DataTargetAdapter : IDataTarget
    {
        /// <inheritdoc />
        public DataTargetAdapter(Microsoft.Diagnostics.Runtime.DataTarget dataTarget)
        {
            DataTarget = dataTarget ?? throw new ArgumentNullException(nameof(dataTarget));
        }

        internal Microsoft.Diagnostics.Runtime.DataTarget DataTarget;

        /// <inheritdoc />
        public void Dispose() => DataTarget.Dispose();

        /// <inheritdoc />
        public IEnumerable<IModuleInfo> EnumerateModules() => DataTarget.EnumerateModules().Select(Converter.Convert);

        /// <inheritdoc />
        public bool ReadProcessMemory(ulong address, byte[] buffer, int bytesRequested, out int bytesRead) =>
            DataTarget.ReadProcessMemory(address, buffer, bytesRequested, out bytesRead);

        /// <inheritdoc />
        public Architecture Architecture { get; }

        /// <inheritdoc />
        public IList<IClrInfo> ClrVersions { get; }

        /// <inheritdoc />
        public IDataReader DataReader { get; }

        /// <inheritdoc />
        public bool IsMinidump => DataTarget.IsMinidump;

        /// <inheritdoc />
        public uint PointerSize => DataTarget.PointerSize;

        /// <inheritdoc />
        public ISymbolLocator SymbolLocator { get; set; }

        /// <inheritdoc />
        public ISymbolProvider SymbolProvider { get; set; }
    }
}