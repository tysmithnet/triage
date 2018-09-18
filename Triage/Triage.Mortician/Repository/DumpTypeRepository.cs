using System;
using System.Collections.Generic;
using Triage.Mortician.Domain;

namespace Triage.Mortician.Repository
{
    /// <summary>
    ///     An object capable of managing all the types extracted from the memory dump
    /// </summary>
    public class DumpTypeRepository : IDumpTypeRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DumpTypeRepository" /> class.
        /// </summary>
        /// <param name="dumpTypes">The dump types.</param>
        /// <exception cref="ArgumentNullException">dumpTypes</exception>
        protected internal DumpTypeRepository(Dictionary<DumpTypeKey, DumpType> dumpTypes)
        {
            Types = dumpTypes ?? throw new ArgumentNullException(nameof(dumpTypes));
        }

        /// <summary>
        ///     The types extracted from the memory dump
        /// </summary>
        protected internal Dictionary<DumpTypeKey, DumpType> Types;
    }
}