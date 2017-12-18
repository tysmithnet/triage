using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace Triage.Mortician
{
    [Export]
    public class EventHub
    {   
        protected internal ReplaySubject<Message> Subject = new ReplaySubject<Message>();

        public void Broadcast(Message message)
        {
            Subject.OnNext(message);
        }

        public IObservable<TMessage> Get<TMessage>() where TMessage : Message
        {
            return Subject.OfType<TMessage>();
        }
    }
}
