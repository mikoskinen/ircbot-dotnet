using ClassicIRCBot.Infrastructure;
using Irc;
using Irc.Bot;
using Irc.Bot.Infrastructure;

namespace ClassicIRCBot.Commands
{
    public class ListCommands : Command
    {
        private readonly Bot bot;

        public ListCommands(Bot bot )
        {
            this.bot = bot;
        }

        public  SecurityLevelEnum GetRequiredSecurityLevel()
        {
            return SecurityLevelEnum.Public;
        }

        public void Execute(MessageSender executor, IrcMessage message, CommandParameters parameters)
        {
            var commands = bot.GetAllCommands();

            foreach (var command in commands)
            {
                bot.SayTo(executor.Channel, command.GetKeyWord());
            }
        }

        public  string GetKeyWord()
        {
            return "commands";
        }

        public string[] GetHelp()
        {
            const string description = "Lists all the available commands";
            const string usage = "Usage: commands";

            return new[] { description, usage };
        }
    }
}
