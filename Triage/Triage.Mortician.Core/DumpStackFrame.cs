// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 12-19-2017
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="DumpStackFrame.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Core
{
    /// <summary>
    ///     Represents a stack frame extracted from the memory dump
    /// </summary>
    public class DumpStackFrame
    {
        /// <summary>
        ///     Gets or sets the user friendly string representation of this frame
        /// </summary>
        /// <value>The display string.</value>
        public string DisplayString { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the instruction pointer for this frame
        /// </summary>
        /// <value>The instruction pointer.</value>
        public ulong InstructionPointer { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this stack frame is a managed frame
        /// </summary>
        /// <value><c>true</c> if this instance is managed; otherwise, <c>false</c>.</value>
        public bool IsManaged { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the name of the module that contains the code for this frame (can be null)
        /// </summary>
        /// <value>The name of the module.</value>
        public string ModuleName { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the stack pointer for this frame
        /// </summary>
        /// <value>The stack pointer.</value>
        public ulong StackPointer { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the thread for this frame
        /// </summary>
        /// <value>The thread.</value>
        public DumpThread Thread { get; protected internal set; }

        public ClrStackFrameType Kind { get; set; }
    }
}