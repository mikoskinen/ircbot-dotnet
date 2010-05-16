using System.Collections.Generic;

namespace Irc.Infrastructure.Messages.Parsers
{
    public class ModeChangeParser
    {
        public static Modes GetModeChanges(string message)
        {
            var parameters = GetModeChangeParameters(message);

            var modeChanges = GetModeChanges(parameters);

            if (message.IsUserModeChangedMessage())
            {
                var modifiedUsers = GetChangedUserNames(parameters);
                modeChanges = CombineModeChangesToUserNames(modeChanges, modifiedUsers);
            }
            else
            {
                modeChanges = CombineModeChangesToChannel(modeChanges, message);
            }

            return modeChanges;

        }

        private static string[] GetModeChangeParameters(string message)
        {
            return message.GetDetails().Parameters.Split(' ');
        }

        private static List<string> GetChangedUserNames(string[] parameterWords)
        {
            var userNames = new List<string>();

            for (int i = 1; i < parameterWords.Length; i++)
            {
                userNames.Add(parameterWords[i]);
            }
            return userNames;
        }


        private static Modes GetModeChanges(string[] parameterWords)
        {
            var modes = new Modes();
            var modeWord = parameterWords[0];

            var currentModifier = modeWord[0];

            for (var i = 1; i < modeWord.Length; i++)
            {
                if (modeWord[i] == '+' || modeWord[i] == '-')
                {
                    if (modeWord[i] != currentModifier)
                    {
                        currentModifier = modeWord[i];
                    }
                }

                else
                {
                    var isOn = currentModifier == '+';
                    var mode = new Mode() { Identifier = modeWord[i].ToString(), IsOn = isOn };
                    modes.Add(mode);
                }
            }
            return modes;
        }

        private static Modes CombineModeChangesToUserNames(Modes modeChanges, List<string> userNames)
        {
            var modifiedModeChanges = modeChanges;

            for (var i = 0; i < modifiedModeChanges.Count; i++)
            {
                modifiedModeChanges[i].UserName = userNames[i];
            }

            return modifiedModeChanges;
        }

        private static Modes CombineModeChangesToChannel(Modes modeChanges, string message)
        {
            var modifiedModeChanges = modeChanges;

            var channel = message.GetIrcChannel();

            for (var i = 0; i < modifiedModeChanges.Count; i++)
            {
                modifiedModeChanges[i].UserName = channel.Name;
            }

            return modifiedModeChanges;
        }

        public static bool IsUserModeChangedMessage(string input)
        {
            if (!input.IsAction("MODE"))
                return false;

            return ParametersContainUser(input);
        }

        private static bool ParametersContainUser(string input)
        {
            var details = input.GetDetails();

            var parameters = details.Parameters.Split(' ');

            return parameters.Length > 1;
        }
    }
}