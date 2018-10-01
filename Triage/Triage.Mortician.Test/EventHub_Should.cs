using System.Reactive.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Triage.Mortician.Core;
using Triage.Testing.Common;
using Xunit;

namespace Triage.Mortician.Test
{
    public class EventHub_Should : BaseTest
    {
        private class A : Message
        {
        }

        private class B : A
        {
        }

        private class C : B
        {
        }

        [Fact]
        public async Task Get_All_Types_At_Or_Bellow_The_Requested()
        {
            // arrange
            var eventHub = new EventHub();
            eventHub.Broadcast(new A());
            eventHub.Broadcast(new B());
            eventHub.Broadcast(new C());
            eventHub.Shutdown();

            // act
            var isGood = await eventHub.Get<B>().All(b => b is B);

            // assert
            isGood.Should().BeTrue();
        }
    }
}