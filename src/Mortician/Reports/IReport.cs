// ***********************************************************************
// Assembly         : Mortician
// Author           : @tysmithnet
// Created          : 09-21-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-26-2018
// ***********************************************************************
// <copyright file="IReport.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Mortician.Reports
{
    /// <summary>
    /// Interface IReport
    /// </summary>
    public interface IReport
    {
        /// <summary>
        /// Gets the raw output.
        /// </summary>
        /// <value>The raw output.</value>
        string RawOutput { get; }
    }
}