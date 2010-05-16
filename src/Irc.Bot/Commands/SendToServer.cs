using System;
using System.Text;
using ClassicIRCBot.Infrastructure;
using Irc;
using Irc.Bot;
using Irc.Bot.Infrastructure;

namespace ClassicIRCBot.Commands
{
    public class SendToServer: Command
    {
        private readonly Bot bot;

        public SendToServer(Bot bot)
        {
            this.bot = bot;
        }

        public void Execute(MessageSender executor, IrcMessage message, CommandParameters parameters)
        {
            if (parameters.Count == 0)
                return;

            var sendToServerMessage = GetParametersInOneSentence(parameters);

            bot.SendToServer(sendToServerMessage);
        }

        private string GetParametersInOneSentence(CommandParameters parameters)
        {
            var sendToServerMessage = new StringBuilder();
            foreach (var parameter in parameters)
            {
                sendToServerMessage.Append(parameter);
                sendToServerMessage.Append(" ");
            }
            return sendToServerMessage.ToString().TrimEnd();
        }

        public string GetKeyWord()
        {
            return "sendtoserver";
        }

        public SecurityLevelEnum GetRequiredSecurityLevel()
        {
            return SecurityLevelEnum.Admin;
        }

        public string[] GetHelp()
        {
            return new string[]{};
        }
    }
}
