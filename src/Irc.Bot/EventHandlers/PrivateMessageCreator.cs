using System;
using Irc;
using Irc.Bot;
using Irc.Bot.Events;
using Irc.Events;

namespace ClassicIRCBot.EventHandlers
{
    public class PrivateMessageCreator : IrcEventHandler<ServerMessageReceived>
    {
        private readonly Bot bot;
        private readonly PrivateMessageParser messageParser;
        private readonly EventAggregator eventAggregator;
        private string receiver;

        public PrivateMessageCreator(Bot bot, PrivateMessageParser messageParser, EventAggregator eventAggregator)
        {
            this.bot = bot;
            this.messageParser = messageParser;
            this.eventAggregator = eventAggregator;
        }

        public void Handle(ServerMessageReceived args)
        {
            if (!IsReceiverSet())
                SetReceiver();

            var inputLine = args.Message;

            if (inputLine.IndexOf("PRIVMSG") == -1) return;
            
            var message = messageParser.Parse(inputLine, receiver);
            try
            {
                eventAggregator.Raise<PrivateMessageReceived>(new PrivateMessageReceived() { Message = message });
            }
            catch (Exception)
            {
            }
        }

        private bool IsReceiverSet()
        {
            return !string.IsNullOrEmpty(receiver);
        }

        private void SetReceiver()
        {
            this.receiver = bot.GetCredentials().Name;
        }
    }
}
