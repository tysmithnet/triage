using System.Collections.Generic;

namespace Triage.Mortician.Abstraction
{
    /// <summary>
    ///     Represents an object that is capable of managing the objects that were extracted from the memory dump
    /// </summary>
    public interface IDumpObjectRepository
    {
        /// <summary>
        ///     Gets the object at the specified address
        /// </summary>
        /// <param name="address">The address</param>
        /// <returns>The object at the specified address</returns>
        IDumpObject Get(ulong address);

        /// <summary>
        ///     Gets all the extracted heap objects
        /// </summary>
        /// <returns>The extracted heap objects</returns>
        IEnumerable<IDumpObject> Get();
    }
}