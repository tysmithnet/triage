// ***********************************************************************
// Assembly         : Triage.Mortician.Core
// Author           : @tysmithnet
// Created          : 10-01-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-01-2018
// ***********************************************************************
// <copyright file="DumpHeapSegment.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Core
{
    /// <summary>
    ///     Class DumpHeapSegment.
    /// </summary>
    public class DumpHeapSegment
    {
        /// <summary>
        ///     Gets or sets the committed end.
        /// </summary>
        /// <value>The committed end.</value>
        public ulong CommittedEnd { get; set; }

        /// <summary>
        ///     Gets or sets the end.
        /// </summary>
        /// <value>The end.</value>
        public ulong End { get; set; }

        /// <summary>
        ///     Gets or sets the first object.
        /// </summary>
        /// <value>The first object.</value>
        public ulong FirstObject { get; set; }

        /// <summary>
        ///     Gets or sets the length of the gen0.
        /// </summary>
        /// <value>The length of the gen0.</value>
        public ulong Gen0Length { get; set; }

        /// <summary>
        ///     Gets or sets the gen0 start.
        /// </summary>
        /// <value>The gen0 start.</value>
        public ulong Gen0Start { get; set; }

        /// <summary>
        ///     Gets or sets the length of the gen1.
        /// </summary>
        /// <value>The length of the gen1.</value>
        public ulong Gen1Length { get; set; }

        /// <summary>
        ///     Gets or sets the gen1 start.
        /// </summary>
        /// <value>The gen1 start.</value>
        public ulong Gen1Start { get; set; }

        /// <summary>
        ///     Gets or sets the length of the gen2.
        /// </summary>
        /// <value>The length of the gen2.</value>
        public ulong Gen2Length { get; set; }

        /// <summary>
        ///     Gets or sets the gen2 start.
        /// </summary>
        /// <value>The gen2 start.</value>
        public ulong Gen2Start { get; set; }

        /// <summary>
        ///     Gets or sets the heap.
        /// </summary>
        /// <value>The heap.</value>
        public IClrHeap Heap { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is ephemeral.
        /// </summary>
        /// <value><c>true</c> if this instance is ephemeral; otherwise, <c>false</c>.</value>
        public bool IsEphemeral { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is large.
        /// </summary>
        /// <value><c>true</c> if this instance is large; otherwise, <c>false</c>.</value>
        public bool IsLarge { get; set; }

        /// <summary>
        ///     Gets or sets the length.
        /// </summary>
        /// <value>The length.</value>
        public ulong Length { get; set; }

        /// <summary>
        ///     Gets or sets the processor affinity.
        /// </summary>
        /// <value>The processor affinity.</value>
        public int ProcessorAffinity { get; set; }

        /// <summary>
        ///     Gets or sets the reserved end.
        /// </summary>
        /// <value>The reserved end.</value>
        public ulong ReservedEnd { get; set; }

        /// <summary>
        ///     Gets or sets the start.
        /// </summary>
        /// <value>The start.</value>
        public ulong Start { get; set; }
    }
}