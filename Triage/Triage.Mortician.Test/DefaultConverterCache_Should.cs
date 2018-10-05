using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Triage.Mortician.Test
{
    public class DefaultConverterCache_Should
    {
        [Fact]
        public void Return_The_Same_Object_When_Requested_Twice()
        {
            // arrange
            var sut = new DefaultConverterCache();
            var key = new StringBuilder(100);
            var val = new StringBuilder(100);

            // act
            var a = sut.GetOrAdd(key, () => val, () => val.Append("setup!"));
            var b = sut.GetOrAdd(key, () => val, () => val.Append("setup 2!"));

            // assert
            a.Should().BeSameAs(b);

            a.ToString().Should().Be("setup!");
        }

        [Fact]
        public void Throw_If_Null_Key_Is_Passed()
        {
            // arrange
            var sut = new DefaultConverterCache();
            Action mightThrow = () => sut.GetOrAdd<object>(null, () => 1);

            // act
            // assert
            mightThrow.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Return_True_Only_If_Item_Is_In_Map()
        {
            // arrange
            var sut = new DefaultConverterCache();
            var key = new StringBuilder(100);
            var val = new StringBuilder(100);

            // act
            var a = sut.GetOrAdd(key, () => val, () => val.Append("setup!"));

            // assert
            sut.Contains(key).Should().BeTrue();
        }
    }
}
