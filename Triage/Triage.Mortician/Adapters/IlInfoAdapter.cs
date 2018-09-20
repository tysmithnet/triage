using System;
using Microsoft.Diagnostics.Runtime;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class IlInfoAdapter : BaseAdapter, IILInfo
    {
        /// <inheritdoc />
        public IlInfoAdapter(IConverter converter, ILInfo info) : base(converter)
        {
            Info = info ?? throw new ArgumentNullException(nameof(info));
        }

        internal ILInfo Info;

        /// <inheritdoc />
        public override void Setup()
        {
        }

        /// <inheritdoc />
        public ulong Address => Info.Address;

        /// <inheritdoc />
        public uint Flags => Info.Flags;

        /// <inheritdoc />
        public int Length => Info.Length;

        /// <inheritdoc />
        public uint LocalVarSignatureToken => Info.LocalVarSignatureToken;

        /// <inheritdoc />
        public int MaxStack => Info.MaxStack;
    }
}