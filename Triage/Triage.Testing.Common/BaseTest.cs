using System;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace Triage.Testing.Common
{
    public class BaseTest : IDisposable
    {
        public BaseTest()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.WithThreadId()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
                {
                    AutoRegisterTemplate = true,
                    AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6,
                    QueueSizeLimit = 1
                }).CreateLogger();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Log.CloseAndFlush();
        }
    }
}