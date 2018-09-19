using System;
using System.ComponentModel.Composition;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class HandleAdapter : IClrHandle
    {
        [Import]
        internal IConverter Converter { get; set; }
        internal Microsoft.Diagnostics.Runtime.ClrHandle Handle;

        /// <inheritdoc />
        public HandleAdapter(Microsoft.Diagnostics.Runtime.ClrHandle handle)
        {
            Handle = handle ?? throw new ArgumentNullException(nameof(handle));
            AppDomain = Converter.Convert(handle.AppDomain);
            DependentType = Converter.Convert(handle.DependentType);
            HandleType = Converter.Convert(handle.HandleType);
            Type = Converter.Convert(handle.Type);
        }

        /// <inheritdoc />
        public ulong Address => Handle.Address;

        /// <inheritdoc />
        public IClrAppDomain AppDomain { get; set; }

        /// <inheritdoc />
        public ulong DependentTarget => Handle.DependentTarget;

        /// <inheritdoc />
        public IClrType DependentType { get; set; }

        /// <inheritdoc />
        public HandleType HandleType { get; set; }

        /// <inheritdoc />
        public bool IsPinned => Handle.IsPinned;

        /// <inheritdoc />
        public bool IsStrong => Handle.IsStrong;

        /// <inheritdoc />
        public ulong Object => Handle.Object;

        /// <inheritdoc />
        public uint RefCount => Handle.RefCount;

        /// <inheritdoc />
        public IClrType Type { get; set; }
    }
}