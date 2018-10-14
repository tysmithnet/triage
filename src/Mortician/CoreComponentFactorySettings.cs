using System.ComponentModel.Composition;
using Mortician.Core;

namespace Mortician
{
    [Export(typeof(ISettings))]
    internal class CoreComponentFactorySettings : ISettings
    {
        internal abstract class SubSettings
        {
            public bool Enabled { get; set; }

        }

        internal class ReportSubSettings : SubSettings
        {
            public bool IncludeMethods { get; set; } = true;
            public bool IncludeFields { get; set; } = true;
        }

        internal class ObjectSubSettings : SubSettings
        {
            public bool IncludeCustomExtractors { get; set; } = true;
        }

        internal class TypeSubSettings : SubSettings
        {
            public bool IncludeMethods { get; set; } = true;
            public bool IncludeFields { get; set; } = true;
            public bool IncludeStaticFields { get; set; } = true;
        }

        public ReportSubSettings ReportSettings { get; set; } = new ReportSubSettings();
        public ObjectSubSettings ObjectSettings { get; set; } = new ObjectSubSettings();
        public TypeSubSettings TypeSettings { get; set; } = new TypeSubSettings();
    }
}