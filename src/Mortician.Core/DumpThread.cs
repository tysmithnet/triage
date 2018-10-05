// ***********************************************************************
// Assembly         : Mortician
// Author           : @tysmithnet
// Created          : 12-19-2017
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-02-2018
// ***********************************************************************
// <copyright file="DumpThread.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using Mortician.Core.ClrMdAbstractions;

namespace Mortician.Core
{
    /// <summary>
    /// Represents a thread that was extracted from the memory dump
    /// </summary>
    /// <seealso cref="System.IEquatable{Mortician.Core.DumpThread}" />
    /// <seealso cref="System.IComparable{Mortician.Core.DumpThread}" />
    /// <seealso cref="System.IComparable" />
    public class DumpThread : IEquatable<DumpThread>, IComparable<DumpThread>, IComparable
    {
        /// <summary>
        /// Backing field for the lazy loaded stack trace
        /// </summary>
        private string _stackTrace;

        /// <summary>
        /// Adds the blocking object.
        /// </summary>
        /// <param name="dumpBlockingObject">The dump blocking object.</param>
        public void AddBlockingObject(DumpBlockingObject dumpBlockingObject)
        {
            BlockingObjectsInternal.Add(dumpBlockingObject);
        }

        /// <summary>
        /// Adds the root.
        /// </summary>
        /// <param name="dumpObjectRoot">The dump object root.</param>
        public void AddRoot(DumpObjectRoot dumpObjectRoot)
        {
            RootsInternal.Add(dumpObjectRoot);
        }

        /// <summary>
        /// Compares to.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>System.Int32.</returns>
        /// <inheritdoc />
        public int CompareTo(DumpThread other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return OsId.CompareTo(other.OsId);
        }

        /// <summary>
        /// Compares to.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="ArgumentException">DumpThread</exception>
        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            if (!(obj is DumpThread)) throw new ArgumentException($"Object must be of type {nameof(DumpThread)}");
            return CompareTo((DumpThread) obj);
        }

        /// <summary>
        /// Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <inheritdoc />
        public bool Equals(DumpThread other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return OsId == other.OsId;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((DumpThread) obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        /// <inheritdoc />
        public override int GetHashCode() => (int) OsId;

        /// <summary>
        /// Implements the &gt; operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >(DumpThread left, DumpThread right) =>
            Comparer<DumpThread>.Default.Compare(left, right) > 0;

        /// <summary>
        /// Implements the &gt;= operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(DumpThread left, DumpThread right) =>
            Comparer<DumpThread>.Default.Compare(left, right) >= 0;

        /// <summary>
        /// Implements the &lt; operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(DumpThread left, DumpThread right) =>
            Comparer<DumpThread>.Default.Compare(left, right) < 0;

        /// <summary>
        /// Implements the &lt;= operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(DumpThread left, DumpThread right) =>
            Comparer<DumpThread>.Default.Compare(left, right) <= 0;

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>The address.</value>
        public ulong Address { get; set; }

        /// <summary>
        /// Gets or sets the application domain address.
        /// </summary>
        /// <value>The application domain address.</value>
        public ulong AppDomainAddress { get; set; }

        /// <summary>
        /// Gets or sets the blocking objects.
        /// </summary>
        /// <value>The blocking objects.</value>
        public ISet<DumpBlockingObject> BlockingObjectsInternal { get; set; } = new SortedSet<DumpBlockingObject>();

        /// <summary>
        /// Gets the blocking objects.
        /// </summary>
        /// <value>The blocking objects.</value>
        public IEnumerable<DumpBlockingObject> BlockingObjects => BlockingObjectsInternal;

        /// <summary>
        /// Gets or sets the current exception.
        /// </summary>
        /// <value>The current exception.</value>
        public DumpObject CurrentException { get; set; }

        /// <summary>
        /// Gets or sets the current frame of the thread
        /// </summary>
        /// <value>The current frame.</value>
        public CodeLocation CurrentFrame { get; protected internal set; }

        /// <summary>
        /// Gets or sets the index of the thread in the debugger. This is a low integer value used by the debugging interface
        /// to make thread references easier
        /// </summary>
        /// <value>The index of the thread in the debugger.</value>
        public uint DebuggerIndex { get; protected internal set; }

        /// <summary>
        /// Gets or sets the gc mode.
        /// </summary>
        /// <value>The gc mode.</value>
        public GcMode GcMode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is aborted.
        /// </summary>
        /// <value><c>true</c> if this instance is aborted; otherwise, <c>false</c>.</value>
        public bool IsAborted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is abort requested.
        /// </summary>
        /// <value><c>true</c> if this instance is abort requested; otherwise, <c>false</c>.</value>
        public bool IsAbortRequested { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is alive.
        /// </summary>
        /// <value><c>true</c> if this instance is alive; otherwise, <c>false</c>.</value>
        public bool IsAlive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is background.
        /// </summary>
        /// <value><c>true</c> if this instance is background; otherwise, <c>false</c>.</value>
        public bool IsBackground { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is coinitialized.
        /// </summary>
        /// <value><c>true</c> if this instance is coinitialized; otherwise, <c>false</c>.</value>
        public bool IsCoinitialized { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is created but not started.
        /// </summary>
        /// <value><c>true</c> if this instance is created but not started; otherwise, <c>false</c>.</value>
        public bool IsCreatedButNotStarted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is debugger helper.
        /// </summary>
        /// <value><c>true</c> if this instance is debugger helper; otherwise, <c>false</c>.</value>
        public bool IsDebuggerHelper { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is debug suspended.
        /// </summary>
        /// <value><c>true</c> if this instance is debug suspended; otherwise, <c>false</c>.</value>
        public bool IsDebugSuspended { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is finalizer.
        /// </summary>
        /// <value><c>true</c> if this instance is finalizer; otherwise, <c>false</c>.</value>
        public bool IsFinalizer { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is gc.
        /// </summary>
        /// <value><c>true</c> if this instance is gc; otherwise, <c>false</c>.</value>
        public bool IsGc { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is gc suspend pending.
        /// </summary>
        /// <value><c>true</c> if this instance is gc suspend pending; otherwise, <c>false</c>.</value>
        public bool IsGcSuspendPending { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is gc thread.
        /// </summary>
        /// <value><c>true</c> if this instance is gc thread; otherwise, <c>false</c>.</value>
        public bool IsGcThread { get; internal set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is MTA.
        /// </summary>
        /// <value><c>true</c> if this instance is MTA; otherwise, <c>false</c>.</value>
        public bool IsMta { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is shutdown helper.
        /// </summary>
        /// <value><c>true</c> if this instance is shutdown helper; otherwise, <c>false</c>.</value>
        public bool IsShutdownHelper { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is sta.
        /// </summary>
        /// <value><c>true</c> if this instance is sta; otherwise, <c>false</c>.</value>
        public bool IsSta { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is suspending ee.
        /// </summary>
        /// <value><c>true</c> if this instance is suspending ee; otherwise, <c>false</c>.</value>
        public bool IsSuspendingEe { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is threadpool completion port.
        /// </summary>
        /// <value><c>true</c> if this instance is threadpool completion port; otherwise, <c>false</c>.</value>
        public bool IsThreadpoolCompletionPort { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is threadpool gate.
        /// </summary>
        /// <value><c>true</c> if this instance is threadpool gate; otherwise, <c>false</c>.</value>
        public bool IsThreadpoolGate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is threadpool timer.
        /// </summary>
        /// <value><c>true</c> if this instance is threadpool timer; otherwise, <c>false</c>.</value>
        public bool IsThreadpoolTimer { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is threadpool wait.
        /// </summary>
        /// <value><c>true</c> if this instance is threadpool wait; otherwise, <c>false</c>.</value>
        public bool IsThreadpoolWait { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is threadpool worker.
        /// </summary>
        /// <value><c>true</c> if this instance is threadpool worker; otherwise, <c>false</c>.</value>
        public bool IsThreadpoolWorker { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is user suspended.
        /// </summary>
        /// <value><c>true</c> if this instance is user suspended; otherwise, <c>false</c>.</value>
        public bool IsUserSuspended { get; set; }

        /// <summary>
        /// Gets or sets the kernel mode time.
        /// </summary>
        /// <value>The kernel mode time.</value>
        public TimeSpan KernelModeTime { get; protected internal set; }

        /// <summary>
        /// Gets or sets the lock count.
        /// </summary>
        /// <value>The lock count.</value>
        public uint LockCount { get; set; }

        // todo: don't expose writable collection
        /// <summary>
        /// Gets or sets the stack frames.
        /// </summary>
        /// <value>The stack frames.</value>
        public IEnumerable<DumpStackFrame> ManagedStackFrames => ManagedStackFramesInternal;

        /// <summary>
        /// Gets or sets the object roots associated with this thread
        /// </summary>
        /// <value>The object roots.</value>
        public IEnumerable<DumpObjectRoot> ObjectRoots { get; protected internal set; }

        /// <summary>
        /// Gets or sets the thread os id
        /// </summary>
        /// <value>The os id</value>
        public uint OsId { get; protected internal set; }

        /// <summary>
        /// Gets the roots.
        /// </summary>
        /// <value>The roots.</value>
        public IEnumerable<DumpObjectRoot> Roots => RootsInternal;

        /// <summary>
        /// Gets or sets the roots.
        /// </summary>
        /// <value>The roots.</value>
        public ISet<DumpObjectRoot> RootsInternal { get; set; } = new SortedSet<DumpObjectRoot>();

        /// <summary>
        /// Gets or sets the stack limit.
        /// </summary>
        /// <value>The stack limit.</value>
        public ulong StackLimit { get; set; }

        /// <summary>
        /// Gets the stack trace.
        /// </summary>
        /// <value>The stack trace.</value>
        public string StackTrace =>
            _stackTrace ?? (_stackTrace = string.Join("\n", ManagedStackFrames.Select(s => s.DisplayString)));

        /// <summary>
        /// Gets or sets the teb.
        /// </summary>
        /// <value>The teb.</value>
        public ulong Teb { get; set; }

        /// <summary>
        /// Gets or sets the total time.
        /// </summary>
        /// <value>The total time.</value>
        public TimeSpan TotalTime { get; protected internal set; }

        /// <summary>
        /// Gets or sets the user mode time.
        /// </summary>
        /// <value>The user mode time.</value>
        public TimeSpan UserModeTime { get; protected internal set; }

        /// <summary>
        /// Gets or sets the managed stack frames internal.
        /// </summary>
        /// <value>The managed stack frames internal.</value>
        internal IList<DumpStackFrame> ManagedStackFramesInternal { get; set; } = new List<DumpStackFrame>();
    }
}