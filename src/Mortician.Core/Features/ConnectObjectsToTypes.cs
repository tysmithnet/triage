namespace Mortician.Core.Features
{
    public abstract partial class Feature
    {
        internal class ConnectObjectsToTypesFeature : Feature
        {
            public override string ShortName { get; set; } = "ConnectObjectsToTypes";
            public override string LongName { get; set; } = "ConnectObjectsToTypes";
            public override string Description { get; set; } = "ConnectObjectsToTypes";
        }
    }
}
