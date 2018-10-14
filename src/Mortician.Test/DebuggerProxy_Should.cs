using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Diagnostics.Runtime.Interop;
using Moq;
using Testing.Common;
using Xunit;

namespace Mortician.Test
{
    public class DebuggerProxy_Should : BaseTest
    {
        public interface IComposite : IDebugClient, IDebugControl
        {

        }

        [Fact]
        public void Only_Allow_One_Thread_To_Use_It_At_A_Time()
        {
            // arrange
            var mock = new Mock<IComposite>();

            Func<DEBUG_OUTCTL, string, DEBUG_EXECUTE, int> valueFunction = (o, c, e) =>
            {
                Thread.Sleep(1000);
                return 0;
            };
            mock.Setup(
                    client => client.Execute(It.IsAny<DEBUG_OUTCTL>(), It.IsAny<string>(), It.IsAny<DEBUG_EXECUTE>()))
                .Returns(valueFunction);
            var proxy = new DebuggerProxy(mock.Object);
            int count = 0;

            var start = DateTime.Now;
            // act
            for (int i = 0; i < 3; i++)
            {
                ThreadPool.QueueUserWorkItem(state =>
                {
                    proxy.Execute("whatever");
                    Interlocked.Increment(ref count);
                });
            }

            while (count != 3)
            {
                Thread.Sleep(500);
            }

            // assert
            (DateTime.Now - start).Should().BeGreaterThan(TimeSpan.FromSeconds(2));
        }

        [Fact]
        public void Only_Add_Text_On_Non_Warning_Or_Error_Masks()
        {
            // arrange
            var mock = new Mock<IComposite>();
            var proxy = new DebuggerProxy(mock.Object);

            // act
            ((IDebugOutputCallbacks)proxy).Output(DEBUG_OUTPUT.ERROR, "a");
            ((IDebugOutputCallbacks)proxy).Output(DEBUG_OUTPUT.WARNING, "b");
            ((IDebugOutputCallbacks)proxy).Output(DEBUG_OUTPUT.EXTENSION_WARNING, "c");
            ((IDebugOutputCallbacks)proxy).Output(DEBUG_OUTPUT.SYMBOLS, "d");
            ((IDebugOutputCallbacks)proxy).Output(DEBUG_OUTPUT.NORMAL, "e");

            // assert
            proxy.Builder.ToString().Should().Be("e");
        }

        [Fact]
        public void Throw_If_Control_Throws()
        {
            // arrange
            var mock = new Mock<IComposite>();
            mock.Setup(composite =>
                    composite.Execute(It.IsAny<DEBUG_OUTCTL>(), It.IsAny<string>(), It.IsAny<DEBUG_EXECUTE>()))
                .Throws<Exception>();
            var proxy = new DebuggerProxy(mock.Object);
            Action mightThrow = () => proxy.Execute("whatever");

            // act
            // assert
            mightThrow.Should().Throw<COMException>();
        }

        [Fact]
        public void Throw_If_The_Wait_Timeout_Expired()
        {
            // arrange
            var mock = new Mock<IComposite>();
            Func<DEBUG_OUTCTL, string, DEBUG_EXECUTE, int> valueFunction = (o, c, e) =>
            {
                Thread.Sleep(1000);
                return 0;
            };
            mock.Setup(
                    client => client.Execute(It.IsAny<DEBUG_OUTCTL>(), It.IsAny<string>(), It.IsAny<DEBUG_EXECUTE>()))
                .Returns(valueFunction);
            var proxy = new DebuggerProxy(mock.Object);
            bool isSuccess = false;

            // act
            var tasks = Enumerable.Range(0, 3).Select(i => Task.Run(() =>
            {
                proxy.Execute("whatever", TimeSpan.FromMilliseconds(10));
            }));

            try
            {
                Task.WaitAll(tasks.ToArray());
            }
            catch (AggregateException ae)
            {
                ae.Flatten().Handle(exception =>
                {
                    if (exception is TimeoutException || exception.InnerException is TimeoutException)
                    {
                        isSuccess = true;
                    }
                    return true;
                });
            }
            // assert
            isSuccess.Should().BeTrue();
        }
    }
}
