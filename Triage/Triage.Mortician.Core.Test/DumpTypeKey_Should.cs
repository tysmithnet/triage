using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Triage.Testing.Common;
using Xunit;

namespace Triage.Mortician.Core.Test
{
    public class DumpTypeKey_Should : BaseTest
    {
        [Fact]
        public void Allow_Zero_And_Null()
        {
            // arrange
            Action throws = () => new DumpTypeKey(0, null);
            
            // act
            // assert
            throws.Should().NotThrow();
        }
    }
}
