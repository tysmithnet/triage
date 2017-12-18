using System.Collections.Generic;
using Triage.Mortician.Domain;

namespace Triage.Mortician.Analyzers
{
    /// <summary>
    ///     Represents a summary of a particular managed call stack and the threads that share this same call stacks
    /// </summary>
    public class UniqueStackFrameRollupRecord
    {
        /// <summary>
        ///     Gets or sets the stack trace.
        /// </summary>
        /// <value>
        ///     The stack trace.
        /// </value>
        public string StackTrace { get; set; }

        /// <summary>
        ///     Gets or sets the threads.
        /// </summary>
        /// <value>
        ///     The threads.
        /// </value>
        public IReadOnlyList<DumpThread> Threads { get; set; }
    }
}