using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using Triage.Mortician.Abstraction;

namespace Triage.Mortician
{
    [Export]
    public class Engine
    {
        [ImportMany]
        public IAnalyzer[] Analyzers { get; set; }

        protected internal ILog Log { get; set; } = LogManager.GetLogger(typeof(Engine));

        public Task Process(CancellationToken cancellationToken)
        {
            if (Analyzers == null || Analyzers.Length == 0)
            {
                Log.Fatal("No analyzers were found!");
                return Task.FromResult(0);
            }
            
            Log.Trace("Engine starting...");

            var tasks = new List<Task>();

            foreach (var analyzer in Analyzers)
            {
                var copy = analyzer;
                var task = analyzer
                    .Setup(cancellationToken)
                    .ContinueWith(async t =>
                    {
                        if (t.IsCanceled)
                        {
                            await t;
                        }

                        if (t.IsFaulted)
                        {
                            Log.Error($"Analyzer: {copy.GetType()} failed", t.Exception);
                            await t;
                        }

                        try
                        {
                            cancellationToken.ThrowIfCancellationRequested();
                            await copy.Process(cancellationToken);
                        }
                        catch (Exception e)
                        { 
                            Log.Error($"Analyzer: {copy.GetType()} failed", e);
                        }
                    }, cancellationToken);

                tasks.Add(task);
            }

            return Task.WhenAll(tasks);
        }          
    }
}