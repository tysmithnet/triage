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
    public class DumpModuleRepository_Should : BaseTest
    {
        [Fact]
        public void Return_The_Correct_Value()
        {
            // arrange
            var a = new DumpModule(new DumpModuleKey(0, "0"));
            var b = new DumpModule(new DumpModuleKey(1, "1"));
            var modules = new Dictionary<DumpModuleKey, DumpModule>
            {
                [a.Key] = a,
                [b.Key] = b
            };
            var sut = new DumpModuleRepository(modules);
            
            // act
            // assert
            sut.Get(0, "0").Should().Be(a);
            sut.Modules.Should().HaveCount(2);
        }

        [Fact]
        public void Throw_If_Does_Not_Exist()
        {
            // arrange
            var modules = new Dictionary<DumpModuleKey, DumpModule>
            {
            };
            var sut = new DumpModuleRepository(modules);
            Action mightThrow = () => sut.Get(12, "");

            // act
            // assert
            mightThrow.Should().Throw<KeyNotFoundException>();
        }
    }
}
