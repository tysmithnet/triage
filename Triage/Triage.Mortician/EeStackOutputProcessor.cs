// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-20-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-20-2018
// ***********************************************************************
// <copyright file="EeStackOutputProcessor.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace Triage.Mortician
{
    /// <summary>
    ///     Class EeStackOutputProcessor.
    /// </summary>
    /// <seealso cref="Triage.Mortician.IEeStackOutputProcessor" />
    public class EeStackOutputProcessor : IEeStackOutputProcessor
    {
        /// <summary>
        ///     Processes the output.
        /// </summary>
        /// <param name="eeStackOutput">The ee stack output.</param>
        /// <returns>EeStackReport.</returns>
        public EeStackReport ProcessOutput(string eeStackOutput)
        {
            var lines = eeStackOutput.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
            return null;
        }
    }
}