// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-19-2018
// ***********************************************************************
// <copyright file="RcwDataAdapter.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Diagnostics.Runtime;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    /// <summary>
    ///     Class RcwDataAdapter.
    /// </summary>
    /// <seealso cref="Triage.Mortician.Core.ClrMdAbstractions.IRcwData" />
    internal class RcwDataAdapter : BaseAdapter, IRcwData
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RcwDataAdapter" /> class.
        /// </summary>
        /// <param name="rcwData">The RCW data.</param>
        /// <exception cref="ArgumentNullException">rcwData</exception>
        /// <inheritdoc />
        public RcwDataAdapter(IConverter converter, RcwData rcwData) : base(converter)
        {
            RcwData = rcwData ?? throw new ArgumentNullException(nameof(rcwData));
            Interfaces = rcwData.Interfaces.Select(Converter.Convert).ToList();
        }

        /// <summary>
        ///     The RCW data
        /// </summary>
        internal RcwData RcwData;

        /// <summary>
        ///     Returns the thread which created this RCW.
        /// </summary>
        /// <value>The creator thread.</value>
        /// <inheritdoc />
        public uint CreatorThread => RcwData.CreatorThread;

        /// <summary>
        ///     Returns true if the RCW is disconnected from the underlying COM type.
        /// </summary>
        /// <value><c>true</c> if disconnected; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool Disconnected => RcwData.Disconnected;

        /// <summary>
        ///     Returns the list of interfaces this RCW implements.
        /// </summary>
        /// <value>The interfaces.</value>
        /// <inheritdoc />
        public IList<IComInterfaceData> Interfaces { get; }

        /// <summary>
        ///     Returns the pointer to the IUnknown representing this CCW.
        /// </summary>
        /// <value>The i unknown.</value>
        /// <inheritdoc />
        public ulong IUnknown => RcwData.IUnknown;

        /// <summary>
        ///     Returns the managed object associated with this of RCW.
        /// </summary>
        /// <value>The object.</value>
        /// <inheritdoc />
        public ulong Object => RcwData.Object;

        /// <summary>
        ///     Returns the RefCount of the RCW.
        /// </summary>
        /// <value>The reference count.</value>
        /// <inheritdoc />
        public int RefCount => RcwData.RefCount;

        /// <summary>
        ///     Returns the external VTable associated with this RCW.  (It's useful to resolve the VTable as a symbol
        ///     which will tell you what the underlying native type is...if you have the symbols for it loaded).
        /// </summary>
        /// <value>The v table pointer.</value>
        /// <inheritdoc />
        public ulong VTablePointer => RcwData.VTablePointer;

        /// <summary>
        ///     Returns the internal WinRT object associated with this RCW (if one exists).
        /// </summary>
        /// <value>The win rt object.</value>
        /// <inheritdoc />
        public ulong WinRTObject => RcwData.WinRTObject;

        /// <summary>
        ///     Gets or sets the converter.
        /// </summary>
        /// <value>The converter.</value>
        [Import]
        internal IConverter Converter { get; set; }
    }
}