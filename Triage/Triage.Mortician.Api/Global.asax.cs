using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dependencies;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Common.Logging;
using Microsoft.Diagnostics.Runtime;
using Triage.Mortician.Abstraction;
using Triage.Mortician.Analyzers;

namespace Triage.Mortician.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var log = LogManager.GetLogger(typeof(WebApiApplication));

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);    

            log.Trace("Looking for MEF components");
            var catalogs = new[]
            {
                new AssemblyCatalog(Assembly.GetAssembly(typeof(WebApiApplication))),
                new AssemblyCatalog(Assembly.GetAssembly(typeof(IDumpObject))),
                new AssemblyCatalog(Assembly.GetAssembly(typeof(ThreadBuildUpAnalyzer))),
            };
            var aggregateCatalog = new AggregateCatalog(catalogs);
            var compositionContainer = new CompositionContainer(aggregateCatalog);
            var heapObjectExtractors = compositionContainer.GetExportedValues<IDumpObjectExtractor>().ToList();

            DumpObjectRepository dumpObjectRepository;
            DebuggerProxy debuggerProxy;
            DumpThreadRepository dumpThreadRepository;
            // todo: error handling
            log.Trace("Loading memory dump");
            DataTarget dt = DataTarget.LoadCrashDump(@"C:\debug\x64\HelloWorld.exe_170803_222445.dmp");

            log.Trace("Creating CLR from dump");
            var rt = dt.ClrVersions.Single().CreateRuntime();
            var stopWatch = Stopwatch.StartNew();
            dumpObjectRepository = new DumpObjectRepository(rt, heapObjectExtractors);
            log.Trace($"DumpObjectRepository created in {TimeSpan.FromMilliseconds(stopWatch.ElapsedMilliseconds).ToString()}");
            debuggerProxy = new DebuggerProxy(dt.DebuggerInterface);
            stopWatch.Restart();
            dumpThreadRepository = new DumpThreadRepository(rt, debuggerProxy, dumpObjectRepository);
            log.Trace($"DumpThreadRepository created in {TimeSpan.FromMilliseconds(stopWatch.ElapsedMilliseconds).ToString()}");

            compositionContainer.ComposeExportedValue<IDumpObjectRepository>(dumpObjectRepository);
            compositionContainer.ComposeExportedValue<IDebuggerProxy>(debuggerProxy);
            compositionContainer.ComposeExportedValue<IDumpThreadRepository>(dumpThreadRepository);

            var engine = compositionContainer.GetExportedValue<Engine>();

            log.Trace("Starting processing...");
            engine.Process(CancellationToken.None).Wait();
            log.Trace("Processing complete!");

            var resolver = new MefDependencyResolver(compositionContainer);
            DependencyResolver.SetResolver(resolver);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;

        }
    }
                          
    public class MefDependencyResolver : System.Web.Http.Dependencies.IDependencyResolver, System.Web.Mvc.IDependencyResolver
    {
        private readonly CompositionContainer _container;

        public MefDependencyResolver(CompositionContainer container)
        {
            _container = container;
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }
                                                              
        public object GetService(Type serviceType)
        {
            if (serviceType == null)
                throw new ArgumentNullException(nameof(serviceType));

            var name = AttributedModelServices.GetContractName(serviceType);
            var export = _container.GetExportedValueOrDefault<object>(name);
            return export;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (serviceType == null)
                throw new ArgumentNullException(nameof(serviceType));

            var exports = _container.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
            return exports;
        }

        public void Dispose()
        {
        }
    }

}
