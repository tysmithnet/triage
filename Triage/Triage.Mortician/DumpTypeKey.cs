namespace Triage.Mortician
{
    public struct DumpTypeKey
    {
        public ulong MethodTable { get; set; }
        public string TypeName { get; set; }

        public DumpTypeKey(ulong methodTable, string typeName)
        {
            MethodTable = methodTable;
            TypeName = typeName;
        }
    }
}