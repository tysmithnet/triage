using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
