using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
