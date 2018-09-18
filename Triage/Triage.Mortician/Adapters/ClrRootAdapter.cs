using System;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class ClrRootAdapter : IClrRoot
    {
        /// <inheritdoc />
        public ClrRootAdapter(Microsoft.Diagnostics.Runtime.ClrRoot root)
        {
            _root = root ?? throw new ArgumentNullException(nameof(root));
        }

        internal Microsoft.Diagnostics.Runtime.ClrRoot _root;

        /// <inheritdoc />
        public ulong Address { get; }

        /// <inheritdoc />
        public IClrAppDomain AppDomain { get; }

        /// <inheritdoc />
        public bool IsInterior { get; }

        /// <inheritdoc />
        public bool IsPinned { get; }

        /// <inheritdoc />
        public bool IsPossibleFalsePositive { get; }

        /// <inheritdoc />
        public GcRootKind Kind { get; }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public ulong Object { get; }

        /// <inheritdoc />
        public IClrStackFrame StackFrame { get; }

        /// <inheritdoc />
        public IClrThread Thread { get; }

        /// <inheritdoc />
        public IClrType Type { get; }
    }
}