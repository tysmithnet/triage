using System;
using System.Diagnostics;
using System.Text;
using Common.Logging;
using Microsoft.Diagnostics.Runtime.Interop;

namespace Triage.Mortician.Domain
{
    /// <summary>
    ///     https://github.com/Microsoft/clrmd/issues/79
    ///     Uses the debugger interface to execute arbitrary commands on the target
    /// </summary>
    /// <seealso cref="T:Microsoft.Diagnostics.Runtime.Interop.IDebugOutputCallbacks" />
    /// <seealso cref="T:System.IDisposable" />
    /// <seealso cref="T:Triage.Mortician.IDebuggerProxy" />
    public sealed class DebuggerProxy : IDebugOutputCallbacks, IDisposable
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DebuggerProxy" /> class.
        /// </summary>
        /// <param name="client">The debugging client provided by the OS</param>
        internal DebuggerProxy(IDebugClient client)
        {
            _client = client;
            _control = (IDebugControl) client;

            var hr = client.GetOutputCallbacks(out _old);
            Debug.Assert(hr == 0);

            hr = client.SetOutputCallbacks(this);
            Debug.Assert(hr == 0);
        }

        private readonly StringBuilder _builder = new StringBuilder();
        private readonly IDebugClient _client;
        private readonly IDebugControl _control;
        private bool _disposed; // To detect redundant calls
        private readonly IDebugOutputCallbacks _old;
        private ILog Log = LogManager.GetLogger(typeof(DebuggerProxy));

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

        int IDebugOutputCallbacks.Output(DEBUG_OUTPUT mask, string text)
        {
            // TODO: Check mask and write to appropriate location.

            lock (_builder)
            {
                _builder.Append(text);
            }

            return 0;
        }

        #region IDisposable Support

        internal void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _client.SetOutputCallbacks(_old);
                _disposed = true;
            }
        }

        ~DebuggerProxy()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}