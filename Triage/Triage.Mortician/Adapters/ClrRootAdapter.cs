using System;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class ClrRootAdapter : IClrRoot
    {
        /// <inheritdoc />
        public ClrRootAdapter(Microsoft.Diagnostics.Runtime.ClrRoot root)
        {
            Root = root ?? throw new ArgumentNullException(nameof(root));
            AppDomain = Converter.Convert(root.AppDomain);
            Kind = Converter.Convert(root.Kind);
            StackFrame = Converter.Convert(root.StackFrame);
            Thread = Converter.Convert(root.Thread);
            Type = Converter.Convert(root.Type);
        }

        internal Microsoft.Diagnostics.Runtime.ClrRoot Root;

        /// <inheritdoc />
        public ulong Address => Root.Address;

        /// <inheritdoc />
        public IClrAppDomain AppDomain { get; }

        /// <inheritdoc />
        public bool IsInterior => Root.IsInterior;

        /// <inheritdoc />
        public bool IsPinned => Root.IsPinned;

        /// <inheritdoc />
        public bool IsPossibleFalsePositive => Root.IsPossibleFalsePositive;

        /// <inheritdoc />
        public GcRootKind Kind { get; }

        /// <inheritdoc />
        public string Name => Root.Name;

        /// <inheritdoc />
        public ulong Object => Root.Object;

        /// <inheritdoc />
        public IClrStackFrame StackFrame { get; }

        /// <inheritdoc />
        public IClrThread Thread { get; }

        /// <inheritdoc />
        public IClrType Type { get; }
    }
}