using Irc.Events;
using Irc.Infrastructure;
using Irc.Tests.Stubs;
using NUnit.Framework;

namespace Irc.EventHandlers.Specs.ConnectionStatusChangerSpecifications
{
    public abstract class ConnectionStatusChangerConcern : ConcernFor<ConnectionStatusChanger>
    {
        protected readonly Connection connection = new ConnectionStub();

        protected override ConnectionStatusChanger CreateSubjectUnderTest()
        {
            return new ConnectionStatusChanger(connection);
        }
    }

    public class When_current_status_is_disconnected_and_connection_happens : ConnectionStatusChangerConcern
    {
        protected string message = ":irc.server.net PONG irc.server.net :irc.server.net";

        protected override void Context()
        {
            connection.SetConnectionStatusTo(ConnectionStatus.Disconnected);
        }

        protected override void Because()
        {
            sut.Handle(new ServerMessageReceived() { Message = message });
        }

        [Test]
        public void Should_set_connection_to_connected()
        {
            Assert.That(connection.GetConnectionStatus(), Is.EqualTo(ConnectionStatus.Connected));
        }
    }
}
