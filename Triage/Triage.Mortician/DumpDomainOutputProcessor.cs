using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Triage.Mortician
{
    /// <summary>
    ///     Class DumpDomainOutputProcessor.
    /// </summary>
    /// <seealso cref="Triage.Mortician.IDumpDomainOutputProcessor" />
    public class DumpDomainOutputProcessor : IDumpDomainOutputProcessor
    {
        private Regex HexRegex = new Regex("[a-fA-F0-9]+", RegexOptions.Compiled);
        private Regex StageRegex = new Regex(@"Stage:\s*(?<stage>\w+)", RegexOptions.Compiled);


        /// <summary>
        ///     Processes the output.
        /// </summary>
        /// <param name="output">The output.</param>
        /// <returns>DumpDomainReport.</returns>
        /// <exception cref="NotImplementedException"></exception>
        /// <inheritdoc />
        public DumpDomainReport ProcessOutput(string output)
        {
            var returnVal = new DumpDomainReport();
            var domainTextChunks = output.Split(new[] {"--------------------------------------"},
                StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < domainTextChunks.Length; i++)
            {
                var chunk = domainTextChunks[i];
                var chunkLines = chunk.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
                var currentAppDomain = new DumpDomainAppDomain();
                var currentAssembly = new DumpDomainAssembly();
                var currentModule = new DumpDomainModule();
                for (int j = 0; j < chunkLines.Length; j++)
                {
                    var line = chunkLines[j];
                    switch (j)
                    {
                        case 0: // address
                        {
                            var match = HexRegex.Match(line);
                            currentAppDomain.Address = match.Success ? Convert.ToUInt64(match.Value) : 0;
                            break; 
                        }
                        case 1: // low freq heap
                        {
                            var match = HexRegex.Match(line);
                            currentAppDomain.LowFrequencyHeap = match.Success ? Convert.ToUInt64(match.Value) : 0;
                            break; 
                        }
                        case 2: // high freq heap
                        {
                            var match = HexRegex.Match(line);
                            currentAppDomain.HighFrequencyHeap = match.Success ? Convert.ToUInt64(match.Value) : 0;
                            break; 
                        }
                        case 3: // stub heap
                        {
                            var match = HexRegex.Match(line);
                            currentAppDomain.StubHeap = match.Success ? Convert.ToUInt64(match.Value) : 0;
                            break; 
                        }
                        case 4: // stage
                        {
                            currentAppDomain.Stage = AppDomainStage.All.First(s => line.Contains(s.SosString));
                            break; 
                        }
                        case 5: // name
                        {
                            currentAppDomain.Name = line.Replace("Name:", "").Trim();
                            break; 
                        }
                        default:
                            
                            break; // assemblies
                    }
                }
            }

            return returnVal;
        }
    }
}