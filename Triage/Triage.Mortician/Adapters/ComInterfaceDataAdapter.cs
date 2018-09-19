using System;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    /// <summary>
    ///     Class ComInterfaceDataAdapter.
    /// </summary>
    /// <seealso cref="Triage.Mortician.Core.ClrMdAbstractions.IComInterfaceData" />
    internal class ComInterfaceDataAdapter : IComInterfaceData
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ComInterfaceDataAdapter" /> class.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <exception cref="ArgumentNullException">data</exception>
        /// <inheritdoc />
        public ComInterfaceDataAdapter(Microsoft.Diagnostics.Runtime.ComInterfaceData data)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
            Type = Converter.Convert(data.Type);
        }

        /// <summary>
        ///     The data
        /// </summary>
        internal Microsoft.Diagnostics.Runtime.ComInterfaceData Data;

        /// <summary>
        ///     The interface pointer of Type.
        /// </summary>
        /// <value>The interface pointer.</value>
        /// <inheritdoc />
        public ulong InterfacePointer => Data.InterfacePointer;

        /// <summary>
        ///     The CLR type this represents.
        /// </summary>
        /// <value>The type.</value>
        /// <inheritdoc />
        public IClrType Type { get; }
    }
}