using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Triage.Mortician.Core;
using Triage.Mortician.Repositories;
using Triage.Testing.Common;
using Xunit;

namespace Triage.Mortician.Test.Repositories
{
    public class DumpHandleRepository_Should : BaseTest
    {
        [Fact]
        public void Get_A_Thread_By_Os_Id()
        {
            // arrange
            var dumpHandles = new Dictionary<ulong, DumpHandle>
            {
                [0x42] = new DumpHandle
                {
                    Address = 42
                },
                [0x1337] = new DumpHandle
                {
                    Address = 1337
                }
            };
            var sut = new DumpHandleRepository(dumpHandles);
            Action throws = () => sut.Get(0x123123);

            // act
            // assert
            sut.Get(0x42).Should().Be(dumpHandles[0x42]);
            sut.Get(0x1337).Should().Be(dumpHandles[0x1337]);
            sut.Handles.Should().HaveCount(2);
            throws.Should().Throw<KeyNotFoundException>();
        }

        [Fact]
        public void Throw_If_Matching_Thread_Isnt_Found()
        {
            // arrange
            var sut = new DumpHandleRepository(new Dictionary<ulong, DumpHandle>());
            Action throws = () => sut.Get(0x123414);

            // act
            // assert
            throws.Should().Throw<KeyNotFoundException>();
        }
    }
}
