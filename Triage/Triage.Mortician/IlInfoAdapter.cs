using System;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician
{
    internal class IlInfoAdapter : IILInfo
    {
        /// <inheritdoc />
        public IlInfoAdapter(Microsoft.Diagnostics.Runtime.ILInfo info)
        {
            _info = info ?? throw new ArgumentNullException(nameof(info));
        }

        internal Microsoft.Diagnostics.Runtime.ILInfo _info;

        /// <inheritdoc />
        public ulong Address { get; }

        /// <inheritdoc />
        public uint Flags { get; }

        /// <inheritdoc />
        public int Length { get; }

        /// <inheritdoc />
        public uint LocalVarSignatureToken { get; }

        /// <inheritdoc />
        public int MaxStack { get; }
    }
}