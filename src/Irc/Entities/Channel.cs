namespace Irc.Entities
{
    /// <summary>
    /// Class that describes a channel
    /// </summary>
    public class Channel
    {
        #region Properties
        private int _id;
        /// <summary>
        /// Gets or sets channel's id
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

        /// <summary>
        /// Channel's name property
        /// </summary>
        private string name;
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
        /// <summary>
        /// Channel's password property
        /// </summary>
        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Class contruction that takes channel id, name and password as paramameters
        /// </summary>
        /// <param name="id">Channel's id</param>
        /// <param name="name">Channel's name</param>
        /// <param name="password">Channel's password</param>
        public Channel(int id, string name, string password)
        {
            _id = id;
            this.name = name;
            this.password = password;
        }
        #endregion

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (this.GetType() != obj.GetType()) 
                return false;

            Channel compareChannel = (Channel)obj;

            if (compareChannel.Id == this._id)
                return true;
            else
                return false;
        }
    }
}