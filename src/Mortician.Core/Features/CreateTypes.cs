namespace Mortician.Core.Features
{
    public abstract partial class Feature
    {
        internal class CreateTypesFeature : Feature
        {
            public override string ShortName { get; set; } = "CreateTypes";
            public override string LongName { get; set; } = "CreateTypes";
            public override string Description { get; set; } = "CreateTypes";
        }
    }
}
