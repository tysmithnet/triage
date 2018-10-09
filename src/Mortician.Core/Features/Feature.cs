namespace Mortician.Core.Features
{
    public abstract partial class Feature
    {
        public abstract string ShortName { get; set; }
        public abstract string LongName { get; set; }
        public abstract string Description { get; set; }
        public static Feature CreateReports { get; } = new CreateReportsFeature();
        public static Feature CreateObjects { get; } = new CreateObjectsFeature();
        public static Feature CreateTypes { get; } = new CreateTypesFeature();
        public static Feature CreateAppDomains { get; } = new CreateAppDomainsFeature();
        public static Feature CreateClrModules { get; } = new CreateClrModulesFeature();
        public static Feature CreateBlockingObjects { get; } = new CreateBlockingObjectsFeature();
        public static Feature CreateRoots { get; } = new CreateRootsFeature();
        public static Feature CreateThreads { get; } = new CreateThreadsFeature();
        public static Feature CreateHandles { get; } = new CreateHandlesFeature();
        public static Feature CreateHeapSegments { get; } = new CreateHeapSegmentsFeature();
        public static Feature CreateDumpModuleInfo { get; } = new CreateDumpModuleInfoFeature();
        public static Feature CreateMemoryRegions { get; } = new CreateMemoryRegionsFeature();
        public static Feature ConnectObjects { get; } = new ConnectObjectsFeature();
        public static Feature ConnectObjectsToTypes { get; } = new ConnectObjectsToTypesFeature();
        public static Feature ConnectTypes { get; } = new ConnectTypesFeature();
        public static Feature ConnectThreads { get; } = new ConnectThreadsFeature();
        public static Feature ConnectAppDomainsAndModules { get; } = new ConnectAppDomainsAndModulesFeature();
        public static Feature ConnectBlockingObjects { get; } = new ConnectBlockingObjectsFeature();
        public static Feature ConnectHandles { get; } = new ConnectHandlesFeature();
        public static Feature ConnectRoots { get; } = new ConnectRootsFeature();
    }
}
