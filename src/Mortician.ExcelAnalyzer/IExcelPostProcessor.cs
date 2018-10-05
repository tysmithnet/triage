// ***********************************************************************
// Assembly         : Mortician.ExcelAnalyzer
// Author           : @tysmithnet
// Created          : 01-14-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 01-15-2018
// ***********************************************************************
// <copyright file="IExcelPostProcessor.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.IO;

namespace Mortician.ExcelAnalyzer
{
    /// <summary>
    ///     Represents an object that can do post processing on
    /// </summary>
    public interface IExcelPostProcessor
    {
        /// <summary>
        ///     Posts the process.
        /// </summary>
        /// <param name="reportFile">The report file.</param>
        void PostProcess(FileInfo reportFile);
    }
}