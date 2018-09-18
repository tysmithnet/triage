using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Triage.Mortician.Core;

namespace Triage.Mortician.Test.Builders
{
    public class AnalyzerBuilder
    {
        private Mock<IAnalyzer> _mock = new Mock<IAnalyzer>();
        
        public IAnalyzer Build()
        {
            return _mock.Object;
        }
    }
}
