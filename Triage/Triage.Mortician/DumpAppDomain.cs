using System.Collections.Generic;

namespace Triage.Mortician
{
    public class DumpAppDomain
    {
        protected internal IList<DumpModule> LoadedModulesInternal = new List<DumpModule>();
        public string Name { get; protected internal set; }
        public string ConfigFile { get; protected internal set; }
        public string ApplicationBase { get; protected internal set; }
        public ulong Address { get; protected internal set; }
        public IEnumerable<DumpModule> LoadedModules { get; protected internal set; }
        public int Id { get; protected internal set; }
    }
}