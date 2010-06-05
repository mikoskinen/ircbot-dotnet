namespace Irc.Infrastructure
{
    public interface EventDispatcher
    {
        void Dispatch(Event ircEvent, object handler);
    }

    public class BasicEventDispatcher : EventDispatcher
    {
        public void Dispatch(Event ircEvent, object handler)
        {
            var method = handler.GetType().GetMethod("Handle", new[] { ircEvent.GetType() });
            method.Invoke(handler, new object[] { ircEvent });
        }
    }
}
