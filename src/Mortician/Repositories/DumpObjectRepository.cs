// ***********************************************************************
// Assembly         : Mortician
// Author           : @tysmithnet
// Created          : 12-19-2017
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-03-2018
// ***********************************************************************
// <copyright file="DumpObjectRepository.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using Mortician.Core;

namespace Mortician.Repositories
{
    /// <summary>
    ///     Repository for objects that were extracted from the managed heap
    /// </summary>
    /// <seealso cref="Mortician.Core.IDumpObjectRepository" />
    /// <seealso cref="IDumpObjectRepository" />
    public class DumpObjectRepository : IDumpObjectRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DumpObjectRepository" /> class.
        /// </summary>
        /// <param name="allObjects">The objects.</param>
        /// <param name="objectRoots">The object roots.</param>
        /// <param name="blockingObjects">The blocking objects.</param>
        /// <param name="finalizableObjects">The finalizable objects.</param>
        /// <exception cref="ArgumentNullException">
        ///     allObjects
        ///     or
        ///     objectRoots
        ///     or
        ///     blockingObjects
        ///     or
        ///     finalizableObjects
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        ///     allObjects
        ///     or
        ///     objectRoots
        ///     or
        ///     blockingObjects
        ///     or
        ///     finalizableObjects
        /// </exception>
        public DumpObjectRepository(
            Dictionary<ulong, DumpObject> allObjects,
            Dictionary<ulong, DumpObjectRoot> objectRoots,
            Dictionary<ulong, DumpBlockingObject> blockingObjects,
            Dictionary<ulong, DumpObject> finalizableObjects)
        {
            ObjectsInternal = allObjects ?? throw new ArgumentNullException(nameof(allObjects));
            ObjectRootsInternal = objectRoots ?? throw new ArgumentNullException(nameof(objectRoots));
            BlockingObjectsInternal = blockingObjects ?? throw new ArgumentNullException(nameof(blockingObjects));
            FinalizerQueueInternal = finalizableObjects ?? throw new ArgumentNullException(nameof(finalizableObjects));
        }

        /// <summary>
        ///     The object roots that keep objects on the heap alive
        /// </summary>
        internal Dictionary<ulong, DumpObjectRoot> ObjectRootsInternal;

        /// <summary>
        ///     The heap objects
        /// </summary>
        internal Dictionary<ulong, DumpObject> ObjectsInternal;

        /// <summary>
        ///     Gets the blocking object.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns>DumpBlockingObject.</returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        /// <inheritdoc />
        public DumpBlockingObject GetBlockingObject(ulong address)
        {
            if (BlockingObjectsInternal.TryGetValue(address, out var obj))
                return obj;
            throw new IndexOutOfRangeException($"There is no object matching address: {address:x}");
        }

        /// <summary>
        ///     Gets the finalize queue object.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns>DumpObject.</returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        /// <inheritdoc />
        public DumpObject GetFinalizeQueueObject(ulong address)
        {
            if (FinalizerQueueInternal.TryGetValue(address, out var obj))
                return obj;
            throw new IndexOutOfRangeException($"There is no object matching address: {address:x}");
        }

        /// <summary>
        ///     Gets the object at the specified address
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns>The object at the specified address</returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        /// <exception cref="System.IndexOutOfRangeException"></exception>
        public DumpObject GetObject(ulong address)
        {
            if (ObjectsInternal.TryGetValue(address, out var obj))
                return obj;
            throw new IndexOutOfRangeException($"There is no object matching address: {address:x}");
        }

        /// <summary>
        ///     Gets the object root.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns>DumpObjectRoot.</returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        /// <inheritdoc />
        public DumpObjectRoot GetObjectRoot(ulong address)
        {
            if (ObjectRootsInternal.TryGetValue(address, out var obj))
                return obj;
            throw new IndexOutOfRangeException($"There is no object matching address: {address:x}");
        }

        /// <summary>
        ///     Blockings the objects.
        /// </summary>
        /// <value>The blocking objects.</value>
        /// <exception cref="System.NotImplementedException"></exception>
        /// <inheritdoc />
        public IEnumerable<DumpBlockingObject> BlockingObjects => BlockingObjectsInternal.Values;

        /// <summary>
        ///     Gets or sets the blocking objects internal.
        /// </summary>
        /// <value>The blocking objects internal.</value>
        public Dictionary<ulong, DumpBlockingObject> BlockingObjectsInternal { get; set; }

        /// <summary>
        ///     Finalizers the queue.
        /// </summary>
        /// <value>The finalizer queue.</value>
        /// <exception cref="System.NotImplementedException"></exception>
        /// <inheritdoc />
        public IEnumerable<DumpObject> FinalizerQueue => FinalizerQueueInternal.Values;

        /// <summary>
        ///     Gets or sets the finalizer queue internal.
        /// </summary>
        /// <value>The finalizer queue internal.</value>
        public Dictionary<ulong, DumpObject> FinalizerQueueInternal { get; set; }

        /// <summary>
        ///     Gets the object roots.
        /// </summary>
        /// <value>The object roots.</value>
        /// <inheritdoc />
        public IEnumerable<DumpObjectRoot> ObjectRoots => ObjectRootsInternal.Values;

        /// <summary>
        ///     Get all dump objects extracted from the heap
        /// </summary>
        /// <value>The objects.</value>
        public IEnumerable<DumpObject> Objects => ObjectsInternal.Values;
    }
}