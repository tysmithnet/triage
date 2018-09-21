using System;
using System.Configuration;
using Triage.Mortician.IntegrationTest;
using static System.Console;
using static System.IO.Path;

namespace Triage.TestApplications.Console
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            WriteLine("hello world");
            DumpHelper.CreateDump(Combine(ConfigurationManager.AppSettings[IntPtr.Size == 4 ? "DumpLocationX86" : "DumpLocationX64"], "helloworld.dmp"));
        }
    }
}