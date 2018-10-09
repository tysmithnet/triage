namespace Mortician.Core.Features
{
    public abstract partial class Feature
    {
        internal class CreateMemoryRegionsFeature : Feature
        {
            public override string ShortName { get; set; } = "CreateMemoryRegions";
            public override string LongName { get; set; } = "CreateMemoryRegions";
            public override string Description { get; set; } = "CreateMemoryRegions";
        }
    }
}
