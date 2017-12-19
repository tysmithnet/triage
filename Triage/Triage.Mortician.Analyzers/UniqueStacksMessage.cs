using System.Collections.Generic;

namespace Triage.Mortician.Analyzers
{
    /// <inheritdoc />
    /// <summary>
    ///     A message that will contain information on the unique managed stack traces in the dump
    /// </summary>
    /// <seealso cref="T:Triage.Mortician.Message" />
    public class UniqueStacksMessage : Message
    {
        /// <summary>
        ///     Gets or sets the unique stack frame rollup records.
        /// </summary>
        /// <value>
        ///     The unique stack frame rollup records.
        /// </value>
        public IReadOnlyList<UniqueStackFrameRollupRecord> UniqueStackFrameRollupRecords { get; protected internal set; }
    }
}