using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using Microsoft.Diagnostics.Runtime;

namespace Triage.Mortician
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<CommandLineOptions>(args).WithParsed(options =>
            {
                Console.WriteLine("hello");
            });
        }      
    }

    public class CommandLineOptions
    {
        [Option('d', "dumpfile", HelpText = "Full path to the dump file", Required = true)]
        public string DumpFilePath { get; set; }

        [Option('n', "name", HelpText = "User provided name for this report", Required = false)]
        public string ReportName { get; set; }

        public CommandLineOptions()
        {
            ReportName = DateTime.Now.ToString("yyyyMMdd_hhmmss-mortician");
        }
    }
}
