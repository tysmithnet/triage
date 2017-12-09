namespace Triage.Mortician.Api
{
    public class StringDumpObject : DumpObject
    {
        public string Value { get; internal set; }

        public StringDumpObject(ulong address, string fullTypeName, ulong size, string value) : base(address, fullTypeName, size)
        {
            Value = value;
        }
    }
}