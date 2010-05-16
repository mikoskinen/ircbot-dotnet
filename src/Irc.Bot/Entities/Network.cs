using System.Collections.Generic;
using Irc.Entities;

namespace Irc.Bot.Entities
{
    /// <summary>
    /// Presents the network-class
    /// </summary>
    public class Network
    {
        /// <summary>
        /// List of the known server addresses to this network
        /// </summary>
        private readonly List<Server> servers = new List<Server>();

        public int NetworkID { get; private set; }
        public string Description { get; private set; }
        public string Name { get; private set; }

        public Network(int networkID, string name, string description)
        {
            this.NetworkID = networkID;
            this.Name = name;
            this.Description = description;
        }

        /// <summary>
        /// Adds a new server to network
        /// </summary>
        /// <param name="server">Server-object</param>
        public void AddServer(Server server)
        {
            this.servers.Add(server);
        }

        /// <summary>
        /// Returns the first know server
        /// </summary>
        /// <returns>Server-object</returns>
        public Server ReturnFirstServer()
        {
            return this.servers.Count > 0 ? this.servers[0] : new Server("", -1, "");
        }
    }
}