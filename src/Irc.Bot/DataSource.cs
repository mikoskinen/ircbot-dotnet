using System.Collections.Generic;
using Irc.Bot.Entities;
using Irc.Entities;

namespace Classic.IRCBot
{
    public interface DataSource
    {
        /// <summary>
        /// Returns a Channel-array about the channels we want the bot to connect to
        /// in this network.
        /// </summary>
        /// <param name="networkID">Irc-network's id</param>
        /// <returns>ArrayList of channel-names and passwords</returns>
        List<Channel> GetChannels(int networkID);

        /// <summary>
        /// Returns a Network-array about the known irc-networks.
        /// </summary>
        /// <returns>Array of irc-networks and addresses</returns>
        System.Collections.ArrayList GetNetworks();

        /// <summary>
        /// Method that checks if user gave a good username and password.
        /// </summary>
        /// <param name="userName">The username user gave</param>
        /// <param name="password">The password user gave</param>
        /// <returns>Returns an user-object with the correct information if username and password matched</returns>
        User LogInUser (string userName, string password);

        /// <summary>
        /// Gets user's securitylevel for channels.
        /// </summary>
        /// <param name="userID">Given userId</param>
        /// <returns>Dictionary of channels and their securitylevels for given user</returns>
        Dictionary<Channel, SecurityLevel> GetChannelSecurityLevelsForUser(int userID);
    }
}