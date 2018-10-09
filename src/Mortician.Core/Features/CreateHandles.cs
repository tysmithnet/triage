namespace Mortician.Core.Features
{
    public abstract partial class Feature
    {
        internal class CreateHandlesFeature : Feature
        {
            public override string ShortName { get; set; } = "CreateHandles";
            public override string LongName { get; set; } = "CreateHandles";
            public override string Description { get; set; } = "CreateHandles";
        }
    }
}
