namespace Mortician.Core.Features
{
    public abstract partial class Feature
    {
        internal class CreateObjectsFeature : Feature
        {
            public override string ShortName { get; set; } = "CreateObjects";
            public override string LongName { get; set; } = "CreateObjects";
            public override string Description { get; set; } = "CreateObjects";
        }
    }
}
