using Irc.Entities;

namespace Irc.Infrastructure.Messages
{
    public class TopicMessage
    {
        public IrcUser User;
        public IrcChannel Channel;
        public string Topic;
    }
}
