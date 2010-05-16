using Irc.Entities;
using Irc.EventHandlers;
using Irc.Events;
using Irc.Infrastructure;
using Irc.Stubs;
using Irc.Tests.Stubs;
using NUnit.Framework;

namespace Irc.Tests.EventHandlers
{
    public abstract class ConnectedToServerEventRiserConcern : ConcernFor<ConnectedToServerEventRiser>
    {
        protected ManualEventAggregator aggregator = new ManualEventAggregator();
        protected ConnectionStatusChanged statusChangedEvent;
        protected ConnectionStub connection = new ConnectionStub();

        protected override ConnectedToServerEventRiser CreateSubjectUnderTest()
        {
            return new ConnectedToServerEventRiser(aggregator, connection);
        }

        protected override void Because()
        {
            sut.Handle(statusChangedEvent);
        }
    }

    public class When_connection_status_is_changed_from_disconnected_to_connected : ConnectedToServerEventRiserConcern
    {
        private Server expectedServer = new Server("testServer.org", 10, "");

        protected override void Context()
        {
            this.connection.Server = expectedServer;
            this.statusChangedEvent = new ConnectionStatusChanged() {OldStatus = ConnectionStatus.Disconnected, NewStatus = ConnectionStatus.Connected};
        }

        [Test]
        public void Should_connected_to_server_event_be_raised()
        {
            Assert.That(aggregator.LastIncomingEvent, Is.TypeOf(typeof(ConnectedToServer)));
        }

        [Test]
        public void Should_event_contain_the_server_address()
        {
            var serverEvent = (ConnectedToServer) aggregator.LastIncomingEvent;
            Assert.That(serverEvent.Server, Is.EqualTo(expectedServer));
        }
    }

    public class When_connection_status_is_set_to_disconnected : ConnectedToServerEventRiserConcern
    {
        protected override void Context()
        {
            this.statusChangedEvent = new ConnectionStatusChanged() { OldStatus = ConnectionStatus.Connected, NewStatus = ConnectionStatus.Disconnected };
        }

        [Test]
        public void Should_no_event_be_risen()
        {
            Assert.That(aggregator.LastIncomingEvent, Is.Null);
        }
    }
}
