// ***********************************************************************
// Assembly         : Triage.Mortician.WebUiAnalyzer
// Author           : @tysmithnet
// Created          : 09-17-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-01-2018
// ***********************************************************************
// <copyright file="WebUiAnalyzer.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Triage.Mortician.Core;
using Slog = Serilog.Log;

namespace Triage.Mortician.WebUiAnalyzer
{
    /// <summary>
    ///     Class WebUiAnalyzer.
    /// </summary>
    /// <seealso cref="Triage.Mortician.Core.IAnalyzer" />
    /// <seealso cref="IAnalyzer" />
    [Export(typeof(IAnalyzer))]
    public class WebUiAnalyzer : IAnalyzer
    {
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
                Log.Error(e, "Error running web server: {Message}", e.Message);
                throw;
            }
        }

        /// <summary>
        ///     Performs any necessary setup prior to processing
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task that when complete will signal the completion of the setup procedure</returns>
        /// <inheritdoc />
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
                Log.Error(e, "Error setting up web server: {Message}", e.Message);
                throw;
            }

            return Task.CompletedTask;
        }

        /// <summary>
        ///     Gets or sets the dump application domain repository.
        /// </summary>
        /// <value>The dump application domain repository.</value>
        [Import]
        internal IDumpAppDomainRepository DumpAppDomainRepository { get; set; }

        /// <summary>
        ///     Gets or sets the dump information repository.
        /// </summary>
        /// <value>The dump information repository.</value>
        [Import]
        internal IDumpInformationRepository DumpInformationRepository { get; set; }

        /// <summary>
        ///     Gets or sets the dump module repository.
        /// </summary>
        /// <value>The dump module repository.</value>
        [Import]
        internal IDumpModuleRepository DumpModuleRepository { get; set; }

        /// <summary>
        ///     Gets or sets the dump object repository.
        /// </summary>
        /// <value>The dump object repository.</value>
        [Import]
        internal IDumpObjectRepository DumpObjectRepository { get; set; }

        /// <summary>
        ///     Gets or sets the dump thread repository.
        /// </summary>
        /// <value>The dump thread repository.</value>
        [Import]
        internal IDumpThreadRepository DumpThreadRepository { get; set; }

        /// <summary>
        ///     Gets or sets the dump type repository.
        /// </summary>
        /// <value>The dump type repository.</value>
        [Import]
        internal IDumpTypeRepository DumpTypeRepository { get; set; }

        /// <summary>
        ///     Gets or sets the event hub.
        /// </summary>
        /// <value>The event hub.</value>
        [Import]
        internal IEventHub EventHub { get; set; }

        /// <summary>
        ///     Gets the log.
        /// </summary>
        /// <value>The log.</value>
        internal ILogger Log { get; } = Slog.ForContext<WebUiAnalyzer>();

        /// <summary>
        ///     Gets or sets the host.
        /// </summary>
        /// <value>The host.</value>
        private IWebHost Host { get; set; }
    }
}