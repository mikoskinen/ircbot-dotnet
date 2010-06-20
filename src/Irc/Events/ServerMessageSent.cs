using System;
using Irc.Entities;

namespace Irc.Events
{
    public class ServerMessageSent : Event
    {
        public DateTime Time;
        public string Message;
        public Server Server;
    }
}
