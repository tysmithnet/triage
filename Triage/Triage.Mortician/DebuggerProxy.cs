// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 12-19-2017
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="DebuggerProxy.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Diagnostics;
using System.Text;
using Common.Logging;
using Microsoft.Diagnostics.Runtime.Interop;

namespace Triage.Mortician
{
    /// <summary>
    ///     https://github.com/Microsoft/clrmd/issues/79
    ///     Uses the debugger interface to execute arbitrary commands on the target
    /// </summary>
    /// <seealso cref="Microsoft.Diagnostics.Runtime.Interop.IDebugOutputCallbacks" />
    /// <seealso cref="System.IDisposable" />
    /// <seealso cref="T:Microsoft.Diagnostics.Runtime.Interop.IDebugOutputCallbacks" />
    /// <seealso cref="T:System.IDisposable" />
    /// <seealso cref="T:Triage.Mortician.IDebuggerProxy" />
    public sealed class DebuggerProxy : IDebugOutputCallbacks, IDisposable, IDebuggerProxy
    {
        // todo: autoresetevent to serialize access to client

        /// <summary>
        ///     Initializes a new instance of the <see cref="DebuggerProxy" /> class.
        /// </summary>
        /// <param name="client">The debugging client provided by the OS</param>
        internal DebuggerProxy(IDebugClient client)
        {
            _client = client;
            _control = (IDebugControl) client;

            var hr = client.GetOutputCallbacks(out _oldCallbacks);
            Debug.Assert(hr == 0);

            hr = client.SetOutputCallbacks(this);
            Debug.Assert(hr == 0);
        }

        /// <summary>
        ///     String builder for the output string
        /// </summary>
        private readonly StringBuilder _builder = new StringBuilder();

        /// <summary>
        ///     The debug client
        /// </summary>
        private readonly IDebugClient _client;

        /// <summary>
        ///     The debug control
        /// </summary>
        private readonly IDebugControl _control;

        /// <summary>
        ///     The disposed flag
        /// </summary>
        private bool _disposed;

        /// <summary>
        ///     The old callbacks
        /// </summary>
        private readonly IDebugOutputCallbacks _oldCallbacks;

        /// <summary>
        ///     Executes the specified command on the debug client
        /// </summary>
        /// <param name="cmd">The command to run.</param>
        /// <returns>The result of the command</returns>
        /// <example>!runaway</example>
        /// <example>!dumpheap -stat</example>
        /// <example>!dlk</example>
        public string Execute(string cmd)
        {
            lock (_builder)
            {
                _builder.Clear();
            }

            var hr = _control.Execute(DEBUG_OUTCTL.THIS_CLIENT, cmd, DEBUG_EXECUTE.NOT_LOGGED);
            Debug.Assert(hr == 0);
            //todo:  Something with hr, it may be an error legitimately.

            lock (_builder)
            {
                return _builder.ToString();
            }
        }

        /// <summary>
        ///     Callback for when text is received by the callbacks object
        /// </summary>
        /// <param name="mask">The mask.</param>
        /// <param name="text">The text.</param>
        /// <returns>System.Int32.</returns>
        int IDebugOutputCallbacks.Output(DEBUG_OUTPUT mask, string text)
        {
            // TODO: Check mask and write to appropriate location.

            lock (_builder)
            {
                _builder.Append(text);
            }

            return 0;
        }

        /// <summary>
        ///     The log
        /// </summary>
        /// <value>The log.</value>
        private ILog Log { get; set; } = LogManager.GetLogger(typeof(DebuggerProxy));

        #region IDisposable Support

        /// <summary>
        ///     Internal dispose routine
        /// </summary>
        /// <param name="disposing">The disposing.</param>
        internal void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _client.SetOutputCallbacks(_oldCallbacks);
                _disposed = true;
            }
        }

        /// <summary>
        ///     Finalizes an instance of the <see cref="DebuggerProxy" /> class.
        /// </summary>
        ~DebuggerProxy()
        {
            Dispose(false);
        }

        /// <summary>
        ///     Disposes this instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}