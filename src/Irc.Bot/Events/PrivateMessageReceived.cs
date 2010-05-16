using System;

namespace Irc.Bot.Events
{
    public class PrivateMessageReceived : Event
    {
        public IrcMessage Message { get; set; }
        public bool DoesOccurBecauseOf(string message)
        {
            throw new NotImplementedException();
        }

        public Event MakeFrom(string message)
        {
            throw new NotImplementedException();
        }
    }
}