using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Triage.Mortician.WebUiAnalyzer
{
    /// <summary>
    ///     Class Startup.
    /// </summary>
    public class Startup
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Startup" /> class.
        /// </summary>
        /// <param name="env">The env.</param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        /// <summary>
        ///     Gets or sets the current environment.
        /// </summary>
        /// <value>The current environment.</value>
        private IHostingEnvironment CurrentEnvironment { get; set; }

        /// <summary>
        ///     Gets the configuration.
        /// </summary>
        /// <value>The configuration.</value>
        private IConfigurationRoot Configuration { get; }

        /// <summary>
        ///     Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
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

        /// <summary>
        ///     Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
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