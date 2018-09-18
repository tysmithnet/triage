using System.Collections.Generic;

namespace Triage.Mortician.Core
{
    public interface IBlockingObject
    {
        /// <summary>
        /// The object associated with the lock.
        /// </summary>
        ulong Object { get; }

        /// <summary>
        /// Whether or not the object is currently locked.
        /// </summary>
        bool Taken { get; }

        /// <summary>
        /// The recursion count of the lock (only valid if Locked is true).
        /// </summary>
        int RecursionCount { get; }

        /// <summary>
        /// The thread which currently owns the lock.  This is only valid if Taken is true and
        /// only valid if HasSingleOwner is true.
        /// </summary>
        IClrThread Owner { get; }

        /// <summary>
        /// Returns true if this lock has only one owner.  Returns false if this lock
        /// may have multiple owners (for example, readers on a RW lock).
        /// </summary>
        bool HasSingleOwner { get; }

        /// <summary>
        /// Returns the list of owners for this object.
        /// </summary>
        IList<IClrThread> Owners { get; }

        /// <summary>
        /// Returns the list of threads waiting on this object.
        /// </summary>
        IList<IClrThread> Waiters { get; }

        /// <summary>
        /// The reason why it's blocking.
        /// </summary>
        BlockingReason Reason { get; }
    }
}