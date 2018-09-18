// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 12-19-2017
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="DumpObjectRepository.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using Common.Logging;
using Triage.Mortician.Domain;

namespace Triage.Mortician.Repository
{
    /// <summary>
    ///     Repository for objects that were extracted from the managed heap
    /// </summary>
    /// <seealso cref="Triage.Mortician.Repository.IDumpObjectRepository" />
    public class DumpObjectRepository : IDumpObjectRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DumpObjectRepository" /> class.
        /// </summary>
        /// <param name="objects">The objects.</param>
        /// <param name="objectRoots">The object roots.</param>
        /// <exception cref="System.ArgumentNullException">
        ///     objects
        ///     or
        ///     objectRoots
        /// </exception>
        public DumpObjectRepository(Dictionary<ulong, DumpObject> objects,
            Dictionary<ulong, DumpObjectRoot> objectRoots)
        {
            Objects = objects ?? throw new ArgumentNullException(nameof(objects));
            ObjectRoots = objectRoots ?? throw new ArgumentNullException(nameof(objectRoots));
        }

        /// <summary>
        ///     The log
        /// </summary>
        protected internal ILog Log = LogManager.GetLogger(typeof(DumpObjectRepository));

        /// <summary>
        ///     The object roots that keep objects on the heap alive
        /// </summary>
        protected internal Dictionary<ulong, DumpObjectRoot> ObjectRoots;

        /// <summary>
        ///     The heap objects
        /// </summary>
        protected internal Dictionary<ulong, DumpObject> Objects;

        /// <summary>
        ///     Gets the object at the specified address
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns>The object at the specified address</returns>
        /// <exception cref="System.IndexOutOfRangeException"></exception>
        /// <exception cref="IndexOutOfRangeException">The provided address is not a valid object address</exception>
        public DumpObject Get(ulong address)
        {
            if (Objects.TryGetValue(address, out var obj))
                return obj;
            throw new IndexOutOfRangeException($"There is no object matching address: {address:x}");
        }

        /// <summary>
        ///     Get all dump objects extracted from the heap
        /// </summary>
        /// <returns>All dump objects extracted from the heap</returns>
        public IEnumerable<DumpObject> Get()
        {
            return Objects.Values;
        }
    }
}