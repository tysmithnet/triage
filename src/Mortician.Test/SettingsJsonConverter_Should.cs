using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using Mortician.Core;
using Testing.Common;
using Xunit;

namespace Mortician.Test
{
    public class SomeSettings : ISettings
    {
        public string Name { get; set; }
    }

    public class OtherSettings : ISettings
    {
        public int Num { get; set; }
    }
    public class SettingsJsonConverter_Should : BaseTest
    {
        [Fact]
        public void Serialize_Settings()
        {
            // arrange
            var settings = new SomeSettings()
            {
                Name = "Duke"
            };
            var other = new OtherSettings()
            {
                Num = 1
            };
            var all = new ISettings[] {settings, other};
            var serializer = new SettingsJsonConverter();

            // act
            var json = JsonConvert.SerializeObject(all, Formatting.None, serializer);

            // assert
            json.Should()
                .Be(
                    @"{""Mortician.Test.SomeSettings, Mortician.Test, Version=0.0.1.0, Culture=neutral, PublicKeyToken=null"":{""Name"":""Duke""},""Mortician.Test.OtherSettings, Mortician.Test, Version=0.0.1.0, Culture=neutral, PublicKeyToken=null"":{""Num"":1}}");
        }

        [Fact]
        public void Deserialize_Settings()
        {
            // arrange
            var serializer = new SettingsJsonConverter();

            // act
            var settings = JsonConvert.DeserializeObject<IEnumerable<ISettings>>(
                @"{""Mortician.Test.SomeSettings, Mortician.Test, Version=0.0.1.0, Culture=neutral, PublicKeyToken=null"":{""Name"":""Duke""},""Mortician.Test.OtherSettings, Mortician.Test, Version=0.0.1.0, Culture=neutral, PublicKeyToken=null"":{""Num"":1}}", serializer).ToList();

            // assert
            settings.Should().HaveCount(2);
            settings.OfType<SomeSettings>().First().Name.Should().Be("Duke");
            settings.OfType<OtherSettings>().First().Num.Should().Be(1);
        }

        [Fact]
        public void Throw_If_Given_Bad_Settings()
        {
            // arrange
            var serializer = new SettingsJsonConverter();
            Action mightThrow = () => JsonConvert.DeserializeObject<IEnumerable<ISettings>>(@"[]", serializer);
            Func<IEnumerable<ISettings>> mightThrow2 = () => JsonConvert.DeserializeObject<IEnumerable<ISettings>>(@"{""Mortician.Test.SomeSettings, Mortician.Test, Version=0.0.1.0, Culture=neutral, PublicKeyToken=null"":{""Name"":""Duke""},""Mortician.Test.OtherSettings, Mortician.Test, Version=0.0.1.0, Culture=neutral, PublicKeyToken=null"":{""Num"":""hello""}}", serializer);
            // act
            // assert
            mightThrow.Should().Throw<SerializationException>();
            mightThrow2().Should().HaveCount(1);
        }

        [Fact]
        public void Should_Skip_If_The_Type_Cannot_Be_Found()
        {
            // arrange
            var serializer = new SettingsJsonConverter();

            // act
            var settings = JsonConvert.DeserializeObject<IEnumerable<ISettings>>(
                @"{""Mortician.Test.SomeSettingsThatDontExist, Mortician.Test, Version=0.0.1.0, Culture=neutral, PublicKeyToken=null"":{""Name"":""Duke""},""Mortician.Test.OtherSettings, Mortician.Test, Version=0.0.1.0, Culture=neutral, PublicKeyToken=null"":{""Num"":1}}", serializer).ToList();

            // assert
            settings.Should().HaveCount(1);
            settings.OfType<OtherSettings>().First().Num.Should().Be(1);
        }

    }
}
