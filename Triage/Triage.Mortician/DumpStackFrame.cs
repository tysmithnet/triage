namespace Triage.Mortician
{
    /// <summary>
    ///     Represents a stack frame extracted from the memory dump
    /// </summary>
    /// <seealso cref="IDumpStackFrame" />
    internal class DumpStackFrame : IDumpStackFrame
    {
        public string DisplayString { get; set; }
    }
}