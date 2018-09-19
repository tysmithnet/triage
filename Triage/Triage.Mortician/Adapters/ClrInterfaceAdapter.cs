using System;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    /// <summary>
    ///     Class ClrInterfaceAdapter.
    /// </summary>
    /// <seealso cref="Triage.Mortician.Core.ClrMdAbstractions.IClrInterface" />
    internal class ClrInterfaceAdapter : IClrInterface
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ClrInterfaceAdapter" /> class.
        /// </summary>
        /// <param name="interface">The interface.</param>
        /// <exception cref="ArgumentNullException">interface</exception>
        /// <inheritdoc />
        public ClrInterfaceAdapter(Microsoft.Diagnostics.Runtime.ClrInterface @interface)
        {
            Interface = @interface ?? throw new ArgumentNullException(nameof(@interface));
        }

        /// <summary>
        ///     The interface
        /// </summary>
        internal Microsoft.Diagnostics.Runtime.ClrInterface Interface;

        /// <summary>
        ///     The interface that this interface inherits from.
        /// </summary>
        /// <value>The base interface.</value>
        /// <inheritdoc />
        public IClrInterface BaseInterface { get; }

        /// <summary>
        ///     The typename of the interface.
        /// </summary>
        /// <value>The name.</value>
        /// <inheritdoc />
        public string Name { get; }
    }
}