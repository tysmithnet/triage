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
            sharedDocument.SelectWorksheet("Heap Instances");

            var stats = new Dictionary<string, StatsLine>();

            foreach (var obj in DumpObjectRepository.Get())
            {
                if(!stats.ContainsKey(obj.FullTypeName))
                    stats.Add(obj.FullTypeName, new StatsLine());

                switch (obj.Gen)
                {
                    case 0:
                        stats[obj.FullTypeName].Gen0++;
                        break;
                    case 1:
                        stats[obj.FullTypeName].Gen1++;
                        break;
                    case 2:
                        stats[obj.FullTypeName].Gen2++;
                        break;
                    case 3:
                        stats[obj.FullTypeName].Loh++;
                        break;
                    default:
                        Log.Warn($"Found obj with invalid gc gen: {obj.FullTypeName}({obj.Address}) - gen{obj.Gen}");
                        continue;
                }
            }

            int count = 0;
            foreach (var statline in stats.OrderByDescending(s => s.Value.Gen0 + s.Value.Gen1 + s.Value.Gen2 + s.Value.Loh))
            {
                sharedDocument.SetCellValue(2 + count, 1, statline.Key);
                sharedDocument.SetCellValue(2 + count, 2, statline.Value.Gen0);
                sharedDocument.SetCellValue(2 + count, 3, statline.Value.Gen1);
                sharedDocument.SetCellValue(2 + count, 4, statline.Value.Gen2);
                sharedDocument.SetCellValue(2 + count, 5, statline.Value.Loh);
                count++;
            }            
        }

        private class StatsLine
        {
            public long Gen0 { get; set; }
            public long Gen1 { get; set; }
            public long Gen2 { get; set; }
            public long Loh { get; set; }
        }
    }  
}
