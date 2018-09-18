using System;
using System.Collections.Generic;
using Triage.Mortician.Domain;

namespace Triage.Mortician.Repository
{
    public interface IDumpObjectRepository
    {
        /// <summary>
        ///     Gets the object at the specified address
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns>The object at the specified address</returns>
        /// <exception cref="IndexOutOfRangeException">The provided address is not a valid object address</exception>
        DumpObject Get(ulong address);

        /// <summary>
        ///     Get all dump objects extracted from the heap
        /// </summary>
        /// <returns>All dump objects extracted from the heap</returns>
        IEnumerable<DumpObject> Get();
    }
}