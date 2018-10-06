using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using FluentAssertions;
using Testing.Common;
using Xunit;

namespace Mortician.Test
{
    public class CoreComponentFactory_Should : BaseTest
    {
        [Fact]
        public void Throw_If_Crash_Dump_Cannot_Be_Loaded()
        {
            // arrange
            var container = new CompositionContainer();
            container.ComposeExportedValue<IConverter>(new Converter());
            Action mightThrow = () => new CoreComponentFactory(container, new FileInfo(@"C:\this\doesnt\exist.dmp"));

            // act
            // assert
            mightThrow.Should().Throw<ApplicationException>();
        }
    }
}