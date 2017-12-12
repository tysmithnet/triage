namespace Triage.Mortician
{
    public class StringDumpObject : DumpObject
    {
        public StringDumpObject(ulong address, string fullTypeName, ulong size, string value, int gen) : base(address,
            fullTypeName, size, gen)
        {
            Value = value;
        }

        public string Value { get; internal set; }
    }
}