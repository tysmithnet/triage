namespace Mortician.Core.Features
{
    public abstract partial class Feature
    {
        internal class CreateHeapSegmentsFeature : Feature
        {
            public override string ShortName { get; set; } = "CreateHeapSegments";
            public override string LongName { get; set; } = "CreateHeapSegments";
            public override string Description { get; set; } = "CreateHeapSegments";
        }
    }
}
