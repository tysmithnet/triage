// ***********************************************************************
// Assembly         : Triage.Mortician.Core
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="IClrStackFrame.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Triage.Mortician.Core.ClrMdAbstractions
{
    /// <summary>
    ///     Interface IClrStackFrame
    /// </summary>
    public interface IClrStackFrame
    {
        /// <summary>
        ///     The string to display in a stack trace.  Similar to !clrstack output.
        /// </summary>
        /// <value>The display string.</value>
        string DisplayString { get; }

        /// <summary>
        ///     The instruction pointer of this frame.
        /// </summary>
        /// <value>The instruction pointer.</value>
        ulong InstructionPointer { get; }

        /// <summary>
        ///     The type of frame (managed or internal).
        /// </summary>
        /// <value>The kind.</value>
        ClrStackFrameType Kind { get; }

        /// <summary>
        ///     Returns the ClrMethod which corresponds to the current stack frame.  This may be null if the
        ///     current frame is actually a CLR "Internal Frame" representing a marker on the stack, and that
        ///     stack marker does not have a managed method associated with it.
        /// </summary>
        /// <value>The method.</value>
        IClrMethod Method { get; }

        /// <summary>
        ///     Returns the module name to use for building the stack trace.
        /// </summary>
        /// <value>The name of the module.</value>
        string ModuleName { get; }

        /// <summary>
        ///     The stack pointer of this frame.
        /// </summary>
        /// <value>The stack pointer.</value>
        ulong StackPointer { get; }

        /// <summary>
        ///     Returns the thread this stack frame came from.
        /// </summary>
        /// <value>The thread.</value>
        IClrThread Thread { get; }
    }
}