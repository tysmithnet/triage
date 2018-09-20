// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-19-2018
// ***********************************************************************
// <copyright file="StackFrameAdapter.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.ComponentModel.Composition;
using Triage.Mortician.Core.ClrMdAbstractions;
using ClrMd = Microsoft.Diagnostics.Runtime;

namespace Triage.Mortician.Adapters
{
    /// <summary>
    ///     Class StackFrameAdapter.
    /// </summary>
    /// <seealso cref="Triage.Mortician.Core.ClrMdAbstractions.IClrStackFrame" />
    internal class StackFrameAdapter : BaseAdapter, IClrStackFrame
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="StackFrameAdapter" /> class.
        /// </summary>
        /// <param name="frame">The frame.</param>
        /// <exception cref="ArgumentNullException">frame</exception>
        /// <inheritdoc />
        public StackFrameAdapter(IConverter converter, ClrMd.ClrStackFrame frame) : base(converter)
        {
            Frame = frame ?? throw new ArgumentNullException(nameof(frame));
            Thread = Converter.Convert(Frame.Thread);
            Kind = Converter.Convert(Frame.Kind);
            Method = Converter.Convert(Frame.Method);
        }

        /// <summary>
        ///     The frame
        /// </summary>
        internal ClrMd.ClrStackFrame Frame;

        /// <summary>
        ///     The string to display in a stack trace.  Similar to !clrstack output.
        /// </summary>
        /// <value>The display string.</value>
        /// <inheritdoc />
        public string DisplayString => Frame.DisplayString;

        /// <summary>
        ///     The instruction pointer of this frame.
        /// </summary>
        /// <value>The instruction pointer.</value>
        /// <inheritdoc />
        public ulong InstructionPointer => Frame.InstructionPointer;

        /// <summary>
        ///     The type of frame (managed or internal).
        /// </summary>
        /// <value>The kind.</value>
        /// <inheritdoc />
        public ClrStackFrameType Kind { get; }

        /// <summary>
        ///     Returns the ClrMethod which corresponds to the current stack frame.  This may be null if the
        ///     current frame is actually a CLR "Internal Frame" representing a marker on the stack, and that
        ///     stack marker does not have a managed method associated with it.
        /// </summary>
        /// <value>The method.</value>
        /// <inheritdoc />
        public IClrMethod Method { get; }

        /// <summary>
        ///     Returns the module name to use for building the stack trace.
        /// </summary>
        /// <value>The name of the module.</value>
        /// <inheritdoc />
        public string ModuleName => Frame.ModuleName;

        /// <summary>
        ///     The stack pointer of this frame.
        /// </summary>
        /// <value>The stack pointer.</value>
        /// <inheritdoc />
        public ulong StackPointer => Frame.StackPointer;

        /// <summary>
        ///     Returns the thread this stack frame came from.
        /// </summary>
        /// <value>The thread.</value>
        /// <inheritdoc />
        public IClrThread Thread { get; }

        /// <summary>
        ///     Gets or sets the converter.
        /// </summary>
        /// <value>The converter.</value>
        [Import]
        internal IConverter Converter { get; set; }
    }
}