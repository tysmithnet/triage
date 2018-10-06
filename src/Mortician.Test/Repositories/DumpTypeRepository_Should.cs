using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Mortician.Core;
using Mortician.Repositories;
using Testing.Common;
using Xunit;

namespace Mortician.Test.Repositories
{
    public class DumpTypeRepository_Should : BaseTest
    {
        [Fact]
        public void Return_The_Correct_Value()
        {
            // arrange
            var a = new DumpType(new DumpTypeKey(0, "0"));
            var b = new DumpType(new DumpTypeKey(1, "1"));
            var dumpTypes = new Dictionary<DumpTypeKey, DumpType>
            {
                [a.Key] = a,
                [b.Key] = b
            };
            var sut = new DumpTypeRepository(dumpTypes);

            // act
            // assert
            sut.Get(new DumpTypeKey(0, "0")).Should().Be(a);
            sut.Types.Should().HaveCount(2);
        }

        [Fact]
        public void Throw_If_Does_Not_Exist()
        {
            // arrange
            var dumpTypes = new Dictionary<DumpTypeKey, DumpType>
            {
            };
            var sut = new DumpTypeRepository(dumpTypes);
            Action mightThrow = () => sut.Get(new DumpTypeKey(12, "12"));

            // act
            // assert
            mightThrow.Should().Throw<KeyNotFoundException>();
        }
    }
}
