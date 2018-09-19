using System;
using System.Collections.Generic;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    /// <summary>
    ///     Class BlockingObjectAdapter.
    /// </summary>
    /// <seealso cref="Triage.Mortician.Core.ClrMdAbstractions.IBlockingObject" />
    internal class BlockingObjectAdapter : IBlockingObject
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BlockingObjectAdapter" /> class.
        /// </summary>
        /// <param name="blockingObject">The blocking object.</param>
        /// <exception cref="ArgumentNullException">blockingObject</exception>
        /// <inheritdoc />
        public BlockingObjectAdapter(Microsoft.Diagnostics.Runtime.BlockingObject blockingObject)
        {
            BlockingObject = blockingObject ?? throw new ArgumentNullException(nameof(blockingObject));
        }

        /// <summary>
        ///     The blocking object
        /// </summary>
        internal Microsoft.Diagnostics.Runtime.BlockingObject BlockingObject;

        /// <summary>
        ///     Returns true if this lock has only one owner.  Returns false if this lock
        ///     may have multiple owners (for example, readers on a RW lock).
        /// </summary>
        /// <value><c>true</c> if this instance has single owner; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool HasSingleOwner { get; }

        /// <summary>
        ///     The object associated with the lock.
        /// </summary>
        /// <value>The object.</value>
        /// <inheritdoc />
        public ulong Object { get; }

        /// <summary>
        ///     The thread which currently owns the lock.  This is only valid if Taken is true and
        ///     only valid if HasSingleOwner is true.
        /// </summary>
        /// <value>The owner.</value>
        /// <inheritdoc />
        public IClrThread Owner { get; }

        /// <summary>
        ///     Returns the list of owners for this object.
        /// </summary>
        /// <value>The owners.</value>
        /// <inheritdoc />
        public IList<IClrThread> Owners { get; }

        /// <summary>
        ///     The reason why it's blocking.
        /// </summary>
        /// <value>The reason.</value>
        /// <inheritdoc />
        public BlockingReason Reason { get; }

        /// <summary>
        ///     The recursion count of the lock (only valid if Locked is true).
        /// </summary>
        /// <value>The recursion count.</value>
        /// <inheritdoc />
        public int RecursionCount { get; }

        /// <summary>
        ///     Whether or not the object is currently locked.
        /// </summary>
        /// <value><c>true</c> if taken; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool Taken { get; }

        /// <summary>
        ///     Returns the list of threads waiting on this object.
        /// </summary>
        /// <value>The waiters.</value>
        /// <inheritdoc />
        public IList<IClrThread> Waiters { get; }
    }
}