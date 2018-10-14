// ***********************************************************************
// Assembly         : Mortician
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
using Mortician.Core.ClrMdAbstractions;

namespace Mortician.Adapters
{
    /// <summary>
    ///     Class ComInterfaceDataAdapter.
    /// </summary>
    /// <seealso cref="Mortician.Core.ClrMdAbstractions.IComInterfaceData" />
    internal class ComInterfaceDataAdapter : BaseAdapter, IComInterfaceData
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ComInterfaceDataAdapter" /> class.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <exception cref="ArgumentNullException">data</exception>
        /// <inheritdoc />
        public ComInterfaceDataAdapter(IConverter converter, Microsoft.Diagnostics.Runtime.ComInterfaceData data) :
            base(converter)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
            InterfacePointer = Data.InterfacePointer;
        }

        /// <summary>
        ///     The data
        /// </summary>
        internal Microsoft.Diagnostics.Runtime.ComInterfaceData Data;

        public override void Setup()
        {
            Type = Converter.Convert(Data.Type);
        }

        /// <summary>
        ///     The interface pointer of Type.
        /// </summary>
        /// <value>The interface pointer.</value>
        /// <inheritdoc />
        public ulong InterfacePointer { get; internal set; }

        /// <summary>
        ///     The CLR type this represents.
        /// </summary>
        /// <value>The type.</value>
        /// <inheritdoc />
        public IClrType Type { get; internal set; }
    }
}