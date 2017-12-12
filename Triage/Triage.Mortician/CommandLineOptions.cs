using System;
using CommandLine;

namespace Triage.Mortician
{
    public class CommandLineOptions
    {
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