// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-20-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-26-2018
// ***********************************************************************
// <copyright file="DumpDomainOutputProcessor.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Text.RegularExpressions;

namespace Triage.Mortician.Reports.DumpDomain
{
    /// <summary>
    ///     Class DumpDomainOutputProcessor.
    /// </summary>
    /// <seealso cref="Triage.Mortician.Reports.IReportFactory" />
    /// <seealso cref="IDumpDomainOutputProcessor" />
    /// <seealso cref="IDumpDomainOutputProcessor" />
    [Export(typeof(IReportFactory))]
    public class DumpDomainReportFactory : IReportFactory
    {
        public string DisplayName { get; } = "!dumpdomain";

        /// <summary>
        ///     The assembly start regex
        /// </summary>
        internal Regex AssemblyStartRegex = new Regex(
            @"^Assembly:\s*(?<addr>[a-fA-F0-9]+)\s*\[?(?<loc>[^*<>""|?\]\[]+)\]?\s*$",
            RegexOptions.Compiled | RegexOptions.Multiline);

        /// <summary>
        ///     The class loader regex
        /// </summary>
        internal Regex ClassLoaderRegex =
            new Regex(@"^ClassLoader:\s*(?<addr>[a-fA-F0-9]+)\s*$", RegexOptions.Compiled | RegexOptions.Multiline);

        /// <summary>
        ///     The high frequency heap regex
        /// </summary>
        internal Regex HighFrequencyHeapRegex =
            new Regex(@"^HighFrequencyHeap:\s*(?<addr>[a-fA-F0-9]+)\s*$",
                RegexOptions.Compiled | RegexOptions.Multiline);

        /// <summary>
        ///     The low frequency heap regex
        /// </summary>
        internal Regex LowFrequencyHeapRegex =
            new Regex(@"^LowFrequencyHeap:\s*(?<addr>[a-fA-F0-9]+)\s*$",
                RegexOptions.Compiled | RegexOptions.Multiline);

        /// <summary>
        ///     The module line regex
        /// </summary>
        internal Regex ModuleLineRegex =
            new Regex(@"^(?<addr>[a-fA-F0-9]{8,16})\s*\[?(?<loc>[^*<>""|?\]\[]+)\]?\s*$",
                RegexOptions.Compiled | RegexOptions.Multiline);

        /// <summary>
        ///     The module start regex
        /// </summary>
        internal Regex ModuleStartRegex =
            new Regex(@"^Module Name\s*$", RegexOptions.Compiled | RegexOptions.Multiline);

        /// <summary>
        ///     The name regex
        /// </summary>
        internal Regex NameRegex = new Regex(@"^Name:\s*\[?(?<name>[^*<>""|?\]\[]+)\]?\s*$$",
            RegexOptions.Compiled | RegexOptions.Multiline);

        /// <summary>
        ///     The non standard domain
        /// </summary>
        internal Regex NonStandardDomain =
            new Regex(@"^Domain \d+:\s*(?<addr>[a-fA-F0-9]+)\s*$", RegexOptions.Compiled | RegexOptions.Multiline);

        /// <summary>
        ///     The security descriptor regex
        /// </summary>
        internal Regex SecurityDescriptorRegex =
            new Regex(@"^SecurityDescriptor:\s*(?<addr>[a-fA-F0-9]+)\s*$",
                RegexOptions.Compiled | RegexOptions.Multiline);

        /// <summary>
        ///     The shared domain regex
        /// </summary>
        internal Regex SharedDomainRegex =
            new Regex(@"^Shared Domain:\s*(?<addr>[a-fA-F0-9]+)\s*$", RegexOptions.Compiled | RegexOptions.Multiline);

        /// <summary>
        ///     The stage regex
        /// </summary>
        internal Regex StageRegex =
            new Regex(@"^Stage:\s*(?<stage>\w+)\s*$", RegexOptions.Compiled | RegexOptions.Multiline);

        /// <summary>
        ///     The stub heap regex
        /// </summary>
        internal Regex StubHeapRegex = new Regex(@"^StubHeap:\s*(?<addr>[a-fA-F0-9]+)\s*$",
            RegexOptions.Compiled | RegexOptions.Multiline);

        /// <summary>
        ///     The system domain regex
        /// </summary>
        internal Regex SystemDomainRegex =
            new Regex(@"^System Domain:\s*(?<addr>[a-fA-F0-9]+)\s*$", RegexOptions.Compiled | RegexOptions.Multiline);

        /// <summary>
        ///     Generate the report artifact
        /// </summary>
        /// <returns>IReport.</returns>
        public IReport Process() => ProcessOutput(RawOutput);

        /// <summary>
        ///     Processes the output.
        /// </summary>
        /// <param name="output">The output.</param>
        /// <returns>DumpDomainReport.</returns>
        internal DumpDomainReport ProcessOutput(string output)
        {
            // todo: break up
            var returnVal = new DumpDomainReport
            {
                RawOutput = output
            };
            var domainTextChunks = output.Split(new[] {"--------------------------------------"},
                StringSplitOptions.RemoveEmptyEntries).Where(c => NameRegex.IsMatch(c)).ToArray();
            for (var i = 0; i < domainTextChunks.Length; i++)
            {
                var chunk = domainTextChunks[i];
                var assemblyStartIndex = chunk.IndexOf("Assembly:", StringComparison.Ordinal);
                var head = assemblyStartIndex < 0 ? chunk : chunk.Substring(0, assemblyStartIndex);
                var body = assemblyStartIndex < 0 ? "" : chunk.Substring(assemblyStartIndex);
                var systemDomainMatch = SystemDomainRegex.Match(head);
                var sharedDomainMatch = SharedDomainRegex.Match(head);
                var nonStandardDomainMatch = NonStandardDomain.Match(head);
                var lowFrequencyHeapMatch = LowFrequencyHeapRegex.Match(head);
                var highFrequencyHeapMatch = HighFrequencyHeapRegex.Match(head);
                var securityDescriptorMatch = SecurityDescriptorRegex.Match(head);
                var stubHeapMatch = StubHeapRegex.Match(head);
                var stageMatch = StageRegex.Match(head);
                var nameMatch = NameRegex.Match(head);

                var systemDomain = systemDomainMatch.Success
                    ? Convert.ToUInt64(systemDomainMatch.Groups["addr"].Value, 16)
                    : 0;
                var sharedDomain = sharedDomainMatch.Success
                    ? Convert.ToUInt64(sharedDomainMatch.Groups["addr"].Value, 16)
                    : 0;
                var nonStandardDomain = nonStandardDomainMatch.Success
                    ? Convert.ToUInt64(nonStandardDomainMatch.Groups["addr"].Value, 16)
                    : 0;
                var lowFrequencyHeap =
                    lowFrequencyHeapMatch.Success
                        ? Convert.ToUInt64(lowFrequencyHeapMatch.Groups["addr"].Value, 16)
                        : 0;
                var highFrequencyHeap =
                    highFrequencyHeapMatch.Success
                        ? Convert.ToUInt64(highFrequencyHeapMatch.Groups["addr"].Value, 16)
                        : 0;
                var securityDescriptor = securityDescriptorMatch.Success
                    ? Convert.ToUInt64(securityDescriptorMatch.Groups["addr"].Value, 16)
                    : 0;
                var stubHeap = stubHeapMatch.Success ? Convert.ToUInt64(stubHeapMatch.Groups["addr"].Value, 16) : 0;
                var stage = stageMatch.Success
                    ? AppDomainStage.All.FirstOrDefault(s => stageMatch.Value.Contains(s.SosString)) ??
                      AppDomainStage.Unknown
                    : AppDomainStage.Unknown;
                var name = nameMatch.Groups["name"].Success ? nameMatch.Groups["name"].Value.Trim() : null;
                var appDomain = new DumpDomainAppDomain
                {
                    Index = (uint) i,
                    SecurityDescriptor = securityDescriptor,
                    HighFrequencyHeap = highFrequencyHeap,
                    LowFrequencyHeap = lowFrequencyHeap,
                    StubHeap = stubHeap,
                    Name = name,
                    Stage = stage
                };
                if (systemDomainMatch.Success)
                {
                    returnVal.SystemDomain = appDomain;
                    appDomain.Address = systemDomain;
                }
                else if (sharedDomainMatch.Success)
                {
                    returnVal.SharedDomain = appDomain;
                    appDomain.Address = sharedDomain;
                }
                else if (i == 2)
                {
                    returnVal.DefaultDomain = appDomain;
                    appDomain.Address = nonStandardDomain;
                }

                var assemblyChunks = Regex.Split(body, @"^\s*$", RegexOptions.Multiline)
                    .Where(x => !string.IsNullOrWhiteSpace(x));
                foreach (var assemblyChunk in assemblyChunks)
                {
                    var assemblyStartLineMatch = AssemblyStartRegex.Match(assemblyChunk);
                    var assemblyClassLoaderMatch = ClassLoaderRegex.Match(assemblyChunk);
                    var assemblySecDescMatch = SecurityDescriptorRegex.Match(assemblyChunk);
                    var assemblyModuleMatches = ModuleLineRegex.Matches(assemblyChunk);
                    var assemblyAddress = assemblyStartLineMatch.Groups["addr"].Success
                        ? Convert.ToUInt64(assemblyStartLineMatch.Groups["addr"].Value, 16)
                        : 0;
                    var assemblyLocation = assemblyStartLineMatch.Groups["loc"].Success
                        ? assemblyStartLineMatch.Groups["loc"].Value.Trim()
                        : null;
                    var assemblyClassLoader = assemblyClassLoaderMatch.Success
                        ? Convert.ToUInt64(assemblyClassLoaderMatch.Groups["addr"].Value, 16)
                        : 0;
                    var assemblySecDesc = assemblySecDescMatch.Success
                        ? Convert.ToUInt64(assemblySecDescMatch.Groups["addr"].Value, 16)
                        : 0;
                    var newAssembly = new DumpDomainAssembly
                    {
                        Address = assemblyAddress,
                        ClassLoader = assemblyClassLoader,
                        Location = assemblyLocation,
                        SecurityDescriptor = assemblySecDesc
                    };

                    foreach (Match assemblyModuleMatch in assemblyModuleMatches)
                    {
                        var newModule = new DumpDomainModule();
                        newModule.Address = assemblyModuleMatch.Groups["addr"].Success
                            ? Convert.ToUInt64(assemblyModuleMatch.Groups["addr"].Value, 16)
                            : 0;
                        newModule.Location = assemblyModuleMatch.Groups["loc"].Success
                            ? assemblyModuleMatch.Groups["loc"].Value.Trim()
                            : null;
                        newAssembly.ModulesInternal.Add(newModule);
                    }

                    appDomain.AssembliesInternal.Add(newAssembly);
                }

                returnVal.AppDomainsInternal.Add(appDomain);
            }

            return returnVal;
        }

        /// <summary>
        ///     Prepare to generate report artifacts. This typically means using the debugger
        ///     interface to run some commands and store those results. This method will be called
        ///     on the main thread serially, and so it is not necessary to lock the debugger.
        /// </summary>
        /// <param name="debugger">The debugger.</param>
        /// <returns>IReport.</returns>
        public void Setup(IDebuggerProxy debugger)
        {
            RawOutput = debugger.Execute("!dumpdomain");
        }

        /// <summary>
        ///     Gets or sets the raw output.
        /// </summary>
        /// <value>The raw output.</value>
        public string RawOutput { get; set; }
    }
}