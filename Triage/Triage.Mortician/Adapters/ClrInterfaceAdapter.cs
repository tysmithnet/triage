// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-19-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-19-2018
// ***********************************************************************
// <copyright file="ClrInterfaceAdapter.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using Microsoft.Diagnostics.Runtime;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    /// <summary>
    ///     Class ClrInterfaceAdapter.
    /// </summary>
    /// <seealso cref="Triage.Mortician.Core.ClrMdAbstractions.IClrInterface" />
    internal class ClrInterfaceAdapter : BaseAdapter, IClrInterface
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ClrInterfaceAdapter" /> class.
        /// </summary>
        /// <param name="interface">The interface.</param>
        /// <exception cref="ArgumentNullException">interface</exception>
        /// <inheritdoc />
        public ClrInterfaceAdapter(IConverter converter, ClrInterface @interface) : base(converter)
        {
            Interface = @interface ?? throw new ArgumentNullException(nameof(@interface));
        }

        /// <summary>
        ///     The interface
        /// </summary>
        internal ClrInterface Interface;

        public override void Setup()
        {
            BaseInterface = Converter.Convert(Interface.BaseInterface);
        }

        /// <summary>
        ///     The interface that this interface inherits from.
        /// </summary>
        /// <value>The base interface.</value>
        /// <inheritdoc />
        public IClrInterface BaseInterface { get; internal set; }

        /// <summary>
        ///     The typename of the interface.
        /// </summary>
        /// <value>The name.</value>
        /// <inheritdoc />
        public string Name => Interface.Name;
    }
}