using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Triage.Mortician.Core.Test
{
    public class DumpThread_Should
    {
        [Fact]
        public void Return_The_Correct_StackTrace()
        {
            // arrange
            var dumpThread = new DumpThread();
            var items = new[]
            {
                new DumpStackFrame()
                {
                    DisplayString = "Foo()"
                },
                new DumpStackFrame()
                {
                    DisplayString = "Bar()"
                }
            };
            foreach (var dumpStackFrame in items)
            {
                dumpThread.ManagedStackFramesInternal.Add(dumpStackFrame);
            }

            // act
            // assert
            dumpThread.StackTrace.Should().Be("Foo()\nBar()");
        }
    }
}
