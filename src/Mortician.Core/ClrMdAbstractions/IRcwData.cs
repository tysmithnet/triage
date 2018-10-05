// ***********************************************************************
// Assembly         : Mortician.Core
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="IRcwData.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;

namespace Mortician.Core.ClrMdAbstractions
{
    /// <summary>
    ///     Interface IRcwData
    /// </summary>
    public interface IRcwData
    {
        /// <summary>
        ///     Returns the thread which created this RCW.
        /// </summary>
        /// <value>The creator thread.</value>
        uint CreatorThread { get; }

        /// <summary>
        ///     Returns true if the RCW is disconnected from the underlying COM type.
        /// </summary>
        /// <value><c>true</c> if disconnected; otherwise, <c>false</c>.</value>
        bool Disconnected { get; }

        /// <summary>
        ///     Returns the list of interfaces this RCW implements.
        /// </summary>
        /// <value>The interfaces.</value>
        IList<IComInterfaceData> Interfaces { get; }

        /// <summary>
        ///     Returns the pointer to the IUnknown representing this CCW.
        /// </summary>
        /// <value>The i unknown.</value>
        ulong IUnknown { get; }

        /// <summary>
        ///     Returns the managed object associated with this of RCW.
        /// </summary>
        /// <value>The object.</value>
        ulong Object { get; }

        /// <summary>
        ///     Returns the RefCount of the RCW.
        /// </summary>
        /// <value>The reference count.</value>
        int RefCount { get; }

        /// <summary>
        ///     Returns the external VTable associated with this RCW.  (It's useful to resolve the VTable as a symbol
        ///     which will tell you what the underlying native type is...if you have the symbols for it loaded).
        /// </summary>
        /// <value>The v table pointer.</value>
        ulong VTablePointer { get; }

        /// <summary>
        ///     Returns the internal WinRT object associated with this RCW (if one exists).
        /// </summary>
        /// <value>The win rt object.</value>
        ulong WinRTObject { get; }
    }
}