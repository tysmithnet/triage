using Moq;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Test
{
    public class ClrRuntimeBuilder
    {
        public ClrRuntimeBuilder()
        {
            Mock.SetupAllProperties();
            HeapMock.SetupAllProperties();
            Mock.Setup(runtime => runtime.Heap).Returns(HeapMock.Object);
        }

        public IClrRuntime Build() => Mock.Object;

        public ClrRuntimeBuilder WithHeap(IClrHeap heap)
        {
            Mock.Setup(runtime => runtime.Heap).Returns(heap);
            return this;
        }

        public Mock<IClrHeap> HeapMock { get; } = new Mock<IClrHeap>();
        public Mock<IClrRuntime> Mock { get; } = new Mock<IClrRuntime>();
    }
}