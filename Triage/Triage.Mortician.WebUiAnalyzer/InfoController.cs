// ***********************************************************************
// Assembly         : Triage.Mortician.WebUiAnalyzer
// Author           : @tysmithnet
// Created          : 09-17-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-17-2018
// ***********************************************************************
// <copyright file="InfoController.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using Microsoft.AspNetCore.Mvc;

namespace Triage.Mortician.WebUiAnalyzer
{
    /// <summary>
    ///     Class ValuesController.
    /// </summary>
    /// <seealso cref="Triage.Mortician.WebUiAnalyzer.BaseController" />
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/[controller]")]
    public class InfoController : BaseController
    {
        /// <summary>
        ///     Gets this instance.
        /// </summary>
        /// <returns>System.Object.</returns>
        [HttpGet]
        public object Get()
        {
            return new
            {
                StartTime = DumpInformationRepository.StartTimeUtc,
                Cpu = DumpInformationRepository.CpuUtilization
            };
        }
    }
}