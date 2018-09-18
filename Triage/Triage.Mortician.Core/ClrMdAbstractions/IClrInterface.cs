// ***********************************************************************
// Assembly         : Triage.Mortician.Core
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="IClrInterface.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Triage.Mortician.Core.ClrMdAbstractions
{
    /// <summary>
    ///     Interface IClrInterface
    /// </summary>
    public interface IClrInterface
    {
        /// <summary>
        ///     Equals override.
        /// </summary>
        /// <param name="obj">Object to compare to.</param>
        /// <returns>True if this interface equals another.</returns>
        bool Equals(object obj);

        /// <summary>
        ///     GetHashCode override.
        /// </summary>
        /// <returns>A hashcode for this object.</returns>
        int GetHashCode();

        /// <summary>
        ///     Display string for this interface.
        /// </summary>
        /// <returns>Display string for this interface.</returns>
        string ToString();

        /// <summary>
        ///     The interface that this interface inherits from.
        /// </summary>
        /// <value>The base interface.</value>
        IClrInterface BaseInterface { get; }

        /// <summary>
        ///     The typename of the interface.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }
    }
}