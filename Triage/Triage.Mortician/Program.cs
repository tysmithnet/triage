using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using Common.Logging;
using Microsoft.Diagnostics.Runtime;
using Triage.Mortician.Abstraction;

namespace Triage.Mortician
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<CommandLineOptions>(args).WithParsed(options =>
            {
                var assemblyCatalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
                var aggregateCatalog = new AggregateCatalog(assemblyCatalog);
                var compositionContainer = new CompositionContainer(aggregateCatalog);
                LogManager.GetLogger(typeof(Program)).Trace("Hello world");
                Console.ReadKey();
            });
        }      
    }                                  

    public class Engine
    {
        [ImportMany]
        public IPlugin[] Plugins { get; set; }
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
