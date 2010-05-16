using ClassicIRCBot.Commands;
using Irc.Bot.Infrastructure;

namespace Irc.Bot.Stubs
{
    public class SecurityLevelCheckerStub : SecurityLevelChecker
    {
        private bool returnValue = false;

        public bool IsCommandExecutionAllowed(Command command, Bot bot, string hostMask)
        {
            return returnValue;
        }

        public void SetReturnValue(bool value)
        {
            this.returnValue = value;
        }
    }
}