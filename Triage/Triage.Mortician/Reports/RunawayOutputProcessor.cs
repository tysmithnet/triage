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
using System.Linq;
using System.Text.RegularExpressions;

namespace Triage.Mortician.Reports
{
    /// <summary>
    ///     Class RunawayOutputProcessor.
    /// </summary>
    /// <seealso cref="Triage.Mortician.Reports.IStandardReportOutputProcessor{Triage.Mortician.Reports.RunawayReport}" />
    public class RunawayOutputProcessor : IStandardReportOutputProcessor<RunawayReport>
    {
        /// <summary>
        ///     The line regex
        /// </summary>
        private static readonly Regex LineRegex =
            new Regex(@"^\s*(?<idx>\d+):(?<id>[a-fA-F0-9]+)\s*(?<days>\d+)\s*days\s(?<ts>[\d\.:]+)\s*$",
                RegexOptions.Compiled | RegexOptions.Multiline);

        /// <summary>
        ///     Processes the output.
        /// </summary>
        /// <param name="output">The output.</param>
        /// <returns>TReport.</returns>
        /// <inheritdoc />
        public RunawayReport ProcessOutput(string output)
        {
            var report = new RunawayReport();
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
    }
}