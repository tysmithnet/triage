using System;
using System.Collections.Generic;
using FluentAssertions;
using Mortician.Core;
using Mortician.Repositories;
using Testing.Common;
using Xunit;

namespace Mortician.Test.Repositories
{
    public class DumpObjectRepository_Should : BaseTest
    {
        [Fact]
        public void Return_Requested_Objects()
        {
            // arrange
            var dumpObjects = new Dictionary<ulong, DumpObject>()
            {
                [0] = new DumpObject(0, "", 0, 0)
            };

            var dumpObjectRoots = new Dictionary<ulong, DumpObjectRoot>()
            {
                [1] = new DumpObjectRoot()
                {
                    Address = 1
                },
                [2] = new DumpObjectRoot()
                {
                    Address = 2
                }
            };
            var dumpBlockingObjects = new Dictionary<ulong, DumpBlockingObject>()
            {
                [3] = new DumpBlockingObject()
                {
                    Address = 3
                }
            };
            
            var finalizerQueue = new Dictionary<ulong, DumpObject>()
            {
                [4] = new DumpObject()
                {
                    Address = 4
                },
                [5] = new DumpObject()
                {
                    Address = 5
                }
            };

            var sut = new DumpObjectRepository(dumpObjects,
                dumpObjectRoots, dumpBlockingObjects, finalizerQueue);

            Action mightThrow0 = () => sut.GetObject(0x1234);
            Action mightThrow1 = () => sut.GetBlockingObject(0x1234);
            Action mightThrow2 = () => sut.GetFinalizeQueueObject(0x2123);
            Action mightThrow3 = () => sut.GetObjectRoot(0x2123);

            // act
            // assert
            sut.GetObject(0).Should().BeSameAs(dumpObjects[0]);
            sut.Objects.Should().HaveCount(1);
            sut.GetBlockingObject(3).Should().BeSameAs(dumpBlockingObjects[3]);
            sut.BlockingObjects.Should().HaveCount(1);
            sut.GetObjectRoot(1).Should().BeSameAs(dumpObjectRoots[1]);
            sut.ObjectRoots.Should().HaveCount(2);
            sut.GetFinalizeQueueObject(4).Should().BeSameAs(finalizerQueue[4]);
            sut.FinalizerQueue.Should().HaveCount(2);
            mightThrow0.Should().Throw<IndexOutOfRangeException>();
            mightThrow1.Should().Throw<IndexOutOfRangeException>();
            mightThrow2.Should().Throw<IndexOutOfRangeException>();
            mightThrow3.Should().Throw<IndexOutOfRangeException>();
        }
    }
}