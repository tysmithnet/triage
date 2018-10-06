// ***********************************************************************
// Assembly         : Mortician.Core
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="BlockingReason.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Mortician.Core.ClrMdAbstractions
{
    /// <summary>
    ///     Every thread which is blocking on an object specifies why the object is waiting.
    /// </summary>
    public enum BlockingReason
    {
        /// <summary>
        ///     Object is not locked.
        /// </summary>
        None,

        /// <summary>
        ///     Not able to determine why the object is blocking.
        /// </summary>
        Unknown,

        /// <summary>
        ///     The thread is waiting for a Mutex or Semaphore (such as Monitor.Enter, lock(obj), etc).
        /// </summary>
        Monitor,

        /// <summary>
        ///     The thread is waiting for a mutex with Monitor.Wait.
        /// </summary>
        MonitorWait,

        /// <summary>
        ///     The thread is waiting for an event (ManualResetEvent.WaitOne, AutoResetEvent.WaitOne).
        /// </summary>
        WaitOne,

        /// <summary>
        ///     The thread is waiting in WaitHandle.WaitAll.
        /// </summary>
        WaitAll,

        /// <summary>
        ///     The thread is waiting in WaitHandle.WaitAny.
        /// </summary>
        WaitAny,

        /// <summary>
        ///     The thread is blocked on a call to Thread.Join.
        /// </summary>
        ThreadJoin,

        /// <summary>
        ///     ReaderWriterLock, reader lock is taken.
        /// </summary>
        ReaderAcquired,

        /// <summary>
        ///     ReaderWriterLock, writer lock is taken.
        /// </summary>
        WriterAcquired
    }
}