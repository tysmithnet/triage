using System.Collections.Generic;
using System.Diagnostics;
using Triage.Mortician.Abstraction;

namespace Triage.Mortician
{
    /// <summary>
    /// Represents a managed object on the managed heap
    /// </summary>
    /// <seealso cref="Triage.Mortician.Abstraction.IDumpObject" />
    [DebuggerDisplay("{FullTypeName} : {Size} : {Address}")]
    public class DumpObject : IDumpObject
    {
        /// <summary>
        /// The references that this object has
        /// </summary>
        internal IList<IDumpObject> ReferencesInternal = new List<IDumpObject>();

        // todo: constructor args are already unwieldy, refactor to factory
        /// <summary>
        /// Initializes a new instance of the <see cref="DumpObject"/> class.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="fullTypeName">Full name of the type.</param>
        /// <param name="size">The size.</param>
        /// <param name="gen">The gc generation 0,1,2,3 (3 is large object heap)</param>
        public DumpObject(ulong address, string fullTypeName, ulong size, int gen)
        {
            Address = address;
            FullTypeName = fullTypeName;
            Size = size;
            Gen = gen;
        }

        /// <summary>
        /// Gets the address of this object
        /// </summary>
        /// <value>
        /// The address of this object
        /// </value>
        public ulong Address { get; internal set; }

        /// <summary>
        /// Gets the full name of the type of this object.
        /// </summary>
        /// <value>
        /// The full name of the type of this object.
        /// </value>
        public string FullTypeName { get; internal set; }

        /// <summary>
        /// Gets the size of this object
        /// Note that this is the type heap for most types, but will be the size of the array in a byte[] for example
        /// </summary>
        /// <value>
        /// The size of this object
        /// </value>
        public ulong Size { get; internal set; }


        /// <summary>
        /// Gets the gc generation for this object. 0,1,2,3 where 3 is the large object heap
        /// </summary>
        /// <value>
        /// The gc generation for this object
        /// </value>
        public int Gen { get; internal set; }


        /// <summary>
        /// Gets the references that this object has.
        /// </summary>
        /// <value>
        /// The references that this object has.
        /// </value>
        public IReadOnlyCollection<IDumpObject> References { get; internal set; }

        /// <summary>
        /// Adds a reference to the list of objects that this object has
        /// </summary>
        /// <param name="obj">The object to add.</param>
        internal void AddReference(IDumpObject obj)
        {
            if(!ReferencesInternal.Contains(obj))
                ReferencesInternal.Add(obj);
        }
    }
}