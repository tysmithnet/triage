// ***********************************************************************
// Assembly         : Mortician
// Author           : @tysmithnet
// Created          : 12-19-2017
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-05-2018
// ***********************************************************************
// <copyright file="DebuggerProxy.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Microsoft.Diagnostics.Runtime.Interop;
using Serilog;

namespace Mortician
{
    /// <summary>
    ///     https://github.com/Microsoft/clrmd/issues/79
    ///     Uses the debugger interface to execute arbitrary commands on the target
    /// </summary>
    /// <seealso cref="Mortician.IDebuggerProxy" />
    /// <seealso cref="Microsoft.Diagnostics.Runtime.Interop.IDebugOutputCallbacks" />
    /// <seealso cref="System.IDisposable" />
    /// <seealso cref="T:Microsoft.Diagnostics.Runtime.Interop.IDebugOutputCallbacks" />
    /// <seealso cref="T:System.IDisposable" />
    /// <seealso cref="T:Mortician.IDebuggerProxy" />
    public sealed class DebuggerProxy : IDebugOutputCallbacks, IDebuggerProxy
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DebuggerProxy" /> class.
        /// </summary>
        /// <param name="client">The debugging client provided by the OS</param>
        internal DebuggerProxy(IDebugClient client)
        {
            Client = client;
            Control = (IDebugControl) client;

            var hr = client.GetOutputCallbacks(out OldCallbacks);
            Debug.Assert(hr == 0);

            hr = client.SetOutputCallbacks(this);
            Debug.Assert(hr == 0);
        }

        /// <summary>
        ///     String builder for the output string
        /// </summary>
        internal StringBuilder Builder = new StringBuilder();

        /// <summary>
        ///     The debug client
        /// </summary>
        internal IDebugClient Client;

        /// <summary>
        ///     The debug control
        /// </summary>
        internal IDebugControl Control;

        /// <summary>
        ///     The disposed flag
        /// </summary>
        internal bool Disposed;

        /// <summary>
        ///     The log
        /// </summary>
        internal ILogger Log = Serilog.Log.ForContext<DebuggerProxy>();

        /// <summary>
        ///     The old callbacks
        /// </summary>
        internal IDebugOutputCallbacks OldCallbacks;

        /// <summary>
        ///     The reset event
        /// </summary>
        internal AutoResetEvent ResetEvent = new AutoResetEvent(true);

        /// <summary>
        ///     Executes the specified command on the debug client
        /// </summary>
        /// <param name="command">The command to run.</param>
        /// <returns>The result of the command</returns>
        /// <exception cref="COMException">Error executing " + command</exception>
        /// <example>!runaway</example>
        /// <example>!dumpheap -stat</example>
        /// <example>!dlk</example>
        public string Execute(string command)
        {
            try
            {
                ResetEvent.WaitOne(); // todo: wait forever? maybe optional timeout? idk
                Builder.Clear();
                Log.Information("Executing {Command}", command);
                var hr = Control.Execute(DEBUG_OUTCTL.ALL_CLIENTS, command, DEBUG_EXECUTE.NOT_LOGGED);
                return Builder.ToString();
            }
            catch (Exception e)
            {
                Log.Error(e, "Error executing {Command}", command);
                throw new COMException("Error executing " + command, e);
            }
            finally
            {
                ResetEvent.Set();
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
            switch (mask)
            {
                case DEBUG_OUTPUT.ERROR:
                    Log.Error("{CommandError}", text);
                    break;
                case DEBUG_OUTPUT.EXTENSION_WARNING:
                case DEBUG_OUTPUT.WARNING:
                    Log.Warning("{CommandWarning}", text);
                    break;
                case DEBUG_OUTPUT.SYMBOLS:
                    Log.Information("{SymbolOutput}", text);
                    break;
                default:
                    Builder.Append(text);
                    break;
            }

            return 0;
        }
    }
}