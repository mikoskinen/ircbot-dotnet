using Irc.Events;

namespace Irc.Stubs
{
    public class MultiHandlerStub : IrcEventHandler<ConnectedToServer>, IrcEventHandler<PingReceived>
    {
        public bool HandledConnectedToServer = false;
        public bool HandledPingReceived = false;

        public void Handle(PingReceived args)
        {
            HandledPingReceived = true;
        }

        public void Handle(ConnectedToServer args)
        {
            HandledConnectedToServer = true;
        }
    }
}