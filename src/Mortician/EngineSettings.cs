// ***********************************************************************
// Assembly         : Mortician
// Author           : @tysmithnet
// Created          : 09-19-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-24-2018
// ***********************************************************************
// <copyright file="EngineSettings.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.ComponentModel.Composition;
using Mortician.Core;

namespace Mortician
{
    /// <summary>
    ///     Class EngineSettings.
    /// </summary>
    /// <seealso cref="Mortician.Core.ISettings" />
    [Export(typeof(ISettings))]
    [Export]
    public class EngineSettings : ISettings
    {
        /// <summary>
        ///     Gets or sets the test string.
        /// </summary>
        /// <value>The test string.</value>
        public string TestString { get; set; }
    }
}