using Irc.Entities;
using Irc.Infrastructure.Messages.Parsers;

namespace Irc.Events
{
    public class TopicChanged : MessageBasedEvent
    {
        public IrcUser User;
        public IrcChannel Channel { get; set; }
        public string NewTopic;

        public bool DoesOccurBecauseOf(string message)
        {
            return message.IsTopicChangedMessage();
        }

        public MessageBasedEvent MakeFrom(string message)
        {
            var user = message.GetUser();
            var channel = message.GetIrcChannel();
            var newTopic = message.GetNewTopic();

            return new TopicChanged() {User = user, Channel = channel, NewTopic = newTopic};
        }
    }
}
