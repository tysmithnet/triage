// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 12-19-2017
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="DumpObject.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;

namespace Triage.Mortician.Core
{
    /// <summary>
    ///     Represents a managed object on the managed heap
    /// </summary>
    [DebuggerDisplay("{FullTypeName} : {Size} : {Address}")]
    public class DumpObject
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DumpObject" /> class.
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
        ///     The objects that reference this object
        /// </summary>
        protected internal ConcurrentDictionary<ulong, DumpObject> ReferencersInternal =
            new ConcurrentDictionary<ulong, DumpObject>();

        /// <summary>
        ///     The references that this object has
        /// </summary>
        protected internal ConcurrentDictionary<ulong, DumpObject> ReferencesInternal =
            new ConcurrentDictionary<ulong, DumpObject>();

        /// <summary>
        ///     Adds a reference to the list of objects that this object has
        /// </summary>
        /// <param name="obj">The object to add.</param>
        protected internal void AddReference(DumpObject obj)
        {
            ReferencesInternal.TryAdd(obj.Address, obj);
        }

        /// <summary>
        ///     Adds the referencer.
        /// </summary>
        /// <param name="obj">The object.</param>
        protected internal void AddReferencer(DumpObject obj)
        {
            ReferencersInternal.TryAdd(obj.Address, obj);
        }

        /// <summary>
        ///     Get a short description of the object.
        /// </summary>
        /// <returns>A short description of this object</returns>
        /// <remarks>The return value is intended to be shown on a single line</remarks>
        protected virtual string ToShortDescription() => $"{FullTypeName} : {Size} : {Address:x8} ({Address})";

        /// <summary>
        ///     Gets the address of this object
        /// </summary>
        /// <value>The address of this object</value>
        public ulong Address { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the type of the object
        /// </summary>
        /// <value>The type of the dump.</value>
        public DumpType DumpType { get; protected internal set; }

        /// <summary>
        ///     Gets the full name of the type of this object.
        /// </summary>
        /// <value>The full name of the type of this object.</value>
        public string FullTypeName { get; protected internal set; }

        /// <summary>
        ///     Gets the gc generation for this object. 0,1,2,3 where 3 is the large object heap
        /// </summary>
        /// <value>The gc generation for this object</value>
        public int Gen { get; protected internal set; }

        /// <summary>
        ///     Gets the objects that reference this object
        /// </summary>
        /// <value>The referencers.</value>
        public IEnumerable<DumpObject> Referencers => ReferencersInternal.Values;

        /// <summary>
        ///     Gets the references that this object has.
        /// </summary>
        /// <value>The references that this object has.</value>
        public IEnumerable<DumpObject> References => ReferencesInternal.Values;

        /// <summary>
        ///     Gets the size of this object
        ///     Note that this is the type heap for most types, but will be the size of the array in a byte[] for example
        /// </summary>
        /// <value>The size of this object</value>
        public ulong Size { get; protected internal set; }
    }
}