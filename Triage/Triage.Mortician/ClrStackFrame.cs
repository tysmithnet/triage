using System;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician
{
    internal class ClrStackFrame : IClrStackFrame
    {
        /// <inheritdoc />
        public ClrStackFrame(Microsoft.Diagnostics.Runtime.ClrStackFrame stackFrame)
        {
            _stackFrame = stackFrame ?? throw new ArgumentNullException(nameof(stackFrame));
        }

        internal Microsoft.Diagnostics.Runtime.ClrStackFrame _stackFrame;

        /// <inheritdoc />
        public string DisplayString { get; }

        /// <inheritdoc />
        public ulong InstructionPointer { get; }

        /// <inheritdoc />
        public ClrStackFrameType Kind { get; }

        /// <inheritdoc />
        public IClrMethod Method { get; }

        /// <inheritdoc />
        public string ModuleName { get; }

        /// <inheritdoc />
        public ulong StackPointer { get; }

        /// <inheritdoc />
        public IClrThread Thread { get; }
    }
}