namespace Mortician.Core.Features
{
    public abstract partial class Feature
    {
        internal class CreateThreadsFeature : Feature
        {
            public override string ShortName { get; set; } = "CreateThreads";
            public override string LongName { get; set; } = "CreateThreads";
            public override string Description { get; set; } = "CreateThreads";
        }
    }
}
