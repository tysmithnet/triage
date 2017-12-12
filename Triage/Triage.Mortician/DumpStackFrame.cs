using Triage.Mortician.Abstraction;

namespace Triage.Mortician
{
    /// <summary>
    /// Represents a stack frame extracted from the memory dump
    /// </summary>
    /// <seealso cref="Triage.Mortician.Abstraction.IDumpStackFrame" />
    internal class DumpStackFrame : IDumpStackFrame
    {
        public string DisplayString { get; set; }
    }
}