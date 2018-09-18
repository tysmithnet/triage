using System.Collections.Generic;

namespace Triage.Mortician.Core
{
    public interface IClrException
    {
        /// <summary>
        /// Returns the GCHeapType for this exception object.
        /// </summary>
        ClrType Type { get; }

        /// <summary>
        /// Returns the exception message.
        /// </summary>
        string Message { get; }

        /// <summary>
        /// Returns the address of the exception object.
        /// </summary>
        ulong Address { get; }

        /// <summary>
        /// Returns the inner exception, if one exists, null otherwise.
        /// </summary>
        ClrException Inner { get; }

        /// <summary>
        /// Returns the HRESULT associated with this exception (or S_OK if there isn't one).
        /// </summary>
        int HResult { get; }

        /// <summary>
        /// Returns the StackTrace for this exception.  Note that this may be empty or partial depending
        /// on the state of the exception in the process.  (It may have never been thrown or we may be in
        /// the middle of constructing the stackwalk.)  This returns an empty list if no stack trace is
        /// associated with this exception object.
        /// </summary>
        IList<ClrStackFrame> StackTrace { get; }
    }
}