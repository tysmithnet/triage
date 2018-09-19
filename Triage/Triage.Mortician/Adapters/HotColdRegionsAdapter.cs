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
    internal class HotColdRegionsAdapter : IHotColdRegions
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="HotColdRegionsAdapter" /> class.
        /// </summary>
        /// <param name="hotColdRegions">The hot cold regions.</param>
        /// <exception cref="ArgumentNullException">hotColdRegions</exception>
        /// <inheritdoc />
        public HotColdRegionsAdapter(HotColdRegions hotColdRegions)
        {
            HotColdRegions = hotColdRegions ?? throw new ArgumentNullException(nameof(hotColdRegions));
        }

        /// <summary>
        ///     The hot cold regions
        /// </summary>
        internal HotColdRegions HotColdRegions;

        /// <summary>
        ///     Returns the size of the cold region.
        /// </summary>
        /// <value>The size of the cold.</value>
        /// <inheritdoc />
        public uint ColdSize => HotColdRegions.ColdSize;

        /// <summary>
        ///     Returns the start address of the method's cold region.
        /// </summary>
        /// <value>The cold start.</value>
        /// <inheritdoc />
        public ulong ColdStart => HotColdRegions.ColdStart;

        /// <summary>
        ///     Returns the size of the hot region.
        /// </summary>
        /// <value>The size of the hot.</value>
        /// <inheritdoc />
        public uint HotSize => HotColdRegions.HotSize;

        /// <summary>
        ///     Returns the start address of the method's hot region.
        /// </summary>
        /// <value>The hot start.</value>
        /// <inheritdoc />
        public ulong HotStart => HotColdRegions.HotStart;
    }
}