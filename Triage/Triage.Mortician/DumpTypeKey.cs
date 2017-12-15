namespace Triage.Mortician
{
    /// <summary>
    ///     Type used to represent a unique type extracted from the memory dump
    ///     todo: the types should store this
    /// </summary>
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