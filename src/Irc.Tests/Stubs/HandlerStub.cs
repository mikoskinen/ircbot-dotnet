using Irc.Events;

namespace Irc.Stubs
{
    public class HandlerStub : Irc.IrcEventHandler<ServerMessageReceived>
    {
        public bool Handled;

        public void Handle(ServerMessageReceived args)
        {
            Handled = true;
        }
    }
}