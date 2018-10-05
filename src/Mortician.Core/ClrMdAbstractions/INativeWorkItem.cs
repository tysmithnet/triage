// ***********************************************************************
// Assembly         : Mortician.Core
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="INativeWorkItem.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Mortician.Core.ClrMdAbstractions
{
    /// <summary>
    ///     Interface INativeWorkItem
    /// </summary>
    public interface INativeWorkItem
    {
        /// <summary>
        ///     Returns the callback's address.
        /// </summary>
        /// <value>The callback.</value>
        ulong Callback { get; }

        /// <summary>
        ///     Returns the pointer to the user's data.
        /// </summary>
        /// <value>The data.</value>
        ulong Data { get; }

        /// <summary>
        ///     The type of work item this is.
        /// </summary>
        /// <value>The kind.</value>
        WorkItemKind Kind { get; }
    }
}