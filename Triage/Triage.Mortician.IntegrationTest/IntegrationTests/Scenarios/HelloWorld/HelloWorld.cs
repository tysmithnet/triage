using Microsoft.Diagnostics.Runtime;

namespace Triage.Mortician.Test.IntegrationTests.Scenarios
{
    public abstract partial class Scenario
    {
        public class HelloWorld : Scenario
        {
            /// <inheritdoc />
            public override DataTarget GetDataTarget()
            {
                return null;
            }
        }
    }
}
