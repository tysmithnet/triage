using Moq;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Test
{
    public class ClrObjectBuilder
    {
        public ClrObjectBuilder()
        {
            Mock.SetupAllProperties();
            TypeMock.SetupAllProperties();
            Mock.Setup(o => o.Type).Returns(TypeMock.Object);
        }

        public IClrObject Build() => Mock.Object;

        public ClrObjectBuilder WithAddress(ulong address)
        {
            Mock.Setup(o => o.Address).Returns(address);
            return this;
        }

        public ClrObjectBuilder WithSimpleValue<T>(T value)
        {
            WithType("");
            TypeMock.Setup(type => type.HasSimpleValue).Returns(true);
            TypeMock.Setup(type => type.GetValue(It.IsAny<ulong>())).Returns(value);
            return this;
        }

        public ClrObjectBuilder WithSize(ulong size)
        {
            Mock.Setup(o => o.Size).Returns(size);
            return this;
        }

        public ClrObjectBuilder WithType(string typeName)
        {
            TypeMock.Setup(type => type.Name).Returns(typeName);
            return this;
        }

        private Mock<IClrObject> Mock { get; } = new Mock<IClrObject>();
        private Mock<IClrType> TypeMock { get; } = new Mock<IClrType>();
    }
}