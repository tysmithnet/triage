using System;
using System.Collections.Generic;

namespace Triage.Mortician
{
    /// <summary>
    ///     An object capable of managing all the types extracted from the memory dump
    /// </summary>
    public class DumpTypeRepository
    {
        /// <summary>
        ///     The types extracted from the memory dump
        /// </summary>
        protected internal Dictionary<DumpTypeKey, DumpType> Types;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DumpTypeRepository" /> class.
        /// </summary>
        /// <param name="dumpTypes">The dump types.</param>
        /// <exception cref="ArgumentNullException">dumpTypes</exception>
        protected internal DumpTypeRepository(Dictionary<DumpTypeKey, DumpType> dumpTypes)
        {
            Types = dumpTypes ?? throw new ArgumentNullException(nameof(dumpTypes));
        }
    }
}