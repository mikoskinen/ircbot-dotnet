using System.Collections.Generic;
using NUnit.Framework;

namespace Irc.Infrastructure.Specs.EventFactorySpecifications
{
    public abstract class EventFactoryConcern : ConcernFor<IrcEventFactory>
    {
        protected List<MessageBasedEvent> events;

        protected override void Context()
        {
            var event1 = new EventStub1();
            var event2 = new EventStub2();

            this.events = new List<MessageBasedEvent> { event1, event2 };
        }

        protected override IrcEventFactory CreateSubjectUnderTest()
        {
            return new IrcEventFactory(events);
        }
    }

    public class When_creating_event_based_on_the_message_from_server : EventFactoryConcern
    {
        private List<MessageBasedEvent> result;

        protected override void Because()
        {
            var message = "PING :627133754";
            this.result = sut.MakeByMessage(message);
        }

        [Test]
        public void Should_make_correct_events()
        {
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0], Is.TypeOf(typeof(EventStub2)));
        }
    }

    public class EventStub1 : MessageBasedEvent
    {
        public bool DoesOccurBecauseOf(string message)
        {
            return message.IndexOf("PONG") > 0;
        }

        public MessageBasedEvent MakeFrom(string message)
        {
            return new EventStub1();
        }
    }

    public class EventStub2 : MessageBasedEvent
    {
        public bool DoesOccurBecauseOf(string message)
        {
            return message.IndexOf("PING") == 0;
        }

        public MessageBasedEvent MakeFrom(string message)
        {
            return new EventStub2();
        }
    }
}
