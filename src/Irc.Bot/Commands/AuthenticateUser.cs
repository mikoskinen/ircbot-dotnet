using System;
using Classic.IRCBot;
using ClassicIRCBot.Infrastructure;
using Irc;
using Irc.Bot;
using Irc.Bot.Infrastructure;

namespace ClassicIRCBot.Commands
{
    public class AuthenticateUser : Command
    {
        private readonly Bot bot;
        private readonly DataSource dataSource;

        
        public AuthenticateUser(Bot bot, DataSource dataSource)
        {
            this.bot = bot;
            this.dataSource = dataSource;
        }

        public SecurityLevelEnum GetRequiredSecurityLevel()
        {
            return SecurityLevelEnum.Public;
        }

        public string[] GetHelp()
        {
            return new[]{""};
        }

        public void Execute(MessageSender executor, IrcMessage message, CommandParameters parameters)
        {
            if (parameters.Count != 2)
            {
                bot.SayTo(executor.Channel, "You must give username and password when authenticating");
                return;
            }

            var users = bot.GetAuthenticatedUsers();
            // First we check that the user who tries to log in isn't already logged in
            if (users.IsLoggedInHostMask(message.Sender.HostMask))
            {
                bot.SayTo(executor.Channel, "You have already authenticated");
                return;
            }

            // Lets continue by checking the login-information.
            var user = dataSource.LogInUser(parameters[0], parameters[1]);
            if (!user.Name.Equals(""))
            {
                // Lets check that user isn't already logged in
                if (users.IsLoggedInUserName(user.Name))
                {
                    var alreadyAuthenticatedMessage = string.Format("User {0} has already authenticated", user.Name);
                    bot.SayTo(executor.Channel, alreadyAuthenticatedMessage);
                }
                else
                {
                    user.HostMask = message.Sender.HostMask;
                    user.NickName = message.Sender.Name;

                    // We get user's permissions to different channels
                    user.ChannelSecurityLevelCollection = dataSource.GetChannelSecurityLevelsForUser(user.Id);

                    users.AddUser(user);
                    bot.SayTo(executor.Channel, "Authenticated succesfully");
                }
            }
            else
                bot.SayTo(executor.Channel, "Wrong username or password");
        }

        public string GetKeyWord()
        {
            return "auth";
        }
    }
}
