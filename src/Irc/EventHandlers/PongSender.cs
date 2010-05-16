using System.Threading;
using Irc.Events;
using Irc.Infrastructure;

namespace Irc.EventHandlers
{
    public class PongSender : IrcEventHandler<PingReceived>
    {
        private readonly Connection connection;

        public PongSender(Connection connection)
        {
            this.connection = connection;
        }

        public void Handle(PingReceived args)
        {
            var identifier = GetIdentifier(args);

            var pongMessage = string.Format("PONG :{0}", identifier);
            connection.SendToServer(pongMessage);

            Thread.Sleep(1000);
        }

        private string GetIdentifier(PingReceived args)
        {
            var index = args.PingMessage.LastIndexOf(":");
            return args.PingMessage.Substring(index + 1);
        }
    }
}