// ***********************************************************************
// Assembly         : Triage.Mortician.Core
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="IComInterfaceData.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Triage.Mortician.Core.ClrMdAbstractions
{
    /// <summary>
    /// Interface IComInterfaceData
    /// </summary>
    public interface IComInterfaceData
    {
        /// <summary>
        /// The CLR type this represents.
        /// </summary>
        /// <value>The type.</value>
        IClrType Type { get; }

        /// <summary>
        /// The interface pointer of Type.
        /// </summary>
        /// <value>The interface pointer.</value>
        ulong InterfacePointer { get; }
    }
}