namespace Mortician.Core.Features
{
    public abstract partial class Feature
    {
        internal class ConnectObjectsFeature : Feature
        {
            public override string ShortName { get; set; } = "ConnectObjects";
            public override string LongName { get; set; } = "ConnectObjects";
            public override string Description { get; set; } = "ConnectObjects";
        }
    }
}
