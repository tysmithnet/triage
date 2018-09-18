using System;

namespace Triage.Mortician.Core.ClrMdAbstractions
{
    public interface IPdbInfo
    {
        /// <summary>
        /// The Guid of the PDB.
        /// </summary>
        Guid Guid { get; set; }

        /// <summary>
        /// The pdb revision.
        /// </summary>
        int Revision { get; set; }

        /// <summary>
        /// The filename of the pdb.
        /// </summary>
        string FileName { get; set; }

        /// <summary>
        /// GetHashCode implementation.
        /// </summary>
        /// <returns></returns>
        int GetHashCode();

        /// <summary>
        /// Override for Equals.  Returns true if the guid, age, and filenames equal.  Note that this compares only the 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>True if the objects match, false otherwise.</returns>
        bool Equals(object obj);

        /// <summary>
        /// To string implementation.
        /// </summary>
        /// <returns>Printing friendly version.</returns>
        string ToString();
    }
}