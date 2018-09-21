// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="FileVersionInfoAdapter.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using Microsoft.Diagnostics.Runtime.Utilities;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    /// <summary>
    ///     Class FileVersionInfoAdapter.
    /// </summary>
    /// <seealso cref="Triage.Mortician.Core.ClrMdAbstractions.IFileVersionInfo" />
    internal class FileVersionInfoAdapter : BaseAdapter, IFileVersionInfo
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="FileVersionInfoAdapter" /> class.
        /// </summary>
        /// <param name="fileVersionInfo">The file version information.</param>
        /// <exception cref="ArgumentNullException">fileVersionInfo</exception>
        /// <inheritdoc />
        public FileVersionInfoAdapter(IConverter converter, FileVersionInfo fileVersionInfo) : base(converter)
        {
            FileVersionInfo = fileVersionInfo ?? throw new ArgumentNullException(nameof(fileVersionInfo));
        }

        /// <summary>
        ///     The file version information
        /// </summary>
        internal FileVersionInfo FileVersionInfo;

        public override void Setup()
        {
        }

        /// <summary>
        ///     Comments to supplement the file version
        /// </summary>
        /// <value>The comments.</value>
        /// <inheritdoc />
        public string Comments => FileVersionInfo.Comments;

        /// <summary>
        ///     The verison string
        /// </summary>
        /// <value>The file version.</value>
        /// <inheritdoc />
        public string FileVersion => FileVersionInfo.FileVersion;
    }
}