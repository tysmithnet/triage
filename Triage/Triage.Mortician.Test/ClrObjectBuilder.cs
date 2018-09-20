using Moq;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Test
{
    public class ClrObjectBuilder
    {
        private Mock<IClrObject> Mock { get; } = new Mock<IClrObject>();

        public ClrObjectBuilder()
        {
            
        }

        public ClrObjectBuilder WithType(string typeName)
        {
            var mockType = new Mock<IClrType>();
            mockType.SetupAllProperties();
            mockType.Setup(type => type.Name).Returns(typeName);
            Mock.Setup(o => o.Type).Returns(mockType.Object);
            return this;
        }

        public IClrObject Build()
        {
            return Mock.Object;
        }
    }
}