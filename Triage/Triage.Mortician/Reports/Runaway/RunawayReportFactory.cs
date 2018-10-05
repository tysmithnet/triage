// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-26-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-26-2018
// ***********************************************************************
// <copyright file="RunawayOutputProcessor.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text.RegularExpressions;

namespace Triage.Mortician.Reports.Runaway
{
    /// <summary>
    /// Class RunawayOutputProcessor.
    /// </summary>
    /// <seealso cref="Triage.Mortician.Reports.IReportFactory" />
    [Export(typeof(IReportFactory))]
    public class RunawayReportFactory : IReportFactory
    {
        /// <summary>
        /// The line regex
        /// </summary>
        private static readonly Regex LineRegex =
            new Regex(@"^\s*(?<idx>\d+):(?<id>[a-fA-F0-9]+)\s*(?<days>\d+)\s*days\s(?<ts>[\d\.:]+)\s*$",
                RegexOptions.Compiled | RegexOptions.Multiline);


        /// <summary>
        /// Generate the report artifact
        /// </summary>
        /// <returns>IReport.</returns>
        /// <inheritdoc />
        public IReport Process() => ProcessOutput(RawOutput);

        /// <summary>
        /// Processes the output.
        /// </summary>
        /// <param name="output">The output.</param>
        /// <returns>TReport.</returns>
        internal RunawayReport ProcessOutput(string output)
        {
            var report = new RunawayReport
            {
                RawOutput = output
            };
            var modes = Regex.Split(output, "Kernel Mode Time");
            var matches = LineRegex.Matches(modes[0]);
            foreach (Match match in matches)
            {
                var line = new RunawayLine();
                report.RunawayLines.Add(line);
                var tidx = Convert.ToUInt32(match.Groups["idx"].Value);
                var tid = Convert.ToUInt32(match.Groups["id"].Value, 16);
                var days = Convert.ToUInt32(match.Groups["days"].Value);
                var time = TimeSpan.Parse(match.Groups["ts"].Value);
                time = time.Add(TimeSpan.FromDays(days));
                line.ThreadId = tid;
                line.ThreadIndex = tidx;
                line.UserModeTime = time;
            }

            if (modes.Length > 1)
            {
                matches = LineRegex.Matches(modes[1]);
                foreach (Match match in matches)
                {
                    var tid = Convert.ToUInt32(match.Groups["id"].Value, 16);
                    var days = Convert.ToUInt32(match.Groups["days"].Value);
                    var time = TimeSpan.Parse(match.Groups["ts"].Value);
                    time = time.Add(TimeSpan.FromDays(days));
                    var existingLine = report.RunawayLines.Single(line => line.ThreadId == tid);
                    existingLine.KernelModeTime = time;
                }
            }

            return report;
        }

        /// <summary>
        /// Prepare to generate report artifacts. This typically means using the debugger
        /// interface to run some commands and store those results. This method will be called
        /// on the main thread serially, and so it is not necessary to lock the debugger.
        /// </summary>
        /// <param name="debugger">The debugger.</param>
        /// <returns>IReport.</returns>
        /// <inheritdoc />
        public void Setup(IDebuggerProxy debugger)
        {
            RawOutput = debugger.Execute("!runaway 3");
        }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        /// <value>The display name.</value>
        public string DisplayName { get; } = "!runaway";

        /// <summary>
        /// Gets or sets the raw output.
        /// </summary>
        /// <value>The raw output.</value>
        public string RawOutput { get; set; }
    }
}