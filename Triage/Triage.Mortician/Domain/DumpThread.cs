// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 12-19-2017
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="DumpThread.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;

namespace Triage.Mortician.Domain
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
        public string CurrentFrame { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the index of the thread in the debugger. This is a low integer value used by the debugging interface
        ///     to make thread references easier
        /// </summary>
        /// <value>The index of the thread in the debugger.</value>
        public uint DebuggerIndex { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the stack frames according to !eestack
        /// </summary>
        /// <value>The ee stack frames.</value>
        public IList<string> EEStackFrames { get; protected internal set; } = new List<string>();

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
        public IList<DumpStackFrame> ManagedStackFrames { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the object roots associated with this thread
        /// </summary>
        /// <value>The object roots.</value>
        public IList<DumpObjectRoot> ObjectRoots { get; protected internal set; }

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
    }
}