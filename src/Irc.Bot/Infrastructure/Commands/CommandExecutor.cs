using ClassicIRCBot.Commands;
using ClassicIRCBot.Infrastructure;
using Irc.Bot.Infrastructure;

namespace Irc.Bot.Commands
{
    public interface CommandExecutor
    {
        void Execute(Command command, MessageSender executor, IrcMessage message, CommandParameters parameters);
    }

    public class CommandExecutorImpl : CommandExecutor
    {
        private readonly Bot bot;
        private readonly SecurityLevelChecker securityLevelChecker;

        public CommandExecutorImpl(Bot bot, SecurityLevelChecker securityLevelChecker)
        {
            this.bot = bot;
            this.securityLevelChecker = securityLevelChecker;
        }

        public void Execute(Command command, MessageSender executor, IrcMessage message, CommandParameters parameters)
        {
            var isExecutionAllowed = securityLevelChecker.IsCommandExecutionAllowed(command, bot, executor.HostMask);
            if (!isExecutionAllowed)
            {
                bot.SayTo(executor.Channel, "You don't have required permission for this command");
                return;
            }

            command.Execute(executor, message, parameters);
        }
    }
}