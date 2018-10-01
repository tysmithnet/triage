// ***********************************************************************
// Assembly         : Triage.Mortician.Core
// Author           : @tysmithnet
// Created          : 09-27-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-01-2018
// ***********************************************************************
// <copyright file="ManagedCodeLocation.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Triage.Mortician.Core
{
    /// <summary>
    ///     Class ManagedCodeLocation.
    /// </summary>
    /// <seealso cref="Triage.Mortician.Core.CodeLocation" />
    public class ManagedCodeLocation : CodeLocation
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ManagedCodeLocation" /> class.
        /// </summary>
        /// <param name="methodDescriptor">The method descriptor.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="method">The method.</param>
        /// <inheritdoc />
        public ManagedCodeLocation(ulong methodDescriptor, ulong offset, string method) : base(null, method, offset)
        {
            MethodDescriptor = methodDescriptor;
        }

        /// <summary>
        ///     Gets the method descriptor.
        /// </summary>
        /// <value>The method descriptor.</value>
        public ulong MethodDescriptor { get; }
    }
}