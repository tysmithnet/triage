using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Triage.Mortician.Core.ClrMdAbstractions;
using Xunit;

namespace Triage.Mortician.Test
{
    public class StringExtractor_Should
    {
        [Fact]
        public void Indicate_It_Can_Extract_Only_Strings()
        {
            // arrange
            var extractor = new StringExtractor();
            var objects = new[]
            {
                stringMock.Object,
                objectMock.Object
            };

            // act

            // assert
        }
    }
}
