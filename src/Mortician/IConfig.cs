using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortician
{
    public interface IConfig
    {
        string[] BlackListedAssemblies { get; }
        string[] BlackListedTypes { get; }
        string[] AdditionalAssemblies { get; }
        Dictionary<string, string[]> ContractMapping { get; }
    }

    internal class Config : IConfig
    {
        /// <inheritdoc />
        public string[] BlackListedAssemblies { get; set; } = new string[0];

        /// <inheritdoc />
        public string[] BlackListedTypes { get; set; } = new string[0];

        /// <inheritdoc />
        public string[] AdditionalAssemblies { get; set; } = new string[0];

        /// <inheritdoc />
        public Dictionary<string, string[]> ContractMapping { get; set; } = new Dictionary<string, string[]>();
    }
}
