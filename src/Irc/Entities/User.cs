namespace Irc.Entities
{
    public class IrcUser 
    {
        public string Name { get; private set; }
        public string HostMask { get; private set; }

        public IrcUser(string name, string hostMask)
        {
            this.Name = name;
            this.HostMask = hostMask;
        }
    }
}
