namespace Irc.Entities
{
    /// <summary>
    /// Simple class that presents the server.
    /// </summary>
    public class Server
    {
        public int Port { get; private set; }

        public string Address { get; private set; }

        public string Description { get; private set; }

        public Server (string address, int port, string description)
        {
            this.Address = address;
            this.Port = port;
            this.Description = description;
        }
    }
}