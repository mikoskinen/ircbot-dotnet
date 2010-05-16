using System;
using System.Collections.Generic;
using Irc.Bot.Entities;
using Irc.Entities;

namespace Classic.IRCBot
{
	#region DataSource
	/// <summary>
	/// Class that takes commands like Login and hides the implementation
	/// from the calling class.
	/// </summary>
	public class BotDataSource : DataSource
	{
		/// <summary>
		/// The object that handles the connection to external resources (XML, database)
		/// </summary>
		private readonly DataSourceConnection dataSourceConnection;

        public BotDataSource(DataSourceConnection dataSourceConnection)
        {
            this.dataSourceConnection = dataSourceConnection;
        }

		#region Public Methods
		/// <summary>
		/// Returns a Channel-array about the channels we want the bot to connect to
		/// in this network.
		/// </summary>
		/// <param name="networkID">Irc-network's id</param>
		/// <returns>ArrayList of channel-names and passwords</returns>
		public List<Channel> GetChannels(int networkID)
		{
            var channels = new List<Channel>();
			System.Data.DataSet ds = this.dataSourceConnection.GetChannels(networkID);
			foreach (System.Data.DataRow dr in ds.Tables[0].Rows)
			{
				string name = dr["name"].ToString();
				string password = dr["password"].ToString();
                int id = int.Parse(dr["channel_id"].ToString());

				Channel channel = new Channel(id, name, password);
				channels.Add(channel);
			}

			return channels;
		}

		/// <summary>
		/// Returns a Network-array about the known irc-networks.
		/// </summary>
		/// <returns>Array of irc-networks and addresses</returns>
		public System.Collections.ArrayList GetNetworks()
		{
			// Gets the known networks
			var networksDataSet = this.dataSourceConnection.GetNetworks();
			var networks = new System.Collections.ArrayList();

			// Now get the servers
			foreach (System.Data.DataRow dr in networksDataSet.Tables[0].Rows)
			{
				var networkID = Int32.Parse(dr["network_id"].ToString().TrimEnd());
				var name = dr["name"].ToString().TrimEnd();
				var description = dr["description"].ToString().TrimEnd();
				var network = new Network(networkID, name, description);
				
				System.Data.DataSet serversDataSet = this.dataSourceConnection.GetServers(networkID);
				foreach (System.Data.DataRow dr2 in serversDataSet.Tables[0].Rows)
				{
					var server_address = dr2["address"].ToString().TrimEnd();
					var server_description = dr2["description"].ToString().TrimEnd();
					var server_port = Int32.Parse(dr2["port"].ToString().TrimEnd());
					var server = new Server(server_address, server_port, server_description);
					network.AddServer(server);
				}
				networks.Add (network);
			}
			return networks;
		}

		/// <summary>
		/// Method that checks if user gave a good username and password.
		/// </summary>
		/// <param name="userName">The username user gave</param>
		/// <param name="password">The password user gave</param>
		/// <returns>Returns an user-object with the correct information if username and password matched</returns>
		public User LogInUser (string userName, string password)
		{
			User user = this.dataSourceConnection.LogInUser(userName, password);
            return user;
		}
        /// <summary>
        /// Gets user's securitylevel for channels.
        /// </summary>
        /// <param name="userID">Given userId</param>
        /// <returns>Dictionary of channels and their securitylevels for given user</returns>
        public System.Collections.Generic.Dictionary<Channel, SecurityLevel> GetChannelSecurityLevelsForUser(int userID)
        {
            return this.dataSourceConnection.GetChannelSecurityLevelsForUser(userID);
        }

		#endregion
	}
	#endregion

	#region Enums
	public enum DataSourceConnectionTypes
	{
		Database,
		XML,
        EnterpriseLibrary
	}
	#endregion
}
