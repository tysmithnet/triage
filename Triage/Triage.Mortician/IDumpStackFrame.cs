namespace Triage.Mortician
{
    /// <summary>
    /// Represents a stack frame from a thread in the dump
    /// </summary>
    public interface IDumpStackFrame
    {
        /// <summary>
        /// Gets a user friendly version of this stack frame
        /// </summary>
        /// <value>
        /// The display string.
        /// </value>
        string DisplayString { get; }
    }
}