// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-19-2018
// ***********************************************************************
// <copyright file="ClrExceptionAdapter.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Diagnostics.Runtime;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    /// <summary>
    /// Class ClrExceptionAdapter.
    /// </summary>
    /// <seealso cref="Triage.Mortician.Core.ClrMdAbstractions.IClrException" />
    internal class ClrExceptionAdapter : IClrException
    {
        /// <summary>
        /// Gets or sets the converter.
        /// </summary>
        /// <value>The converter.</value>
        [Import]
        internal IConverter Converter { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="ClrExceptionAdapter"/> class.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <exception cref="ArgumentNullException">exception</exception>
        /// <inheritdoc />
        public ClrExceptionAdapter(ClrException exception)
        {
            Exception = exception ?? throw new ArgumentNullException(nameof(exception));
            StackTrace = Exception.StackTrace.Select(Converter.Convert).ToList();
            Type = Converter.Convert(Exception.Type);
            Inner = Converter.Convert(Exception.Inner);
        }

        /// <summary>
        /// The exception
        /// </summary>
        internal ClrException Exception;

        /// <summary>
        /// Returns the address of the exception object.
        /// </summary>
        /// <value>The address.</value>
        /// <inheritdoc />
        public ulong Address => Exception.Address;

        /// <summary>
        /// Returns the HRESULT associated with this exception (or S_OK if there isn't one).
        /// </summary>
        /// <value>The h result.</value>
        /// <inheritdoc />
        public int HResult => Exception.HResult;

        /// <summary>
        /// Returns the inner exception, if one exists, null otherwise.
        /// </summary>
        /// <value>The inner.</value>
        /// <inheritdoc />
        public IClrException Inner { get; }

        /// <summary>
        /// Returns the exception message.
        /// </summary>
        /// <value>The message.</value>
        /// <inheritdoc />
        public string Message => Exception.Message;

        /// <summary>
        /// Returns the StackTrace for this exception.  Note that this may be empty or partial depending
        /// on the state of the exception in the process.  (It may have never been thrown or we may be in
        /// the middle of constructing the stackwalk.)  This returns an empty list if no stack trace is
        /// associated with this exception object.
        /// </summary>
        /// <value>The stack trace.</value>
        /// <inheritdoc />
        public IList<IClrStackFrame> StackTrace { get; }

        /// <summary>
        /// Returns the GCHeapType for this exception object.
        /// </summary>
        /// <value>The type.</value>
        /// <inheritdoc />
        public IClrType Type { get; }
    }
}