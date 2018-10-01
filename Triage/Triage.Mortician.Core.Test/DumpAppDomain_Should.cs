using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Triage.Testing.Common;
using Xunit;

namespace Triage.Mortician.Core.Test
{
    public class DumpAppDomain_Should : BaseTest
    {
        [Fact]
        public void Correctly_Add_Modules()
        {
            // arrange
            var appDomain = new DumpAppDomain();
            var module = new DumpModule();

            // act
            appDomain.AddModule(module);

            // assert
            appDomain.LoadedModules.Should().HaveCount(1);
        }
    }
}
