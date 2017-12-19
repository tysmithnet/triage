namespace Triage.Mortician.Analyzers
{
    /// <summary>
    ///     Represents a summary statistic for a particular kind of stack frame found in the memory dump
    /// </summary>
    public class StackFrameRollupRecord
    {
        /// <summary>
        ///     Gets or sets the display string of the frame
        /// </summary>
        /// <value>
        ///     The display string.
        /// </value>
        public string DisplayString { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the name of the module that this frame is defined in
        /// </summary>
        /// <value>
        ///     The name of the module.
        /// </value>
        public string ModuleName { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the count of times this frame is seen in the thread pool
        /// </summary>
        /// <value>
        ///     The count.
        /// </value>
        public int Count { get; protected internal set; }
    }
}