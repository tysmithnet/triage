// ***********************************************************************
// Assembly         : Mortician
// Author           : @tysmithnet
// Created          : 09-17-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-03-2018
// ***********************************************************************
// <copyright file="IDumpObjectRepository.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;

namespace Mortician.Core
{
    /// <summary>
    /// Interface IDumpObjectRepository
    /// </summary>
    public interface IDumpObjectRepository
    {
        /// <summary>
        /// Gets the blocking object.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns>DumpBlockingObject.</returns>
        DumpBlockingObject GetBlockingObject(ulong address);

        /// <summary>
        /// Gets the object at the specified address
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns>The object at the specified address</returns>
        /// <exception cref="IndexOutOfRangeException">The provided address is not a valid object address</exception>
        DumpObject GetObject(ulong address);


        /// <summary>
        /// Gets the object root.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns>DumpObjectRoot.</returns>
        DumpObjectRoot GetObjectRoot(ulong address);

        /// <summary>
        /// Gets the finalize queue object.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns>DumpObject.</returns>
        DumpObject GetFinalizeQueueObject(ulong address);

        /// <summary>
        /// Gets the object roots.
        /// </summary>
        /// <value>The object roots.</value>
        IEnumerable<DumpObjectRoot> ObjectRoots { get; }

        /// <summary>
        /// Blockings the objects.
        /// </summary>
        /// <value>The blocking objects.</value>
        IEnumerable<DumpBlockingObject> BlockingObjects { get; }

        /// <summary>
        /// Finalizers the queue.
        /// </summary>
        /// <value>The finalizer queue.</value>
        IEnumerable<DumpObject> FinalizerQueue { get; }

        /// <summary>
        /// Gets the objects.
        /// </summary>
        /// <value>The objects.</value>
        IEnumerable<DumpObject> Objects { get; }
    }
}