using System.Text;
using ClassicIRCBot.Infrastructure;
using Irc;
using Irc.Bot;
using Irc.Bot.Infrastructure;

namespace ClassicIRCBot.Commands
{
    public class SayTo : Command
    {
        private readonly Bot bot;

        public SayTo(Bot bot)
        {
            this.bot = bot;
        }

        public SecurityLevelEnum GetRequiredSecurityLevel()
        {
            return SecurityLevelEnum.Admin;
        }

        public string[] GetHelp()
        {
            return new[] { "" };
        }

        public string GetKeyWord()
        {
            return "say";
        }

        public void Execute(MessageSender source, IrcMessage message, CommandParameters parameters)
        {
            if (parameters == null || parameters.Count == 0)
                return;

            var sayToMessage = GetMessageFromParameters(parameters);

            bot.SayTo(source.Channel, sayToMessage);
        }

        private string GetMessageFromParameters(CommandParameters parameters)
        {
            var sayToMessage = new StringBuilder();

            for (int i = 0; i < parameters.Count; i++)
            {
                sayToMessage.AppendFormat("{0} ", parameters[i]);
            }

            return sayToMessage.ToString().TrimEnd();
        }
    }
}
