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
        public string DisplayString { get; internal set; }

        /// <inheritdoc />
        public ulong InstructionPointer { get; internal set; }

        /// <inheritdoc />
        public ClrStackFrameType Kind { get; internal set; }

        /// <inheritdoc />
        public IClrMethod Method { get; internal set; }

        /// <inheritdoc />
        public string ModuleName { get; internal set; }

        /// <inheritdoc />
        public ulong StackPointer { get; internal set; }

        /// <inheritdoc />
        public IClrThread Thread { get; internal set; }
    }
}