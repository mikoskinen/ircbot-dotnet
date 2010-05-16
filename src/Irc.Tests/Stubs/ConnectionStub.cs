using System;
using Irc.Entities;
using Irc.Infrastructure;

namespace Irc.Tests.Stubs
{
    public class ConnectionStub : Connection
    {
        public ConnectionStatus ConnectionStatus = ConnectionStatus.Disconnected;
        public Server Server;

        public void ConnectTo(Server server, Credentials credentials)
        {
            throw new NotImplementedException();
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public void SendToServer(string message)
        {
            throw new NotImplementedException();
        }

        public void SetConnectionStatusTo(ConnectionStatus newConnectionStatus)
        {
            this.ConnectionStatus = newConnectionStatus;
        }

        ConnectionStatus Connection.GetConnectionStatus()
        {
            return this.ConnectionStatus;
        }

        public Server GetServer()
        {
            return this.Server;
        }
    }
}
