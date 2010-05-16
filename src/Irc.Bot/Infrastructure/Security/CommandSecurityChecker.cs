using Irc.Bot;
using Irc.Bot.Infrastructure;

namespace ClassicIRCBot.Commands
{
    public interface SecurityLevelChecker
    {
        bool IsCommandExecutionAllowed(Command command, Bot bot, string hostMask);
    }

    public class CommandSecurityChecker : SecurityLevelChecker
    {
        public bool IsCommandExecutionAllowed(Command command, Bot bot, string hostMask)
        {
            var users = bot.GetAuthenticatedUsers();
            var user = users.GetUserByHostMask(hostMask);

            return user.SecurityLevel >= (int)command.GetRequiredSecurityLevel();
        }
    }
}