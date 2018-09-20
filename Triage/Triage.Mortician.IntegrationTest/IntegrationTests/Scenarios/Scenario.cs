using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Microsoft.CSharp;
using Microsoft.Diagnostics.Runtime;

namespace Triage.Mortician.IntegrationTest.IntegrationTests.Scenarios
{
    public abstract partial class Scenario
    {
        public abstract bool IsLibrary { get; }
        public abstract string X86ScenarioDumpFile { get; }
        public abstract string X64ScenarioDumpFile { get; }
        public abstract string X64ExeLocation { get; }
        public abstract string X86ExeLocation { get; }

        public virtual void EnsureDumpFileExists(string dumpFile, string exe)
        {
            if (!File.Exists(dumpFile))
            {
                var testproc = Process.Start(exe);
                testproc?.WaitForExit();
                Thread.Sleep(TimeSpan.FromSeconds(5));
            }

            if (!File.Exists(dumpFile))
            {
                throw new System.ApplicationException($"Scenario dump file, {dumpFile}, doesn't exist and could not be created. Check the configuration paths and permissions.");
            }
        }

        private (string, string) GetDumpAndExe()
        {
            var proc = Process.GetCurrentProcess();
            var is32 = proc.Is32BitProcess();
            var dumpFile = is32 ? X86ScenarioDumpFile : X64ScenarioDumpFile;
            var exe = is32 ? X86ExeLocation : X64ExeLocation;
            return (dumpFile, exe);
        }

        public virtual FileInfo GetDumpFile()
        {
            var (dumpFile, exe) = GetDumpAndExe();
            EnsureDumpFileExists(dumpFile, exe);
            return new FileInfo(dumpFile);
        }

        public virtual DataTarget GetDataTarget()
        {
            var proc = Process.GetCurrentProcess();
            var is32 = proc.Is32BitProcess();
            var dumpFile = is32 ? X86ScenarioDumpFile : X64ScenarioDumpFile;
            var exe = is32 ? X86ExeLocation : X64ExeLocation;
            EnsureDumpFileExists(dumpFile, exe);
            return DataTarget.LoadCrashDump(dumpFile);
        }
    }
}