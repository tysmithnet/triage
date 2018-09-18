namespace Triage.Mortician.Core
{
    public interface IClrStackFrame
    {
        /// <summary>
        /// Returns the thread this stack frame came from.
        /// </summary>
        IClrThread Thread { get; }

        /// <summary>
        /// The instruction pointer of this frame.
        /// </summary>
        ulong InstructionPointer { get; }

        /// <summary>
        /// The stack pointer of this frame.
        /// </summary>
        ulong StackPointer { get; }

        /// <summary>
        /// The type of frame (managed or internal).
        /// </summary>
        ClrStackFrameType Kind { get; }

        /// <summary>
        /// The string to display in a stack trace.  Similar to !clrstack output.
        /// </summary>
        string DisplayString { get; }

        /// <summary>
        /// Returns the ClrMethod which corresponds to the current stack frame.  This may be null if the
        /// current frame is actually a CLR "Internal Frame" representing a marker on the stack, and that
        /// stack marker does not have a managed method associated with it.
        /// </summary>
        IClrMethod Method { get; }

        /// <summary>
        /// Returns the module name to use for building the stack trace.
        /// </summary>
        string ModuleName { get; }
    }
}