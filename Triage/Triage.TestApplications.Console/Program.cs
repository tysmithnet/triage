using System;
using System.Configuration;
using System.Threading;
using Triage.Mortician.IntegrationTest;
using static System.Console;
using static System.IO.Path;

namespace Triage.TestApplications.Console
{
    internal class Program
    {
        [ThreadStatic]
        public static string ImportantInfo;

        public static void Main(string[] args)
        {
            var p = new Person();
            p.Name = "Duke";
            WriteLine($"Hello {p.Name}");
            ImportantInfo = p.Name;
            DumpHelper.CreateDump(Combine(
                ConfigurationManager.AppSettings[IntPtr.Size == 4 ? "DumpLocationX86" : "DumpLocationX64"],
                "helloworld.dmp"));
        }
    }
}