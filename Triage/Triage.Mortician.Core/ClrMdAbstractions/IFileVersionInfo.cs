// ***********************************************************************
// Assembly         : Triage.Mortician.Core
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="IFileVersionInfo.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Triage.Mortician.Core.ClrMdAbstractions
{
    /// <summary>
    ///     Interface IFileVersionInfo
    /// </summary>
    public interface IFileVersionInfo
    {
        /// <summary>
        ///     Comments to supplement the file version
        /// </summary>
        /// <value>The comments.</value>
        string Comments { get; }

        /// <summary>
        ///     The verison string
        /// </summary>
        /// <value>The file version.</value>
        string FileVersion { get; }
    }
}