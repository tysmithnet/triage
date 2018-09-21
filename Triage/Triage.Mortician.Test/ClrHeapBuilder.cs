using Moq;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Test
{
    public class ClrHeapBuilder
    {
        public ClrHeapBuilder()
        {
            Mock.SetupAllProperties();
        }

        public IClrHeap Build() => Mock.Object;

        public ClrHeapBuilder WithGetGeneration(int generation)
        {
            Mock.Setup(heap => heap.GetGeneration(It.IsAny<ulong>())).Returns(generation);
            return this;
        }

        public Mock<IClrHeap> Mock { get; } = new Mock<IClrHeap>();
    }
}