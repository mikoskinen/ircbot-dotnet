using System.Collections.Generic;

namespace Irc.Infrastructure
{
    public interface EventFactory
    {
        List<MessageBasedEvent> MakeByMessage(string message);
    }
}