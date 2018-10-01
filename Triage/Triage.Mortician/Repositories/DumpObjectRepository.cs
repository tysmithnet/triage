// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 12-19-2017
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-26-2018
// ***********************************************************************
// <copyright file="DumpObjectRepository.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using Triage.Mortician.Core;

namespace Triage.Mortician.Repositories
{
    /// <summary>
    ///     Repository for objects that were extracted from the managed heap
    /// </summary>
    /// <seealso cref="Triage.Mortician.Core.IDumpObjectRepository" />
    /// <seealso cref="IDumpObjectRepository" />
    public class DumpObjectRepository : IDumpObjectRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DumpObjectRepository" /> class.
        /// </summary>
        /// <param name="allObjects">The objects.</param>
        /// <param name="objectRoots">The object roots.</param>
        /// <param name="finalizerQueue">The finalizer queue.</param>
        /// <param name="blockingObjects">The blocking objects.</param>
        /// <exception cref="System.ArgumentNullException">
        ///     objects
        ///     or
        ///     objectRoots
        /// </exception>
        public DumpObjectRepository(Dictionary<ulong, DumpObject> allObjects,
            Dictionary<ulong, DumpObjectRoot> objectRoots,
            Dictionary<ulong, DumpBlockingObject> blockingObjects)
        {
            Objects = allObjects ?? throw new ArgumentNullException(nameof(allObjects));
            ObjectRoots = objectRoots ?? throw new ArgumentNullException(nameof(objectRoots));
            BlockingObjectsInternal = blockingObjects;
        }

        /// <summary>
        ///     The object roots that keep objects on the heap alive
        /// </summary>
        internal Dictionary<ulong, DumpObjectRoot> ObjectRoots;

        /// <summary>
        ///     The heap objects
        /// </summary>
        internal Dictionary<ulong, DumpObject> Objects;

        /// <summary>
        ///     Blockings the objects.
        /// </summary>
        /// <returns>IEnumerable&lt;DumpBlockingObject&gt;.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        /// <inheritdoc />
        public IEnumerable<DumpBlockingObject> BlockingObjects() => BlockingObjectsInternal.Values;

        /// <summary>
        ///     Finalizers the queue.
        /// </summary>
        /// <returns>IEnumerable&lt;DumpObject&gt;.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        /// <inheritdoc />
        public IEnumerable<DumpObject> FinalizerQueue() => FinalizerQueueInternal.Values;

        /// <summary>
        ///     Gets the object at the specified address
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns>The object at the specified address</returns>
        /// <exception cref="System.IndexOutOfRangeException"></exception>
        /// <exception cref="IndexOutOfRangeException"></exception>
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
        public IEnumerable<DumpObject> Get() => Objects.Values;

        /// <summary>
        ///     Gets or sets the blocking objects internal.
        /// </summary>
        /// <value>The blocking objects internal.</value>
        public Dictionary<ulong, DumpBlockingObject> BlockingObjectsInternal { get; set; }

        /// <summary>
        ///     Gets or sets the finalizer queue internal.
        /// </summary>
        /// <value>The finalizer queue internal.</value>
        public Dictionary<ulong, DumpObject> FinalizerQueueInternal { get; set; }
    }
}