// ***********************************************************************
// Assembly         : Triage.Mortician.Core
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="IBlockingObject.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;

namespace Triage.Mortician.Core.ClrMdAbstractions
{
    /// <summary>
    ///     Interface IBlockingObject
    /// </summary>
    public interface IBlockingObject
    {
        /// <summary>
        ///     Returns true if this lock has only one owner.  Returns false if this lock
        ///     may have multiple owners (for example, readers on a RW lock).
        /// </summary>
        /// <value><c>true</c> if this instance has single owner; otherwise, <c>false</c>.</value>
        bool HasSingleOwner { get; }

        /// <summary>
        ///     The object associated with the lock.
        /// </summary>
        /// <value>The object.</value>
        ulong Object { get; }

        /// <summary>
        ///     The thread which currently owns the lock.  This is only valid if Taken is true and
        ///     only valid if HasSingleOwner is true.
        /// </summary>
        /// <value>The owner.</value>
        IClrThread Owner { get; }

        /// <summary>
        ///     Returns the list of owners for this object.
        /// </summary>
        /// <value>The owners.</value>
        IList<IClrThread> Owners { get; }

        /// <summary>
        ///     The reason why it's blocking.
        /// </summary>
        /// <value>The reason.</value>
        BlockingReason Reason { get; }

        /// <summary>
        ///     The recursion count of the lock (only valid if Locked is true).
        /// </summary>
        /// <value>The recursion count.</value>
        int RecursionCount { get; }

        /// <summary>
        ///     Whether or not the object is currently locked.
        /// </summary>
        /// <value><c>true</c> if taken; otherwise, <c>false</c>.</value>
        bool Taken { get; }

        /// <summary>
        ///     Returns the list of threads waiting on this object.
        /// </summary>
        /// <value>The waiters.</value>
        IList<IClrThread> Waiters { get; }
    }
}