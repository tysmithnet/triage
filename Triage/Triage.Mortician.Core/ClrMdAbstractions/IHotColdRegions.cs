// ***********************************************************************
// Assembly         : Triage.Mortician.Core
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="IHotColdRegions.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Triage.Mortician.Core.ClrMdAbstractions
{
    /// <summary>
    /// Interface IHotColdRegions
    /// </summary>
    public interface IHotColdRegions
    {
        /// <summary>
        /// Returns the start address of the method's hot region.
        /// </summary>
        /// <value>The hot start.</value>
        ulong HotStart { get; }

        /// <summary>
        /// Returns the size of the hot region.
        /// </summary>
        /// <value>The size of the hot.</value>
        uint HotSize { get; }

        /// <summary>
        /// Returns the start address of the method's cold region.
        /// </summary>
        /// <value>The cold start.</value>
        ulong ColdStart { get; }

        /// <summary>
        /// Returns the size of the cold region.
        /// </summary>
        /// <value>The size of the cold.</value>
        uint ColdSize { get; }
    }
}