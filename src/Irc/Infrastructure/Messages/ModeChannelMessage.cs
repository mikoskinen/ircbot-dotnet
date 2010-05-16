using Irc.Entities;

namespace Irc.Infrastructure.Messages
{
    public class ModeChannelMessage
    {
        public Mode Mode;
        public IrcChannel Channel;
    }
}
