using System;
using ClassicIRCBot.Infrastructure;
using Irc;
using Irc.Bot;
using Irc.Bot.Infrastructure;

namespace ClassicIRCBot.Commands
{
    public class DisconnectFromServer :  Command
    {
        private readonly Bot bot;

        public DisconnectFromServer(Bot bot)
        {
            this.bot = bot;
        }

        public  SecurityLevelEnum GetRequiredSecurityLevel()
        {
            return SecurityLevelEnum.Owner;
        }

        public string[] GetHelp()
        {
            return new[] { "" };
        }

        public void Execute(MessageSender executor, IrcMessage message, CommandParameters parameters)
        {
            bot.DisconnectFromServer();
        }

        public  string GetKeyWord()
        {
            return "disconnect";
        }
    }
}
