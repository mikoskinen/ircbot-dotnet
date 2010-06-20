using System;

namespace Irc.Events
{
    public class ServerMessageReceived : MessageBasedEvent
    {
        public DateTime Time;
        public string Message;

        public bool DoesOccurBecauseOf(string message)
        {
            return true;
        }

        public MessageBasedEvent MakeFrom(string message)
        {
            var instance = new ServerMessageReceived {Message = message, Time = DateTime.Now};

            return instance;
        }
    }
}
