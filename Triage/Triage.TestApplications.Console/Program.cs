using Triage.Mortician.Test;
using static System.Console;

namespace Triage.TestApplications.Console
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            WriteLine("hello world");
            DumpHelper.CreateDump("helloworld.dmp");
        }
    }
}