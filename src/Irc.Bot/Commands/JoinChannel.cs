using ClassicIRCBot.Infrastructure;
using Irc;
using Irc.Bot;
using Irc.Bot.Infrastructure;

namespace ClassicIRCBot.Commands
{
    public class JoinChannel : Command
    {
        private readonly Bot bot;

        public JoinChannel(Bot bot)
        {
            this.bot = bot;
        }

        public SecurityLevelEnum GetRequiredSecurityLevel()
        {
            return SecurityLevelEnum.Owner;
        }

        public string[] GetHelp()
        {
            return new[] { "" };
        }

        public void Execute(MessageSender executor, IrcMessage message, CommandParameters parameters)
        {
            if (parameters.Count == 0 || parameters.Count > 2)
            {
                var invalidParametersMessage = string.Format("Invalid amount of parameters. Usage: {0} {1} [{2}]",
                                                             GetKeyWord(), "#channel", "password");
                bot.SayTo(executor.Channel, invalidParametersMessage);
                return;
            }

            var channel = parameters[0];
            var password = GetPassword(parameters);

            bot.JoinChannel(channel, password);
        }

        public string GetKeyWord()
        {
            return "join";
        }

        private string GetPassword(CommandParameters parameters)
        {
            var password = "";
            if (parameters.Count > 1)
                password = parameters[1];

            return password;
        }
    }
}
