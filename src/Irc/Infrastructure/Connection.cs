using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Irc.Entities;
using Irc.Events;

namespace Irc.Infrastructure
{
    public interface Connection
    {
        void ConnectTo(Server server, Credentials credentials);
        void Disconnect();
        void SendToServer(string message);
        void SetConnectionStatusTo(ConnectionStatus connectionStatus);
        ConnectionStatus GetConnectionStatus();
        Server GetServer();
    }

    public class IrcConnection : Connection
    {
        private readonly EventFactory eventFactory;
        private readonly EventAggregator eventAggregator;
        private StreamWriter writer;
        private NetworkStream stream;
        private TcpClient irc;
        private StreamReader reader;
        private PingSender ping;
        private ConnectionStatus connectionStatus;
        private Server server;

        public IrcConnection(EventFactory eventFactory, EventAggregator eventAggregator)
        {
            this.eventFactory = eventFactory;
            this.eventAggregator = eventAggregator;
        }

        public void ConnectTo(Server toServer, Credentials credentials)
        {
            this.server = toServer;

            string inputLine;
            try
            {
                irc = new TcpClient(toServer.Address, toServer.Port);
                stream = irc.GetStream();
                reader = new StreamReader(stream, Encoding.Default);
                writer = new StreamWriter(stream);

                var serverAddress = toServer.Address;

                ping = new PingSender(ref writer, ref serverAddress);
                ping.Start();

                SendCredentials(credentials);

                while (true)
                {
                    // Odotetaan viestejä
                    while ((inputLine = reader.ReadLine()) != null)
                    {
                        ProcessIncomingMessage(inputLine);
                    }

                    // Suljetaan streamit
                    writer.Close();
                    reader.Close();
                    irc.Close();
                }
            }
            catch (Exception e)
            {
                Thread.Sleep(1000);
            }
        }

        private void ProcessIncomingMessage(string inputLine)
        {
            var events = eventFactory.MakeByMessage(inputLine);
            foreach (var happenedEvent in events)
            {
                eventAggregator.Raise(happenedEvent);
            }
            Thread.Sleep(1000);
        }

        private void SendCredentials(Credentials credentials)
        {
            SendToServer(credentials.Identifier);
            SendToServer("NICK " + credentials.Name);
        }

        public void Disconnect()
        {
            try
            {

                //this.SendToServer("QUIT :" + this.botQuitMessage);
                this.SendToServer("QUIT : quit");
                ping.Abort();
                writer.Close();
                reader.Close();
                irc.Close();
            }

            catch (Exception e)
            {
            }
        }

        public void SendToServer(string message)
        {
            writer.WriteLine(message);
            writer.Flush();
        }

        public void SetConnectionStatusTo(ConnectionStatus newStatus)
        {
            eventAggregator.Raise(new ConnectionStatusChanged() { OldStatus = this.connectionStatus, NewStatus = newStatus });

            this.connectionStatus = newStatus;
        }

        public ConnectionStatus GetConnectionStatus()
        {
            return this.connectionStatus;
        }

        public Server GetServer()
        {
            return this.server;
        }
    }
}
