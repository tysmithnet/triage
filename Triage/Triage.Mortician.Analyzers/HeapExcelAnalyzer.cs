using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using DocumentFormat.OpenXml.Wordprocessing;
using SpreadsheetLight;
using SpreadsheetLight.Charts;
using Triage.Mortician.Abstraction;

namespace Triage.Mortician.Analyzers
{
    [Export(typeof(IExcelAnalyzer))]
    public class HeapExcelAnalyzer : IExcelAnalyzer
    {                                 
        public ILog Log { get; set; } = LogManager.GetLogger(typeof(HeapExcelAnalyzer));

        [Import]
        public IDumpObjectRepository DumpObjectRepository { get; set; }
        
        public Task Setup(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        
        public void Contribute(SLDocument sharedDocument)
        {
            

            var stats = new Dictionary<string, StatsLine>();  

            foreach (var obj in DumpObjectRepository.Get())
            {
                if(!stats.ContainsKey(obj.FullTypeName))
                    stats.Add(obj.FullTypeName, new StatsLine());

                switch (obj.Gen)
                {
                    case 0:
                        stats[obj.FullTypeName].Gen0Count++;
                        stats[obj.FullTypeName].Gen0Size += obj.Size;
                        break;
                    case 1:
                        stats[obj.FullTypeName].Gen1Count++;
                        stats[obj.FullTypeName].Gen1Size += obj.Size;
                        break;
                    case 2:
                        stats[obj.FullTypeName].Gen2Count++;
                        stats[obj.FullTypeName].Gen2Size += obj.Size;
                        break;
                    case 3:
                        stats[obj.FullTypeName].LohCount++;
                        stats[obj.FullTypeName].LohSize += obj.Size;
                        break;
                    default:
                        Log.Warn($"Found obj with invalid gc gen: {obj.FullTypeName}({obj.Address}) - gen{obj.Gen}");
                        continue;
                }
            }
            sharedDocument.SelectWorksheet("Object Counts");
            int count = 0;
            foreach (var statline in stats.OrderByDescending(s => s.Value.Gen0Count + s.Value.Gen1Count + s.Value.Gen2Count + s.Value.LohCount))
            {
                sharedDocument.SetCellValue(2 + count, 1, statline.Key);
                sharedDocument.SetCellValue(2 + count, 2, statline.Value.Gen0Count);
                sharedDocument.SetCellValue(2 + count, 3, statline.Value.Gen1Count);
                sharedDocument.SetCellValue(2 + count, 4, statline.Value.Gen2Count);
                sharedDocument.SetCellValue(2 + count, 5, statline.Value.LohCount);
                count++;
            }

            sharedDocument.SelectWorksheet("Object Sizes");
            count = 0;
            foreach (var statline in stats.OrderByDescending(s => s.Value.Gen0Size + s.Value.Gen1Size + s.Value.Gen2Size + s.Value.LohSize))
            {
                sharedDocument.SetCellValue(2 + count, 1, statline.Key);
                sharedDocument.SetCellValue(2 + count, 2, statline.Value.Gen0Size);
                sharedDocument.SetCellValue(2 + count, 3, statline.Value.Gen1Size);
                sharedDocument.SetCellValue(2 + count, 4, statline.Value.Gen2Size);
                sharedDocument.SetCellValue(2 + count, 5, statline.Value.LohSize);
                count++;
            }
        }

        private class StatsLine
        {
            public ulong Gen0Count { get; set; }
            public ulong Gen0Size { get; set; }
            public ulong Gen1Count { get; set; }
            public ulong Gen1Size { get; set; }
            public ulong Gen2Count { get; set; }
            public ulong Gen2Size { get; set; }
            public ulong LohCount { get; set; }
            public ulong LohSize { get; set; }
        }
    }  
}
