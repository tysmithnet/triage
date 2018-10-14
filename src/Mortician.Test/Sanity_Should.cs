using FluentAssertions;
using Serilog;
using Testing.Common;
using Xunit;

namespace Mortician.Test
{
    public class Sanity_Should : BaseTest
    {
        [Fact]
        public void Be_Restored()
        {
            Log.Information("Sanity is being tested");
            true.Should().BeTrue("true is true");
        }
    }
}