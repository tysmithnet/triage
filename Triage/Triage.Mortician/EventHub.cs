// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 12-17-2017
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="EventHub.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.ComponentModel.Composition;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Triage.Mortician.Core;

namespace Triage.Mortician
{
    /// <summary>
    ///     Represents a centralized hub for message passing between components
    /// </summary>
    /// <seealso cref="IEventHub" />
    [Export(typeof(IEventHub))]
    public class EventHub : IEventHub
    {
        /// <summary>
        ///     The observable backing object
        /// </summary>
        protected internal ReplaySubject<Message> Subject = new ReplaySubject<Message>();

        /// <summary>
        ///     Broadcasts the specified message to interested observers
        /// </summary>
        /// <param name="message">The message.</param>
        public void Broadcast(Message message)
        {
            Subject.OnNext(message);
        }

        /// <summary>
        ///     Gets an observable of messages that are assignable to TMessage
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <returns>IObservable&lt;TMessage&gt;.</returns>
        // todo: need to do some thread logging to verify that we are maximizing concurrency
        public IObservable<TMessage> Get<TMessage>() where TMessage : Message => Subject.OfType<TMessage>();

        /// <summary>
        ///     Indicate to observers that there are no more events coming
        /// </summary>
        public void Shutdown()
        {
            Subject.OnCompleted();
        }
    }
}