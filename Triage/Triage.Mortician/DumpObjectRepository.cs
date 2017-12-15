using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection.Configuration;
using Common.Logging;
using Microsoft.Diagnostics.Runtime;

namespace Triage.Mortician
{
    /// <summary>
    ///     Repository for objects that were extracted from the managed heap
    /// </summary>
    // todo: fix access modifiers
    public class DumpObjectRepository
    {
        /// <summary>
        ///     The heap objects
        /// </summary>
        protected internal Dictionary<ulong, DumpObject> Objects;

        protected internal Dictionary<ulong, DumpObjectRoot> ObjectRoots;

        /// <summary>
        ///     The log
        /// </summary>
        protected internal ILog Log = LogManager.GetLogger(typeof(DumpObjectRepository));

        public DumpObjectRepository(Dictionary<ulong, DumpObject> objects, Dictionary<ulong, DumpObjectRoot> objectRoots)
        {
            Objects = objects ?? throw new ArgumentNullException(nameof(objects));
            ObjectRoots = objectRoots ?? throw new ArgumentNullException(nameof(objectRoots));
        }

        /// <summary>
        ///     Gets the object at the specified address
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns>The object at the specified address</returns>
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