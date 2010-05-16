namespace Irc.Events
{
    public class ServerMessageReceived : MessageBasedEvent
    {
        public string Message;

        public bool DoesOccurBecauseOf(string message)
        {
            return true;
        }

        public MessageBasedEvent MakeFrom(string message)
        {
            var instance = new ServerMessageReceived {Message = message};

            return instance;
        }
    }
}
