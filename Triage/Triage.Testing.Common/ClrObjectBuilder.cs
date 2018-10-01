// ***********************************************************************
// Assembly         : Triage.Testing.Common
// Author           : @tysmithnet
// Created          : 10-01-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-01-2018
// ***********************************************************************
// <copyright file="ClrObjectBuilder.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using Moq;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Testing.Common
{
    /// <summary>
    ///     Class ClrObjectBuilder.
    /// </summary>
    public class ClrObjectBuilder
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ClrObjectBuilder" /> class.
        /// </summary>
        public ClrObjectBuilder()
        {
            Mock.SetupAllProperties();
            TypeMock.SetupAllProperties();
            Mock.Setup(o => o.Type).Returns(TypeMock.Object);
        }

        /// <summary>
        ///     Builds this instance.
        /// </summary>
        /// <returns>IClrObject.</returns>
        public IClrObject Build() => Mock.Object;

        /// <summary>
        ///     Withes the address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns>ClrObjectBuilder.</returns>
        public ClrObjectBuilder WithAddress(ulong address)
        {
            Mock.Setup(o => o.Address).Returns(address);
            return this;
        }

        /// <summary>
        ///     Withes the simple value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns>ClrObjectBuilder.</returns>
        public ClrObjectBuilder WithSimpleValue<T>(T value)
        {
            WithType("");
            TypeMock.Setup(type => type.HasSimpleValue).Returns(true);
            TypeMock.Setup(type => type.GetValue(It.IsAny<ulong>())).Returns(value);
            return this;
        }

        /// <summary>
        ///     Withes the size.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns>ClrObjectBuilder.</returns>
        public ClrObjectBuilder WithSize(ulong size)
        {
            Mock.Setup(o => o.Size).Returns(size);
            return this;
        }

        /// <summary>
        ///     Withes the type.
        /// </summary>
        /// <param name="typeName">Name of the type.</param>
        /// <returns>ClrObjectBuilder.</returns>
        public ClrObjectBuilder WithType(string typeName)
        {
            TypeMock.Setup(type => type.Name).Returns(typeName);
            return this;
        }

        /// <summary>
        ///     Gets the mock.
        /// </summary>
        /// <value>The mock.</value>
        private Mock<IClrObject> Mock { get; } = new Mock<IClrObject>();

        /// <summary>
        ///     Gets the type mock.
        /// </summary>
        /// <value>The type mock.</value>
        private Mock<IClrType> TypeMock { get; } = new Mock<IClrType>();
    }
}