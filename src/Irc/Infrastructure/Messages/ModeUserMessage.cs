using Irc.Entities;

namespace Irc.Infrastructure.Messages
{
    public class ModeUserMessage
    {
        public Mode Mode;
        public IrcChannel Channel;
        public IrcUser User;
    }
}
