using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Triage.Mortician.Core.Test
{
    public class DumpTypeKey_Should
    {
        [Fact]
        public void Disallow_Method_Table_Value_Of_Zero()
        {
            // arrange
            Action throws = () => new DumpTypeKey(0, "ThisDoesntMatter");
            
            // act
            // assert
            throws.Should().Throw<ArgumentOutOfRangeException>();
        }
    }
}
