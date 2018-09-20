// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-19-2018
// ***********************************************************************
// <copyright file="CcwDataAdapter.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Diagnostics.Runtime;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    /// <summary>
    ///     Class CcwDataAdapter.
    /// </summary>
    /// <seealso cref="Triage.Mortician.Core.ClrMdAbstractions.ICcwData" />
    internal class CcwDataAdapter : BaseAdapter, ICcwData
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CcwDataAdapter" /> class.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <exception cref="ArgumentNullException">data</exception>
        /// <inheritdoc />
        public CcwDataAdapter(IConverter converter, CcwData data) : base(converter)
        {
            CcwData = data ?? throw new ArgumentNullException(nameof(data));
        }

        /// <summary>
        ///     The CCW data
        /// </summary>
        internal CcwData CcwData;

        /// <inheritdoc />
        public override void Setup()
        {
            Interfaces = CcwData.Interfaces.Select(Converter.Convert).ToList();
        }

        /// <summary>
        ///     Returns the CLR handle associated with this CCW.
        /// </summary>
        /// <value>The handle.</value>
        /// <inheritdoc />
        public ulong Handle => CcwData.Handle;

        /// <summary>
        ///     Returns the interfaces that this CCW implements.
        /// </summary>
        /// <value>The interfaces.</value>
        /// <inheritdoc />
        public IList<IComInterfaceData> Interfaces { get; internal set; }

        /// <summary>
        ///     Returns the pointer to the IUnknown representing this CCW.
        /// </summary>
        /// <value>The i unknown.</value>
        /// <inheritdoc />
        public ulong IUnknown => CcwData.IUnknown;

        /// <summary>
        ///     Returns the pointer to the managed object representing this CCW.
        /// </summary>
        /// <value>The object.</value>
        /// <inheritdoc />
        public ulong Object => CcwData.Object;

        /// <summary>
        ///     Returns the refcount of this CCW.
        /// </summary>
        /// <value>The reference count.</value>
        /// <inheritdoc />
        public int RefCount => CcwData.RefCount;
    }
}