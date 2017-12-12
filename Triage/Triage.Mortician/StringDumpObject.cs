using Triage.Mortician.Abstraction;

namespace Triage.Mortician
{
    public class StringDumpObject : DumpObject
    {
        public string Value { get; internal set; }

        public StringDumpObject(ulong address, string fullTypeName, ulong size, string value, int gen) : base(address, fullTypeName, size, gen)
        {
            Value = value;
        }
    }
}