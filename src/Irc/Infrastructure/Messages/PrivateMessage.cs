using Irc.Entities;

namespace Irc.Infrastructure.Messages
{
    public class PrivateMessage
    {
        public IrcUser User;
        public string Target;
        public string Message;
    }
}
