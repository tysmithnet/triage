// ***********************************************************************
// Assembly         : Triage.Testing.Common
// Author           : @tysmithnet
// Created          : 10-01-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-01-2018
// ***********************************************************************
// <copyright file="BaseTest.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace Triage.Testing.Common
{
    /// <summary>
    ///     Class BaseTest.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public class BaseTest : IDisposable
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseTest" /> class.
        /// </summary>
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

        /// <summary>
        ///     Disposes this instance.
        /// </summary>
        /// <inheritdoc />
        public void Dispose()
        {
            Log.CloseAndFlush();
        }
    }
}