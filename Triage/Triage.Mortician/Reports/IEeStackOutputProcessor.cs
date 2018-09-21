// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-21-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-21-2018
// ***********************************************************************
// <copyright file="IEeStackOutputProcessor.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Triage.Mortician.Reports
{
    /// <summary>
    ///     Interface IEeStackOutputProcessor
    /// </summary>
    /// <seealso cref="Triage.Mortician.Reports.IStandardReportOutputProcessor{Triage.Mortician.Reports.EeStackReport}" />
    public interface IEeStackOutputProcessor : IStandardReportOutputProcessor<EeStackReport>
    {
    }
}