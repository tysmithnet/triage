using System;
using Triage.Mortician.Adapters;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class IlInfoAdapter : BaseAdapter, IILInfo
    {
        /// <inheritdoc />
        public IlInfoAdapter(IConverter converter, Microsoft.Diagnostics.Runtime.ILInfo info) : base(converter)
        {
            Info = info ?? throw new ArgumentNullException(nameof(info));
        }

        internal Microsoft.Diagnostics.Runtime.ILInfo Info;

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

        /// <inheritdoc />
        public override void Setup()
        {
            
        }
    }
}