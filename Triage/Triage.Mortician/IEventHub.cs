using System;

namespace Triage.Mortician
{
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
        /// <returns></returns>
        IObservable<TMessage> Get<TMessage>() where TMessage : Message;

        /// <summary>
        ///     Indicate to observers that there are no more events coming
        /// </summary>
        void Shutdown();
    }
}