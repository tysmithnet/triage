// ***********************************************************************
// Assembly         : Triage.Mortician.ExcelAnalyzer
// Author           : @tysmithnet
// Created          : 01-14-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="HeapExcelAnalyzer.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Serilog;
using SpreadsheetLight;
using Triage.Mortician.Core;
using Slog = Serilog.Log;

namespace Triage.Mortician.ExcelAnalyzer
{
    /// <summary>
    ///     Represents an excel analyzer that is capable of producing a report based on the objects in the heap
    /// </summary>
    /// <seealso cref="Triage.Mortician.ExcelAnalyzer.IExcelAnalyzer" />
    /// <inheritdoc />
    /// <seealso cref="T:Triage.Mortician.ExcelAnalyzer.IExcelAnalyzer" />
    [Export(typeof(IExcelAnalyzer))]
    public class HeapExcelAnalyzer : IExcelAnalyzer
    {

        internal ILogger Log { get; } = Slog.ForContext<HeapExcelAnalyzer>();
        /// <summary>
        ///     Contributes the specified shared document.
        /// </summary>
        /// <param name="sharedDocument">The shared document.</param>
        /// <inheritdoc />
        public void Contribute(SLDocument sharedDocument)
        {
            var stats = new Dictionary<string, StatsLine>();

            foreach (var obj in DumpObjectRepository.Objects)
            {
                if (!stats.ContainsKey(obj.FullTypeName))
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
                        Log.Warning("Found obj with invalid gc gen: {FullTypeName}({Address}) - gen{Gen}", obj.FullTypeName, obj.Address, obj.Gen);
                        continue;
                }
            }

            sharedDocument.SelectWorksheet("Object Counts");
            var count = 0;
            foreach (var statline in stats.OrderByDescending(s =>
                s.Value.Gen0Count + s.Value.Gen1Count + s.Value.Gen2Count + s.Value.LohCount))
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
            foreach (var statline in stats.OrderByDescending(s =>
                s.Value.Gen0Size + s.Value.Gen1Size + s.Value.Gen2Size + s.Value.LohSize))
            {
                sharedDocument.SetCellValue(2 + count, 1, statline.Key);
                sharedDocument.SetCellValue(2 + count, 2, statline.Value.Gen0Size);
                sharedDocument.SetCellValue(2 + count, 3, statline.Value.Gen1Size);
                sharedDocument.SetCellValue(2 + count, 4, statline.Value.Gen2Size);
                sharedDocument.SetCellValue(2 + count, 5, statline.Value.LohSize);
                count++;
            }
        }

        /// <summary>
        ///     Setups the specified cancellation token.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task, that when complete will signal the setup completion</returns>
        /// <inheritdoc />
        public Task Setup(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        ///     Gets or sets the dump object repository.
        /// </summary>
        /// <value>The dump object repository.</value>
        [Import]
        protected internal IDumpObjectRepository DumpObjectRepository { get; set; }


        /// <summary>
        ///     DTO for the running totals
        /// </summary>
        private class StatsLine
        {
            /// <summary>
            ///     Gets or sets the gen0 count.
            /// </summary>
            /// <value>The gen0 count.</value>
            public ulong Gen0Count { get; set; }

            /// <summary>
            ///     Gets or sets the size of the gen0.
            /// </summary>
            /// <value>The size of the gen0.</value>
            public ulong Gen0Size { get; set; }

            /// <summary>
            ///     Gets or sets the gen1 count.
            /// </summary>
            /// <value>The gen1 count.</value>
            public ulong Gen1Count { get; set; }

            /// <summary>
            ///     Gets or sets the size of the gen1.
            /// </summary>
            /// <value>The size of the gen1.</value>
            public ulong Gen1Size { get; set; }

            /// <summary>
            ///     Gets or sets the gen2 count.
            /// </summary>
            /// <value>The gen2 count.</value>
            public ulong Gen2Count { get; set; }

            /// <summary>
            ///     Gets or sets the size of the gen2.
            /// </summary>
            /// <value>The size of the gen2.</value>
            public ulong Gen2Size { get; set; }

            /// <summary>
            ///     Gets or sets the loh count.
            /// </summary>
            /// <value>The loh count.</value>
            public ulong LohCount { get; set; }

            /// <summary>
            ///     Gets or sets the size of the loh.
            /// </summary>
            /// <value>The size of the loh.</value>
            public ulong LohSize { get; set; }
        }
    }
}