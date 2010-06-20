using Irc.Entities;

namespace Irc.Events
{
    public class ServerConnectionDisconnected : Event
    {
        public Server Server;
    }
}
