namespace Mortician.Core.Features
{
    public abstract partial class Feature
    {
        internal class CreateReportsFeature : Feature
        {
            public override string ShortName { get; set; } = "CreateReports";
            public override string LongName { get; set; } = "CreateReports";
            public override string Description { get; set; } = "CreateReports";
        }
    }
}
