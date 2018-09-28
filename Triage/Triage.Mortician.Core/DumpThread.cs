// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 12-19-2017
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-26-2018
// ***********************************************************************
// <copyright file="DumpThread.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Core
{
    /// <summary>
    ///     Represents a thread that was extracted from the memory dump
    /// </summary>
    public class DumpThread
    {
        /// <summary>
        ///     Backing field for the lazy loaded stack trace
        /// </summary>
        private string _stackTrace;

        /// <summary>
        ///     Gets or sets the current frame of the thread
        /// </summary>
        /// <value>The current frame.</value>
        public CodeLocation CurrentFrame { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the index of the thread in the debugger. This is a low integer value used by the debugging interface
        ///     to make thread references easier
        /// </summary>
        /// <value>The index of the thread in the debugger.</value>
        public uint DebuggerIndex { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the kernel mode time.
        /// </summary>
        /// <value>The kernel mode time.</value>
        public TimeSpan KernelModeTime { get; protected internal set; }

        // todo: don't expose writable collection
        /// <summary>
        ///     Gets or sets the stack frames.
        /// </summary>
        /// <value>The stack frames.</value>
        public IEnumerable<DumpStackFrame> ManagedStackFrames => ManagedStackFramesInternal;

        /// <summary>
        ///     Gets or sets the object roots associated with this thread
        /// </summary>
        /// <value>The object roots.</value>
        public IEnumerable<DumpObjectRoot> ObjectRoots { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the thread os id
        /// </summary>
        /// <value>The os id</value>
        public uint OsId { get; protected internal set; }

        /// <summary>
        ///     Gets the stack trace.
        /// </summary>
        /// <value>The stack trace.</value>
        public string StackTrace =>
            _stackTrace ?? (_stackTrace = string.Join("\n", ManagedStackFrames.Select(s => s.DisplayString)));

        /// <summary>
        ///     Gets or sets the total time.
        /// </summary>
        /// <value>The total time.</value>
        public TimeSpan TotalTime { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the user mode time.
        /// </summary>
        /// <value>The user mode time.</value>
        public TimeSpan UserModeTime { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the managed stack frames internal.
        /// </summary>
        /// <value>The managed stack frames internal.</value>
        internal IList<DumpStackFrame> ManagedStackFramesInternal { get; set; } = new List<DumpStackFrame>();

        public bool IsGcThread { get; internal set; }
        public uint LockCount { get; set; }
        public ulong StackLimit { get; set; }
        public ulong Address { get; set; }
        public ulong AppDomainAddress { get; set; }
        public GcMode GcMode { get; set; }
        public bool IsSta { get; set; }
        public bool IsAbortRequested { get; set; }
        public bool IsAborted { get; set; }
        public bool IsDebuggerHelper { get; set; }
        public bool IsGc { get; set; }
        public bool IsDebugSuspended { get; set; }
        public bool IsCoinitialized { get; set; }
        public bool IsBackground { get; set; }
        public bool IsAlive { get; set; }
        public ulong Teb { get; set; }
        public bool IsUserSuspended { get; set; }
        public bool IsFinalizer { get; set; }
        public bool IsGcSuspendPending { get; set; }
        public bool IsMta { get; set; }
        public bool IsSuspendingEe { get; set; }
        public bool IsShutdownHelper { get; set; }
        public bool IsThreadpoolCompletionPort { get; set; }
        public bool IsThreadpoolGate { get; set; }
        public bool IsThreadpoolTimer { get; set; }
        public bool IsThreadpoolWait { get; set; }
        public bool IsThreadpoolWorker { get; set; }
        public bool IsCreatedButNotStarted { get; set; }
    }
}