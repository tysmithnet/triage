using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class DataReaderAdapter : IDataReader
    {
        /// <inheritdoc />
        public DataReaderAdapter(Microsoft.Diagnostics.Runtime.IDataReader dataReader)
        {
            DataReader = dataReader ?? throw new ArgumentNullException(nameof(dataReader));
        }

        internal Microsoft.Diagnostics.Runtime.IDataReader DataReader;

        /// <inheritdoc />
        public void Close() => DataReader.Close();

        /// <inheritdoc />
        public IEnumerable<uint> EnumerateAllThreads() => DataReader.EnumerateAllThreads();

        /// <inheritdoc />
        public IList<IModuleInfo> EnumerateModules() => DataReader.EnumerateModules().Select(Converter.Convert).ToList();

        /// <inheritdoc />
        public void Flush() => DataReader.Flush();

        /// <inheritdoc />
        public Architecture GetArchitecture() => Converter.Convert(DataReader.GetArchitecture());

        /// <inheritdoc />
        public uint GetPointerSize() => DataReader.GetPointerSize();

        /// <inheritdoc />
        public bool GetThreadContext(uint threadID, uint contextFlags, uint contextSize, IntPtr context) =>
            DataReader.GetThreadContext(threadID, contextFlags, contextSize, context);

        /// <inheritdoc />
        public bool GetThreadContext(uint threadID, uint contextFlags, uint contextSize, byte[] context) =>
            DataReader.GetThreadContext(threadID, contextFlags, contextSize, context);

        /// <inheritdoc />
        public ulong GetThreadTeb(uint thread) => GetThreadTeb(thread);

        /// <inheritdoc />
        public void GetVersionInfo(ulong baseAddress, out VersionInfo version) => GetVersionInfo(baseAddress, out version);

        /// <inheritdoc />
        public uint ReadDwordUnsafe(ulong addr) => ReadDwordUnsafe(addr);

        /// <inheritdoc />
        public bool ReadMemory(ulong address, byte[] buffer, int bytesRequested, out int bytesRead) => DataReader.ReadMemory(address, buffer, bytesRequested, out bytesRead);

        /// <inheritdoc />
        public bool ReadMemory(ulong address, IntPtr buffer, int bytesRequested, out int bytesRead) => DataReader.ReadMemory(address, buffer, bytesRequested, out bytesRead);

        /// <inheritdoc />
        public ulong ReadPointerUnsafe(ulong addr) => DataReader.ReadPointerUnsafe(addr);

        /// <inheritdoc />
        public bool VirtualQuery(ulong addr, out VirtualQueryData virtualQuery)
        {
            var res = DataReader.VirtualQuery(addr, out var outVar);
            virtualQuery = Converter.Convert(outVar);
            return res;
        }
        [Import]
        internal IConverter Converter { get; set; }
        /// <inheritdoc />
        public bool IsMinidump => DataReader.IsMinidump;
    }
}