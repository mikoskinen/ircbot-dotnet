using Irc.Entities;
using Irc.Infrastructure.Messages;
using Irc.Infrastructure.Messages.Parsers;

namespace Irc.Events
{
    public class UserModeChanged : MessageBasedEvent
    {
        public IrcChannel Channel;
        public IrcUser User;
        public Modes ModeChanges;

        public virtual bool DoesOccurBecauseOf(string message)
        {
            return message.IsUserModeChangedMessage();
        }

        public virtual MessageBasedEvent MakeFrom(string message)
        {
            var modes= message.GetModeChanges();
            var user = message.GetUser();
            var channel = message.GetIrcChannel();

            return new UserModeChanged() {Channel = channel, ModeChanges = modes, User = user};
        }
    }
}
