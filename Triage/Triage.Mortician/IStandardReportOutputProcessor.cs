namespace Triage.Mortician
{
    /// <summary>
    ///     Interface IStandardReportOutputProcessor
    /// </summary>
    /// <typeparam name="TReport">The type of the t report.</typeparam>
    public interface IStandardReportOutputProcessor<out TReport> where TReport : IReport
    {
        /// <summary>
        ///     Processes the output.
        /// </summary>
        /// <param name="output">The output.</param>
        /// <returns>TReport.</returns>
        TReport ProcessOutput(string output);
    }
}