using Irc.Entities;

namespace Irc.Infrastructure.Messages
{
    public class QuitMessage
    {
        public IrcUser User;
        public string Message;
        public bool IsSplit;
    }
}
