// ***********************************************************************
// Assembly         : Mortician.Core
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="IPdbInfo.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace Mortician.Core.ClrMdAbstractions
{
    /// <summary>
    ///     Interface IPdbInfo
    /// </summary>
    public interface IPdbInfo
    {
        /// <summary>
        ///     Override for Equals.  Returns true if the guid, age, and filenames equal.  Note that this compares only the
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>True if the objects match, false otherwise.</returns>
        bool Equals(object obj);

        /// <summary>
        ///     GetHashCode implementation.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        int GetHashCode();

        /// <summary>
        ///     To string implementation.
        /// </summary>
        /// <returns>Printing friendly version.</returns>
        string ToString();

        /// <summary>
        ///     The filename of the pdb.
        /// </summary>
        /// <value>The name of the file.</value>
        string FileName { get; }

        /// <summary>
        ///     The Guid of the PDB.
        /// </summary>
        /// <value>The unique identifier.</value>
        Guid Guid { get; }

        /// <summary>
        ///     The pdb revision.
        /// </summary>
        /// <value>The revision.</value>
        int Revision { get; }
    }
}