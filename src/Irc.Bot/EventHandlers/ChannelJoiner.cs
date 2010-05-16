using System.Threading;
using Irc.Bot;
using Irc.Events;

namespace ClassicIRCBot.EventHandlers
{
    public class ChannelJoiner : Irc.IrcEventHandler<ConnectedToServer>
    {
        private readonly Bot bot;

        public ChannelJoiner(Bot bot)
        {
            this.bot = bot;
        }

        public void Handle(ConnectedToServer args)
        {
            bot.JoinAllChannels();
            Thread.Sleep(200);
        }
    }
}
