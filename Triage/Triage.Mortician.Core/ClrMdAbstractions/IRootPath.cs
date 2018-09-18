// ***********************************************************************
// Assembly         : Triage.Mortician.Core
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="IRootPath.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Triage.Mortician.Core.ClrMdAbstractions
{
    /// <summary>
    ///     Interface IRootPath
    /// </summary>
    public interface IRootPath
    {
        /// <summary>
        ///     The path from Root to a given target object.
        /// </summary>
        /// <value>The path.</value>
        IClrObject[] Path { get; set; }

        /// <summary>
        ///     The location that roots the object.
        /// </summary>
        /// <value>The root.</value>
        IClrRoot Root { get; set; }
    }
}