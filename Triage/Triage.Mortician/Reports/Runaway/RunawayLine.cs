// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-26-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-26-2018
// ***********************************************************************
// <copyright file="RunawayLine.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace Triage.Mortician.Reports.Runaway
{
    /// <summary>
    ///     Class RunawayLine.
    /// </summary>
    public class RunawayLine
    {
        /// <summary>
        ///     Gets the kernel mode time.
        /// </summary>
        /// <value>The kernel mode time.</value>
        public TimeSpan KernelModeTime { get; internal set; }

        /// <summary>
        ///     Gets the thread identifier.
        /// </summary>
        /// <value>The thread identifier.</value>
        public uint ThreadId { get; internal set; }

        /// <summary>
        ///     Gets the index of the thread.
        /// </summary>
        /// <value>The index of the thread.</value>
        public uint ThreadIndex { get; internal set; }

        /// <summary>
        ///     Gets the total time.
        /// </summary>
        /// <value>The total time.</value>
        public TimeSpan TotalTime => UserModeTime + KernelModeTime;

        /// <summary>
        ///     Gets the user mode time.
        /// </summary>
        /// <value>The user mode time.</value>
        public TimeSpan UserModeTime { get; internal set; }
    }
}