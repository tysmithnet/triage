using System;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace Triage.Testing.Common
{
    public class BaseTest : IDisposable
    {
        /// <inheritdoc />
        public void Dispose()
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.WithThreadId()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
                {
                    AutoRegisterTemplate = true,
                    AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6,
                    QueueSizeLimit = 1
                }).CreateLogger();
        }
    }
}