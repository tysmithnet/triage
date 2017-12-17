using System;
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
        protected internal ReplaySubject<Message> Messages = new ReplaySubject<Message>();
        
        public void Broadcast(Message message)
        {
            Messages.OnNext(message);
        }

        public IObservable<TMessage> Get<TMessage>() where TMessage : Message
        {
            return Messages.OfType<TMessage>();
        }
    }
}
