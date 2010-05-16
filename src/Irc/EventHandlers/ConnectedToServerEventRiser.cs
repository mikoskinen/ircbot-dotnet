using Irc.Events;
using Irc.Infrastructure;

namespace Irc.EventHandlers
{
    public class ConnectedToServerEventRiser : IrcEventHandler<ConnectionStatusChanged>
    {
        private readonly EventAggregator eventAggregator;
        private readonly Connection connection;

        public ConnectedToServerEventRiser(EventAggregator eventAggregator, Connection connection)
        {
            this.eventAggregator = eventAggregator;
            this.connection = connection;
        }

        public void Handle(ConnectionStatusChanged args)
        {
            if (args.OldStatus == ConnectionStatus.Disconnected && args.NewStatus == ConnectionStatus.Connected)
                eventAggregator.Raise(new ConnectedToServer() { Server = connection.GetServer() });
        }
    }
}
