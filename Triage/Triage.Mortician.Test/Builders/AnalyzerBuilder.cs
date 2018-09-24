using Moq;
using Triage.Mortician.Core;

namespace Triage.Mortician.Test.Builders
{
    public class AnalyzerBuilder
    {
        private readonly Mock<IAnalyzer> _mock = new Mock<IAnalyzer>();

        public IAnalyzer Build() => _mock.Object;
    }
}