using System.Collections.Generic;
using ClassicIRCBot.Infrastructure;
using Irc;
using Irc.Bot;
using Irc.Bot.Infrastructure;

namespace ClassicIRCBot.Commands
{
    public class Help : Command
    {
        private readonly Bot bot;

        public Help(Bot bot)
        {
            this.bot = bot;
        }

        public SecurityLevelEnum GetRequiredSecurityLevel()
        {
            return SecurityLevelEnum.Public;
        }

        public string[] GetHelp()
        {
            return new[] { "" };
        }

        public void Execute(MessageSender executor, IrcMessage message, CommandParameters parameters)
        {
            if (parameters.Count == 0)
            {
                bot.SayTo(executor.Channel, "Usage: help commandname");
                return;
            }

            var commands = bot.GetAllCommands();
            foreach (var command in commands)
            {
                if (command.GetKeyWord() != parameters[0]) continue;

                var help = command.GetHelp();

                this.ShowHelpTo(help, executor.Channel);
                return;
            }

            var unknownCommandMessage = string.Format("Unknown command {0}", parameters[0]);
            bot.SayTo(executor.Channel, unknownCommandMessage);
        }

        public string GetKeyWord()
        {
            return "help";
        }

        private void ShowHelpTo(IEnumerable<string> helpLines, string receiver)
        {
            foreach (var helpLine in helpLines)
            {
                bot.SayTo(receiver, helpLine);
            }
        }
    }

}
