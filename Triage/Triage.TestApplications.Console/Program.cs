using System;
using System.Threading;
using static System.Console;

namespace Triage.TestApplications.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Which scenario do you want to run?");
            WriteLine("1) Thread Buildup");
            do
            {
                Write("Selection: ");
                var isConverted = Int32.TryParse(ReadLine(), out var selection);
                if (!isConverted)
                {
                    WriteLine("Please enter a number");
                    continue;
                }
                switch (selection)
                {
                    case 1:
                        ThreadBuildup();
                        Thread.Sleep(1000);
                        WriteLine("Ok to take dump now");
                        ReadLine();
                        return;
                    default:
                        continue;
                }
                
            } while (true);
        }

        private static void ThreadBuildup()
        {
            const int NUM_THREADS = 10;
            for (int i = 0; i < NUM_THREADS; i++)
            {                                                  
                var thread = new Thread(DoNothing);
                thread.IsBackground = true;
                thread.Start();
            }
        }

        private static void DoNothing()
        {
            for (int i = 0; i < 1000000; i++)
            {
                Thread.Sleep(1000);
            }
        }
    }
}
