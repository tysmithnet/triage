// ***********************************************************************
// Assembly         : Triage.Mortician.Core
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="IILInfo.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Triage.Mortician.Core.ClrMdAbstractions
{
    /// <summary>
    /// Interface IILInfo
    /// </summary>
    public interface IILInfo
    {
        /// <summary>
        /// The address in memory of where the IL for a particular method is located.
        /// </summary>
        /// <value>The address.</value>
        ulong Address { get; }

        /// <summary>
        /// The length (in bytes) of the IL method body.
        /// </summary>
        /// <value>The length.</value>
        int Length { get; }

        /// <summary>
        /// The maximum IL stack size in this method.
        /// </summary>
        /// <value>The maximum stack.</value>
        int MaxStack { get; }

        /// <summary>
        /// The flags associated with the IL code.
        /// </summary>
        /// <value>The flags.</value>
        uint Flags { get; }

        /// <summary>
        /// The local variable signature token for this IL method.
        /// </summary>
        /// <value>The local variable signature token.</value>
        uint LocalVarSignatureToken { get; }
    }
}