// ***********************************************************************
// Assembly         : Mortician
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="ObjectSetAdapter.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using Microsoft.Diagnostics.Runtime;
using Mortician.Core.ClrMdAbstractions;

namespace Mortician.Adapters
{
    /// <summary>
    ///     Class ObjectSetAdapter.
    /// </summary>
    /// <seealso cref="Mortician.Core.ClrMdAbstractions.IObjectSet" />
    internal class ObjectSetAdapter : BaseAdapter, IObjectSet
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ObjectSetAdapter" /> class.
        /// </summary>
        /// <param name="objectSet">The object set.</param>
        /// <exception cref="ArgumentNullException">objectSet</exception>
        /// <inheritdoc />
        public ObjectSetAdapter(IConverter converter, ObjectSet objectSet) : base(converter)
        {
            ObjectSet = objectSet ?? throw new ArgumentNullException(nameof(objectSet));
            Count = ObjectSet.Count;
        }

        /// <summary>
        ///     The object set
        /// </summary>
        internal ObjectSet ObjectSet;

        /// <summary>
        ///     Adds the given object to the set.  Returns true if the object was added to the set, returns false if the object was
        ///     already in the set.
        /// </summary>
        /// <param name="obj">The object to add to the set.</param>
        /// <returns>True if the object was added to the set, returns false if the object was already in the set.</returns>
        /// <inheritdoc />
        public bool Add(ulong obj) => ObjectSet.Add(obj);

        /// <summary>
        ///     Empties the set.
        /// </summary>
        /// <inheritdoc />
        public void Clear() => ObjectSet.Clear();

        /// <summary>
        ///     Returns true if this set contains the given object, false otherwise.  The behavior of this function is undefined if
        ///     obj lies outside the GC heap.
        /// </summary>
        /// <param name="obj">The object to check.</param>
        /// <returns>True if this set contains the given object, false otherwise.</returns>
        /// <inheritdoc />
        public bool Contains(ulong obj) => ObjectSet.Contains(obj);

        /// <summary>
        ///     Removes the given object from the set.  Returns true if the object was removed, returns false if the object was not
        ///     in the set.
        /// </summary>
        /// <param name="obj">The object to remove from the set.</param>
        /// <returns>True if the object was removed, returns false if the object was not in the set.</returns>
        /// <inheritdoc />
        public bool Remove(ulong obj) => ObjectSet.Remove(obj);

        public override void Setup()
        {
        }

        /// <summary>
        ///     Returns the count of objects in this set.
        /// </summary>
        /// <value>The count.</value>
        /// <inheritdoc />
        public int Count { get; internal set; }
    }
}