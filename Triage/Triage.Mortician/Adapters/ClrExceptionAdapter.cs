using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Diagnostics.Runtime;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class ClrExceptionAdapter : IClrException
    {
        /// <inheritdoc />
        public ClrExceptionAdapter(ClrException exception)
        {
            Exception = exception ?? throw new ArgumentNullException(nameof(exception));
        }

        internal ClrException Exception;

        /// <inheritdoc />
        public ulong Address => Exception.Address;

        /// <inheritdoc />
        public int HResult => Exception.HResult;

        /// <inheritdoc />
        public IClrException Inner => Converter.Convert(Exception.Inner);

        /// <inheritdoc />
        public string Message => Exception.Message;

        /// <inheritdoc />
        public IList<IClrStackFrame> StackTrace => Exception.StackTrace.Select(Converter.Convert).ToList();

        /// <inheritdoc />
        public IClrType Type => Converter.Convert(Exception.Type);
    }
}