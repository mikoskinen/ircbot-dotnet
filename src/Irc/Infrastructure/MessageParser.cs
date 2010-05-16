namespace Irc
{
    public interface PrivateMessageParser
    {
        IrcMessage Parse(string inputLine, string receiverName);
    }
}