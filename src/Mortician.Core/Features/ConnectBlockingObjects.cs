namespace Mortician.Core.Features
{
    public abstract partial class Feature
    {
        internal class ConnectBlockingObjectsFeature : Feature
        {
            public override string ShortName { get; set; } = "ConnectBlockingObjects";
            public override string LongName { get; set; } = "ConnectBlockingObjects";
            public override string Description { get; set; } = "ConnectBlockingObjects";
        }
    }
}
