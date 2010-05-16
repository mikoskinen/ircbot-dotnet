using System;

namespace Irc
{
    public class PrivateMessageParserImpl : PrivateMessageParser
    {
        private string receiver;
        private string input;

        public IrcMessage Parse(string inputLine, string receiverName)
        {
            // Parse input line which is in format ":UserNick!~user@as34-4334-3434.inter.net PRIVMSG #mychannel :#auth user password"

            try
            {

                this.input = inputLine;
                this.receiver = receiverName;

                return Parse();

            }
            catch (Exception)
            {
                return IrcMessage.Empty;
            }
        }

        private IrcMessage Parse()
        {
            var messageSender = GetMessageSender();
            var message = GetMessage();

            return new IrcMessage(messageSender, message, input);
        }

        private MessageSender GetMessageSender()
        {
            var senderName = ParseSenderName();
            var hostMask = ParseHostMask();
            var channel = ParseChannel(senderName);
            return new MessageSender(senderName, hostMask, channel);
        }

        private string ParseSenderName()
        {
            return input.Substring(1, input.IndexOf("!") - 1);
        }



        private string ParseHostMask()
        {
            return input.Substring(input.IndexOf("!") + 1, input.IndexOf(" ") - input.IndexOf("!") - 1);
        }
        
        private string ParseChannel(string senderName)
        {
            var messageWithoutIdentifier = this.GetMessageWithoutIdentifier();

            var channel = messageWithoutIdentifier.Substring(0, messageWithoutIdentifier.IndexOf(" "));
            if (channel == receiver)
                channel = senderName;
            return channel;
        }

        private string GetMessage()
        {
            var messageWithoutIdentifier = this.GetMessageWithoutIdentifier();
            
            return messageWithoutIdentifier.Substring(messageWithoutIdentifier.IndexOf(":") + 1);
        }

        private string GetMessageWithoutIdentifier()
        {
            return input.Substring(input.IndexOf("PRIVMSG") + 8);
        }


    }
}