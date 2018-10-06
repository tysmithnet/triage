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
    public class DumpAppDomainRepository_Should : BaseTest
    {
        [Fact]
        public void Return_The_Correct_Value()
        {
            // arrange
            var a = new DumpAppDomain()
            {
                Address = 0
            };
            var b = new DumpAppDomain()
            {
                Address = 1
            };
            var appDomains = new Dictionary<ulong, DumpAppDomain>
            {
                [a.Address] = a,
                [b.Address] = b
            };
            var sut = new DumpAppDomainRepository(appDomains);

            // act
            // assert
            sut.Get(0).Should().Be(a);
            sut.AppDomains.Should().HaveCount(2);
        }

        [Fact]
        public void Throw_If_Does_Not_Exist()
        {
            // arrange
            var appDomains = new Dictionary<ulong, DumpAppDomain>
            {
            };
            var sut = new DumpAppDomainRepository(appDomains);
            Action mightThrow = () => sut.Get(12);

            // act
            // assert
            mightThrow.Should().Throw<KeyNotFoundException>();
        }
    }
}
