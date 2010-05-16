using Irc.Infrastructure.Messages.Parsers;

namespace Irc.Events
{
    public class UserPartChannel : UserJoinedChannel
    {
        public override bool DoesOccurBecauseOf(string message)
        {
            return message.IsPartMessage();
        }

        public override MessageBasedEvent MakeFrom(string message)
        {
            var user = message.GetUser();
            var channel = message.GetIrcChannel();

            return new UserPartChannel() { Channel = channel, User = user };
        }
    }
}
