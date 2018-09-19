// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-19-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-19-2018
// ***********************************************************************
// <copyright file="ComInterfaceDataAdapter.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.ComponentModel.Composition;
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

        /// <summary>
        ///     Gets or sets the converter.
        /// </summary>
        /// <value>The converter.</value>
        [Import]
        internal IConverter Converter { get; set; }
    }
}