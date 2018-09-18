// ***********************************************************************
// Assembly         : Triage.Mortician.Core
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="GcSegmentType.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Triage.Mortician.Core.ClrMdAbstractions
{
    /// <summary>
    ///     Types of GC segments.
    /// </summary>
    public enum GcSegmentType
    {
        /// <summary>
        ///     Ephemeral segments are the only segments to contain Gen0 and Gen1 objects.
        ///     It may also contain Gen2 objects, but not always.  Objects are only allocated
        ///     on the ephemeral segment.  There is one ephemeral segment per logical GC heap.
        ///     It is important to not have too many pinned objects in the ephemeral segment,
        ///     or you will run into a performance problem where the runtime runs too many GCs.
        /// </summary>
        Ephemeral,

        /// <summary>
        ///     Regular GC segments only contain Gen2 objects.
        /// </summary>
        Regular,

        /// <summary>
        ///     The large object heap contains objects greater than a certain threshold.  Large
        ///     object segments are never compacted.  Large objects are directly allocated
        ///     onto LargeObject segments, and all large objects are considered gen2.
        /// </summary>
        LargeObject
    }
}