// ***********************************************************************
// Assembly         : Triage.Mortician.WebUiAnalyzer
// Author           : @tysmithnet
// Created          : 09-17-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="BaseController.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using Microsoft.AspNetCore.Mvc;
using Triage.Mortician.Repository;

namespace Triage.Mortician.WebUiAnalyzer
{
    /// <summary>
    ///     Class BaseController.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class BaseController : Controller
    {
        /// <summary>
        ///     Gets or sets the dump application domain repository.
        /// </summary>
        /// <value>The dump application domain repository.</value>
        internal static IDumpAppDomainRepository DumpAppDomainRepository { get; set; }

        /// <summary>
        ///     Gets or sets the dump information repository.
        /// </summary>
        /// <value>The dump information repository.</value>
        internal static IDumpInformationRepository DumpInformationRepository { get; set; }

        /// <summary>
        ///     Gets or sets the dump module repository.
        /// </summary>
        /// <value>The dump module repository.</value>
        internal static IDumpModuleRepository DumpModuleRepository { get; set; }

        /// <summary>
        ///     Gets or sets the dump object repository.
        /// </summary>
        /// <value>The dump object repository.</value>
        internal static IDumpObjectRepository DumpObjectRepository { get; set; }

        /// <summary>
        ///     Gets or sets the dump thread repository.
        /// </summary>
        /// <value>The dump thread repository.</value>
        internal static IDumpThreadRepository DumpThreadRepository { get; set; }

        /// <summary>
        ///     Gets or sets the dump type repository.
        /// </summary>
        /// <value>The dump type repository.</value>
        internal static IDumpTypeRepository DumpTypeRepository { get; set; }

        /// <summary>
        ///     Gets or sets the event hub.
        /// </summary>
        /// <value>The event hub.</value>
        internal static IEventHub EventHub { get; set; }
    }
}