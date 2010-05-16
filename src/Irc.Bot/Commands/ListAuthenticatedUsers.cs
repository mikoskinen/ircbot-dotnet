using ClassicIRCBot.Infrastructure;
using Irc;
using Irc.Bot;
using Irc.Bot.Infrastructure;

namespace ClassicIRCBot.Commands
{
    public class ListAuthenticatedUsers : Command
    {
        private readonly Bot bot;

        public ListAuthenticatedUsers(Bot bot)
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
            return "whosein";
        }

        public void Execute(MessageSender source, IrcMessage message, CommandParameters parameters)
        {
            var users = bot.GetAuthenticatedUsers();
            if (users.Count == 0)
            {
                bot.SayTo(source.Channel, "No authenticated users");
                return;
            }

            bot.SayTo(source.Channel, "Authenticated users:");
            bot.SayTo(source.Channel, "----");

            foreach (var user in users.GetLoggedInUsers())
            {
                var userInformationMessage = string.Format("{0}({1}) - {2}", 
                    user.Name, user.HostMask, user.SecurityLevelInformation.SecurityLevelDescription);

                bot.SayTo(source.Channel, userInformationMessage);
            }
        }
    }
}
