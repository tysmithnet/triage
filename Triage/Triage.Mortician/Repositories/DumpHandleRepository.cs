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
            HandlesInternal = handleStore.Values.ToList();
        }

        /// <summary>
        ///     Gets this instance.
        /// </summary>
        /// <returns>IEnumerable&lt;DumpHandle&gt;.</returns>
        /// <inheritdoc />
        public IEnumerable<DumpHandle> Get() => HandlesInternal;

        /// <summary>
        ///     Gets or sets the handles internal.
        /// </summary>
        /// <value>The handles internal.</value>
        internal IList<DumpHandle> HandlesInternal { get; set; }
    }
}