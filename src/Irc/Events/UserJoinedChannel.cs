using Irc.Entities;
using Irc.Infrastructure.Messages.Parsers;

namespace Irc.Events
{
    public class UserJoinedChannel : MessageBasedEvent
    {
        public IrcUser User { get; set; }
        public IrcChannel Channel { get; set; }

        public virtual bool DoesOccurBecauseOf(string message)
        {
            return message.IsJoinMessage();
        }

        public virtual MessageBasedEvent MakeFrom(string message)
        {
            var user = message.GetUser();
            var channel = message.GetIrcChannel();

            return new UserJoinedChannel() {Channel = channel, User = user};
        }
    }

}
