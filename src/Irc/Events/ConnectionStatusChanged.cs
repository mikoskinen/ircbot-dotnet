using Irc.Infrastructure;

namespace Irc.Events
{
    public class ConnectionStatusChanged : Event
    {
        public ConnectionStatus OldStatus;
        public ConnectionStatus NewStatus;
    }
}
