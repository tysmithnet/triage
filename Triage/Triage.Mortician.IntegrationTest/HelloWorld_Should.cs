﻿using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Types;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using Triage.Mortician.Core;
using Triage.Mortician.IntegrationTest.Scenarios;
using Xunit;

namespace Triage.Mortician.IntegrationTest
{
    public class HelloWorld_Should : IDisposable
    {
        internal class TestAnalyzer : IAnalyzer
        {
            public int AppDomainCount { get; set; }

            /// <inheritdoc />
            public Task Process(CancellationToken cancellationToken)
            {
                AppDomainCount = AppDomainRepo.Get().Count();
                return Task.CompletedTask;
            }

            /// <inheritdoc />
            public Task Setup(CancellationToken cancellationToken) => Task.CompletedTask;

            [Import]
            public IDumpAppDomainRepository AppDomainRepo { get; set; }

            [Import]
            public IDumpInformationRepository DumpInfoRepo { get; set; }

            [Import]
            public IDumpModuleRepository ModuleRepo { get; set; }

            [Import]
            public IDumpObjectRepository ObjectRepo { get; set; }

            [Import]
            public IDumpThreadRepository ThreadRepo { get; set; }

            [Import]
            public IDumpTypeRepository TypeRepo { get; set; }
        }

        [Fact]
        public void Perform_Basic_Startup_Without_Failure()
        {
            // arrange
            Log.Logger = new LoggerConfiguration()
                .Enrich.WithThreadId()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
                {
                    AutoRegisterTemplate = true,
                    AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6,
                    QueueSizeLimit = 1,
                }).CreateLogger();
            var dumpFile = Scenario.HelloWorld.GetDumpFile();
            var options = new DefaultOptions
            {
                DumpFile = dumpFile.FullName,
                SettingsFile = "Settings/Mortician_Should.json"
            };
            var analyzer = new TestAnalyzer();

            // act
            var result = Program.DefaultExecution(options, container =>
            {
                container.ComposeParts(analyzer);
                container.ComposeExportedValue<IAnalyzer>(analyzer);
                return container;
            });

            // assert
            result.Should().Be(0);
            analyzer.AppDomainCount.Should().Be(3);
            analyzer.TypeRepo.Get().FirstOrDefault(t => t.Name == "Triage.TestApplications.Console.Person").Should()
                .NotBeNull();
            analyzer.TypeRepo.Get().FirstOrDefault(t => t.Name == "Triage.TestApplications.Console.Address").Should()
                .NotBeNull();
            analyzer.ThreadRepo.Get().Any(t =>
                t.ManagedStackFrames.Any(f => f.DisplayString.Contains("Triage.TestApplications.Console.Program.Main"))).Should().BeTrue();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Log.CloseAndFlush();
        }
    }
}