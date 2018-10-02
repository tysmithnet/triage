// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="HotColdRegionsAdapter.cs" company="">
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
    ///     Class HotColdRegionsAdapter.
    /// </summary>
    /// <seealso cref="Triage.Mortician.Core.ClrMdAbstractions.IHotColdRegions" />
    internal class HotColdRegionsAdapter : BaseAdapter, IHotColdRegions
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="HotColdRegionsAdapter" /> class.
        /// </summary>
        /// <param name="hotColdRegions">The hot cold regions.</param>
        /// <exception cref="ArgumentNullException">hotColdRegions</exception>
        /// <inheritdoc />
        public HotColdRegionsAdapter(IConverter converter, HotColdRegions hotColdRegions) : base(converter)
        {
            HotColdRegions = hotColdRegions ?? throw new ArgumentNullException(nameof(hotColdRegions));
            ColdSize = HotColdRegions.ColdSize;
            ColdStart = HotColdRegions.ColdStart;
            HotSize = HotColdRegions.HotSize;
            HotStart = HotColdRegions.HotStart;
        }

        /// <summary>
        ///     The hot cold regions
        /// </summary>
        internal HotColdRegions HotColdRegions;

        public override void Setup()
        {
        }

        /// <summary>
        ///     Returns the size of the cold region.
        /// </summary>
        /// <value>The size of the cold.</value>
        /// <inheritdoc />
        public uint ColdSize { get; internal set; }

        /// <summary>
        ///     Returns the start address of the method's cold region.
        /// </summary>
        /// <value>The cold start.</value>
        /// <inheritdoc />
        public ulong ColdStart { get; internal set; }

        /// <summary>
        ///     Returns the size of the hot region.
        /// </summary>
        /// <value>The size of the hot.</value>
        /// <inheritdoc />
        public uint HotSize { get; internal set; }

        /// <summary>
        ///     Returns the start address of the method's hot region.
        /// </summary>
        /// <value>The hot start.</value>
        /// <inheritdoc />
        public ulong HotStart { get; internal set; }
    }
}