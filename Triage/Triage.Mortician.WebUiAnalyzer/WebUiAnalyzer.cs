using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Triage.Mortician.WebUiAnalyzer
{
    [Export(typeof(IAnalyzer))]
    public class WebUiAnalyzer : IAnalyzer
    {
        private IWebHost Host { get; set; }
        private ILog Log { get; } = LogManager.GetLogger<WebUiAnalyzer>();

        public Task Setup(CancellationToken cancellationToken)
        {
            try
            {
                Host = new WebHostBuilder()
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

    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new[] {"value1", "value2"};
        }
    }

    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        private IHostingEnvironment CurrentEnvironment { get; set; }
        private IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddRazorOptions(options =>
            {
                var previous = options.CompilationCallback;
                options.CompilationCallback = context =>
                {
                    previous?.Invoke(context);
                    var refs = AppDomain.CurrentDomain.GetAssemblies()
                        .Where(x => !x.IsDynamic)
                        .Select(x => MetadataReference.CreateFromFile(x.Location))
                        .ToList();
                    context.Compilation = context.Compilation.AddReferences(refs);
                };
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}