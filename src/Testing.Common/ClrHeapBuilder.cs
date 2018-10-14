// ***********************************************************************
// Assembly         : Testing.Common
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
using Mortician.Core.ClrMdAbstractions;

namespace Testing.Common
{
    /// <summary>
    ///     Class ClrHeapBuilder.
    /// </summary>
    public class ClrHeapBuilder : Builder<IClrHeap>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ClrHeapBuilder" /> class.
        /// </summary>
        public ClrHeapBuilder()
        {
            Mock.SetupAllProperties();
        }
        
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

        public ClrHeapBuilder WithTotalSize(ulong size)
        {
            Mock.Setup(heap => heap.TotalHeapSize).Returns(size);
            return this;
        }

        public ClrHeapBuilder WithGetObjectType(IClrType objType)
        {
            Mock.Setup(heap => heap.GetObjectType(It.IsAny<ulong>())).Returns(objType);
            return this;
        }
    }
}