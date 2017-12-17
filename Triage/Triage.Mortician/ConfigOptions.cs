using System.Collections.Generic;
using CommandLine;

namespace Triage.Mortician
{
    /// <summary>
    /// The command line options for configuring mortician
    /// </summary>
    [Verb("config", HelpText = "Configure mortician")]
    public class ConfigOptions
    {
        [Option('d', "display", HelpText = "Display the current settings", SetName = "Display")]
        public bool ShouldDisplay { get; set; }

        [Option('k', "key", HelpText = "The key to use when retrieving this setting", SetName = "Upsert")]
        public IEnumerable<string> Keys { get; set; }

        [Option('v', "value", HelpText = "The value of the setting", SetName = "Upsert")]
        public IEnumerable<string> Values { get; set; }
    }
}