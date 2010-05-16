using Irc.Entities;

namespace Irc.Infrastructure.Messages.Parsers
{
    public static class MessageParser
    {
        public static MessageDetails GetDetails(this string inputLine)
        {
            return DetailsParser.GetDetails(inputLine);
        }

        public static Modes GetModeChanges(this string message)
        {
            var modeChanges = ModeChangeParser.GetModeChanges(message);

            return modeChanges;
        }

        public static IrcUser GetUser(this string input)
        {
            var hostMask = GetHostmask(input);
            var userName = GetSenderName(input);
            return new IrcUser(userName, hostMask);
        }

        public static IrcChannel GetIrcChannel(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return new IrcChannel("");

            var details = GetDetails(input);
            return new IrcChannel(details.Target);
        }

        public static string GetSenderName(this string input)
        {
            var details = input.GetDetails();
            
            return details.User;
        }

        public static string GetHostmask(this string input)
        {
            var details = input.GetDetails();

            return details.Hostmask;
        }

        public static string GetNewTopic(this string input)
        {
            var details = input.GetDetails();

            return details.Parameters;
        }

        public static JoinMessage CreateJoinMessage(this string input)
        {
            var user = GetUser(input);
            var channel = GetIrcChannel(input);
            return new JoinMessage(user, channel);
        }

        public static bool IsTopicChangedMessage(this string input)
        {
            return input.IsAction("TOPIC");
        }

        public static bool IsJoinMessage(this string input)
        {
            return input.IsAction("JOIN");
        }

        public static bool IsPartMessage(this string input)
        {
            return input.IsAction("PART");
        }

        public static bool IsUserModeChangedMessage(this string input)
        {

            return ModeChangeParser.IsUserModeChangedMessage(input);
        }

        public static bool IsChannelModeChangedMessage(this string input)
        {
            return IsUserModeChangedMessage(input);
        }

        public static bool IsAction(this string input, string action)
        {
            var details = GetDetails(input);

            return details.Action == action;
        }

    }
}