using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triage.Mortician.Core.ClrMdAbstractions;
using ClrMd = Microsoft.Diagnostics.Runtime;

namespace Triage.Mortician.Adapters
{
    internal class StackFrameAdapter : IClrStackFrame
    {
        internal ClrMd.ClrStackFrame Frame;

        /// <inheritdoc />
        public StackFrameAdapter(ClrMd.ClrStackFrame frame)
        {
            Frame = frame ?? throw new ArgumentNullException(nameof(frame));
            Thread = Converter.Convert(Frame.Thread);
            Kind = Converter.Convert(Frame.Kind);
            Method = Converter.Convert(Frame.Method);
        }

        /// <inheritdoc />
        public string DisplayString => Frame.DisplayString;

        /// <inheritdoc />
        public ulong InstructionPointer => Frame.InstructionPointer;

        /// <inheritdoc />
        public ClrStackFrameType Kind { get; }

        /// <inheritdoc />
        public IClrMethod Method { get; }

        /// <inheritdoc />
        public string ModuleName => Frame.ModuleName;

        /// <inheritdoc />
        public ulong StackPointer => Frame.StackPointer;

        /// <inheritdoc />
        public IClrThread Thread { get; }

    }
}
