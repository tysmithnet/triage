using CommandLine;

namespace Triage.Mortician
{
    /// <summary>
    /// The command line options for the default running options
    /// </summary>
    [Verb("run", HelpText = "Run mortician on the provided dump file")]
    public class DefaultOptions
    {
        [Option('d', "dumpfile", HelpText = "The dump file to operate on", Required = true)]
        public string DumpFile { get; set; }
    }
}