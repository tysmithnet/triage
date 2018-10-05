// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-24-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-24-2018
// ***********************************************************************
// <copyright file="ComInterfaceData.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using Triage.Mortician.Core.ClrMdAbstractions;
using ClrMd = Microsoft.Diagnostics.Runtime;

namespace Triage.Mortician
{
    /// <summary>
    ///     Class ComInterfaceData.
    /// </summary>
    /// <seealso cref="Triage.Mortician.Core.ClrMdAbstractions.IComInterfaceData" />
    internal class ComInterfaceData : IComInterfaceData
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ComInterfaceData" /> class.
        /// </summary>
        /// <param name="comInterfaceData">The COM interface data.</param>
        /// <exception cref="System.ArgumentNullException">comInterfaceData</exception>
        /// <inheritdoc />
        public ComInterfaceData(ClrMd.ComInterfaceData comInterfaceData)
        {
            _comInterfaceData = comInterfaceData ?? throw new ArgumentNullException(nameof(comInterfaceData));
        }

        /// <summary>
        ///     The COM interface data
        /// </summary>
        internal ClrMd.ComInterfaceData _comInterfaceData;

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