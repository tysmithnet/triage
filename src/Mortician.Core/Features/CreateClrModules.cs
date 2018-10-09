namespace Mortician.Core.Features
{
    public abstract partial class Feature
    {
        internal class CreateClrModulesFeature : Feature
        {
            public override string ShortName { get; set; } = "CreateClrModules";
            public override string LongName { get; set; } = "CreateClrModules";
            public override string Description { get; set; } = "CreateClrModules";
        }
    }
}
