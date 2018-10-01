// ***********************************************************************
// Assembly         : Triage.Mortician.IntegrationTest
// Author           : @tysmithnet
// Created          : 09-20-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-20-2018
// ***********************************************************************
// <copyright file="Scenario.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Microsoft.Diagnostics.Runtime;

namespace Triage.Mortician.IntegrationTest.Scenarios
{
    /// <summary>
    ///     Class Scenario.
    /// </summary>
    public abstract partial class Scenario
    {
        /// <summary>
        ///     Ensures the dump file exists.
        /// </summary>
        /// <param name="dumpFile">The dump file.</param>
        /// <param name="exe">The executable.</param>
        /// <exception cref="ApplicationException"></exception>
        public virtual void EnsureDumpFileExists(string dumpFile, string exe)
        {
            if (!File.Exists(dumpFile))
            {
                var testproc = Process.Start(exe);
                testproc?.WaitForExit();
                Thread.Sleep(TimeSpan.FromSeconds(5));
            }

            var tries = 0;
            try
            {
                for (; tries < 3; tries++)
                    if (!File.Exists(dumpFile))
                        Thread.Sleep((int) (1000 * Math.Pow(2, tries)));
            }
            catch (Exception e)
            {
                throw new ApplicationException(
                    $"Scenario dump file, {dumpFile}, doesn't exist and could not be created after {tries} retries. Check the configuration paths and permissions. It is also possible that the file is there now -check.",
                    e);
            }
        }

        /// <summary>
        ///     Gets the data target.
        /// </summary>
        /// <returns>DataTarget.</returns>
        public virtual DataTarget GetDataTarget()
        {
            var proc = Process.GetCurrentProcess();
            var is32 = proc.Is32BitProcess();
            var dumpFile = is32 ? X86ScenarioDumpFile : X64ScenarioDumpFile;
            var exe = is32 ? X86ExeLocation : X64ExeLocation;
            EnsureDumpFileExists(dumpFile, exe);
            return DataTarget.LoadCrashDump(dumpFile);
        }

        /// <summary>
        ///     Gets the dump file.
        /// </summary>
        /// <returns>FileInfo.</returns>
        public virtual FileInfo GetDumpFile()
        {
            var (dumpFile, exe) = GetDumpAndExe();
            EnsureDumpFileExists(dumpFile, exe);
            return new FileInfo(dumpFile);
        }

        /// <summary>
        ///     Gets the dump and executable.
        /// </summary>
        /// <returns>System.String.</returns>
        private (string, string) GetDumpAndExe()
        {
            var proc = Process.GetCurrentProcess();
            var is32 = proc.Is32BitProcess();
            var dumpFile = is32 ? X86ScenarioDumpFile : X64ScenarioDumpFile;
            var exe = is32 ? X86ExeLocation : X64ExeLocation;
            return (dumpFile, exe);
        }

        /// <summary>
        ///     Gets a value indicating whether this instance is library.
        /// </summary>
        /// <value><c>true</c> if this instance is library; otherwise, <c>false</c>.</value>
        public abstract bool IsLibrary { get; }

        /// <summary>
        ///     Gets the X64 executable location.
        /// </summary>
        /// <value>The X64 executable location.</value>
        public abstract string X64ExeLocation { get; }

        /// <summary>
        ///     Gets the X64 scenario dump file.
        /// </summary>
        /// <value>The X64 scenario dump file.</value>
        public abstract string X64ScenarioDumpFile { get; }

        /// <summary>
        ///     Gets the X86 executable location.
        /// </summary>
        /// <value>The X86 executable location.</value>
        public abstract string X86ExeLocation { get; }

        /// <summary>
        ///     Gets the X86 scenario dump file.
        /// </summary>
        /// <value>The X86 scenario dump file.</value>
        public abstract string X86ScenarioDumpFile { get; }
    }
}