using System.ComponentModel.Composition;
using Triage.Mortician.Abstraction;

namespace Triage.Mortician
{
    public class Engine
    {
        [ImportMany]
        public IAnalyzer[] Analyzers { get; set; }
    }
}