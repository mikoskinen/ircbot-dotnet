using System.Text.RegularExpressions;

namespace Irc.Infrastructure.Messages.Parsers
{
    public class DetailsParser
    {
        public static MessageDetails GetDetails(string inputLine)
        {
            var ircRegex = GetIrcRegex();

            var matchCollection = ircRegex.Matches(inputLine);

            var details = CreateDetails(matchCollection);

            return details;
        }

        private static Regex GetIrcRegex()
        {
            const string user = @":(?<user>([^!]+)) !";
            const string hostMask = @"(?<hostmask>(\S+) )\s+";
            const string action = @"(?<action>(\S+) )\s+";
            const string target = @":?(?<target>(\S+) )\s*";
            const string parameters = @"(?<parameters>(?:[:+-]+(.*))  )?";
            var pattern = string.Format("{0}{1}{2}{3}{4}", user, hostMask, action, target, parameters);

            const RegexOptions myRegexOptions = RegexOptions.IgnorePatternWhitespace;

            return new Regex(pattern, myRegexOptions);
        }

        private static MessageDetails CreateDetails(MatchCollection matchCollection)
        {

            if (matchCollection.Count == 0)
                return MessageDetails.Empty;

            var details = new MessageDetails
                              {
                                  User = matchCollection[0].Groups["user"].ToString(),
                                  Hostmask = matchCollection[0].Groups["hostmask"].ToString(),
                                  Action = matchCollection[0].Groups["action"].ToString(),
                                  Target = matchCollection[0].Groups["target"].ToString(),
                                  Parameters = matchCollection[0].Groups["parameters"].ToString()
                              };

            if (details.Parameters.Length > 0)
            {
                if (details.Parameters[0] == ':')
                    details.Parameters = details.Parameters.Substring(1);
            }

            return details;
        }
    }
}