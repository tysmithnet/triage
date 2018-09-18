using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triage.Mortician.Core
{
    /// <summary>
    /// A method's mapping from IL to native offsets.
    /// </summary>
    public struct ILToNativeMap
    {
        /// <summary>
        /// The IL offset for this entry.
        /// </summary>
        public int ILOffset;

        /// <summary>
        /// The native start offset of this IL entry.
        /// </summary>
        public ulong StartAddress;

        /// <summary>
        /// The native end offset of this IL entry.
        /// </summary>
        public ulong EndAddress;

        /// <summary>
        /// To string.
        /// </summary>
        /// <returns>A visual display of the map entry.</returns>
        public override string ToString()
        {
            return string.Format("{0,2:X} - [{1:X}-{2:X}]", ILOffset, StartAddress, EndAddress);
        }

#pragma warning disable 0169
        /// <summary>
        /// Reserved.
        /// </summary>
        private int _reserved;
#pragma warning restore 0169
    }
}
