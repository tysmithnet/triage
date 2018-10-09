namespace Mortician.Core.Features
{
    public abstract partial class Feature
    {
        internal class ConnectRootsFeature : Feature
        {
            public override string ShortName { get; set; } = "ConnectRoots";
            public override string LongName { get; set; } = "ConnectRoots";
            public override string Description { get; set; } = "ConnectRoots";
        }
    }
}
