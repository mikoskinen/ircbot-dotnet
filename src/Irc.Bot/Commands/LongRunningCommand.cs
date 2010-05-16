//using System.Threading;
//using ClassicIRCBot.Infrastructure;
//using Irc;
//using Irc.Bot.Infrastructure;

//namespace ClassicIRCBot.Commands
//{
//    public class LongRunningCommand : Command
//    {
//        public SecurityLevelEnum GetRequiredSecurityLevel()
//        {
//            return SecurityLevelEnum.Registered;
//        }

//        public string[] GetHelp()
//        {
//            return new[] { "" };
//        }

//        public void Execute(MessageSender executor, IrcMessage message, CommandParameters parameters)
//        {
//            Thread.Sleep(10 * 1000);
//        }

//        public string GetKeyWord()
//        {
//            return "longrunning";
//        }

//    }
//}
