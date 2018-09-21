using System;

namespace Triage.Mortician
{
    /// <summary>
    ///     Class DumpDomainOutputProcessor.
    /// </summary>
    /// <seealso cref="Triage.Mortician.IDumpDomainOutputProcessor" />
    public class DumpDomainOutputProcessor : IDumpDomainOutputProcessor
    {
        /// <summary>
        ///     Processes the output.
        /// </summary>
        /// <param name="output">The output.</param>
        /// <returns>DumpDomainReport.</returns>
        /// <exception cref="NotImplementedException"></exception>
        /// <inheritdoc />
        public DumpDomainReport ProcessOutput(string output) => throw new NotImplementedException();
    }
}