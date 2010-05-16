namespace Irc.Bot.Entities
{
    /// <summary>
    /// Presents the securitylevel
    /// </summary>
    public class SecurityLevel
    {
        #region Properties
        private int securityLevelID;
        /// <summary>
        /// Securitylevel's ID-property
        /// </summary>
        public int SecurityLevelID
        {
            get
            {
                return securityLevelID;
            }
            set
            {
                securityLevelID = value;
            }
        }

        private int securityLevelReal;
        /// <summary>
        /// Securitylevel-property
        /// </summary>
        public int SecurityLevelReal
        {
            get
            {
                return securityLevelReal;
            }
            set
            {
                securityLevelReal = value;
            }
        }

        private string securityLevelDescription;
        /// <summary>
        /// Securitylevel's description-property
        /// </summary>
        public string SecurityLevelDescription
        {
            get
            {
                return securityLevelDescription;
            }
            set
            {
                securityLevelDescription = value;
            }
        }
        #endregion

        #region Constructors
        public SecurityLevel ()
        {
        }
        public SecurityLevel(int level)
        {
            this.securityLevelReal = level;
        }

        #endregion

        #region Public Methods
        #endregion
    }
}