using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Triage.Mortician
{
    /// <summary>
    ///     Represents a thread that was extracted from the memory dump
    /// </summary>
    /// <seealso cref="IDumpThread" />
    internal class DumpThread : IDumpThread
    {
        private string _stackTrace;

        /// <summary>
        ///     The stack objects
        /// </summary>
        public IList<IDumpObject> StackObjectsInternal = new List<IDumpObject>();

        /// <summary>
        ///     Initializes a new instance of the <see cref="DumpThread" /> class.
        /// </summary>
        public DumpThread()
        {
            StackObjects = new ReadOnlyCollection<IDumpObject>(StackObjectsInternal);
        }

        /// <summary>
        ///     Gets the stack trace.
        /// </summary>
        /// <value>
        ///     The stack trace.
        /// </value>
        public string StackTrace =>
            _stackTrace ?? (_stackTrace = string.Join("\n", StackFrames.Select(s => s.DisplayString)));

        /// <summary>
        ///     Gets or sets the total time.
        /// </summary>
        /// <value>
        ///     The total time.
        /// </value>
        public TimeSpan TotalTime { get; set; }

        /// <summary>
        ///     Gets or sets the thread os id
        /// </summary>
        /// <value>
        ///     The os id
        /// </value>
        public uint OsId { get; set; }

        /// <summary>
        ///     Gets or sets the kernel mode time.
        /// </summary>
        /// <value>
        ///     The kernel mode time.
        /// </value>
        public TimeSpan KernelModeTime { get; set; }

        /// <summary>
        ///     Gets or sets the user mode time.
        /// </summary>
        /// <value>
        ///     The user mode time.
        /// </value>
        public TimeSpan UserModeTime { get; set; }

        // todo: don't expose writable collection
        /// <summary>
        ///     Gets or sets the stack frames.
        /// </summary>
        /// <value>
        ///     The stack frames.
        /// </value>
        public IList<IDumpStackFrame> StackFrames { get; set; }

        /// <summary>
        ///     Gets or sets the stack objects.
        /// </summary>
        /// <value>
        ///     The stack objects.
        /// </value>
        public IReadOnlyCollection<IDumpObject> StackObjects { get; set; }

        /// <summary>
        ///     Gets or sets the index of the thread in the debugger. This is a low integer value used by the debugging interface
        ///     to make thread references easier
        /// </summary>
        /// <value>
        ///     The index of the thread in the debugger.
        /// </value>
        public uint DebuggerIndex { get; set; }

        IEnumerable<IDumpObject> IDumpThread.StackObjects => throw new NotImplementedException();
    }
}