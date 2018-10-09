namespace Mortician.Core.Features
{
    public abstract partial class Feature
    {
        internal class ConnectThreadsFeature : Feature
        {
            public override string ShortName { get; set; } = "ConnectThreads";
            public override string LongName { get; set; } = "ConnectThreads";
            public override string Description { get; set; } = "ConnectThreads";
        }
    }
}
