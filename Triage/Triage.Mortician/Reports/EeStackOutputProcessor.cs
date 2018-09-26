// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-24-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-25-2018
// ***********************************************************************
// <copyright file="EeStackOutputProcessor.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Linq;
using System.Text.RegularExpressions;
using Triage.Mortician.Core;

namespace Triage.Mortician.Reports
{
    /// <summary>
    ///     Class EeStackOutputProcessor.
    /// </summary>
    /// <seealso cref="Triage.Mortician.Reports.IEeStackOutputProcessor" />
    /// <seealso cref="IEeStackOutputProcessor" />
    public class EeStackOutputProcessor : IEeStackOutputProcessor
    {
        /// <summary>
        ///     The frame regex
        /// </summary>
        private readonly Regex _frameRegex =
            new Regex(@"^(?<mod>.*)!(?<meth>[^+]+)(\+0x(?<off>[a-fA-F0-9]+))?", RegexOptions.Compiled);

        /// <summary>
        ///     The thread identifier regex
        /// </summary>
        private readonly Regex _threadIdRegex =
            new Regex(@"^Thread\s*(?<tidx>\d+)\s*$", RegexOptions.Compiled | RegexOptions.Multiline);

        /// <summary>
        ///     The error regex
        /// </summary>
        private static readonly Regex ErrorRegex = new Regex(@"\*\*\* ERROR: ", RegexOptions.Compiled);

        /// <summary>
        ///     The header regex
        /// </summary>
        private static readonly Regex HeaderRegex = new Regex(@"([a-fA-F0-9]+ ){2}");

        /// <summary>
        ///     The hexadecimal regex
        /// </summary>
        private static readonly Regex HexRegex = new Regex(@"[a-fA-F0-9]+", RegexOptions.Compiled);

        /// <summary>
        ///     The is caller and callee regex
        /// </summary>
        private static readonly Regex IsCallerAndCalleeRegex = new Regex(@", calling", RegexOptions.Compiled);

        /// <summary>
        ///     The managed regex
        /// </summary>
        private static readonly Regex ManagedRegex =
            new Regex(@"(?<md>[a-fA-F0-9]+)\s(?<off>\+0x[a-fA-F0-9]+)?\s*(?<rest>.*)\)", RegexOptions.Compiled);

        /// <summary>
        ///     The native regex
        /// </summary>
        private static readonly Regex NativeRegex =
            new Regex(@"(?<mod>[a-zA-Z0-9_\-.]+)!(?<meth>[a-zA-Z0-9_\-.]+)(\+0x)?(?<off>[a-fA-F0-9]+)?",
                RegexOptions.Compiled);

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
                var thread = ExtractThread(chunk);
                report.ThreadsInternal.Add(thread);
            }

            return report;
        }

        /// <summary>
        ///     Extracts the frame.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <returns>EeStackFrame.</returns>
        internal EeStackFrame ExtractFrame(string line)
        {
            var frame = new EeStackFrame();
            var header = HeaderRegex.Match(line);
            var hexes = HexRegex.Matches(header.Value);
            var sp = Convert.ToUInt64(hexes[0].Value, 16);
            var ret = Convert.ToUInt64(hexes[1].Value, 16);
            frame.ChildStackPointer = sp;
            frame.ReturnAddress = ret;
            var body = line.Substring(header.Value.Length);
            var isCallerAndCallee = IsCallerAndCalleeRegex.IsMatch(body);
            if (isCallerAndCallee)
            {
                var halves = IsCallerAndCalleeRegex.Split(body).Select(x => x.Replace("(MethodDesc", "")).ToArray();
                var callerManaged = ManagedRegex.Match(halves[0]);
                var callerNative = NativeRegex.Match(halves[0]);
                var calleeManaged = ManagedRegex.Match(halves[1]);
                var calleeNative = NativeRegex.Match(halves[1]);
                if (callerManaged.Success)
                    frame.Caller = ExtractManagedCodeLocation(callerManaged);
                else if (callerNative.Success) frame.Caller = ExtractNativeCodeLocation(callerNative);

                if (calleeManaged.Success)
                    frame.Callee = ExtractManagedCodeLocation(calleeManaged);
                else if (calleeNative.Success) frame.Callee = ExtractNativeCodeLocation(calleeNative);
            }
            // caller only
            else
            {
                body = body.Replace("(MethodDesc", "");
                var managed = ManagedRegex.Match(body);
                var native = NativeRegex.Match(body);
                if (managed.Success)
                    frame.Caller = ExtractManagedCodeLocation(managed);
                else if (native.Success) frame.Caller = ExtractNativeCodeLocation(native);
            }

            return frame;
        }

        /// <summary>
        ///     Extracts the thread.
        /// </summary>
        /// <param name="chunk">The chunk.</param>
        /// <returns>EeStackThread.</returns>
        internal EeStackThread ExtractThread(string chunk)
        {
            var thread = new EeStackThread();
            var lines = Regex.Split(chunk, "\n").Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
            var head = lines.Take(3).ToArray();
            var body = lines.Skip(3).ToArray();
            var index = Convert.ToUInt32(_threadIdRegex.Match(head[0]).Groups["tidx"].Value);
            var currentFrameMatch = _frameRegex.Match(head[1]);
            thread.Index = index;
            var currentFrameModule = currentFrameMatch.Groups["mod"].Value;
            var currentFrameMethod = currentFrameMatch.Groups["meth"].Value;
            var currentFrameOffset = Convert.ToUInt64(currentFrameMatch.Groups["off"].Value, 16);
            thread.CurrentLocation = new CodeLocation(currentFrameModule, currentFrameMethod, currentFrameOffset);
            for (var i = 0; i < body.Length; i++)
            {
                var line = body[i];
                var errorMatch = ErrorRegex.Match(line);
                if (errorMatch.Success)
                {
                    line = line.Substring(0, errorMatch.Index);
                    line += body[i + 1];
                    i++;
                }

                var frame = ExtractFrame(line);
                thread.StackFramesInternal.Add(frame);
            }

            return thread;
        }

        /// <summary>
        ///     Gets the thread chunks.
        /// </summary>
        /// <param name="eeStackOutput">The ee stack output.</param>
        /// <returns>System.String[].</returns>
        internal string[] GetThreadChunks(string eeStackOutput)
        {
            // todo: account for error messages
            return Regex.Split(eeStackOutput, "---------------------------------------------")
                .Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
        }

        /// <summary>
        ///     Extracts the managed code location.
        /// </summary>
        /// <param name="nativeMatch">The native match.</param>
        /// <returns>CodeLocation.</returns>
        private CodeLocation ExtractManagedCodeLocation(Match nativeMatch)
        {
            var methodDescriptor = Convert.ToUInt64(nativeMatch.Groups["md"].Value, 16);
            var offset = nativeMatch.Groups["off"].Success ? Convert.ToUInt64(nativeMatch.Groups["off"].Value, 16) : 0;
            var method = nativeMatch.Groups["rest"].Value.Trim();
            return new ManagedCodeLocation(methodDescriptor, offset, method);
        }

        /// <summary>
        ///     Extracts the native code location.
        /// </summary>
        /// <param name="nativeMatch">The native match.</param>
        /// <returns>CodeLocation.</returns>
        private CodeLocation ExtractNativeCodeLocation(Match nativeMatch)
        {
            var module = nativeMatch.Groups["mod"].Value;
            var method = nativeMatch.Groups["meth"].Value;
            var offset = nativeMatch.Groups["off"].Success ? Convert.ToUInt64(nativeMatch.Groups["off"].Value, 16) : 0;
            return new CodeLocation(module, method, offset);
        }
    }
}