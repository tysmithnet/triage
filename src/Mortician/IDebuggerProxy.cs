// ***********************************************************************
// Assembly         : Mortician
// Author           : @tysmithnet
// Created          : 09-27-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-07-2018
// ***********************************************************************
// <copyright file="IDebuggerProxy.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace Mortician
{
    /// <summary>
    ///     Interface IDebuggerProxy
    /// </summary>
    public interface IDebuggerProxy
    {
        /// <summary>
        ///     Executes the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="waitTimeout">The wait timeout.</param>
        /// <returns>System.String.</returns>
        string Execute(string command, TimeSpan? waitTimeout = null);
    }
}