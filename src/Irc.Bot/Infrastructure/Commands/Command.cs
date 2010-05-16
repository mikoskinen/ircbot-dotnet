using ClassicIRCBot.Commands;
using ClassicIRCBot.Infrastructure;

namespace Irc.Bot.Infrastructure
{
    public interface Command
    {
        void Execute(MessageSender executor, IrcMessage message, CommandParameters parameters);
        string GetKeyWord();
        SecurityLevelEnum GetRequiredSecurityLevel();
        string[] GetHelp();
    }
}