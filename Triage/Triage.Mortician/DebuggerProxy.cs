using System;
using System.Diagnostics;
using System.Text;
using Microsoft.Diagnostics.Runtime.Interop;
using Triage.Mortician.Abstraction;

namespace Triage.Mortician
{
    // https://github.com/Microsoft/clrmd/issues/79
    internal class DebuggerProxy : IDebugOutputCallbacks, IDisposable, IDebuggerProxy
    {
        private readonly StringBuilder _builder = new StringBuilder();
        private readonly IDebugClient _client;
        private readonly IDebugControl _control;
        private bool _disposed; // To detect redundant calls
        private readonly IDebugOutputCallbacks _old;

        public DebuggerProxy(IDebugClient client)
        {
            _client = client;
            _control = (IDebugControl) client;

            var hr = client.GetOutputCallbacks(out _old);
            Debug.Assert(hr == 0);

            hr = client.SetOutputCallbacks(this);
            Debug.Assert(hr == 0);
        }

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

        protected virtual void Dispose(bool disposing)
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