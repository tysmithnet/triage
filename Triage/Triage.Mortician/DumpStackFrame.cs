namespace Triage.Mortician
{
    /// <summary>
    ///     Represents a stack frame extracted from the memory dump
    /// </summary>
    /// <seealso cref="IDumpStackFrame" />
    public class DumpStackFrame
    {
        public string DisplayString { get; set; }
    }
}