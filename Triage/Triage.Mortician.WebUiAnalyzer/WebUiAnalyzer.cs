using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using Microsoft.AspNetCore.Hosting;
using Triage.Mortician.Repository;

namespace Triage.Mortician.WebUiAnalyzer
{
    /// <summary>
    ///     Class WebUiAnalyzer.
    /// </summary>
    /// <seealso cref="Triage.Mortician.IAnalyzer" />
    [Export(typeof(IAnalyzer))]
    public class WebUiAnalyzer : IAnalyzer
    {
        [Import]
        internal DumpInformationRepository DumpInformationRepository { get; set; }

        [Import]
        internal DumpObjectRepository DumpObjectRepository { get; set; }

        [Import]
        internal DumpAppDomainRepository DumpAppDomainRepository { get; set; }

        [Import]
        internal DumpModuleRepository DumpModuleRepository { get; set; }

        [Import]
        internal DumpThreadRepository DumpThreadRepository { get; set; }

        [Import]
        internal DumpTypeRepository DumpTypeRepository { get; set; }

        [Import]
        internal EventHub EventHub { get; set; }

        /// <summary>
        ///     Gets or sets the host.
        /// </summary>
        /// <value>The host.</value>
        private IWebHost Host { get; set; }

        /// <summary>
        ///     Gets the log.
        /// </summary>
        /// <value>The log.</value>
        private ILog Log { get; } = LogManager.GetLogger<WebUiAnalyzer>();

        /// <inheritdoc />
        /// <summary>
        ///     Performs any necessary setup prior to processing
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task that when complete will signal the completion of the setup procedure</returns>
        public Task Setup(CancellationToken cancellationToken)
        {
            try
            {
                BaseController.EventHub = EventHub;
                BaseController.DumpAppDomainRepository = DumpAppDomainRepository;
                BaseController.DumpInformationRepository = DumpInformationRepository;
                BaseController.DumpModuleRepository = DumpModuleRepository;
                BaseController.DumpObjectRepository = DumpObjectRepository;
                BaseController.DumpThreadRepository = DumpThreadRepository;
                BaseController.DumpTypeRepository = DumpTypeRepository;
                Host = new WebHostBuilder()
                    .UseKestrel()
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseStartup<Startup>()
                    .Build();                                          
            }
            catch (Exception e)
            {
                Log.Error($"Error setting up web server: {e.Message}");
                throw;
            }
            return Task.CompletedTask;
        }

        /// <summary>
        ///     Performs the analysis
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task that when complete will signal the completion of the setup procedure</returns>
        public async Task Process(CancellationToken cancellationToken)
        {
            try
            {
                await Host.RunAsync(cancellationToken);
            }
            catch (Exception e)
            {
                Log.Error($"Error running web server: {e.Message}");
                throw;
            }
        }
    }
}