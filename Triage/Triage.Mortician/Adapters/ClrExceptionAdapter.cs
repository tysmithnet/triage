using System;
using System.Collections.Generic;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class ClrExceptionAdapter : IClrException
    {
        /// <inheritdoc />
        public ClrExceptionAdapter(Microsoft.Diagnostics.Runtime.ClrException exception)
        {
            _exception = exception ?? throw new ArgumentNullException(nameof(exception));
        }

        internal Microsoft.Diagnostics.Runtime.ClrException _exception;

        /// <inheritdoc />
        public ulong Address { get; }

        /// <inheritdoc />
        public int HResult { get; }

        /// <inheritdoc />
        public IClrException Inner { get; }

        /// <inheritdoc />
        public string Message { get; }

        /// <inheritdoc />
        public IList<IClrStackFrame> StackTrace { get; }

        /// <inheritdoc />
        public IClrType Type { get; }
    }
}