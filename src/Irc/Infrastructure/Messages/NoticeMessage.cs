using Irc.Entities;

namespace Irc.Infrastructure.Messages
{
    public class NoticeMessage
    {
        public IrcUser User;
        public string Target;
        public string Notice;
    }
}
