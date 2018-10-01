using FluentAssertions;
using Triage.Testing.Common;
using Xunit;

namespace Triage.Mortician.Test
{
    public class StringExtractor_Should : BaseTest
    {
        [Fact]
        public void Extract_The_Correct_String_Value()
        {
            // arrange
            var extractor = new StringExtractor();
            var builder = new ClrObjectBuilder();
            builder
                .WithType("System.String")
                .WithAddress(0x1234)
                .WithSize(100)
                .WithSimpleValue("Duke the corgi");
            var runtimeBuilder = new ClrRuntimeBuilder();
            var heapBuilder = new ClrHeapBuilder();
            heapBuilder.WithGetGeneration(2);
            runtimeBuilder.WithHeap(heapBuilder.Build());
            var runtime = runtimeBuilder.Build();
            var built = builder.Build();

            // act
            var result = (StringDumpObject) extractor.Extract(built, runtime);

            // assert
            result.Address.Should().Be(0x1234);
            result.Size.Should().Be(100);
            result.Gen.Should().Be(2);
            result.FullTypeName.Should().Be("System.String");
            result.Value.Should().Be("Duke the corgi");
        }

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