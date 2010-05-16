namespace Irc.Infrastructure.Messages
{
    public class MessageDetails
    {
        public string User;
        public string Hostmask;
        public string Action;
        public string Target;
        public string Parameters;

        public static MessageDetails Empty
        {
            get
            {
                return new MessageDetails() { User = "", Hostmask = "", Action = "", Target = "", Parameters = "" };
            }
        }
    }
}