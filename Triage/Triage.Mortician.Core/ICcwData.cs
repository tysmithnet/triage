using System.Collections.Generic;

namespace Triage.Mortician.Core
{
    public interface ICcwData
    {
        /// <summary>
        /// Returns the pointer to the IUnknown representing this CCW.
        /// </summary>
        ulong IUnknown { get; }

        /// <summary>
        /// Returns the pointer to the managed object representing this CCW.
        /// </summary>
        ulong Object { get; }

        /// <summary>
        /// Returns the CLR handle associated with this CCW.
        /// </summary>
        ulong Handle { get; }

        /// <summary>
        /// Returns the refcount of this CCW.
        /// </summary>
        int RefCount { get; }

        /// <summary>
        /// Returns the interfaces that this CCW implements.
        /// </summary>
        IList<IComInterfaceData> Interfaces { get; }
    }
}