// ***********************************************************************
// Assembly         : Triage.Mortician.Core
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="GCRootProgressEvent.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Triage.Mortician.Core.ClrMdAbstractions
{
    /// <summary>
    /// A delegate for reporting GCRoot progress.
    /// </summary>
    /// <param name="source">The GCRoot sending the event.</param>
    /// <param name="current">The total number of objects processed.</param>
    /// <param name="total">The total number of objects in the heap, if that number is known, otherwise -1.</param>
    public delegate void GCRootProgressEvent(IGcRoot source, long current, long total);
}