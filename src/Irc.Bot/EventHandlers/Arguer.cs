using Irc;
using Irc.Bot;
using Irc.Events;
using Irc.Infrastructure.Messages;

namespace ClassicIRCBot.EventHandlers
{
    public class Arguer : IrcEventHandler<UserModeChanged>
    {
        private readonly Bot bot;

        public Arguer(Bot bot)
        {
            this.bot = bot;
        }

        public void Handle(UserModeChanged args)
        {
            var modeChange = args.ModeChanges[0];

            if (!WasSomeoneGivenOp(modeChange))
                return;

            if (DidBotGetOp(modeChange))
            {
                var thankyouMessage = string.Format("Thank you {0}!", args.User.Name);
                bot.SayTo(args.Channel.Name, thankyouMessage);
                return;
            }

            var argument = string.Format("{0}, why did you op {1} instead of me?", args.User.Name, modeChange.UserName);
            bot.SayTo(args.Channel.Name, argument);

        }

        private bool WasSomeoneGivenOp(Mode modeChange)
        {
            return modeChange.Identifier == "o" && modeChange.IsOn;
        }

        private bool DidBotGetOp(Mode modeChange)
        {
            return modeChange.UserName == bot.GetCredentials().Name && WasSomeoneGivenOp(modeChange);
        }
    }
}
