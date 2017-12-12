using System;
using System.Collections.Generic;

namespace Triage.Mortician.Abstraction
{
    /// <summary>
    /// Represents a thread that was in the memory dump
    /// </summary>
    public interface IDumpThread
    {
        /// <summary>
        /// Gets the index of thread in the debugger
        /// </summary>
        /// <value>
        /// The index of the thread in the debugger
        /// </value>
        uint DebuggerIndex { get; }


        /// <summary>
        /// Gets the kernel mode time.
        /// </summary>
        /// <value>
        /// The kernel mode time.
        /// </value>
        TimeSpan KernelModeTime { get; }
        /// <summary>
        /// Gets or sets the os identifier.
        /// </summary>
        /// <value>
        /// The os identifier.
        /// </value>
        uint OsId { get; set; }
        /// <summary>
        /// Gets the stack objects.
        /// </summary>
        /// <value>
        /// The stack objects.
        /// </value>
        IReadOnlyCollection<IDumpObject> StackObjects { get; }
        
        /// <summary>
        /// Gets the user mode time.
        /// </summary>
        /// <value>
        /// The user mode time.
        /// </value>
        TimeSpan UserModeTime { get; }
        
        /// <summary>
        /// Gets the stack frames.
        /// </summary>
        /// <value>
        /// The stack frames.
        /// </value>
        IList<IDumpStackFrame> StackFrames { get; }
    }
}