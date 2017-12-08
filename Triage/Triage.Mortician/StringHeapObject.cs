namespace Triage.Mortician
{
    public class StringHeapObject : HeapObject
    {
        public string Value { get; internal set; }

        public StringHeapObject(ulong address, string fullTypeName, ulong size, string value) : base(address, fullTypeName, size)
        {
            Value = value;
        }
    }
}