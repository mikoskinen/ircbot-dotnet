namespace Irc.Events
{
    public class PingReceived : MessageBasedEvent
    {
        public string PingMessage { get; set; }

        public bool DoesOccurBecauseOf(string message)
        {
            return message.IndexOf("PING :") == 0;
        }

        public MessageBasedEvent MakeFrom(string message)
        {
            return new PingReceived() {PingMessage = message};
        }
    }
}
