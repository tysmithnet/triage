﻿// ***********************************************************************
// Assembly         : Mortician
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
using System.Linq;
using Microsoft.Diagnostics.Runtime;
using Mortician.Core.ClrMdAbstractions;

namespace Mortician.Adapters
{
    /// <summary>
    ///     Class RcwDataAdapter.
    /// </summary>
    /// <seealso cref="Mortician.Core.ClrMdAbstractions.IRcwData" />
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
            CreatorThread = RcwData.CreatorThread;
            Disconnected = RcwData.Disconnected;
            IUnknown = RcwData.IUnknown;
            Object = RcwData.Object;
            RefCount = RcwData.RefCount;
            VTablePointer = RcwData.VTablePointer;
            WinRTObject = RcwData.WinRTObject;
        }

        /// <summary>
        ///     The RCW data
        /// </summary>
        internal RcwData RcwData;

        public override void Setup()
        {
            Interfaces = RcwData.Interfaces.Select(Converter.Convert).ToList();
        }

        /// <summary>
        ///     Returns the thread which created this RCW.
        /// </summary>
        /// <value>The creator thread.</value>
        /// <inheritdoc />
        public uint CreatorThread { get; internal set; }

        /// <summary>
        ///     Returns true if the RCW is disconnected from the underlying COM type.
        /// </summary>
        /// <value><c>true</c> if disconnected; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool Disconnected { get; internal set; }

        /// <summary>
        ///     Returns the list of interfaces this RCW implements.
        /// </summary>
        /// <value>The interfaces.</value>
        /// <inheritdoc />
        public IList<IComInterfaceData> Interfaces { get; internal set; }

        /// <summary>
        ///     Returns the pointer to the IUnknown representing this CCW.
        /// </summary>
        /// <value>The i unknown.</value>
        /// <inheritdoc />
        public ulong IUnknown { get; internal set; }

        /// <summary>
        ///     Returns the managed object associated with this of RCW.
        /// </summary>
        /// <value>The object.</value>
        /// <inheritdoc />
        public ulong Object { get; internal set; }

        /// <summary>
        ///     Returns the RefCount of the RCW.
        /// </summary>
        /// <value>The reference count.</value>
        /// <inheritdoc />
        public int RefCount { get; internal set; }

        /// <summary>
        ///     Returns the external VTable associated with this RCW.  (It's useful to resolve the VTable as a symbol
        ///     which will tell you what the underlying native type is...if you have the symbols for it loaded).
        /// </summary>
        /// <value>The v table pointer.</value>
        /// <inheritdoc />
        public ulong VTablePointer { get; internal set; }

        /// <summary>
        ///     Returns the internal WinRT object associated with this RCW (if one exists).
        /// </summary>
        /// <value>The win rt object.</value>
        /// <inheritdoc />
        public ulong WinRTObject { get; internal set; }
    }
}