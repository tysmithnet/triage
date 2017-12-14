using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triage.Mortician
{
    public class DumpAppDomain
    {
        public string Name { get; }
        public string ConfigFile { get; }
        public string ApplicationBase { get; }
        public ulong Address { get; }
        public IEnumerable<DumpModule> LoadedModules { get; }
    }
}
