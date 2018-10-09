namespace Mortician.Core.Features
{
    public abstract partial class Feature
    {
        internal class CreateBlockingObjectsFeature : Feature
        {
            public override string ShortName { get; set; } = "CreateBlockingObjects";
            public override string LongName { get; set; } = "CreateBlockingObjects";
            public override string Description { get; set; } = "CreateBlockingObjects";
        }
    }
}
