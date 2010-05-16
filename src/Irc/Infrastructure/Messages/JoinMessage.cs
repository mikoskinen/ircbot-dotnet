using Irc.Entities;

namespace Irc.Infrastructure.Messages
{
    public class JoinMessage : Message
    {
        public readonly IrcUser User;
        public readonly IrcChannel Channel;

        public JoinMessage(IrcUser user, IrcChannel channel)
        {
            User = user;
            Channel = channel;
        }
    }
}
