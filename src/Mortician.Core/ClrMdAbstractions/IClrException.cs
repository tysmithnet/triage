// ***********************************************************************
// Assembly         : Mortician.Core
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="IClrException.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;

namespace Mortician.Core.ClrMdAbstractions
{
    /// <summary>
    ///     Interface IClrException
    /// </summary>
    public interface IClrException
    {
        /// <summary>
        ///     Returns the address of the exception object.
        /// </summary>
        /// <value>The address.</value>
        ulong Address { get; }

        /// <summary>
        ///     Returns the HRESULT associated with this exception (or S_OK if there isn't one).
        /// </summary>
        /// <value>The h result.</value>
        int HResult { get; }

        /// <summary>
        ///     Returns the inner exception, if one exists, null otherwise.
        /// </summary>
        /// <value>The inner.</value>
        IClrException Inner { get; }

        /// <summary>
        ///     Returns the exception message.
        /// </summary>
        /// <value>The message.</value>
        string Message { get; }

        /// <summary>
        ///     Returns the StackTrace for this exception.  Note that this may be empty or partial depending
        ///     on the state of the exception in the process.  (It may have never been thrown or we may be in
        ///     the middle of constructing the stackwalk.)  This returns an empty list if no stack trace is
        ///     associated with this exception object.
        /// </summary>
        /// <value>The stack trace.</value>
        IList<IClrStackFrame> StackTrace { get; }

        /// <summary>
        ///     Returns the GCHeapType for this exception object.
        /// </summary>
        /// <value>The type.</value>
        IClrType Type { get; }
    }
}