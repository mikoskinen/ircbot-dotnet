using Irc.Events;
using Irc.Infrastructure;

namespace Irc.EventHandlers
{
    public class ConnectionStatusChanger : IrcEventHandler<ServerMessageReceived>
    {
        private readonly Connection connection;
        private readonly EventAggregator eventAggregator;

        public ConnectionStatusChanger(Connection connection)
        {
            this.connection = connection;
        }

        public void Handle(ServerMessageReceived args)
        {
            var newStatus = this.GetNewConnectionStatus(args.Message);
            if (newStatus == GetCurrentConnectionStatus()) return;

            connection.SetConnectionStatusTo(newStatus);
        }

        private ConnectionStatus GetNewConnectionStatus(string message)
        {
            if (GetCurrentConnectionStatus() == ConnectionStatus.Disconnected && message.IndexOf("PONG") >= 0)
            {
                return ConnectionStatus.Connected;   
            }

            return GetCurrentConnectionStatus();
        }

        private ConnectionStatus GetCurrentConnectionStatus()
        {
            return connection.GetConnectionStatus();
        }
    }
}
