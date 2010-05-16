using System;

namespace Irc
{
    public class IrcMessage : IEquatable<IrcMessage>
    {
        private readonly string message;
        private readonly MessageSender sender;
        private string originalMessage;

        public string Message
        {
            get { return message; }
        }

        public MessageSender Sender
        {
            get
            {
                return sender;
            }
        }

        public string OriginalMessage
        {
            get {
                return originalMessage;
            }
        }

        public IrcMessage(MessageSender sender, string message, string originalMessage)
        {
            this.message = message;
            this.sender = sender;
            this.originalMessage = originalMessage;
        }

        public static IrcMessage Empty
        {
            get
            {
                var sender = new MessageSender("", "", "");
                var message = new IrcMessage(sender, "", "");

                return message;

            }
        }

        #region Equals

        public bool Equals(IrcMessage other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.message, message) && Equals(other.sender, sender) && Equals(other.originalMessage, originalMessage);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(IrcMessage)) return false;
            return Equals((IrcMessage)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (message != null ? message.GetHashCode() : 0);
                result = (result * 397) ^ (sender != null ? sender.GetHashCode() : 0);
                result = (result * 397) ^ (originalMessage != null ? originalMessage.GetHashCode() : 0);
                return result;
            }
        }

        #endregion
    }
}