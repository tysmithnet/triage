// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-21-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-21-2018
// ***********************************************************************
// <copyright file="IDumpDomainOutputProcessor.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Triage.Mortician.Reports
{
    /// <summary>
    ///     Interface IDumpDomainOutputProcessor
    /// </summary>
    /// <seealso cref="Triage.Mortician.Reports.IStandardReportOutputProcessor{Triage.Mortician.Reports.DumpDomainReport}" />
    /// <seealso cref="Triage.Mortician.IStandardReportOutputProcessor{Triage.Mortician.DumpDomainReport}" />
    public interface IDumpDomainOutputProcessor : IStandardReportOutputProcessor<DumpDomainReport>
    {
    }
}