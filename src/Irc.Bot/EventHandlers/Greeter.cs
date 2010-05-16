using Irc;
using Irc.Bot;
using Irc.Events;

namespace ClassicIRCBot.EventHandlers
{
    public class Greeter : IrcEventHandler<UserJoinedChannel>
    {
        private readonly Bot bot;

        public Greeter(Bot bot)
        {
            this.bot = bot;
        }

        public void Handle(UserJoinedChannel args)
        {
            if (args.User.Name == bot.GetCredentials().Name)
                return;

            var message = string.Format("Hello {0}", args.User.Name);
            bot.SayTo(args.Channel.Name, message);
        }
    }
}
