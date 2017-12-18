using System.Collections.Generic;

namespace Triage.Mortician.Analyzers
{
    /// <inheritdoc />
    /// <summary>
    ///     A message that communicates the distribution of stack frames found in the memor dump
    /// </summary>
    /// <seealso cref="T:Triage.Mortician.Message" />
    public class StackFrameBreakdownMessage : Message
    {
        /// <summary>
        ///     Gets or sets the records.
        /// </summary>
        /// <value>
        ///     The records.
        /// </value>
        public IReadOnlyList<StackFrameRollupRecord> Records { get; protected internal set; }
    }
}