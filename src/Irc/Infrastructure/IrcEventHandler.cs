namespace Irc
{
    public interface IrcEventHandler<T> where T : Event
    {
        void Handle(T args);
    }
}