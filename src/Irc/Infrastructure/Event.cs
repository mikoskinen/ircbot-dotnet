namespace Irc
{
    public interface Event
    {
    }

    public interface MessageBasedEvent : Event
    {
        bool DoesOccurBecauseOf(string message);
        MessageBasedEvent MakeFrom(string message);
    }
}

