using System.Collections.Generic;

namespace Triage.Mortician.Abstraction
{
    /// <summary>
    ///     Represents an object that was extracted from the managed heap
    /// </summary>
    public interface IDumpObject
    {
        /// <summary>
        ///     Gets the address of this object
        /// </summary>
        /// <value>
        ///     The address.
        /// </value>
        ulong Address { get; }

        /// <summary>
        ///     Gets the full name of the type.
        /// </summary>
        /// <value>
        ///     The full name of the type.
        /// </value>
        string FullTypeName { get; }

        /// <summary>
        ///     Gets the type of this object
        /// </summary>
        /// <value>
        ///     The type.
        /// </value>
        IDumpType DumpType { get; }

        /// <summary>
        ///     Gets the size.
        /// </summary>
        /// <value>
        ///     The size.
        /// </value>
        ulong Size { get; }

        /// <summary>
        ///     Gets the gen.
        /// </summary>
        /// <value>
        ///     The gen.
        /// </value>
        int Gen { get; }

        /// <summary>
        ///     Gets the objects that this object references
        /// </summary>
        /// <value>
        ///     The references.
        /// </value>
        IEnumerable<IDumpObject> References { get; }
    }
}