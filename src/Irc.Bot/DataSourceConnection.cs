using System;
using Irc.Bot.Entities;
using Irc.Entities;

namespace Classic.IRCBot
{
	#region Interface-class
	/// <summary>
	/// Interface-class. Classes implementing this return mostly datasets
	/// to DataSource-class.
	/// </summary>
	public interface DataSourceConnection
	{
		/// <summary>
		/// Method that returns all the known irc-networks
		/// </summary>
		/// <returns>DataSet of known irc-networks</returns>
		System.Data.DataSet GetNetworks();
		/// <summary>
		/// Method that returns all the known channels from given irc-network
		/// </summary>
		/// <param name="networkID">ID of the given irc-network</param>
		/// <returns>Known channels from the given irc-network</returns>
		System.Data.DataSet GetChannels(int networkID);
        /// <summary>
        /// Method that returns the known channel for given channelID
        /// </summary>
        /// <param name="networkID">Channel's id</param>
        /// <returns>Channel-object</returns>
        Channel GetChannelByID(int channelID);
        /// <summary>
		/// Method that returns all the known servers from given irc-network.
		/// </summary>
		/// <param name="networkID">ID of the given irc-network</param>
		/// <returns>Known servers from the given irc-network</returns>
		System.Data.DataSet GetServers(int networkID);
		/// <summary>
		/// Method that returns a user-object if given username and password were correct.
		/// </summary>
		/// <param name="userName">Given username.</param>
		/// <param name="password">Given password.</param>
		/// <returns>User-object</returns>
		User LogInUser(string userName, string password);
        /// <summary>
        /// Method that gets securitylevel-object based on its id.
        /// </summary>
        /// <param name="securityLevelID">Given securitylevel id</param>
        /// <returns>Securitylevel object</returns>
        SecurityLevel GetSecurityLevelByID(int securityLevelID);
        /// <summary>
        /// Gets user's securitylevel for channels.
        /// </summary>
        /// <param name="userID">Given userId</param>
        /// <returns>Dictionary of channels and their securitylevels for given user</returns>
        System.Collections.Generic.Dictionary<Channel, SecurityLevel> GetChannelSecurityLevelsForUser(int userID);
	}

	#endregion

    //#region EnterpriseLibraryDataSourceConnection
    //public class EnterpriseLibraryDataSourceConnection : DataSourceConnection
    //{
    //    private Database _db;

    //    private Database GetDB()
    //    {
    //        return DatabaseFactory.CreateDatabase();
    //    }

    //    public EnterpriseLibraryDataSourceConnection()
    //    {
    //        _db = DatabaseFactory.CreateDatabase();
    //    }

    //    #region DataSourceConnection Members

    //    public System.Data.DataSet GetNetworks()
    //    {
    //        DbCommand dbCommand = _db.GetSqlStringCommand("SELECT name, network_id, description FROM networks");

    //        return _db.ExecuteDataSet(dbCommand);
    //    }

    //    public System.Data.DataSet GetServers(int networkID)
    //    {
    //        _db = GetDB();

    //        DbCommand dbCommand = _db.GetSqlStringCommand("SELECT address, description, port FROM servers WHERE network_id=@NetworkID");
    //        _db.AddInParameter(dbCommand, "@NetworkID", DbType.Int32, networkID);

    //        return _db.ExecuteDataSet(dbCommand);
    //    }

    //    public System.Data.DataSet GetChannels(int networkID)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Channel GetChannelByID(int channelID)
    //    {
    //        throw new NotImplementedException();
    //    }



    //    public User LogInUser(string userName, string password)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public SecurityLevel GetSecurityLevelByID(int securityLevelID)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public System.Collections.Generic.Dictionary<Channel, SecurityLevel> GetChannelSecurityLevelsForUser(int userID)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    #endregion
    //}
    //#endregion
    //#region DatabaseDataSourceConnection
    ///// <summary>
    ///// Class, that implements DataSourceConnection. This class is used to
    ///// get information through a database-connection.
    ///// </summary>
    //public class DatabaseDataSourceConnection : DataSourceConnection
    //{
    //    #region Variables
    //    /// <summary>
    //    /// Connection-string which is used to acces database
    //    /// </summary>
    //    private string connectionString = "";
    //    #endregion

    //    #region Constructors

    //    /// <summary>
    //    /// Constructor to class. Reads and sets the connection string.
    //    /// </summary>
    //    public DatabaseDataSourceConnection()
    //    {
    //        System.Configuration.AppSettingsReader settingsReader = new System.Configuration.AppSettingsReader();
    //        this.connectionString = settingsReader.GetValue("ConnectionString", typeof(string)).ToString();
    //    }

    //    #endregion

    //    #region Public methods

    //    public System.Data.DataSet GetNetworks()
    //    {
    //        // Connects to database
    //        System.Data.SqlClient.SqlConnection sqlCon = new System.Data.SqlClient.SqlConnection(this.connectionString);
    //        sqlCon.Open();
    //        System.Data.DataSet dataSet = new System.Data.DataSet();
    //        string sqlString = "SELECT name, network_id, description FROM dbo.ircnetworks";

    //        // Fills the dataset with information about irc-networks
    //        System.Data.SqlClient.SqlDataAdapter dataAdapter = new System.Data.SqlClient.SqlDataAdapter(sqlString, sqlCon);
    //        dataAdapter.Fill(dataSet, "networks");

    //        sqlCon.Close();
    //        return dataSet;
    //    }

    //    public System.Data.DataSet GetServers(int networkID)
    //    {
    //        // Connects to database
    //        System.Data.SqlClient.SqlConnection sqlCon = new System.Data.SqlClient.SqlConnection(this.connectionString);
    //        sqlCon.Open();
    //        System.Data.DataSet dataSet = new System.Data.DataSet();
    //        string sqlString = "SELECT address, description, port FROM dbo.ircservers WHERE network_id=" + networkID;

    //        // Fills the dataset with information about irc-networks
    //        System.Data.SqlClient.SqlDataAdapter dataAdapter = new System.Data.SqlClient.SqlDataAdapter(sqlString, sqlCon);
    //        dataAdapter.Fill(dataSet, "servers");

    //        sqlCon.Close();
    //        return dataSet;
    //    }

    //    public System.Data.DataSet GetChannels(int networkID)
    //    {
    //        // Connects to database
    //        try
    //        {
    //            System.Data.SqlClient.SqlConnection sqlCon = new System.Data.SqlClient.SqlConnection(this.connectionString);
    //            sqlCon.Open();
    //            System.Data.DataSet dataSet = new System.Data.DataSet();
    //            string sqlString = "SELECT dbo.ircchannels.password as password, dbo.ircchannels.name as name FROM dbo.ircchannels INNER JOIN dbo.ircnetworks ON dbo.ircchannels.network_id = dbo.ircnetworks.network_id WHERE dbo.ircchannels.network_id =" + networkID;

    //            // Fills the dataset with information about channels
    //            System.Data.SqlClient.SqlDataAdapter dataAdapter = new System.Data.SqlClient.SqlDataAdapter(sqlString, sqlCon);
    //            dataAdapter.Fill(dataSet, "channels");

    //            sqlCon.Close();
    //            return dataSet;
    //        } 
    //        catch (Exception e)
    //        {
    //            Console.WriteLine(e.ToString());
    //        }
    //        return new System.Data.DataSet();
    //    }

    //    public User LogInUser (string userName, string password)
    //    {
    //        return new User("", 0);
    //    }

    //    #endregion



    //    public SecurityLevel GetSecurityLevelByID(int securityLevelID)
    //    {
    //        throw new NotImplementedException();
    //    }


    //    public System.Collections.Generic.Dictionary<Channel, SecurityLevel> GetChannelSecurityLevelsForUser(int userID)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Channel GetChannelByID(int channelID)
    //    {
    //        throw new NotImplementedException();
    //    }

    //}

    //#endregion

	#region XMLDataSourceConnection
	/// <summary>
 	/// Class, that implements DataSourceConnection. This class is used to
	/// get information through a xml-files.
	/// </summary>
	public class XMLDataSourceConnection : DataSourceConnection
	{
		System.Configuration.AppSettingsReader settingsReader;
		string configDirectory;
		string passwordHashAlgorithm;

		public XMLDataSourceConnection()
		{
			this.settingsReader = new System.Configuration.AppSettingsReader();
			this.configDirectory = settingsReader.GetValue("XmlConfigDirectory", typeof(string)).ToString();
			this.passwordHashAlgorithm = settingsReader.GetValue("PasswordHashAlgorithm", typeof(string)).ToString();
		}

		public System.Data.DataSet GetNetworks()
		{
			// Reads a xml-file and creates a dataset from it
			System.Data.DataSet dataSet = new System.Data.DataSet();
			dataSet.ReadXml(this.configDirectory + settingsReader.GetValue("NetworksConfigXml", typeof(string)).ToString());

			return dataSet;
		}

		public System.Data.DataSet GetChannels(int networkID)
		{
			// Reads xml-file and creates a dataset from it
			try
			{
				System.Data.DataSet dataSet = new System.Data.DataSet();
				dataSet.ReadXml(this.configDirectory + settingsReader.GetValue("ChannelsConfigXml", typeof(string)).ToString());
				// Removes the channels which are from a wrong network
				for (int n = dataSet.Tables[0].Rows.Count-1; n >= 0; n--)
				{
					if (Int32.Parse(dataSet.Tables[0].Rows[n]["network_id"].ToString()) != networkID)
						dataSet.Tables[0].Rows.RemoveAt(n);
				}
				return dataSet;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}
			return new System.Data.DataSet();
		}

		public System.Data.DataSet GetServers(int networkID)
		{
			// Reads xml-file and creates a dataset
			System.Data.DataSet dataSet = new System.Data.DataSet();
			dataSet.ReadXml(this.configDirectory + settingsReader.GetValue("ServersConfigXml", typeof(string)).ToString());

			// Removes the server which are from a wrong network
			for (int n = dataSet.Tables[0].Rows.Count-1; n >= 0; n--)
			{
				if (Int32.Parse(dataSet.Tables[0].Rows[n]["network_id"].ToString()) != networkID)
					dataSet.Tables[0].Rows.RemoveAt(n);
			}

			return dataSet;
		}

		public User LogInUser(string userName, string password)
		{
			// Reads the xml-file and creates a dataset
			System.Data.DataSet dsUsers = new System.Data.DataSet();
			dsUsers.ReadXml(this.configDirectory + settingsReader.GetValue("UsersConfigXml", typeof(string)).ToString());
			foreach (System.Data.DataRow dr in dsUsers.Tables[0].Rows)
			{
				// Lets read the username in the datarow
				string tmpUserName = dr["name"].ToString().ToLower();
				// If the username matches, lets check the password
				if (tmpUserName.Equals(userName.ToLower()))
				{
					string tmpPassword = dr["password"].ToString().ToUpper();
					if (System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(password, "md5").Equals(tmpPassword))
					{
						// Given username and password was correct. Lets read user's securitylevel
						int securityLevelID = Int32.Parse(dr["securitylevel_id"].ToString());

                        // Lets create the securitylevel-object
						SecurityLevel securityLevel = this.GetSecurityLevelByID(securityLevelID);

                        // We get user's id
                        int userID = Int32.Parse(dr["user_id"].ToString());

                        // And then finally we create the user object
                        User newUser = new User(userID, userName, securityLevel);

                        return newUser;
					}
				}
			}
			// Username and password were incorrect
			return new User("", 0);
		}

        public SecurityLevel GetSecurityLevelByID(int securityLevelID)
        {
            SecurityLevel securityLevel = new SecurityLevel();
            securityLevel.SecurityLevelReal = 0;
            securityLevel.SecurityLevelID = 0;
            securityLevel.SecurityLevelDescription = "";

            // We need some objects
            System.Configuration.AppSettingsReader settingsReader = new System.Configuration.AppSettingsReader();
            string configDirectory = settingsReader.GetValue("XmlConfigDirectory", typeof(string)).ToString();

            // We read the required information from the xml-file
            System.Data.DataSet dataSet = new System.Data.DataSet();
            dataSet.ReadXml(configDirectory + settingsReader.GetValue("SecuritylevelsConfigXml", typeof(string)).ToString());

            // Cycle through the dataset and set object's information
            foreach (System.Data.DataRow dr in dataSet.Tables[0].Rows)
            {
                if (Int32.Parse(dr["securitylevel_id"].ToString()) == securityLevelID)
                {
                    securityLevel.SecurityLevelID = securityLevelID;
                    securityLevel.SecurityLevelDescription = dr["name"].ToString();
                    securityLevel.SecurityLevelReal = Int32.Parse(dr["level"].ToString());
                    break;
                }
            }
            return securityLevel;
        }



        public System.Collections.Generic.Dictionary<Channel, SecurityLevel> GetChannelSecurityLevelsForUser(int inUserID)
        {
            System.Collections.Generic.Dictionary<Channel, SecurityLevel> collection = new System.Collections.Generic.Dictionary<Channel, SecurityLevel>();

            System.Data.DataSet dsPermissions = new System.Data.DataSet();
            dsPermissions.ReadXml(this.configDirectory + settingsReader.GetValue("PermissionsConfigXml", typeof(string)).ToString());

            // We get the permissions one by one
            foreach (System.Data.DataRow dr in dsPermissions.Tables[0].Rows)
            {
                int userId = Int32.Parse(dr["user_id"].ToString());

                // We check if permission relates to this user
                if (inUserID == userId)
                {
                    // If so, we first get the securitylevel
                    int securityLevelID = Int32.Parse(dr["securitylevel_id"].ToString());
                    SecurityLevel securityLevel = this.GetSecurityLevelByID(securityLevelID);

                    // Then we get the channel
                    int channelID = Int32.Parse(dr["channel_id"].ToString());
                    Channel channel = this.GetChannelByID(channelID);

                    // And now we add this info into a collection
                    if (channel != null)
                    {
                        if (securityLevel != null)
                        {
                            collection.Add(channel, securityLevel);
                        }
                    }
                }
            }

            return collection;
        }

        public Channel GetChannelByID(int inChannelID)
        {
            System.Data.DataSet dataSet = new System.Data.DataSet();
            dataSet.ReadXml(this.configDirectory + settingsReader.GetValue("ChannelsConfigXml", typeof(string)).ToString());

            foreach (System.Data.DataRow dr in dataSet.Tables[0].Rows)
            {
                int id = int.Parse(dr["channel_id"].ToString());
                if (id == inChannelID)
                {
                    string name = dr["name"].ToString();
                    string password = dr["password"].ToString();
                    Channel channel = new Channel(id, name, password);
                    return channel;
                }
            }
            return null;
        }

    }
	#endregion
}
