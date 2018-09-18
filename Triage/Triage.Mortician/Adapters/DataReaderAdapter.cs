using System;
using System.Collections.Generic;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class DataReaderAdapter : IDataReader
    {
        /// <inheritdoc />
        public DataReaderAdapter(Microsoft.Diagnostics.Runtime.IDataReader dataReader)
        {
            _dataReader = dataReader ?? throw new ArgumentNullException(nameof(dataReader));
        }

        internal Microsoft.Diagnostics.Runtime.IDataReader _dataReader;

        /// <inheritdoc />
        public void Close()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IEnumerable<uint> EnumerateAllThreads()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IList<IModuleInfo> EnumerateModules()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void Flush()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Architecture GetArchitecture()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public uint GetPointerSize()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool GetThreadContext(uint threadID, uint contextFlags, uint contextSize, IntPtr context)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool GetThreadContext(uint threadID, uint contextFlags, uint contextSize, byte[] context)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ulong GetThreadTeb(uint thread)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void GetVersionInfo(ulong baseAddress, out VersionInfo version)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public uint ReadDwordUnsafe(ulong addr)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool ReadMemory(ulong address, byte[] buffer, int bytesRequested, out int bytesRead)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool ReadMemory(ulong address, IntPtr buffer, int bytesRequested, out int bytesRead)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ulong ReadPointerUnsafe(ulong addr)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool VirtualQuery(ulong addr, out VirtualQueryData vq)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool IsMinidump { get; }
    }
}