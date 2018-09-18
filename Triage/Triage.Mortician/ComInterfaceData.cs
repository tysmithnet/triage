using System;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician
{
    internal class ComInterfaceData : IComInterfaceData
    {
        /// <inheritdoc />
        public ComInterfaceData(Microsoft.Diagnostics.Runtime.ComInterfaceData comInterfaceData)
        {
            _comInterfaceData = comInterfaceData ?? throw new ArgumentNullException(nameof(comInterfaceData));
        }

        internal Microsoft.Diagnostics.Runtime.ComInterfaceData _comInterfaceData;

        /// <inheritdoc />
        public ulong InterfacePointer { get; }

        /// <inheritdoc />
        public IClrType Type { get; }
    }
}