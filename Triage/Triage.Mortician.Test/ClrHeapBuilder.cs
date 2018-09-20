using Moq;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Test
{
    public class ClrHeapBuilder
    {
        public Mock<IClrHeap> Mock { get; } = new Mock<IClrHeap>();

        public ClrHeapBuilder()
        {
            Mock.SetupAllProperties();
        }

        public ClrHeapBuilder WithGetGeneration(int generation)
        {
            Mock.Setup(heap => heap.GetGeneration(It.IsAny<ulong>())).Returns(generation);
            return this;
        }

        public IClrHeap Build()
        {
            return Mock.Object;
        }
    }
}