using System;
using System.Collections.Generic;
using Irc.Bot.Entities;
using Irc.Bot.Infrastructure;
using Irc.Entities;
using Irc.Infrastructure;

namespace Irc.Bot
{
    /// <summary>
    /// ClassicIrcBot, which is inherited from IrcBot
    /// </summary>
    /// 
    public class ClassicIrcBot : Bot
    {
        #region Members

        private readonly Users users = new Users();
        private List<Channel> channels = new List<Channel>();
        private readonly Connection connection;
        private Credentials credentials;
        private IEnumerable<Command> commands;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor that fits in with the BotHandler-class
        /// </summary>
        public ClassicIrcBot(Connection connection)
        {
            this.connection = connection;

            // Let's read some settings from app.config
            var settingsReader = new System.Configuration.AppSettingsReader();
            try
            {
                //this.botQuitMessage = settingsReader.GetValue("QuitMessage", typeof(string)).ToString();
                //this.botUser = settingsReader.GetValue("BotUser", typeof(string)).ToString();
                //this.botNick = settingsReader.GetValue("BotNick", typeof(string)).ToString();
                //this.ScriptsDirectory = settingsReader.GetValue("ScriptsDirectory", typeof(string)).ToString();
                //this.scriptsErrorDirectory = settingsReader.GetValue("ScriptsErrorDirectory", typeof(string)).ToString();
                //this.ScriptsBackUpDirectory = settingsReader.GetValue("ScriptsBackupDirectory", typeof(string)).ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        #endregion

        #region Public Methods

        public void JoinChannel(string channel, string password)
        {
            var message = string.Format("JOIN {0} {1}", channel, password);
            this.SendToServer(message);
        }

        public void DisconnectFromServer()
        {
            connection.Disconnect();
        }

        public void SendToServer(string message)
        {
            try
            {
                this.connection.SendToServer(message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public Credentials GetCredentials()
        {
            return this.credentials;
        }

        public void PartChannel(string channel)
        {
            var partMessage = string.Format("PART {0}", channel);
            this.connection.SendToServer(partMessage);
        }

        public void SetCommands(IEnumerable<Command> commands)
        {
            this.commands = commands;
        }

        public void JoinAllChannels()
        {
            foreach (var channel in this.channels)
            {
                this.JoinChannel(channel.Name, channel.Password);
            }
        }

        public Users GetAuthenticatedUsers()
        {
            return users;
        }

        public IList<Command> GetAllCommands()
        {
            var commandCollection = new List<Command>();

            if (commands == null)
                return commandCollection;

            foreach (var command in commands)
            {
                commandCollection.Add(command);
            }

            return commandCollection;
        }

        public void SayTo(string receiver, string message)
        {
            try
            {
                var ircMessage = string.Format("PRIVMSG {0} :{1}", receiver, message);
                this.connection.SendToServer(ircMessage);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

        public void SetChannels(List<Channel> newChannels)
        {
            this.channels = newChannels;
        }

        public void ConnectToServer(Server server, Credentials botCredentials)
        {
            this.credentials = botCredentials;
            connection.ConnectTo(server, botCredentials);
        }

        #endregion

    }
}