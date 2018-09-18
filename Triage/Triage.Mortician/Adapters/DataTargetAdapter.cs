using System;
using System.Collections.Generic;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class DataTargetAdapter : IDataTarget
    {
        /// <inheritdoc />
        public DataTargetAdapter(Microsoft.Diagnostics.Runtime.DataTarget dataTarget)
        {
            _dataTarget = dataTarget ?? throw new ArgumentNullException(nameof(dataTarget));
        }

        internal Microsoft.Diagnostics.Runtime.DataTarget _dataTarget;

        /// <inheritdoc />
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IEnumerable<IModuleInfo> EnumerateModules()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool ReadProcessMemory(ulong address, byte[] buffer, int bytesRequested, out int bytesRead)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Architecture Architecture { get; }

        /// <inheritdoc />
        public IList<IClrInfo> ClrVersions { get; }

        /// <inheritdoc />
        public IDataReader DataReader { get; }

        /// <inheritdoc />
        public bool IsMinidump { get; }

        /// <inheritdoc />
        public uint PointerSize { get; }

        /// <inheritdoc />
        public ISymbolLocator SymbolLocator { get; set; }

        /// <inheritdoc />
        public ISymbolProvider SymbolProvider { get; set; }
    }
}