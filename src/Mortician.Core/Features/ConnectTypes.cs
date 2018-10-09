namespace Mortician.Core.Features
{
    public abstract partial class Feature
    {
        internal class ConnectTypesFeature : Feature
        {
            public override string ShortName { get; set; } = "ConnectTypes";
            public override string LongName { get; set; } = "ConnectTypes";
            public override string Description { get; set; } = "ConnectTypes";
        }
    }
}
