using Triage.Mortician.Core;

namespace Triage.Mortician.Reports
{
    /// <summary>
    ///     Class EeStackFrame. This class cannot be inherited.
    /// </summary>
    public sealed class EeStackFrame
    {
        public CodeLocation Caller { get; internal set; }
        public CodeLocation Callee { get; internal set; }

        /// <summary>
        ///     Gets the child stack pointer.
        /// </summary>
        /// <value>The child stack pointer.</value>
        public ulong ChildStackPointer { get; internal set; }

        /// <summary>
        ///     Gets the return address.
        /// </summary>
        /// <value>The return address.</value>
        public ulong ReturnAddress { get; internal set; }

        public ulong MethodDescriptor { get; internal set; }
    }
}