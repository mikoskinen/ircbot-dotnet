using System.Collections.Generic;

namespace Irc.Infrastructure
{
    public class IrcEventFactory : EventFactory
    {
        private readonly List<MessageBasedEvent> events;

        public IrcEventFactory(List<MessageBasedEvent> events)
        {
            this.events = events;
        }

        public List<MessageBasedEvent> MakeByMessage(string message)
        {
            var eventCollection = new List<MessageBasedEvent>();
            if (events == null)
                return eventCollection;

            foreach (var knownEvent in events)
            {
                if (knownEvent.DoesOccurBecauseOf(message))
                    eventCollection.Add(knownEvent.MakeFrom(message));
            }

            return eventCollection;
        }
    }
}