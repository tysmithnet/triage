// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 12-19-2017
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-01-2018
// ***********************************************************************
// <copyright file="DumpStackFrame.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Core
{
    /// <summary>
    ///     Represents a stack frame extracted from the memory dump
    /// </summary>
    /// <seealso cref="System.IEquatable{Triage.Mortician.Core.DumpStackFrame}" />
    /// <seealso cref="System.IComparable{Triage.Mortician.Core.DumpStackFrame}" />
    /// <seealso cref="System.IComparable" />
    public class DumpStackFrame : IEquatable<DumpStackFrame>, IComparable<DumpStackFrame>, IComparable
    {
        /// <summary>
        ///     Compares to.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>System.Int32.</returns>
        /// <inheritdoc />
        public int CompareTo(DumpStackFrame other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return StackPointer.CompareTo(other.StackPointer);
        }

        /// <summary>
        ///     Compares to.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="ArgumentException">DumpStackFrame</exception>
        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            if (!(obj is DumpStackFrame))
                throw new ArgumentException($"Object must be of type {nameof(DumpStackFrame)}");
            return CompareTo((DumpStackFrame) obj);
        }

        /// <summary>
        ///     Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <inheritdoc />
        public bool Equals(DumpStackFrame other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return StackPointer == other.StackPointer;
        }

        /// <summary>
        ///     Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((DumpStackFrame) obj);
        }

        /// <summary>
        ///     Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        /// <inheritdoc />
        public override int GetHashCode() => StackPointer.GetHashCode();

        /// <summary>
        ///     Implements the &gt; operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >(DumpStackFrame left, DumpStackFrame right) =>
            Comparer<DumpStackFrame>.Default.Compare(left, right) > 0;

        /// <summary>
        ///     Implements the &gt;= operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(DumpStackFrame left, DumpStackFrame right) =>
            Comparer<DumpStackFrame>.Default.Compare(left, right) >= 0;

        /// <summary>
        ///     Implements the &lt; operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(DumpStackFrame left, DumpStackFrame right) =>
            Comparer<DumpStackFrame>.Default.Compare(left, right) < 0;

        /// <summary>
        ///     Implements the &lt;= operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(DumpStackFrame left, DumpStackFrame right) =>
            Comparer<DumpStackFrame>.Default.Compare(left, right) <= 0;

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
        ///     Gets or sets the kind.
        /// </summary>
        /// <value>The kind.</value>
        public ClrStackFrameType Kind { get; set; }

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
    }
}