using Irc;
using Irc.Bot;
using Irc.Events;

namespace ClassicIRCBot.EventHandlers
{
    public class GoodByer : IrcEventHandler<UserPartChannel>
    {
        private readonly Bot bot;

        public GoodByer(Bot bot)
        {
            this.bot = bot;
        }

        public void Handle(UserPartChannel args)
        {
            bot.SayTo(args.User.Name, "Goodbye!");
        }
    }
}
