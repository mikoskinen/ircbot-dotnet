using Irc.Events;
using Irc.Infrastructure;
using NUnit.Framework;
using Rhino.Mocks;

namespace Irc.EventHandlers.Specs.PongSenderSpecifications
{
    public abstract class PongSenderConcern : ConcernFor<PongSender>
    {
        protected Connection Connection;

        protected override void Context()
        {
            this.Connection = MockRepository.GenerateMock<Connection>();
        }

        protected override PongSender CreateSubjectUnderTest()
        {
            return new PongSender(Connection);
        }
    }

    public class When_receiving_ping_message_when_connecting : PongSenderConcern
    {
        protected override void Because()
        {
            sut.Handle(new PingReceived() { PingMessage = "PING :3789480634" });
        }

        [Test]
        public void Should_respond_with_correct_pong_message()
        {
            Connection.AssertWasCalled(x => x.SendToServer("PONG :3789480634"));
        }
    }

    public class When_receiving_ping_message_for_keep_alive : PongSenderConcern
    {
        protected override void Because()
        {
            sut.Handle(new PingReceived() { PingMessage = "PING :irc.server.org" });
        }

        [Test]
        public void Should_respond_with_correct_pong_message()
        {
            Connection.AssertWasCalled(x => x.SendToServer("PONG :irc.server.org"));
        }
    }
}
