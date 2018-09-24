using FluentAssertions;
using Xunit;

namespace Triage.Mortician.Test
{
    public class Sanity_Should
    {
        [Fact]
        public void Be_Restored()
        {
            true.Should().BeTrue("true is true");
        }
    }
}