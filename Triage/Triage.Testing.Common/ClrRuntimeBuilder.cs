// ***********************************************************************
// Assembly         : Triage.Testing.Common
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
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Testing.Common
{
    /// <summary>
    ///     Class ClrRuntimeBuilder.
    /// </summary>
    public class ClrRuntimeBuilder
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
        ///     Builds this instance.
        /// </summary>
        /// <returns>IClrRuntime.</returns>
        public IClrRuntime Build() => Mock.Object;

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

        /// <summary>
        ///     Gets the mock.
        /// </summary>
        /// <value>The mock.</value>
        public Mock<IClrRuntime> Mock { get; } = new Mock<IClrRuntime>();
    }
}