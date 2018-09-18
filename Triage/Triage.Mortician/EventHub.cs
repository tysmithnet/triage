using System;
using System.ComponentModel.Composition;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Triage.Mortician
{
    /// <summary>
    ///     Represents a centralized hub for message passing between components
    /// </summary>
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
        /// <returns></returns>
        public IObservable<TMessage> Get<TMessage>() where TMessage : Message
        {
            return Subject.OfType<TMessage>();
        }

        /// <summary>
        ///     Indicate to observers that there are no more events coming
        /// </summary>
        public void Shutdown()
        {
            Subject.OnCompleted();
        }
    }
}