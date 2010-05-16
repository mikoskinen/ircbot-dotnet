using Irc.Events;

namespace Irc.Stubs
{
    public class ConnectedToServerHandlerStub : IrcEventHandler<ConnectedToServer>
    {
        public bool Handled;

        public void Handle(ConnectedToServer args)
        {
            Handled = true;
        }
    }
}