// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 12-13-2017
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="IAnalyzer.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Threading;
using System.Threading.Tasks;

namespace Triage.Mortician
{
    /// <summary>
    ///     Represents an object that is capable of doing some intelligent analysis
    ///     and reporting on it
    /// </summary>
    public interface IAnalyzer
    {
        /// <summary>
        ///     Performs the analysis
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task that when complete will signal the completion of the setup procedure</returns>
        Task Process(CancellationToken cancellationToken);

        /// <summary>
        ///     Performs any necessary setup prior to processing
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task that when complete will signal the completion of the setup procedure</returns>
        Task Setup(CancellationToken cancellationToken);
    }
}