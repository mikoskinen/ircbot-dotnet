using System;

namespace Irc
{
    public class MessageSender : IEquatable<MessageSender>
    {
        private readonly string name;
        private readonly string hostMask;
        private readonly string channel;

        public MessageSender(string name, string hostMask, string channel)
        {
            this.name = name;
            this.channel = channel;
            this.hostMask = hostMask;
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public string HostMask
        {
            get
            {
                return hostMask;
            }
        }

        public string Channel
        {
            get
            {
                return channel;
            }
        }

        public static MessageSender Empty
        {
            get
            {
                var sender = new MessageSender("", "", "");
                return sender;                    
            }
        }

        #region Equals

        public bool Equals(MessageSender other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.name, name) && Equals(other.hostMask, hostMask) && Equals(other.channel, channel);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(MessageSender)) return false;
            return Equals((MessageSender)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (name != null ? name.GetHashCode() : 0);
                result = (result * 397) ^ (hostMask != null ? hostMask.GetHashCode() : 0);
                result = (result * 397) ^ (channel != null ? channel.GetHashCode() : 0);
                return result;
            }
        }

        #endregion

    }
}