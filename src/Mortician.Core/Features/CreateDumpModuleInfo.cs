namespace Mortician.Core.Features
{
    public abstract partial class Feature
    {
        internal class CreateDumpModuleInfoFeature : Feature
        {
            public override string ShortName { get; set; } = "CreateDumpModuleInfo";
            public override string LongName { get; set; } = "CreateDumpModuleInfo";
            public override string Description { get; set; } = "CreateDumpModuleInfo";
        }
    }
}
