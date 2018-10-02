using System;
using FluentAssertions;
using Triage.Testing.Common;
using Xunit;

namespace Triage.Mortician.Core.Test
{
    public class DumpThread_Should : BaseTest
    {
        [Fact]
        public void Return_The_Correct_StackTrace()
        {
            // arrange
            var dumpThread = new DumpThread();
            var items = new[]
            {
                new DumpStackFrame
                {
                    DisplayString = "Foo()"
                },
                new DumpStackFrame
                {
                    DisplayString = "Bar()"
                }
            };
            foreach (var dumpStackFrame in items) dumpThread.ManagedStackFramesInternal.Add(dumpStackFrame);

            // act
            // assert
            dumpThread.StackTrace.Should().Be("Foo()\nBar()");
            dumpThread.StackTrace.Should().Be("Foo()\nBar()");
        }

        [Fact]
        public void Exhibit_Entity_Equality()
        {
            // arrange
            var a = new DumpThread()
            {
                OsId = 0
            };
            var b = new DumpThread
            {
                OsId = 0
            };
            var c = new DumpThread
            {
                OsId = 1
            };

            // act
            // assert
            a.GetHashCode().Should().Be(b.GetHashCode());
            a.GetHashCode().Should().NotBe(c.GetHashCode());

            a.Equals(a).Should().BeTrue();
            a.Equals(b).Should().BeTrue();
            a.Equals(c).Should().BeFalse();
            a.Equals(null).Should().BeFalse();
            a.Equals("").Should().BeFalse();
            a.CompareTo(a).Should().Be(0);
            a.CompareTo(b).Should().Be(0);
            a.CompareTo(c).Should().Be(-1);
            a.CompareTo(null).Should().Be(1);
            a.Equals((object)a).Should().BeTrue();
            a.Equals((object)b).Should().BeTrue();
            a.Equals((object)c).Should().BeFalse();
            a.Equals((object)null).Should().BeFalse();
            a.CompareTo((object)a).Should().Be(0);
            a.CompareTo((object)b).Should().Be(0);
            a.CompareTo((object)c).Should().Be(-1);
            a.CompareTo((object)null).Should().Be(1);
            (a < b).Should().BeFalse();
            (a <= b).Should().BeTrue();
            (c > a).Should().BeTrue();
            (c >= a).Should().BeTrue();
            Action throws = () => a.CompareTo("");
            throws.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Add_A_Root_Only_Once()
        {
            // arrange
            var dumpThread = new DumpThread();
            var root = new DumpObjectRoot();

            // act
            dumpThread.AddRoot(root);
            dumpThread.AddRoot(root);

            // assert
            dumpThread.Roots.Should().HaveCount(1);
        }

        [Fact]
        public void Add_A_Blocking_Object_Only_Once()
        {
            // arrange
            var dumpThread = new DumpThread();
            var blockingObject = new DumpBlockingObject();

            // act
            dumpThread.AddBlockingObject(blockingObject);
            dumpThread.AddBlockingObject(blockingObject);

            // assert
            dumpThread.BlockingObjects.Should().HaveCount(1);
        }
    }
}