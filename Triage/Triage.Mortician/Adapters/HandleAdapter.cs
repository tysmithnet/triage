using System;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class HandleAdapter : IClrHandle
    {
        internal Microsoft.Diagnostics.Runtime.ClrHandle Handle;

        /// <inheritdoc />
        public HandleAdapter(Microsoft.Diagnostics.Runtime.ClrHandle handle)
        {
            Handle = handle ?? throw new ArgumentNullException(nameof(handle));
        }

        /// <inheritdoc />
        public ulong Address { get; set; }

        /// <inheritdoc />
        public IClrAppDomain AppDomain { get; set; }

        /// <inheritdoc />
        public ulong DependentTarget { get; set; }

        /// <inheritdoc />
        public IClrType DependentType { get; set; }

        /// <inheritdoc />
        public HandleType HandleType { get; set; }

        /// <inheritdoc />
        public bool IsPinned { get; }

        /// <inheritdoc />
        public bool IsStrong { get; }

        /// <inheritdoc />
        public ulong Object { get; set; }

        /// <inheritdoc />
        public uint RefCount { get; set; }

        /// <inheritdoc />
        public IClrType Type { get; set; }
    }
}