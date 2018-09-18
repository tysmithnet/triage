// ***********************************************************************
// Assembly         : Triage.Mortician.Core
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="IManagedWorkItem.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Triage.Mortician.Core.ClrMdAbstractions
{
    /// <summary>
    /// Interface IManagedWorkItem
    /// </summary>
    public interface IManagedWorkItem
    {
        /// <summary>
        /// The object address of this entry.
        /// </summary>
        /// <value>The object.</value>
        ulong Object { get; }

        /// <summary>
        /// The type of Object.
        /// </summary>
        /// <value>The type.</value>
        IClrType Type { get; }
    }
}