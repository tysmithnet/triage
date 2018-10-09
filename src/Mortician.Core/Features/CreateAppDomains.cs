namespace Mortician.Core.Features
{
    public abstract partial class Feature
    {
        internal class CreateAppDomainsFeature : Feature
        {
            public override string ShortName { get; set; } = "CreateAppDomains";
            public override string LongName { get; set; } = "CreateAppDomains";
            public override string Description { get; set; } = "CreateAppDomains";
        }
    }
}
