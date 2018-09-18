using System.Collections.Generic;
using CommandLine;

namespace Triage.Mortician
{
    /// <summary>
    ///     The command line options for configuring mortician
    /// </summary>
    [Verb("config", HelpText = "Configure mortician")]
    public class ConfigOptions
    {
        [Option('k', "key", HelpText = "The key to use when retrieving this setting", SetName = "Upsert")]
        public IEnumerable<string> Keys { get; set; }

        [Option('d', "delete", HelpText = "Delete keys from the settings")]
        public IEnumerable<string> KeysToDelete { get; set; }

        [Option('l', "list", HelpText = "List the current settings", SetName = "List")]
        public bool ShouldList { get; set; }

        [Option('v', "value", HelpText = "The value of the setting", SetName = "Upsert")]
        public IEnumerable<string> Values { get; set; }
    }
}