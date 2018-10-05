// ***********************************************************************
// Assembly         : Triage.Mortician.Core
// Author           : @tysmithnet
// Created          : 10-01-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-01-2018
// ***********************************************************************
// <copyright file="DumpHandleRepository.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using Triage.Mortician.Core;

namespace Triage.Mortician.Repositories
{
    /// <summary>
    ///     Class DumpHandleRepository.
    /// </summary>
    /// <seealso cref="Triage.Mortician.Core.IDumpHandleRepository" />
    public class DumpHandleRepository : IDumpHandleRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DumpHandleRepository" /> class.
        /// </summary>
        /// <param name="handleStore">The handle store.</param>
        public DumpHandleRepository(Dictionary<ulong, DumpHandle> handleStore)
        {
            HandlesInternal = handleStore ?? throw new ArgumentNullException(nameof(handleStore));
        }

        /// <summary>
        ///     Gets this instance.
        /// </summary>
        /// <returns>IEnumerable&lt;DumpHandle&gt;.</returns>
        /// <inheritdoc />
        public IEnumerable<DumpHandle> Handles => HandlesInternal.Values;

        /// <inheritdoc />
        public DumpHandle Get(ulong address)
        {
            if (HandlesInternal.TryGetValue(address, out var handle))
            {
                return handle;
            }
            throw new KeyNotFoundException($"Unable to find handle at {address:x8}");
        }

        /// <summary>
        ///     Gets or sets the handles internal.
        /// </summary>
        /// <value>The handles internal.</value>
        internal Dictionary<ulong, DumpHandle> HandlesInternal { get; set; }
    }
}