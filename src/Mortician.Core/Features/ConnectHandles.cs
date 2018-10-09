namespace Mortician.Core.Features
{
    public abstract partial class Feature
    {
        internal class ConnectHandlesFeature : Feature
        {
            public override string ShortName { get; set; } = "ConnectHandles";
            public override string LongName { get; set; } = "ConnectHandles";
            public override string Description { get; set; } = "ConnectHandles";
        }
    }
}
