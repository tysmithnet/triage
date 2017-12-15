namespace Triage.Mortician
{
    /// <summary>
    ///     Represents a stack frame extracted from the memory dump
    /// </summary>
    public class DumpStackFrame
    {
        /// <summary>
        ///     Gets or sets the user friendly string representation of this frame
        /// </summary>
        /// <value>
        ///     The display string.
        /// </value>
        public string DisplayString { get; set; }
    }
}