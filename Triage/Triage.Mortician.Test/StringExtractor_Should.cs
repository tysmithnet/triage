using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
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
            var stringBuilder = new ClrObjectBuilder();
            stringBuilder.WithType("System.String");
            var s = stringBuilder.Build();

            var builder = new ClrObjectBuilder();
            builder.WithType("System.Object");
            var o = builder.Build();

            // act
            // assert
            extractor.CanExtract(s, null).Should().BeTrue();
            extractor.CanExtract(o, null).Should().BeFalse();
        }
    }
}
