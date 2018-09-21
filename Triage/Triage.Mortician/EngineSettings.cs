using System.ComponentModel.Composition;
using Triage.Mortician.Core;

namespace Triage.Mortician
{
    [Export(typeof(ISettings))]
    [Export(typeof(EngineSettings))]
    public class EngineSettings : ISettings
    {
        public string TestString { get; set; }
    }
}