using System;

namespace Triage.Mortician.Core
{
    public interface IPEFile
    {
        /// <summary>
        /// Holds information about the pdb for the current PEFile
        /// </summary>
        IPdbInfo PdbInfo { get; }

        /// <summary>
        /// Whether this object has been disposed.
        /// </summary>
        bool Disposed { get; }

        /// <summary>
        /// Looks up the debug signature information in the EXE.   Returns true and sets the parameters if it is found. 
        /// 
        /// If 'first' is true then the first entry is returned, otherwise (by default) the last entry is used 
        /// (this is what debuggers do today).   Thus NGEN images put the IL PDB last (which means debuggers 
        /// pick up that one), but we can set it to 'first' if we want the NGEN PDB.
        /// </summary>
        bool GetPdbSignature(out string pdbName, out Guid pdbGuid, out int pdbAge, bool first = false);

        /// <summary>
        /// Gets the File Version Information that is stored as a resource in the PE file.  (This is what the
        /// version tab a file's property page is populated with).  
        /// </summary>
        IFileVersionInfo GetFileVersionInfo();

        /// <summary>
        /// For side by side dlls, the manifest that decribes the binding information is stored as the RT_MANIFEST resource, and it
        /// is an XML string.   This routine returns this.  
        /// </summary>
        /// <returns></returns>
        string GetSxSManfest();

        /// <summary>
        /// Closes any file handles and cleans up resources.  
        /// </summary>
        void Dispose();
    }
}