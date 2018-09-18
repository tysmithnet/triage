// ***********************************************************************
// Assembly         : Triage.Mortician.Core
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="IObjectSet.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Triage.Mortician.Core.ClrMdAbstractions
{
    /// <summary>
    /// Interface IObjectSet
    /// </summary>
    public interface IObjectSet
    {
        /// <summary>
        /// Returns the count of objects in this set.
        /// </summary>
        /// <value>The count.</value>
        int Count { get; }

        /// <summary>
        /// Returns true if this set contains the given object, false otherwise.  The behavior of this function is undefined if
        /// obj lies outside the GC heap.
        /// </summary>
        /// <param name="obj">The object to check.</param>
        /// <returns>True if this set contains the given object, false otherwise.</returns>
        bool Contains(ulong obj);

        /// <summary>
        /// Adds the given object to the set.  Returns true if the object was added to the set, returns false if the object was already in the set.
        /// </summary>
        /// <param name="obj">The object to add to the set.</param>
        /// <returns>True if the object was added to the set, returns false if the object was already in the set.</returns>
        bool Add(ulong obj);

        /// <summary>
        /// Removes the given object from the set.  Returns true if the object was removed, returns false if the object was not in the set.
        /// </summary>
        /// <param name="obj">The object to remove from the set.</param>
        /// <returns>True if the object was removed, returns false if the object was not in the set.</returns>
        bool Remove(ulong obj);

        /// <summary>
        /// Empties the set.
        /// </summary>
        void Clear();
    }
}