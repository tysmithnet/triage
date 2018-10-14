// ***********************************************************************
// Assembly         : Testing.Common
// Author           : @tysmithnet
// Created          : 10-01-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-01-2018
// ***********************************************************************
// <copyright file="ClrRuntimeBuilder.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using Moq;
using Mortician.Core.ClrMdAbstractions;

namespace Testing.Common
{
    /// <summary>
    ///     Class ClrRuntimeBuilder.
    /// </summary>
    public class ClrRuntimeBuilder : Builder<IClrRuntime>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ClrRuntimeBuilder" /> class.
        /// </summary>
        public ClrRuntimeBuilder()
        {
            Mock.SetupAllProperties();
            HeapMock.SetupAllProperties();
            Mock.Setup(runtime => runtime.Heap).Returns(HeapMock.Object);
        }
        
        /// <summary>
        ///     Withes the heap.
        /// </summary>
        /// <param name="heap">The heap.</param>
        /// <returns>ClrRuntimeBuilder.</returns>
        public ClrRuntimeBuilder WithHeap(IClrHeap heap)
        {
            Mock.Setup(runtime => runtime.Heap).Returns(heap);
            return this;
        }

        /// <summary>
        ///     Gets the heap mock.
        /// </summary>
        /// <value>The heap mock.</value>
        public Mock<IClrHeap> HeapMock { get; } = new Mock<IClrHeap>();
        

        public ClrRuntimeBuilder WithHeapCount(int count)
        {
            Mock.Setup(runtime => runtime.HeapCount).Returns(count);
            return this;
        }

        public ClrRuntimeBuilder WithServerGc(bool isServerGc)
        {
            Mock.Setup(runtime => runtime.IsServerGc).Returns(isServerGc);
            return this;
        }

        public ClrRuntimeBuilder WithThreadPool(IClrThreadPool pool)
        {
            Mock.Setup(runtime => runtime.ThreadPool).Returns(pool);
            return this;
        }
    }
}