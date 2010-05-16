using Irc.Bot;
using Irc.Bot.Infrastructure;
using StructureMap;

namespace IrcBot.GUI
{
    public class BotFactoryImpl : BotFactory
    {
        private readonly IContainer container;

        public BotFactoryImpl(IContainer container)
        {
            this.container = container;
        }

        public Bot Make()
        {
            var bot = container.GetInstance<Bot>();
            var commands = container.GetAllInstances<Command>();
            bot.SetCommands(commands);

            return bot;
        }
    }
}
