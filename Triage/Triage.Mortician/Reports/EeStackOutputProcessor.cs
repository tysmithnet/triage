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
            var basicMatch = _basicLineRegex.Match(line);
            if (basicMatch.Success)
            {
                var toMatch = _frameRegex.Match(basicMatch.Groups["to"].Value);
                var fromMatch = _frameRegex.Match(basicMatch.Groups["from"].Value);
                frame.ChildStackPointer = Convert.ToUInt64(basicMatch.Groups["sp"].Value, 16);
                frame.ReturnAddress = Convert.ToUInt64(basicMatch.Groups["ret"].Value, 16);
                var callerModule = fromMatch.Groups["mod"].Value.Trim();
                var callerMethod = fromMatch.Groups["meth"].Value.Trim();
                var callerOffset = Convert.ToUInt64(fromMatch.Groups["off"].Value, 16);
                frame.Caller = new CodeLocation(callerModule, callerMethod, callerOffset);
                if (toMatch.Success)
                {
                    var calleeModule = toMatch.Groups["mod"].Value.Trim();
                    var calleeMethod = toMatch.Groups["meth"].Value.Trim();
                    var calleeOffset = toMatch.Groups["off"].Success
                        ? Convert.ToUInt64(toMatch.Groups["off"].Value, 16)
                        : 0;
                    frame.Callee = new CodeLocation(calleeModule, calleeMethod, calleeOffset);
                    return frame;
                }
            }

            var singleManagedMatch = _singleManagedRegex.Match(line);
            if (singleManagedMatch.Success)
            {
                var sp = Convert.ToUInt64(singleManagedMatch.Groups["sp"].Value, 16);
                var ret = Convert.ToUInt64(singleManagedMatch.Groups["ret"].Value, 16);
                var md = Convert.ToUInt64(singleManagedMatch.Groups["md"].Value, 16);
                var offset = Convert.ToUInt64(singleManagedMatch.Groups["off"].Value, 16);
                var text = singleManagedMatch.Groups["rest"].Value.Trim();
                frame.ChildStackPointer = sp;
                frame.ReturnAddress = ret;
                frame.MethodDescriptor = md;
                frame.Caller = new ManagedCodeLocation(md, offset, text);
                return frame;
            }

            var noTargetMatch = _noTargetRegex.Match(line);
            if (noTargetMatch.Success)
            {
                var fromMatch = _frameRegex.Match(noTargetMatch.Groups["from"].Value);
                frame.ChildStackPointer = Convert.ToUInt64(noTargetMatch.Groups["sp"].Value, 16);
                frame.ReturnAddress = Convert.ToUInt64(noTargetMatch.Groups["ret"].Value, 16);
                var callerModule = fromMatch.Groups["mod"].Value.Trim();
                var callerMethod = fromMatch.Groups["meth"].Value.Trim();
                var callerOffset = Convert.ToUInt64(fromMatch.Groups["off"].Value, 16);
                frame.Caller = new CodeLocation(callerModule, callerMethod, callerOffset);
                return frame;
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