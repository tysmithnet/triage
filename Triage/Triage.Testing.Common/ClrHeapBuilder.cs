// ***********************************************************************
// Assembly         : Triage.Testing.Common
// Author           : @tysmithnet
// Created          : 10-01-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-01-2018
// ***********************************************************************
// <copyright file="ClrHeapBuilder.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using Moq;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Testing.Common
{
    /// <summary>
    ///     Class ClrHeapBuilder.
    /// </summary>
    public class ClrHeapBuilder
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ClrHeapBuilder" /> class.
        /// </summary>
        public ClrHeapBuilder()
        {
            Mock.SetupAllProperties();
        }

        /// <summary>
        ///     Builds this instance.
        /// </summary>
        /// <returns>IClrHeap.</returns>
        public IClrHeap Build() => Mock.Object;

        /// <summary>
        ///     Withes the get generation.
        /// </summary>
        /// <param name="generation">The generation.</param>
        /// <returns>ClrHeapBuilder.</returns>
        public ClrHeapBuilder WithGetGeneration(int generation)
        {
            Mock.Setup(heap => heap.GetGeneration(It.IsAny<ulong>())).Returns(generation);
            return this;
        }

        /// <summary>
        ///     Gets the mock.
        /// </summary>
        /// <value>The mock.</value>
        public Mock<IClrHeap> Mock { get; } = new Mock<IClrHeap>();
    }
}