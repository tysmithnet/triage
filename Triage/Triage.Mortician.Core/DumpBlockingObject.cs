// ***********************************************************************
// Assembly         : Triage.Mortician.Core
// Author           : @tysmithnet
// Created          : 09-26-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-01-2018
// ***********************************************************************
// <copyright file="DumpBlockingObject.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Core
{
    /// <summary>
    ///     Class DumpBlockingObject.
    /// </summary>
    public class DumpBlockingObject
    {
        /// <summary>
        ///     Gets or sets the address.
        /// </summary>
        /// <value>The address.</value>
        public ulong Address { get; set; }

        /// <summary>
        ///     Gets or sets the blocking reason.
        /// </summary>
        /// <value>The blocking reason.</value>
        public BlockingReason BlockingReason { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is locked.
        /// </summary>
        /// <value><c>true</c> if this instance is locked; otherwise, <c>false</c>.</value>
        public bool IsLocked { get; set; }

        /// <summary>
        ///     Gets or sets the owners.
        /// </summary>
        /// <value>The owners.</value>
        public IList<DumpThread> Owners { get; set; }

        /// <summary>
        ///     Gets or sets the recursion count.
        /// </summary>
        /// <value>The recursion count.</value>
        public int RecursionCount { get; set; }

        /// <summary>
        ///     Gets or sets the waiters.
        /// </summary>
        /// <value>The waiters.</value>
        public IList<DumpThread> Waiters { get; set; }
    }
}