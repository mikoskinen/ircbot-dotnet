namespace Irc.Bot.Entities
{
    /// <summary>
    /// Class that manages stuff like keeping track of all the logged users
    /// and their security ranks etc.
    /// </summary>
    public class Users
    {
        #region Variables
        /// <summary>
        /// Collection of the users
        /// </summary>
        private System.Collections.Generic.List<User> _userCollection = new System.Collections.Generic.List<User>();
        #endregion

        #region Properties
        /// <summary>
        /// Gets user-count
        /// </summary>
        public int Count
        {
            get
            {
                return this._userCollection.Count;
            }
        }

        #endregion

        #region Constructors
        public Users()
        {
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Returns a user-object based on given nickname
        /// </summary>
        /// <param name="nickName">Given nickname</param>
        /// <returns>User-object</returns>
        public User GetUserByNickName (string nickName)
        {
            foreach (User user in this._userCollection)
            {
                if (user.NickName.Equals(nickName))
                    return user;
            }
            return new User("", 0);
        }

        /// <summary>
        /// Returns user-object based on user's id
        /// </summary>
        /// <param name="userId">Given userid</param>
        /// <returns>User-object</returns>
        public User GetUserByID(int userId)
        {
            foreach (User user in this._userCollection)
            {
                if (user.Id == userId)
                    return user;
            }
            return new User("", null);
        }

        /// <summary>
        /// Returns a user-object based on given hostmask
        /// </summary>
        /// <param name="nickName">Given hostmask</param>
        /// <returns>User-object</returns>
        public User GetUserByHostMask (string hostMask)
        {
            foreach (User user in this._userCollection)
            {
                if (user.HostMask.Equals(hostMask))
                    return user;
            }
            return new User("", 0);
        }
        /// <summary>
        /// Method that cycles through all the logged users and returns true if the user
        /// is logged in.
        /// </summary>
        /// <param name="userName">User's nickname to be found</param>
        /// <returns>True if logged in, else false.</returns>
        public bool IsLoggedInNickName (string nickName)
        {
            foreach (User user in this._userCollection)
            {
                if (user.NickName.Equals(nickName))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Method that cycles through all the logged users and returns true if the user
        /// is logged in.
        /// </summary>
        /// <param name="userName">User's hostmask to be found</param>
        /// <returns>True if logged in, else false.</returns>
        public bool IsLoggedInHostMask (string hostMask)
        {
            foreach (User user in this._userCollection)
            {
                if (user.HostMask.Equals(hostMask))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Method that cycles through all the logged users and returns true if the user
        /// is logged in.
        /// </summary>
        /// <param name="userName">Username to be found</param>
        /// <returns>True if logged in, else false.</returns>
        public bool IsLoggedInUserName (string userName)
        {
            foreach (User user in this._userCollection)
            {
                if (user.Name.Equals(userName))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Method that adds a new user to users-collection
        /// </summary>
        /// <param name="user">User-object</param>
        public void AddUser (User user)
        {
            this._userCollection.Add(user);
        }

        /// <summary>
        /// Returns an arraylist of logged in users
        /// </summary>
        /// <returns></returns>
        public System.Collections.Generic.List<User> GetLoggedInUsers()
        {
            return this._userCollection;
        }

        #endregion
    }
}