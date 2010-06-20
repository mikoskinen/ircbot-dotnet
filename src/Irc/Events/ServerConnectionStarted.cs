using Irc.Entities;
using Irc.Infrastructure;

namespace Irc.Events
{
    public class ServerConnectionStarted : Event
    {
        public Connection ServerConnection;
    }
}
