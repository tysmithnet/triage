namespace Mortician.Core.Features
{
    public abstract partial class Feature
    {
        internal class ConnectAppDomainsAndModulesFeature : Feature
        {
            public override string ShortName { get; set; } = "ConnectAppDomainsAndModules";
            public override string LongName { get; set; } = "ConnectAppDomainsAndModules";
            public override string Description { get; set; } = "ConnectAppDomainsAndModules";
        }
    }
}
