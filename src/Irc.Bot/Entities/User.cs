using Irc.Entities;

namespace Irc.Bot.Entities
{
    public class User
    {
        #region Variables
        /// <summary>
        /// Contains information about user's channels and channels' 
        /// securitylevels. Key = Channel, Value = SecurityLevel.
        /// </summary>
        private System.Collections.Generic.Dictionary<Channel, SecurityLevel> _channelSecurityLevels = new System.Collections.Generic.Dictionary<Channel, SecurityLevel>();

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets user's channel + securitylevel-information.
        /// </summary>
        public System.Collections.Generic.Dictionary<Channel, SecurityLevel> ChannelSecurityLevelCollection
        {
            get
            {
                return _channelSecurityLevels;
            }
            set
            {
                _channelSecurityLevels = value;
            }
        }

        private int _id;
        /// <summary>
        /// Gets or sets user's id
        /// </summary>
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        private SecurityLevel securityLevelInformation;
        /// <summary>
        /// Securitylevel-information-property
        /// </summary>
        public SecurityLevel SecurityLevelInformation
        {
            get
            {
                return securityLevelInformation;
            }
            set
            {
                securityLevelInformation = value;
            }
        }


        private string name;
        /// <summary>
        /// User's name-property
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        private int securityLevel;
        /// <summary>
        /// User's securitylevel-property
        /// </summary>
        public int SecurityLevel
        {
            get
            {
                return securityLevel;
            }
            set
            {
                securityLevel = value;
            }
        }

        private string nickName;
        /// <summary>
        /// User's nickname-property
        /// </summary>
        public string NickName
        {
            get
            {
                return nickName;
            }
            set
            {
                nickName = value;
            }
        }

        private string hostMask;
        /// <summary>
        /// User's nickname-property
        /// </summary>
        public string HostMask
        {
            get
            {
                return hostMask;
            }
            set
            {
                hostMask = value;
            }
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Constructor that creates a user-object from name and securityLevel
        /// information.
        /// </summary>
        /// <param name="name">User's name</param>
        /// <param name="securityLevel">User's securitylevel</param>
        public User(string name, int securityLevel)
        {
            this.name = name;
            this.securityLevel = securityLevel;
        }

        /// <summary>
        /// Constructor that creates a user-object from name and securityLevel.
        /// </summary>
        /// <param name="name">User's name</param>
        /// <param name="securityLevel">SecurityLevel-object</param>
        public User(string name, SecurityLevel securityLevel)
        {
            this.name = name;
            this.securityLevelInformation = securityLevel;
            this.securityLevel = securityLevel.SecurityLevelReal;
        }

        /// <summary>
        /// Constructor that creates a user-object from name and securityLevel.
        /// </summary>
        /// <param name="userId">User's id</param>
        /// <param name="name">User's name</param>
        /// <param name="securityLevel">SecurityLevel-object</param>
        public User(int userId, string name, SecurityLevel securityLevel)
        {
            this._id = userId;
            this.name = name;
            this.securityLevelInformation = securityLevel;
            this.securityLevel = securityLevel.SecurityLevelReal;
        }
        /// <summary>
        /// Constructor that creates a user-object from name, securityLevel and
        /// nickname information.
        /// </summary>
        /// <param name="name">User's name</param>
        /// <param name="securityLevel">User's securitylevel</param>
        /// <param name="nickName">User's nickname</param>
        public User(string name, int securityLevel, string nickName)
        {
            this.name = name;
            this.securityLevel = securityLevel;
            this.nickName = nickName;
        }

        /// <summary>
        /// Constructor that creates a user-object from name, securityLevel and
        /// nickname information.
        /// </summary>
        /// <param name="userId">User's id</param>
        /// <param name="name">User's name</param>
        /// <param name="securityLevel">User's securitylevel</param>
        /// <param name="nickName">User's nickname</param>
        public User(int userId, string name, int securityLevel, string nickName)
        {
            this._id = userId;
            this.name = name;
            this.securityLevel = securityLevel;
            this.nickName = nickName;
        }
        
        /// <summary>
        /// Constructor that creates a user-object from name, securityLevel and
        /// nickname information.
        /// </summary>
        /// <param name="name">User's name</param>
        /// <param name="securityLevel">SecurityLevel-object</param>
        /// <param name="nickName">User's nickname</param>
        public User(string name, SecurityLevel securityLevel, string nickName)
        {
            this.name = name;
            this.securityLevelInformation = securityLevel;
            this.nickName = nickName;
            this.securityLevel = securityLevel.SecurityLevelReal;
        }
        #endregion

        #region Public methods

        /// <summary>
        /// Returns user's securitylevel based on given channel
        /// </summary>
        /// <param name="channelName">Given channel</param>
        /// <returns>Securitylevel-object</returns>
        public SecurityLevel GetUsersSecurityLevelForChannel(string channelName)
        {
            foreach (Channel key in this._channelSecurityLevels.Keys)
            {
                if (key.Name == channelName)
                {
                    return this._channelSecurityLevels[key];
                }
            }
            return new SecurityLevel(-1);
        }
        #endregion
    }
}