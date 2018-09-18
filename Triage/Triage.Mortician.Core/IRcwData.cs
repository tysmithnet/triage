using System.Collections.Generic;

namespace Microsoft.Diagnostics.Runtime
{
    public interface IRcwData
    {
        /// <summary>
        /// Returns the pointer to the IUnknown representing this CCW.
        /// </summary>
        ulong IUnknown { get; }

        /// <summary>
        /// Returns the external VTable associated with this RCW.  (It's useful to resolve the VTable as a symbol
        /// which will tell you what the underlying native type is...if you have the symbols for it loaded).
        /// </summary>
        ulong VTablePointer { get; }

        /// <summary>
        /// Returns the RefCount of the RCW.
        /// </summary>
        int RefCount { get; }

        /// <summary>
        /// Returns the managed object associated with this of RCW.
        /// </summary>
        ulong Object { get; }

        /// <summary>
        /// Returns true if the RCW is disconnected from the underlying COM type.
        /// </summary>
        bool Disconnected { get; }

        /// <summary>
        /// Returns the thread which created this RCW.
        /// </summary>
        uint CreatorThread { get; }

        /// <summary>
        /// Returns the internal WinRT object associated with this RCW (if one exists).
        /// </summary>
        ulong WinRTObject { get; }

        /// <summary>
        /// Returns the list of interfaces this RCW implements.
        /// </summary>
        IList<ComInterfaceData> Interfaces { get; }
    }
}