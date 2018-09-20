// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-19-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-19-2018
// ***********************************************************************
// <copyright file="BlockingObjectAdapter.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Diagnostics.Runtime;
using Triage.Mortician.Core.ClrMdAbstractions;
using BlockingReason = Triage.Mortician.Core.ClrMdAbstractions.BlockingReason;

namespace Triage.Mortician.Adapters
{
    /// <summary>
    ///     Class BlockingObjectAdapter.
    /// </summary>
    /// <seealso cref="Triage.Mortician.Core.ClrMdAbstractions.IBlockingObject" />
    internal class BlockingObjectAdapter : BaseAdapter, IBlockingObject
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BlockingObjectAdapter" /> class.
        /// </summary>
        /// <param name="blockingObject">The blocking object.</param>
        /// <exception cref="ArgumentNullException">blockingObject</exception>
        /// <inheritdoc />
        public BlockingObjectAdapter(IConverter converter, BlockingObject blockingObject) : base(converter)
        {
            BlockingObject = blockingObject ?? throw new ArgumentNullException(nameof(blockingObject));   
        }

        /// <summary>
        ///     The blocking object
        /// </summary>
        internal BlockingObject BlockingObject;

        /// <summary>
        ///     Returns true if this lock has only one owner.  Returns false if this lock
        ///     may have multiple owners (for example, readers on a RW lock).
        /// </summary>
        /// <value><c>true</c> if this instance has single owner; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool HasSingleOwner => BlockingObject.HasSingleOwner;

        /// <summary>
        ///     The object associated with the lock.
        /// </summary>
        /// <value>The object.</value>
        /// <inheritdoc />
        public ulong Object => BlockingObject.Object;

        /// <summary>
        ///     The thread which currently owns the lock.  This is only valid if Taken is true and
        ///     only valid if HasSingleOwner is true.
        /// </summary>
        /// <value>The owner.</value>
        /// <inheritdoc />
        public IClrThread Owner { get; internal set; }

        /// <summary>
        ///     Returns the list of owners for this object.
        /// </summary>
        /// <value>The owners.</value>
        /// <inheritdoc />
        public IList<IClrThread> Owners { get; internal set; }

        /// <summary>
        ///     The reason why it's blocking.
        /// </summary>
        /// <value>The reason.</value>
        /// <inheritdoc />
        public BlockingReason Reason { get; internal set; }

        /// <summary>
        ///     The recursion count of the lock (only valid if Locked is true).
        /// </summary>
        /// <value>The recursion count.</value>
        /// <inheritdoc />
        public int RecursionCount => BlockingObject.RecursionCount;

        /// <summary>
        ///     Whether or not the object is currently locked.
        /// </summary>
        /// <value><c>true</c> if taken; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool Taken => BlockingObject.Taken;

        /// <summary>
        ///     Returns the list of threads waiting on this object.
        /// </summary>
        /// <value>The waiters.</value>
        /// <inheritdoc />
        public IList<IClrThread> Waiters { get; internal set; }

        /// <inheritdoc />
        public override void Setup()
        {
            Owner = Converter.Convert(BlockingObject.Owner);
            Owners = BlockingObject.Owners.Select(Converter.Convert).ToList();
            Reason = Converter.Convert(BlockingObject.Reason);
            Waiters = BlockingObject.Waiters.Select(Converter.Convert).ToList();
        }
    }
}