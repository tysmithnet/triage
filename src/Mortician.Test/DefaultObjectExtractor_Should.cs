using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Testing.Common;
using Xunit;

namespace Mortician.Test
{
    public class DefaultObjectExtractor_Should : BaseTest
    {
        [Fact]
        public void Always_Indicate_It_Can_Extract()
        {
            // arrange
            var extractor = new DefaultObjectExtractor();

            // act
            // assert
            extractor.CanExtract(0, null).Should().BeTrue();
            extractor.CanExtract(null, null).Should().BeTrue();
        }

        [Fact]
        public void Correctly_Extract_Objects()
        {
            var type = new ClrTypeBuilder()
                .WithName("Duke")
                .WithGetSize(42)
                .Build();

            var obj = new ClrObjectBuilder()
                .WithAddress(0x100)
                .WithSize(42)
                .WithType(type)
                .Build();

            var heap = new ClrHeapBuilder()
                .WithGetGeneration(1)
                .WithGetObjectType(obj.Type)
                .Build();

            var rt = new ClrRuntimeBuilder()
                .WithHeap(heap)
                .Build();
            
            

            // arrange
            var extractor = new DefaultObjectExtractor();

            // act
            var res = extractor.Extract(obj, rt);
            var res2 = extractor.Extract(0x100, rt);

            // assert
            res.Address.Should().Be(0x100);
            res.Gen.Should().Be(1);
            res.Size.Should().Be(42);
            res.FullTypeName.Should().Be("Duke");

            res2.Address.Should().Be(0x100);
            res2.Gen.Should().Be(1);
            res2.Size.Should().Be(42);
            res2.FullTypeName.Should().Be("Duke");
        }
    }
}
