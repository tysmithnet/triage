using System;
using CommandLine;

namespace Triage.Mortician
{
    /// <summary>
    ///     Command line arguments for the process
    /// </summary>
    public class CommandLineOptions
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CommandLineOptions" /> class.
        /// </summary>
        public CommandLineOptions()
        {
            ReportName = DateTime.Now.ToString("yyyyMMdd_hhmmss-mortician");
        }

        [Option('d', "dumpfile", HelpText = "Full path to the dump file", Required = true)]
        public string DumpFilePath { get; set; }

        [Option('n', "name", HelpText = "User provided name for this report", Required = false)]
        public string ReportName { get; set; }
    }
}