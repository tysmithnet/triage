namespace Triage.Mortician.Analyzers
{
    /// <inheritdoc />
    /// <summary>
    ///     A message that indicates that an excel report has been saved to disk
    /// </summary>
    /// <seealso cref="T:Triage.Mortician.Message" />
    public class ExcelReportComplete : Message
    {
        /// <summary>
        ///     Gets or sets the report file.
        /// </summary>
        /// <value>
        ///     The report file.
        /// </value>
        public string ReportFile { get; protected internal set; }
    }
}