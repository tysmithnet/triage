using System;
using System.Linq;
using System.Text.RegularExpressions;
using Triage.Mortician.Core;

namespace Triage.Mortician.Reports
{
    /// <summary>
    ///     Class EeStackOutputProcessor.
    /// </summary>
    /// <seealso cref="IEeStackOutputProcessor" />
    public class EeStackOutputProcessor : IEeStackOutputProcessor
    {
        private readonly Regex _basicLineRegex =
            new Regex(
                @"^(?<sp>[a-fA-F0-9]{8,16})\s*(?<ret>[a-fA-F0-9]{8,16})\s*(?<from>.*), calling (?<to>.*)$");

        private readonly Regex _frameRegex =
            new Regex(@"^(?<mod>.*)!(?<meth>[^+]+)(\+0x(?<off>[a-fA-F0-9]+))?", RegexOptions.Compiled);

        private readonly Regex _noTargetRegex =
            new Regex(
                @"^(?<sp>[a-fA-F0-9]{8,16})\s*(?<ret>[a-fA-F0-9]{8,16})\s*(!?<from>\(MethodDesc)$");

        private readonly Regex _singleManagedRegex =
            new Regex(
                @"^(?<sp>[a-fA-F0-9]+)\s*(?<ret>[a-fA-F0-9]+)\s*\(MethodDesc\s*(?<md>[a-fA-F0-9]+)\s*\+0x(?<off>[a-fA-F0-9]+)(?<rest>.*)\)$");

        private readonly Regex _threadIdRegex =
            new Regex(@"^Thread\s*(?<tidx>\d+)\s*$", RegexOptions.Compiled | RegexOptions.Multiline);

        private readonly Regex _managedMethodRegex = new Regex(@"\(MethodDesc \s*(?<md>[a-fA-F0-9]+)\s\+0x(?<off>[a-fA-F0-9]+)\s*(?<rest>.*)\)");

        /// <summary>
        ///     Processes the output.
        /// </summary>
        /// <param name="eeStackOutput">The ee stack output.</param>
        /// <returns>EeStackReport.</returns>
        public EeStackReport ProcessOutput(string eeStackOutput)
        {
            var report = new EeStackReport();
            var threadChunks = GetThreadChunks(eeStackOutput);

            foreach (var chunk in threadChunks)
            {
                var thread = new EeStackThread();
                var lines = chunk.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
                var head = lines.Take(3).ToArray();
                var body = lines.Skip(3).ToArray();
                var index = Convert.ToUInt32(_threadIdRegex.Match(head[0]).Groups["tidx"].Value);
                var currentFrameMatch = _frameRegex.Match(head[1]);
                thread.Index = index;
                var currentFrameModule = currentFrameMatch.Groups["mod"].Value;
                var currentFrameMethod = currentFrameMatch.Groups["meth"].Value;
                var currentFrameOffset = Convert.ToUInt64(currentFrameMatch.Groups["off"].Value, 16);
                thread.Location = new CodeLocation(currentFrameModule, currentFrameMethod, currentFrameOffset);
                foreach (var line in body)
                {
                    var frame = ExtractFrame(line);
                    thread.StackFramesInternal.Add(frame);
                }
            }

            return report;
        }

        internal EeStackFrame ExtractFrame(string line)
        {
            var frame = new EeStackFrame();
            var managedRegex = new Regex(@"(?<md>[a-fA-F0-9]+)\s(?<off>\+0x[a-fA-F0-9]+)?\s*(?<rest>.*)\)");
            var managedLineRegex = new Regex(@"\(MethodDesc", RegexOptions.Compiled);
            var isBothRegex = new Regex(@", calling", RegexOptions.Compiled);
            var headerRegex = new Regex(@"([a-fA-F0-9]+ ){2}");
            var header = headerRegex.Match(line);
            var hexRegex = new Regex(@"[a-fA-F0-9]+", RegexOptions.Compiled);
            var hexes = hexRegex.Matches(header.Value);
            var sp = Convert.ToUInt64(hexes[0].Value, 16);
            var ret = Convert.ToUInt64(hexes[1].Value, 16);
            frame.ChildStackPointer = sp;
            frame.ReturnAddress = ret;
            var body = line.Substring(header.Value.Length);
            var isManagedLine = managedLineRegex.IsMatch(body);
            var isBoth = isBothRegex.IsMatch(body);
            if (isManagedLine)
            {
                if (isBoth)
                {
                    var halves = isBothRegex.Split(body).Select(x => x.Replace("(MethodDesc", "")).ToArray();
                    var first = managedRegex.Match(halves[0]);
                    var second = managedRegex.Match(halves[1]);
                    var callerMd = Convert.ToUInt64(first.Groups["md"].Value, 16);
                    var callerOff = first.Groups["off"].Success ? Convert.ToUInt64(first.Groups["off"].Value, 16) : 0;
                    var calleeMd = Convert.ToUInt64(second.Groups["md"].Value, 16);
                    var calleeOff = second.Groups["off"].Success ? Convert.ToUInt64(second.Groups["off"].Value, 16) : 0;
                    var caller = new ManagedCodeLocation(callerMd, callerOff, first.Groups["rest"].Value.Trim());
                    var callee = new ManagedCodeLocation(calleeMd, calleeOff, second.Groups["rest"].Value.Trim());
                    frame.Caller = caller;
                    frame.Callee = callee;
                }
                else
                {
                    var first = managedRegex.Match(body.Replace("(MethodDesc", ""));
                    var callerMd = Convert.ToUInt64(first.Groups["md"].Value, 16);
                    var callerOff = first.Groups["off"].Success ? Convert.ToUInt64(first.Groups["off"].Value, 16) : 0;
                    var caller = new ManagedCodeLocation(callerMd, callerOff, first.Groups["rest"].Value.Trim());
                    frame.Caller = caller;
                }
            }
            

            return frame;
        }

        internal string[] GetThreadChunks(string eeStackOutput)
        {
            // todo: account for error messages
            return Regex.Split(eeStackOutput, "---------------------------------------------")
                .Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
        }
    }
}