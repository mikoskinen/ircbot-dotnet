using System;
using ClassicIRCBot.Infrastructure;
using Irc;
using Irc.Bot;
using Irc.Bot.Infrastructure;

namespace ClassicIRCBot.Commands
{
    public class PartChannel : Command
    {
        private readonly Bot bot;

        public PartChannel(Bot bot)
        {
            this.bot = bot;
        }

        public void Execute(MessageSender source, IrcMessage message, CommandParameters parameters)
        {
            if (parameters.Count == 0 || parameters.Count > 1)
                throw new ParameterException();

            bot.PartChannel(parameters[0]);
        }

        public string GetKeyWord()
        {
            return "part";
        }

        public SecurityLevelEnum GetRequiredSecurityLevel()
        {
            return SecurityLevelEnum.Owner;
        }

        public string[] GetHelp()
        {
            return new[] { "" };
        }
    }
}
