using Irc.Entities;

namespace Irc.Infrastructure.Messages
{
    public class PartMessage
    {
        public IrcUser User;
        public IrcChannel Channel;
    }
}
