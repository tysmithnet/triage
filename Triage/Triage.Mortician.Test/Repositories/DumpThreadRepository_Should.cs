using System;
using System.Collections.Generic;
using FluentAssertions;
using Triage.Mortician.Core;
using Triage.Mortician.Repositories;
using Triage.Testing.Common;
using Xunit;

namespace Triage.Mortician.Test.Repositories
{
    public class DumpThreadRepository_Should : BaseTest
    {
        [Fact]
        public void Get_A_Thread_By_Os_Id()
        {
            // arrange
            var threads = new Dictionary<uint, DumpThread>
            {
                [0x42] = new DumpThread
                {
                    OsId = 42
                },
                [0x1337] = new DumpThread
                {
                    OsId = 1337
                }
            };
            var sut = new DumpThreadRepository(threads);
            Action throws = () => sut.Get(0x123123);

            // act
            // assert
            sut.Get(0x42).Should().Be(threads[0x42]);
            sut.Get(0x1337).Should().Be(threads[0x1337]);
            sut.Threads.Should().HaveCount(2);
            throws.Should().Throw<KeyNotFoundException>();
        }

        [Fact]
        public void Throw_If_Matching_Thread_Isnt_Found()
        {
            // arrange
            var repo = new DumpThreadRepository(new Dictionary<uint, DumpThread>());
            Action throws = () => repo.Get(0x123414);

            // act
            // assert
            throws.Should().Throw<KeyNotFoundException>();
        }
    }
}