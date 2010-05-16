using ClassicIRCBot.Commands;
using ClassicIRCBot.Infrastructure;
using Irc.Bot.Infrastructure;

namespace Irc.Bot.Stubs
{
    public class CommandStub : Command
    {
        public bool Executed = false;
        public CommandParameters Parameters;
        private SecurityLevelEnum securityLevel;
        public SecurityLevelEnum GetRequiredSecurityLevel()
        {
            return securityLevel;
        }

        public static CommandStub Make()
        {
            return new CommandStub();
        }

        public void Execute(MessageSender executor, IrcMessage message, CommandParameters parameters)
        {
            Executed = true;

            Parameters = parameters;
        }

        public string GetKeyWord()
        {
            return "test";
        }

        public string[] GetHelp()
        {
            const string description = "Test command";
            const string usage = "Usage: test";

            return new[] { description, usage };
        }

        public void SetRequiredSecurityLevel(SecurityLevelEnum securityLevelEnum)
        {
            securityLevel = securityLevelEnum;
        }

    }
}