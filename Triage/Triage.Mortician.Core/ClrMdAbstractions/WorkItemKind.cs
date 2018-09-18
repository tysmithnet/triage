// ***********************************************************************
// Assembly         : Triage.Mortician.Core
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="WorkItemKind.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Triage.Mortician.Core.ClrMdAbstractions
{
    /// <summary>
    /// The type of work item this is.
    /// </summary>
    public enum WorkItemKind
    {
        /// <summary>
        /// Unknown.
        /// </summary>
        Unknown,

        /// <summary>
        /// Callback for an async timer.
        /// </summary>
        AsyncTimer,

        /// <summary>
        /// Async callback.
        /// </summary>
        AsyncCallback,

        /// <summary>
        /// From ThreadPool.QueueUserWorkItem.
        /// </summary>
        QueueUserWorkItem,

        /// <summary>
        /// Timer delete callback.
        /// </summary>
        TimerDelete
    }
}