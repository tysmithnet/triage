// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-17-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="IEventHub.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace Triage.Mortician.Core
{
    /// <summary>
    ///     Interface IEventHub
    /// </summary>
    public interface IEventHub
    {
        /// <summary>
        ///     Broadcasts the specified message to interested observers
        /// </summary>
        /// <param name="message">The message.</param>
        void Broadcast(Message message);

        /// <summary>
        ///     Gets an observable of messages that are assignable to TMessage
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <returns>IObservable&lt;TMessage&gt;.</returns>
        IObservable<TMessage> Get<TMessage>() where TMessage : Message;

        /// <summary>
        ///     Indicate to observers that there are no more events coming
        /// </summary>
        void Shutdown();
    }
}