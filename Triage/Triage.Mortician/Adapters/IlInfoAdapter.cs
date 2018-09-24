// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-20-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-20-2018
// ***********************************************************************
// <copyright file="IlInfoAdapter.cs" company="">
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
    ///     Class IlInfoAdapter.
    /// </summary>
    /// <seealso cref="Triage.Mortician.Adapters.BaseAdapter" />
    /// <seealso cref="Triage.Mortician.Core.ClrMdAbstractions.IILInfo" />
    internal class IlInfoAdapter : BaseAdapter, IILInfo
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="IlInfoAdapter" /> class.
        /// </summary>
        /// <param name="converter">The converter.</param>
        /// <param name="info">The information.</param>
        /// <exception cref="ArgumentNullException">info</exception>
        /// <inheritdoc />
        public IlInfoAdapter(IConverter converter, ILInfo info) : base(converter)
        {
            Info = info ?? throw new ArgumentNullException(nameof(info));
        }

        /// <summary>
        ///     The information
        /// </summary>
        internal ILInfo Info;

        /// <summary>
        ///     Setups this instance.
        /// </summary>
        /// <inheritdoc />
        public override void Setup()
        {
        }

        /// <summary>
        ///     The address in memory of where the IL for a particular method is located.
        /// </summary>
        /// <value>The address.</value>
        /// <inheritdoc />
        public ulong Address => Info.Address;

        /// <summary>
        ///     The flags associated with the IL code.
        /// </summary>
        /// <value>The flags.</value>
        /// <inheritdoc />
        public uint Flags => Info.Flags;

        /// <summary>
        ///     The length (in bytes) of the IL method body.
        /// </summary>
        /// <value>The length.</value>
        /// <inheritdoc />
        public int Length => Info.Length;

        /// <summary>
        ///     The local variable signature token for this IL method.
        /// </summary>
        /// <value>The local variable signature token.</value>
        /// <inheritdoc />
        public uint LocalVarSignatureToken => Info.LocalVarSignatureToken;

        /// <summary>
        ///     The maximum IL stack size in this method.
        /// </summary>
        /// <value>The maximum stack.</value>
        /// <inheritdoc />
        public int MaxStack => Info.MaxStack;
    }
}