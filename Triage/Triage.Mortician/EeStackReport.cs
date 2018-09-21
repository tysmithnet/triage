using System.Collections.Generic;

namespace Triage.Mortician
{
    /// <summary>
    ///     Class EeStackReport. This class cannot be inherited.
    /// </summary>
    /// <seealso cref="Triage.Mortician.IReport" />
    public sealed class EeStackReport : IReport
    {
        /// <summary>
        ///     Gets the threads.
        /// </summary>
        /// <value>The threads.</value>
        public IEnumerable<EeStackThread> Threads => ThreadsInternal;

        /// <summary>
        ///     Gets or sets the threads internal.
        /// </summary>
        /// <value>The threads internal.</value>
        internal IList<EeStackThread> ThreadsInternal { get; set; } = new List<EeStackThread>();
    }
}