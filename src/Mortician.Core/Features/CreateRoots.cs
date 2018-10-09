namespace Mortician.Core.Features
{
    public abstract partial class Feature
    {
        internal class CreateRootsFeature : Feature
        {
            public override string ShortName { get; set; } = "CreateRoots";
            public override string LongName { get; set; } = "CreateRoots";
            public override string Description { get; set; } = "CreateRoots";
        }
    }
}
