// ***********************************************************************
// Assembly         : Mortician
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
using Mortician.Core.ClrMdAbstractions;

namespace Mortician.Adapters
{
    /// <summary>
    ///     Class FileVersionInfoAdapter.
    /// </summary>
    /// <seealso cref="Mortician.Core.ClrMdAbstractions.IFileVersionInfo" />
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
            Comments = FileVersionInfo.Comments;
            FileVersion = FileVersionInfo.FileVersion;
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
        public string Comments { get; internal set; }

        /// <summary>
        ///     The verison string
        /// </summary>
        /// <value>The file version.</value>
        /// <inheritdoc />
        public string FileVersion { get; internal set; }
    }
}