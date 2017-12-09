﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Common.Logging;
using Microsoft.Diagnostics.Runtime;            
using Triage.Mortician.Abstraction;
using Triage.Mortician.Analyzers;

namespace Triage.Mortician.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var log = LogManager.GetLogger(typeof(MvcApplication));

            log.Trace("Hello world");
            var catalogs = new []
            {
                new AssemblyCatalog(Assembly.GetAssembly(typeof(MvcApplication))),
                new AssemblyCatalog(Assembly.GetAssembly(typeof(IDumpObject))),
                new AssemblyCatalog(Assembly.GetAssembly(typeof(ThreadBuildUpAnalyzer))),
            };
            var aggregateCatalog = new AggregateCatalog(catalogs);
            var compositionContainer = new CompositionContainer(aggregateCatalog);
            var heapObjectExtractors = compositionContainer.GetExportedValues<IDumpObjectExtractor>().ToList();
            DumpObjectRepository dumpObjectRepository;
            DebuggerProxy debuggerProxy;
            DumpThreadRepository dumpThreadRepository;
            using (var dt = DataTarget.LoadCrashDump(@"C:\debug\x64\HelloWorld.exe_170803_222445.dmp"))
            {
                var rt = dt.ClrVersions.Single().CreateRuntime();
                var stopWatch = Stopwatch.StartNew();
                dumpObjectRepository = new DumpObjectRepository(rt, heapObjectExtractors);
                log.Trace($"DumpObjectRepository created in {TimeSpan.FromMilliseconds(stopWatch.ElapsedMilliseconds).ToString()}");
                debuggerProxy = new DebuggerProxy(dt.DebuggerInterface);
                stopWatch.Restart();
                dumpThreadRepository = new DumpThreadRepository(rt, debuggerProxy, dumpObjectRepository);
                log.Trace($"DumpThreadRepository created in {TimeSpan.FromMilliseconds(stopWatch.ElapsedMilliseconds).ToString()}");
            }

            compositionContainer.ComposeExportedValue<IDumpObjectRepository>(dumpObjectRepository);
            compositionContainer.ComposeExportedValue<IDebuggerProxy>(debuggerProxy);
            compositionContainer.ComposeExportedValue<IDumpThreadRepository>(dumpThreadRepository);

            var engine = compositionContainer.GetExportedValue<Engine>();

            log.Trace("Starting processing...");
            engine.Process(CancellationToken.None).Wait();
            log.Trace("Processing complete!");
        }
    }
}
