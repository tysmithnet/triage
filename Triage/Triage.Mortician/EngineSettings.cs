// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-19-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-20-2018
// ***********************************************************************
// <copyright file="EngineSettings.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.ComponentModel.Composition;
using Triage.Mortician.Core;

namespace Triage.Mortician
{
    /// <summary>
    ///     Class EngineSettings.
    /// </summary>
    /// <seealso cref="Triage.Mortician.Core.ISettings" />
    [Export(typeof(ISettings))]
    [Export(typeof(EngineSettings))]
    public class EngineSettings : ISettings
    {
        /// <summary>
        ///     Gets or sets the test string.
        /// </summary>
        /// <value>The test string.</value>
        public string TestString { get; set; }
    }
}