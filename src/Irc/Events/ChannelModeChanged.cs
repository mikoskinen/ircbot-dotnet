using Irc.Infrastructure.Messages.Parsers;

namespace Irc.Events
{
    public class ChannelModeChanged : UserModeChanged
    {
        public override bool DoesOccurBecauseOf(string message)
        {
            return message.IsChannelModeChangedMessage();
        }

        public override MessageBasedEvent MakeFrom(string message)
        {
            var modes = message.GetModeChanges();
            var user = message.GetUser();
            var channel = message.GetIrcChannel();

            return new ChannelModeChanged() { Channel = channel, ModeChanges = modes, User = user };
        }
    }
}
